//-----------------------------------------------------------------------
// <copyright file="ActivationContextActivator.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.IPE.LU.UnifiedModeling.ExternalToolsFacade
{
    using System;
    using System.Runtime.ConstrainedExecution;
    using System.Runtime.InteropServices;
    using Win32.SafeHandles;

    /// <summary>
    /// Helper class that can be used to create and activate COM activation contexts.
    /// </summary>
    public sealed class ActivationContextActivator : IDisposable
    {
        private readonly ActivationContextHandle activationContextHandle;
        private readonly ActivationContextCookie cookie;

        #region Win32 Imports

        private static class NativeMethods
        {
            public const uint ACTCTX_FLAG_PROCESSOR_ARCHITECTURE_VALID = 0x001;
            public const uint ACTCTX_FLAG_LANGID_VALID = 0x002;
            public const uint ACTCTX_FLAG_ASSEMBLY_DIRECTORY_VALID = 0x004;
            public const uint ACTCTX_FLAG_RESOURCE_NAME_VALID = 0x008;
            public const uint ACTCTX_FLAG_SET_PROCESS_DEFAULT = 0x010;
            public const uint ACTCTX_FLAG_APPLICATION_NAME_VALID = 0x020;
            public const uint ACTCTX_FLAG_HMODULE_VALID = 0x080;

            public const ushort ISOLATIONAWARE_MANIFEST_RESOURCE_ID = 2;

            [StructLayout(LayoutKind.Sequential)]
            public struct ACTCTX
            {
                public int cbSize;
                public uint dwFlags;
                public string lpSource;
                public ushort wProcessorArchitecture;
                public ushort wLangId;
                public string lpAssemblyDirectory;
                public ushort lpResourceName;
                public string lpApplicationName;
            }

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern ActivationContextHandle CreateActCtx(ref ACTCTX ActCtx);

            [DllImport("Kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool ActivateActCtx(ActivationContextHandle hActCtx, out ActivationContextCookie lpCookie);

            [DllImport("Kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool DeactivateActCtx(int dwFlags, IntPtr lpCookie);

            [DllImport("Kernel32.dll", SetLastError = true)]
            public static extern void ReleaseActCtx(IntPtr hActCtx);
        }

        #endregion

        #region Safe Handles

        #region ActivationContextHandle

        /// <summary>
        /// SafeHandle-derived class that can be used with activation context handles.
        /// </summary>
        private sealed class ActivationContextHandle : SafeHandleMinusOneIsInvalid
        {
            /// <summary>
            /// Creates an ActivationContextHandle.
            /// </summary>
            private ActivationContextHandle()
                : base(true)
            {
            }

            /// <summary>
            /// Releases the activation context held by this object.
            /// </summary>
            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override bool ReleaseHandle()
            {
                NativeMethods.ReleaseActCtx(this.handle);
                return true;
            }
        }

        #endregion

        #region ActivationContextCookie

        /// <summary>
        /// SafeHandle-derived class that can be used with activation context cookies.
        /// </summary>
        private sealed class ActivationContextCookie : SafeHandle
        {
            /// <summary>
            /// Creates an ActivationContextCookie.
            /// </summary>
            private ActivationContextCookie()
                : base(IntPtr.Zero, true)
            {
            }

            /// <summary>
            /// Determines if this cookie is invalid.
            /// </summary>
            public override bool IsInvalid
            {
                get
                {
                    // assume this handle is valid if it was created
                    return false;
                }
            }

            /// <summary>
            /// Deactivates the activation context held by this object.
            /// </summary>
            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
            protected override bool ReleaseHandle()
            {
                return NativeMethods.DeactivateActCtx(0, this.handle);
            }
        }

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Creates and activates the specified activation context.
        /// </summary>
        /// <param name="actctx">The activation context to create and activate</param>
        private ActivationContextActivator(NativeMethods.ACTCTX actctx)
        {
            this.activationContextHandle = NativeMethods.CreateActCtx(ref actctx);
            if (this.activationContextHandle.IsInvalid)
            {
                throw new Exception(CreateExceptionMessage("CreateActCtx", actctx));
            }

            if (!NativeMethods.ActivateActCtx(this.activationContextHandle, out this.cookie))
            {
                throw new Exception(CreateExceptionMessage("ActivateActCtx", actctx));
            }
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Disposes this object.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            // note that this class does not do anything when disposing is false because it does not hold any
            // native resources directly; all native resources are held indirectly by SafeHandle-derived classes
            // and will get cleaned up correctly by those classes
            if (disposing)
            {
                DisposeIfValid(this.cookie);
                DisposeIfValid(this.activationContextHandle);
            }
        }

        #endregion

        #region Factory Functions

        /// <summary>
        /// Creates and activates the activation context in the specified external manifest.
        /// </summary>
        /// <param name="source">The external manifest</param>
        /// <param name="assemblyDirectory">The directory to probe for assemblies</param>
        /// <returns>An ActivationContextActivator that activates the specified manifset</returns>
        public static ActivationContextActivator FromExternalManifest(string source, string assemblyDirectory)
        {
            var actctx = new NativeMethods.ACTCTX();
            actctx.cbSize = Marshal.SizeOf(actctx);
            actctx.lpSource = source;
            actctx.lpAssemblyDirectory = assemblyDirectory;
            actctx.dwFlags =
                NativeMethods.ACTCTX_FLAG_ASSEMBLY_DIRECTORY_VALID |
                NativeMethods.ACTCTX_FLAG_APPLICATION_NAME_VALID;
            return FromActivationContext(actctx);
        }

        /// <summary>
        /// Creates and activates the activation context in the specified internal manifest.
        /// </summary>
        /// <param name="source">The module containing an internal manifest</param>
        /// <param name="assemblyDirectory">The directory to probe for assemblies</param>
        /// <returns>An ActivationContextActivator that activates the specified manifset</returns>
        public static ActivationContextActivator FromInternalManifest(string source, string assemblyDirectory)
        {
            var actctx = new NativeMethods.ACTCTX();
            actctx.cbSize = Marshal.SizeOf(actctx);
            actctx.lpSource = source;
            actctx.lpAssemblyDirectory = assemblyDirectory;
            actctx.lpResourceName = NativeMethods.ISOLATIONAWARE_MANIFEST_RESOURCE_ID;
            actctx.dwFlags =
                NativeMethods.ACTCTX_FLAG_ASSEMBLY_DIRECTORY_VALID |
                NativeMethods.ACTCTX_FLAG_APPLICATION_NAME_VALID |
                NativeMethods.ACTCTX_FLAG_RESOURCE_NAME_VALID;
            return FromActivationContext(actctx);
        }

        private static ActivationContextActivator FromActivationContext(NativeMethods.ACTCTX actctx)
        {
            return new ActivationContextActivator(actctx);
        }

        #endregion

        #region Helper Functions

        private static string CreateExceptionMessage(string functionName, NativeMethods.ACTCTX actctx)
        {
            int lastError = Marshal.GetLastWin32Error();
            string errorMessage = string.Format(
                "{0} failed: {1} (source={2}, assemblyDirectory={3})",
                functionName,
                lastError,
                actctx.lpSource,
                actctx.lpAssemblyDirectory);
            return errorMessage;
        }

        private static void DisposeIfValid(SafeHandle handle)
        {
            if (null != handle && !handle.IsInvalid)
            {
                handle.Dispose();
            }
        }

        #endregion
    }
}