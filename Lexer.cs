namespace Bedrock
{
    public class Lexer
    {
        string Source { get; set; }
        int current { get; set; }
        int start { get; set; }
        int line { get; set; }
        List<Token> tokens { get; set; }
        public Lexer(string buff)
        {
            this.Source = buff;
            current = 0;
            start = 0;
            line = 1;
            tokens = new();
        }

        private bool Next(out char c)
        {
            if (current < Source.Length)
            {
                c = Source[current++];
                return true;
            }
            c = '\0';
            return false;
        }
        private bool Peek(out char c)
        {
            if (current < Source.Length)
            {
                c = Source[current];
                return true;
            }
            c = '\0';
            return false;
        }
        private char Peek()
        {
            if (current < Source.Length)
                return Source[current];
            return '\0';
        }
        private bool Match(char check)
        {
            if (Peek(out char c))
            {
                if (c == check)
                    current++;
                return c == check;
            }
            return false;
        }
        private bool MatchNumOrChar()
        {
            if (Peek(out char c))
            {
                if (char.IsLetterOrDigit(c) || c == '_')
                    current++;
                return char.IsLetterOrDigit(c) || c == '_';
            }
            return false;
        }
        private void AddToken(TokenType tokenType, object literal)
        {
            string text = Source.Substring(start, current - start);
            tokens.Add(new(tokenType, text, literal, line));
        }
        private void AddToken(TokenType tokenType)
        {
            AddToken(tokenType, null);
        }
        private void ScanToken()
        {
            if (Next(out char c))
            {
                switch (c)
                {
                    case '(': AddToken(TokenType.LEFT_PAREN); break;
                    case ')': AddToken(TokenType.RIGHT_PAREN); break;
                    case '{': AddToken(TokenType.LEFT_BRACE); break;
                    case '}': AddToken(TokenType.RIGHT_BRACE); break;
                    case ',': AddToken(TokenType.COMMA); break;
                    case '.': AddToken(TokenType.DOT); break;
                    case '-': AddToken(TokenType.MINUS); break;
                    case '+': AddToken(TokenType.PLUS); break;
                    case ';': AddToken(TokenType.SEMICOLON); break;
                    case '*': AddToken(TokenType.STAR); break;

                    case '!':
                        AddToken(Match('=') ? TokenType.BANG_EQUAL : TokenType.BANG);
                        break;
                    case '=':
                        AddToken(Match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL);
                        break;
                    case '<':
                        AddToken(Match('=') ? TokenType.LESS_EQUAL : TokenType.LESS);
                        break;
                    case '>':
                        AddToken(Match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER);
                        break;
                }
            }
        }
    }
}