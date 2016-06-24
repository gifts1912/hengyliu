using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IntentEngineApi.Interop;
using Microsoft.IPE.LU.UnifiedModeling.ExternalToolsFacade;

namespace QU.Utility
{
    public class SMCRFParser
    {
        private CCrfModelFacadeClass crfModel;
        private CrfModelInit modelInfo;
        private bool modelLoaded = false;

        /// <summary>
        /// Load SM-CRF Model.
        /// </summary>
        /// <param name="binDirectory">Directory containing IntentEngineApi.dll.</param>
        /// <param name="schemaFile">Schema File.</param>
        /// <param name="lexiconFile">Lexicon File.</param>
        /// <param name="grammarFile">Grammar File.</param>
        /// <param name="modelFile">Model File.</param>
        /// <param name="domain">Domain Name.</param>
        /// <returns>Succeed or not.</returns>
        public bool LoadModel(string binDirectory,
                                string schemaFile, 
                                string lexiconFile, 
                                string grammarFile, 
                                string modelFile, 
                                string domain,
                                int nBest)
        {
            this.modelLoaded = false;
            try
            {
                using (ActivationContextActivator.FromInternalManifest("IntentEngineApi.dll", binDirectory))
                {
                    this.crfModel = new CCrfModelFacadeClass();
                    this.modelInfo = new CrfModelInit
                    {
                        Domain = domain,
                        SchemaFile = schemaFile,
                        BinFile = lexiconFile,
                        GrammarFile = grammarFile,
                        ModelFile = modelFile,
                        NBests = nBest,
                        TokenSeparators = " "
                    };
                    this.crfModel.Initialize(this.modelInfo);
                }
                this.modelLoaded = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                this.modelLoaded = false;
            }

            return this.modelLoaded;
        }

        /// <summary>
        /// Load SM-CRF Model. Binary is in Environment.CurrentDirectory.
        /// </summary>
        /// <param name="schemaFile">Schema File.</param>
        /// <param name="lexiconFile">Lexicon File.</param>
        /// <param name="grammarFile">Grammar File.</param>
        /// <param name="modelFile">Model File.</param>
        /// <param name="domain">Domain Name.</param>
        /// <returns>Succeed or not.</returns>
        public bool LoadModel(string schemaFile,
                                string lexiconFile,
                                string grammarFile,
                                string modelFile,
                                string domain)
        {
            return LoadModel(Environment.CurrentDirectory, schemaFile, lexiconFile, grammarFile, modelFile);
        }

        /// <summary>
        /// Extract Slot Information from query.
        /// </summary>
        /// <param name="query">Query.</param>
        /// <returns>Slots.</returns>
        public SlotInfo[] Evaluate(string query)
        {
            try
            {
                Slot[] slots = this.crfModel.Evaluate(query);
                return (from s in slots select new SlotInfo { NBest = s.Nbest, Tag = s.Tag, Text = s.Text, Score = s.Score }).ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public ChunkInfo[] Chunk(string query)
        {
            try
            {
                Chunk[] chunks = this.crfModel.ChunkFeature(query);
                return
                    (from c in chunks
                     select new ChunkInfo { Feature = c.Feature, Text = c.Text }
                    ).ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
