using static Bedrock.BedrockError;

namespace Bedrock
{
    public class Parser
    {
        private readonly List<Token> tokens;
        int current = 0;

        public List<Statement> parse()
        {
            List<Statement> statements = new List<Statement>();
            while (!isAtEnd())
            {
                statements.Add(statement());
            }

            return statements;
        }

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
            Expression left = comparison();

            while (match(TokenType.BangEquals, TokenType.EqualEquals))
            {
                Token opp = previous();
                Expression right = comparison();
                left = new Expression.BinaryExpression(left, opp, right);
            }

            return left;
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
        
        /// <summary>
        /// checks if current token is equal to `type`, if it is,
        /// advance and return true, if not return false and not advance
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
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

        /// <summary>
        /// returns if current token is equal to `type` and if
        /// not at end
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool check(TokenType type)
        {
            if (isAtEnd())
                return false;
            return peek().tokenType == type;
        }

        /// <summary>
        /// checks if the next token is `type`, if it is, returns advance
        /// else throws an exception
        /// </summary>
        /// <param name="type"></param>
        /// <param name="error_message"></param>
        /// <returns></returns>
        private Token consume(TokenType type, string error_message)
        {
            if (check(type))
                return advance();

            throw error(peek(), error_message);
        }

        private BedrockError.ParseError error(Token token, String message)
        {
            BedrockError.Error(token, message);
            return new BedrockError.ParseError();
        }

        /// <summary>
        /// returns the current token and then increments the pointer
        /// </summary>
        /// <returns></returns>
        private Token advance()
        {
            if (!isAtEnd())
                current++;
            return previous();
        }

        /// <summary>
        /// returns if the current token is EOF
        /// </summary>
        /// <returns></returns>
        private bool isAtEnd()
        {
            return peek().tokenType == TokenType.EOF;
        }
        /// <summary>
        /// returns the current token
        /// </summary>
        /// <returns></returns>
        private Token peek()
        {
            return tokens[current];
        }
        /// <summary>
        /// returns the previous token, ie `current - 1`
        /// </summary>
        /// <returns></returns>
        private Token previous()
        {
            return tokens[current - 1];
        }
    }
}
