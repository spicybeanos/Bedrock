namespace Bedrock
{
    public class Interpreter : Visitor<object>
    {
        public override object VisitBinary(Expression.BinaryExpression expr)
        {
            object left = Evaluate(expr.Left);
            object right = Evaluate(expr.Right);
            var t1 = BedrockTypeHandler.GetNativeType(left);
            var t2 = BedrockTypeHandler.GetNativeType(right);
            var ht = BedrockTypeHandler.GetHigherType(left, right);
            Number l = new Number(left);
            Number r = new Number(right);

            switch (expr.Operator)
            {
                case TokenType.EqualEquals:
                    return isEqual(left, right);
                case TokenType.BangEquals:
                    return !isEqual(left, right);
                default:
                {
                    switch (Number.IsNumber(ht))
                    {
                        case true:
                        {
                            switch (expr.Operator)
                            {
                                case TokenType.Minus:
                                    return (l - r).value;
                                case TokenType.Plus:
                                    return (l + r).value;
                                case TokenType.Slash:
                                    return (l / r).value;
                                case TokenType.Star:
                                    return (l * r).value;
                                case TokenType.GreaterEquals:
                                    return l >= r;
                                case TokenType.RightAngle_Greater:
                                    return l > r;
                                case TokenType.LesserEquals:
                                    return l <= r;
                                case TokenType.LeftAngle_Lesser:
                                    return l < r;
                                //unreachable
                                default:
                                    return null;
                            }
                        }
                        case false:
                        {
                            if (t1 == BedrockNativeType.String && t1 == t2)
                            {
                                int c = ((string)left).CompareTo((string)right);
                                switch (expr.Operator)
                                {
                                    case TokenType.GreaterEquals:
                                        return c >= 0;
                                    case TokenType.RightAngle_Greater:
                                        return c > 0;
                                    case TokenType.LesserEquals:
                                        return c <= 0;
                                    case TokenType.LeftAngle_Lesser:
                                        return c < 0;
                                    case TokenType.Plus:
                                        return (string)BedrockTypeHandler.Cast(left, BedrockNativeType.String)
                                + (string)BedrockTypeHandler.Cast(right, BedrockNativeType.String);
                                    //unreachable
                                    default:
                                        throw new BedrockError.RuntimeError(expr.token,$"Operator '{expr.Operator}' is not valid for {ht}");
                                }
                            }
                            else{
                                throw new BedrockError.RuntimeError(expr.token,$"No operator operation exists for {ht}!");
                            }
                        }
                    }
                }
            }
        }

        private bool isEqual(object a, object b)
        {
            if (a == null && b == null)
                return true;
            if (a == null)
                return false;

            return a.Equals(b);
        }

        public override object VisitGroup(Expression.GroupingExpression expr)
        {
            return Evaluate(expr.expression);
        }

        public override object VisitLiteral(Expression.LiteralExpresion expr)
        {
            return expr.Value;
        }

        public override object VisitUnary(Expression.UnaryExpression expr)
        {
            object right = Evaluate(expr.expression);

            switch (expr.Sign)
            {
                case TokenType.Bang:
                    return !IsTruthy(right);
                case TokenType.Minus:
                    return (-new Number(right)).value;
            }

            // Unreachable.
            return null;
        }

        private bool IsTruthy(object right)
        {
            if (right == null)
                return false;
            if (right is bool)
                return (bool)right;
            return true;
        }

        public object Evaluate(Expression expr)
        {
            return expr.Accept(this);
        }
    }
}
