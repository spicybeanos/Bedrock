namespace Bedrock
{
    public abstract class Expression
    {
        public abstract object Accept();

        public class LiteralExpresion : Expression
        {
            public object Value { get; set; }
            public BedrockType type { get; set; }

            public LiteralExpresion(object val, BedrockType type)
            {
                this.type = type;
                Value = val;
            }

            public override object Accept()
            {
                return Value;
            }
        }

        public class UnaryExpression
        {
            public TokenType Sign { get; set; }
            public Expression expression { get; set; }
        }

        public class BinaryExpression
        {
            public Expression Left { get; set; }
            public TokenType Operator { get; set; }
            public Expression Right { get; set; }

            public BinaryExpression(Expression left, TokenType opp, Expression right)
            {
                Left = left;
                Right = right;
                Operator = opp;
            }
        }
    }
}
