using AEtherUtilities;
using QU.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QU.Miscs
{
    public class ExpressionEvaluator
    {
        private CSharpCompiler.Invoker _invoker = null;

        public static ExpressionEvaluator ParseExpression(string expression)
        {
            string errMsg;

            string source = CSharpCompiler.BuildCSharpSource(
                string.Format("double Evaluator(QU.Utility.ReformulationFeatures features) {{ return {0}; }}", expression));

            Console.WriteLine(source);

            //string source = string.Format("using System;\nusing TSVUtility;\nnamespace AEtherUtilities.Dynamic {{\npublic class Expression_1 {{public static bool Evaluator(TSVLine line) {{ return {0}; }}}}}}", expression);

            var results = CSharpCompiler.CompileSource(source, new string[] { "QU.Utility.dll" }, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                Console.Error.WriteLine(errMsg);
                return null;
            }

            ExpressionEvaluator evaluator = new ExpressionEvaluator();
            evaluator._invoker = CSharpCompiler.GetInvoker(
                CSharpCompiler.GetMethod(results, "Evaluator", BindingFlags.Static | BindingFlags.Public)
                );

            return evaluator;
        }

        public double Evaluate(ReformulationFeatures features)
        {
            return (double)_invoker(features);
        }
    }

    public class BooleanEvaluator
    {
        private CSharpCompiler.Invoker _invoker = null;

        public static BooleanEvaluator ParseExpression(string expression)
        {
            string errMsg;

            string source = CSharpCompiler.BuildCSharpSource(
                string.Format("bool Evaluator(string pattern) {{ return {0}; }}", expression));

            Console.WriteLine(source);

            var results = CSharpCompiler.CompileSource(source, new string[0], out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                Console.Error.WriteLine(errMsg);
                return null;
            }

            BooleanEvaluator evaluator = new BooleanEvaluator();
            evaluator._invoker = CSharpCompiler.GetInvoker(
                CSharpCompiler.GetMethod(results, "Evaluator", BindingFlags.Static | BindingFlags.Public)
                );

            return evaluator;
        }

        public bool Evaluate(string pattern)
        {
            return (bool)_invoker(pattern);
        }
    }
}
