namespace Bedrock
{
    public class BedrockError
    {
        public static void UnterminatedString(int line)
        {
            throw new Exception($"Unterminated string at line {line}");
        }

        public static void InvalidNumber(int line)
        {
            throw new Exception($"Invalid number at line {line}");
        }

        internal static void Error(Token token, string message)
        {
            if (token.tokenType == TokenType.EOF)
            {
                Console.WriteLine($"[line {token.line}] at end, {message}");
            }
            else
            {
                Console.WriteLine($"[line {token.line}] at '{token.lexeme}', {message}");
            }
        }

        public class ParseError : Exception { }

        public class RuntimeError : Exception
        {
            public readonly Token token;

            public RuntimeError(Token token, string message) : base(message)
            {
                this.token = token;
            }
        }
    }
}
