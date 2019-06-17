using Antlr4.Runtime;
using System;
using Ultramarine.QueryLanguage.Grammars;

namespace Ultramarine.QueryLanguage
{
    public class ConditionCompiler
    {
        public const string ThisAlias = "$this";
        public ConditionCompiler(string expression, string value = null)
        {
            if (string.IsNullOrWhiteSpace(expression) && string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Given expression is not valid. You need to specify expression or value to be checked.");

            var safeExpression = string.IsNullOrWhiteSpace(expression)
                ? $"'{value}' equals '{value}'"
                : expression.Replace(ThisAlias, $"'{value}'");

            var input = new AntlrInputStream(safeExpression);
            var lexer = new QueryLanguageLexer(input);
            var tokens = new CommonTokenStream(lexer);
            var parser = new QueryLanguageParser(tokens);
            var visitor = new LogicalExpressionVisitor();
            OriginalExpression = safeExpression;
            Expression = visitor.Visit(parser.condition().LogicalExpression);
            if (Expression == null)
                throw new ArgumentException($"Given expression '{expression}' cannot be parsed.");
        }

        public LogicalExpression Expression { get; private set; }
        public string OriginalExpression { get; private set; }

        public bool Execute()
        {
            return Expression.Evaluate();
        }


    }
}
