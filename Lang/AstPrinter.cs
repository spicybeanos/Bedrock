using System.Text;

namespace Bedrock
{
    class AstPrinter : Visitor<String>
    {
        public override string VisitBinary(Expression.BinaryExpression expr)
        {
            return parenthesize(expr.Operator + "", expr.Left, expr.Right);
        }

        public override string VisitGroup(Expression.GroupingExpression expr)
        {
            return parenthesize("group", expr.expression);
        }

        public override string VisitLiteral(Expression.LiteralExpresion expr)
        {
            if (expr.Value == null)
                return "null";
            return expr.Value.ToString();
        }

        public override string VisitUnary(Expression.UnaryExpression expr)
        {
            return parenthesize(expr.Sign + "", expr.expression);
        }

        private String parenthesize(string name, params Expression[] exprs)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("(").Append(name);
            foreach (Expression expr in exprs)
            {
                builder.Append(" ");
                builder.Append(expr.Accept(this));
            }
            builder.Append(")");

            return builder.ToString();
        }

        public string Print(Expression expr)
        {
            return expr.Accept(this);
        }
    }
}
