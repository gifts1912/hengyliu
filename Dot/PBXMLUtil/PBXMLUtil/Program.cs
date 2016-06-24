using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.XPath;
using MS.Internal.Xml.XPath;
using System.Xml;
using System.Collections;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace PBXMLUtil
{
    public static class PBXMLUtil
    {
        private static Regex augRegex = new Regex(Regex.Escape("[") + "APlusAnswer RankerDebugSatoriID=.*?" + Regex.Escape("]"), RegexOptions.Compiled);
        private static Regex sidRegex = new Regex("\"(.*)\"", RegexOptions.Compiled);

        public static XPathExpression GetKif(string answerServiceName, string answerScenarioName)
        {
            return XPathExpression.Compile(string.Format("/PropertyBag/s_AnswerResponseCommand/s_AnswerQueryResponse/a_AnswerDataArray/s_AnswerData[c_AnswerServiceName='{0}'][c_AnswerDataScenario='{1}']/k_AnswerDataKifResponse", (object)answerServiceName, (object)answerScenarioName));
        }

    }

    public abstract class XPathExpression
    {
        public abstract string Expression { get; }
        public abstract XPathResultType ReturnType { get; }
        internal XPathExpression()
        {
        }
        public abstract XPathExpression Clone();

        public static XPathExpression Compile(string xpath)
        {
            return XPathExpression.Compile(xpath, (IXmlNamespaceResolver)null);
        }
        public static XPathExpression Compile(string xpath, IXmlNamespaceResolver nsResolver)
        {
            bool needContext;
            CompiledXpathExpr compiledXpathExpr = new CompiledXpathExpr(new QueryBuilder().Build(xpath, out needContext), xpath, needContext);
            if (nsResolver != null)
                compiledXpathExpr.SetContext(nsResolver);
            return (XPathExpression)compiledXpathExpr;
        }

    }

    internal class CompiledXpathExpr : XPathExpression
    {
        private Query query;
        private string expr;
        private bool needContext;

        internal Query QueryTree
        {
            get
            {
                if (this.needContext)
                    throw XPathException.Create("Xp_NoContext");
                return this.query;
            }
        }

        public override string Expression
        {
            get
            {
                return this.expr;
            }
        }

        public override XPathResultType ReturnType
        {
            get
            {
                return this.query.StaticType;
            }
        }

        internal CompiledXpathExpr(Query query, string expression, bool needContext)
        {
            this.query = query;
            this.expr = expression;
            this.needContext = needContext;
        }

        public virtual void CheckErrors()
        {
        }

        public override void AddSort(object expr, IComparer comparer)
        {
            Query evalQuery;
            if (expr is string)
            {
                evalQuery = new QueryBuilder().Build((string)expr, out this.needContext);
            }
            else
            {
                if (!(expr is CompiledXpathExpr))
                    throw XPathException.Create("Xp_BadQueryObject");
                evalQuery = ((CompiledXpathExpr)expr).QueryTree;
            }
            SortQuery sortQuery = this.query as SortQuery;
            if (sortQuery == null)
                this.query = (Query)(sortQuery = new SortQuery(this.query));
            sortQuery.AddSort(evalQuery, comparer);
        }

        public override void AddSort(object expr, XmlSortOrder order, XmlCaseOrder caseOrder, string lang, XmlDataType dataType)
        {
            this.AddSort(expr, (IComparer)new XPathComparerHelper(order, caseOrder, lang, dataType));
        }

        public override XPathExpression Clone()
        {
            return (XPathExpression)new CompiledXpathExpr(Query.Clone(this.query), this.expr, this.needContext);
        }

        public override void SetContext(XmlNamespaceManager nsManager)
        {
            this.SetContext((IXmlNamespaceResolver)nsManager);
        }

        public override void SetContext(IXmlNamespaceResolver nsResolver)
        {
            XsltContext context = nsResolver as XsltContext;
            if (context == null)
            {
                if (nsResolver == null)
                    nsResolver = (IXmlNamespaceResolver)new XmlNamespaceManager((XmlNameTable)new NameTable());
                context = (XsltContext)new CompiledXpathExpr.UndefinedXsltContext(nsResolver);
            }
            this.query.SetXsltContext(context);
            this.needContext = false;
        }

        private class UndefinedXsltContext : XsltContext
        {
            private IXmlNamespaceResolver nsResolver;

            public override string DefaultNamespace
            {
                get
                {
                    return string.Empty;
                }
            }

            public override bool Whitespace
            {
                get
                {
                    return false;
                }
            }

            public UndefinedXsltContext(IXmlNamespaceResolver nsResolver)
              : base(false)
            {
                this.nsResolver = nsResolver;
            }

            public override string LookupNamespace(string prefix)
            {
                if (prefix.Length == 0)
                    return string.Empty;
                string str = this.nsResolver.LookupNamespace(prefix);
                if (str != null)
                    return str;
                throw XPathException.Create("XmlUndefinedAlias", prefix);
            }

            public override IXsltContextVariable ResolveVariable(string prefix, string name)
            {
                throw XPathException.Create("Xp_UndefinedXsltContext");
            }

            public override IXsltContextFunction ResolveFunction(string prefix, string name, XPathResultType[] ArgTypes)
            {
                throw XPathException.Create("Xp_UndefinedXsltContext");
            }

            public override bool PreserveWhitespace(XPathNavigator node)
            {
                return false;
            }

            public override int CompareDocument(string baseUri, string nextbaseUri)
            {
                return string.CompareOrdinal(baseUri, nextbaseUri);
            }
        }
    }


}
