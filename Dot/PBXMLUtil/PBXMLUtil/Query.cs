// Decompiled with JetBrains decompiler
// Type: MS.Internal.Xml.XPath.Query
// Assembly: System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: E13E1177-5319-40B0-8DFF-38453D86AA7D
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Xml.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
    [DebuggerDisplay("{ToString()}")]
    internal abstract class Query : ResetableIterator
    {
        public const XPathResultType XPathResultType_Navigator = (XPathResultType)4;

        public override int Count
        {
            get
            {
                if (this.count == -1)
                {
                    Query query = (Query)this.Clone();
                    query.Reset();
                    this.count = 0;
                    while (query.MoveNext())
                        this.count = this.count + 1;
                }
                return this.count;
            }
        }

        public virtual double XsltDefaultPriority
        {
            get
            {
                return 0.5;
            }
        }

        public abstract XPathResultType StaticType { get; }

        public virtual QueryProps Properties
        {
            get
            {
                return QueryProps.Merge;
            }
        }

        public Query()
        {
        }

        protected Query(Query other)
          : base((ResetableIterator)other)
        {
        }

        public override bool MoveNext()
        {
            return this.Advance() != null;
        }

        public virtual void SetXsltContext(XsltContext context)
        {
        }

        public abstract object Evaluate(XPathNodeIterator nodeIterator);

        public abstract XPathNavigator Advance();

        public virtual XPathNavigator MatchNode(XPathNavigator current)
        {
            throw XPathException.Create("Xp_InvalidPattern");
        }

        public static Query Clone(Query input)
        {
            if (input != null)
                return (Query)input.Clone();
            return (Query)null;
        }

        protected static XPathNodeIterator Clone(XPathNodeIterator input)
        {
            if (input != null)
                return input.Clone();
            return (XPathNodeIterator)null;
        }

        protected static XPathNavigator Clone(XPathNavigator input)
        {
            if (input != null)
                return input.Clone();
            return (XPathNavigator)null;
        }

        public bool Insert(List<XPathNavigator> buffer, XPathNavigator nav)
        {
            int num = 0;
            int r = buffer.Count;
            if (r != 0)
            {
                switch (Query.CompareNodes(buffer[r - 1], nav))
                {
                    case XmlNodeOrder.Before:
                        buffer.Add(nav.Clone());
                        return true;
                    case XmlNodeOrder.Same:
                        return false;
                    default:
                        --r;
                        break;
                }
            }
            while (num < r)
            {
                int median = Query.GetMedian(num, r);
                switch (Query.CompareNodes(buffer[median], nav))
                {
                    case XmlNodeOrder.Before:
                        num = median + 1;
                        continue;
                    case XmlNodeOrder.Same:
                        return false;
                    default:
                        r = median;
                        continue;
                }
            }
            buffer.Insert(num, nav.Clone());
            return true;
        }

        private static int GetMedian(int l, int r)
        {
            return (int)((uint)(l + r) >> 1);
        }

        public static XmlNodeOrder CompareNodes(XPathNavigator l, XPathNavigator r)
        {
            XmlNodeOrder xmlNodeOrder = l.ComparePosition(r);
            if (xmlNodeOrder == XmlNodeOrder.Unknown)
            {
                XPathNavigator xpathNavigator = l.Clone();
                xpathNavigator.MoveToRoot();
                string baseUri1 = xpathNavigator.BaseURI;
                if (!xpathNavigator.MoveTo(r))
                    xpathNavigator = r.Clone();
                xpathNavigator.MoveToRoot();
                string baseUri2 = xpathNavigator.BaseURI;
                int num = string.CompareOrdinal(baseUri1, baseUri2);
                xmlNodeOrder = num < 0 ? XmlNodeOrder.Before : (num > 0 ? XmlNodeOrder.After : XmlNodeOrder.Unknown);
            }
            return xmlNodeOrder;
        }

        [Conditional("DEBUG")]
        private void AssertDOD(List<XPathNavigator> buffer, XPathNavigator nav, int pos)
        {
            if (nav.GetType().ToString() == "Microsoft.VisualStudio.Modeling.StoreNavigator" || nav.GetType().ToString() == "System.Xml.DataDocumentXPathNavigator")
                return;
            if (0 < pos)
            {
                int num1 = (int)Query.CompareNodes(buffer[pos - 1], nav);
            }
            if (pos >= buffer.Count)
                return;
            int num2 = (int)Query.CompareNodes(nav, buffer[pos]);
        }

        [Conditional("DEBUG")]
        public static void AssertQuery(Query query)
        {
            if (query is FunctionQuery)
                return;
            query = Query.Clone(query);
            XPathNavigator l = (XPathNavigator)null;
            int count = query.Clone().Count;
            int num1 = 0;
            XPathNavigator r;
            while ((r = query.Advance()) != null && (!(r.GetType().ToString() == "Microsoft.VisualStudio.Modeling.StoreNavigator") && !(r.GetType().ToString() == "System.Xml.DataDocumentXPathNavigator")))
            {
                if (l != null && (l.NodeType != XPathNodeType.Namespace || r.NodeType != XPathNodeType.Namespace))
                {
                    int num2 = (int)Query.CompareNodes(l, r);
                }
                l = r.Clone();
                ++num1;
            }
        }

        protected XPathResultType GetXPathType(object value)
        {
            if (value is XPathNodeIterator)
                return XPathResultType.NodeSet;
            if (value is string)
                return XPathResultType.String;
            if (value is double)
                return XPathResultType.Number;
            return value is bool ? XPathResultType.Boolean : (XPathResultType)4;
        }

        public virtual void PrintQuery(XmlWriter w)
        {
            w.WriteElementString(this.GetType().Name, string.Empty);
        }
    }
}
