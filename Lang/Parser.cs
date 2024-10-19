using static Bedrock.BedrockError;

namespace Bedrock
{
    public class Parser
    {
        private readonly List<Token> tokens;
        int current = 0;

        public Expression Parse()
        {
            try
            {
                return expression();
            }
            catch (ParseError)
            {
                return null;
            }
        }

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        private Expression expression()
        {
            return equality();
        }

        private Expression equality()
        {
            Expression expr = comparison();

            while (match(TokenType.BangEquals, TokenType.EqualEquals))
            {
                Token opp = previous();
                Expression right = comparison();
                expr = new Expression.BinaryExpression(expr, opp, right);
            }

            return expr;
        }

        private Expression comparison()
        {
            Expression expr = term();

            while (
                match(
                    TokenType.RightAngle_Greater,
                    TokenType.GreaterEquals,
                    TokenType.LeftAngle_Lesser,
                    TokenType.LesserEquals
                )
            )
            {
                Token opp = previous();
                Expression right = term();
                expr = new Expression.BinaryExpression(expr, opp, right);
            }

            return expr;
        }

        private Expression term()
        {
            Expression expr = factor();

            while (match(TokenType.Minus, TokenType.Plus))
            {
                Token op = previous();
                Expression right = factor();
                expr = new Expression.BinaryExpression(expr, op, right);
            }

            return expr;
        }

        private Expression factor()
        {
            Expression expr = unary();

            while (match(TokenType.Slash, TokenType.Star))
            {
                Token op = previous();
                Expression right = unary();
                expr = new Expression.BinaryExpression(expr, op, right);
            }

            return expr;
        }

        private Expression unary()
        {
            if (match(TokenType.Bang, TokenType.Minus))
            {
                Token opr = previous();
                Expression right = unary();
                return new Expression.UnaryExpression(opr.tokenType, right);
            }

            return primary();
        }

        private Expression primary()
        {
            if (match(TokenType.False))
                return new Expression.LiteralExpresion(false, BedrockNativeType.Bool);
            if (match(TokenType.True))
                return new Expression.LiteralExpresion(true, BedrockNativeType.Bool);
            if (match(TokenType.Null))
                return new Expression.LiteralExpresion(null, BedrockNativeType.Void);

            if (match(TokenType.IntegerLiteral, TokenType.DecimalLiteral, TokenType.StringLiteral))
            {
                var lit = previous().literalValue;
                return new Expression.LiteralExpresion(lit);
            }

            if (match(TokenType.LeftParentesis))
            {
                Expression expr = expression();
                consume(TokenType.RightParentesis, "Expect ')' after expression.");
                return new Expression.GroupingExpression(expr);
            }

            throw error(peek(), "Expect expression.");
        }

        private void synchronize()
        {
            advance();

            while (!isAtEnd())
            {
                if (previous().tokenType == TokenType.EndStatement)
                    return;

                switch (peek().tokenType)
                {
                    case TokenType.Class:
                    case TokenType.Fxn:
                    case TokenType.Var:
                    case TokenType.For:
                    case TokenType.If:
                    case TokenType.While:
                    case TokenType.Return:
                        return;
                }

                advance();
            }
        }

        private bool match(params TokenType[] types)
        {
            foreach (TokenType type in types)
            {
                if (check(type))
                {
                    advance();
                    return true;
                }
            }

            return false;
        }

        private bool check(TokenType type)
        {
            if (isAtEnd())
                return false;
            return peek().tokenType == type;
        }

        private Token consume(TokenType type, String message)
        {
            if (check(type))
                return advance();

            throw error(peek(), message);
        }

        private BedrockError.ParseError error(Token token, String message)
        {
            BedrockError.Error(token, message);
            return new BedrockError.ParseError();
        }

        private Token advance()
        {
            if (!isAtEnd())
                current++;
            return previous();
        }

        private bool isAtEnd()
        {
            return peek().tokenType == TokenType.EOF;
        }

        private Token peek()
        {
            return tokens[current];
        }

        private Token previous()
        {
            return tokens[current - 1];
        }
    }
}
