namespace Bedrock
{
    public abstract class Statement
    {
        public class StatementExpression : Statement
        {
            public StatementExpression(Expression expression)
            {
                this.expression = expression;
            }

            public readonly Expression expression;

            public override R Accept<R>(StatementVisitor<R> visitor)
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

            public readonly Expression expression;

            public override R Accept<R>(StatementVisitor<R> visitor)
            {
                return visitor.VisitPrintStatement(this);
            }
        }

        public abstract R Accept<R>(StatementVisitor<R> visitor);
    }
}
