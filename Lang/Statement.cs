namespace Bedrock
{
    public abstract class Statement
    {
        public class Expression : Statement
        {
            public Expression(Expression expression)
            {
                this.expression = expression;
            }

            readonly Expression expression;

            public override R Accept<R>(Visitor<R> visitor)
            {
                return visitor.VisitExpressionStatement(this);
            }
        }

        public class Print : Statement
        {
            public Print(Expression expression)
            {
                this.expression = expression;
            }

            readonly Expression expression;

            public override R Accept<R>(Visitor<R> visitor)
            {
                return visitor.VisitPrintStatement(this);
            }
        }

        public abstract R Accept<R>(Visitor<R> visitor);
    }
}
