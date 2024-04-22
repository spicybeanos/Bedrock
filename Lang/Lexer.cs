using System.Reflection.PortableExecutable;

namespace Bedrock
{
    public class Lexer
    {
        public string Text { get; set; } = "";
        public int current { get; set; } = 0;
        public int line { get; set; } = 1;
        public List<Token> tokens { get; set; }

        public Lexer(string text)
        {
            Text = text;
            tokens = new List<Token>();
            current = 0;
            line = 1;
        }

        public bool Advance(out char c)
        {
            if (!IsAtEnd())
            {
                c = Text[current++];
                return true;
            }
            c = (char)0;
            return false;
        }

        public bool Peep(out char c)
        {
            if (!IsAtEnd())
            {
                c = Text[current];
                return true;
            }
            c = (char)0;
            return false;
        }

        public char Peek()
        {

            if (!IsAtEnd())
            {
                return Text[current];
            }
            return (char)0;
        }

        public bool IsAtEnd()
        {
            return current >= Text.Length;
        }

        public void AddToken(TokenType type, int length = 1)
        {
            tokens.Add(
                new(
                    type,
                    current,
                    line,
                    length
                )
            );
        }

        public void ScanAll()
        {
            while (!IsAtEnd())
            {
                ScanNext();
            }
        }

        public bool Match(char expected)
        {
            if (Peep(out char c))
            {
                if (c == expected)
                {
                    current++;
                    return true;
                }
            }
            return false;
        }

        public bool CheckPeep(char compare)
        {
            if (Peep(out char c))
                return c == compare;
            return false;
        }

        public void ScanNext()
        {
            if (Advance(out char c))
            {
                switch (c)
                {
                    case '(':
                        AddToken(TokenType.LeftParentesis);
                        break;
                    case ')':
                        AddToken(TokenType.RightParentesis);
                        break;
                    case '[':
                        AddToken(TokenType.LeftSquare);
                        break;
                    case ']':
                        AddToken(TokenType.RightSquare);
                        break;
                    case '{':
                        AddToken(TokenType.LeftBrace);
                        break;
                    case '}':
                        AddToken(TokenType.RightBrace);
                        break;

                    case '<':
                        AddToken(Match('=') ? TokenType.LesserEquals : TokenType.LeftAngle);
                        break;
                    case '>':
                        AddToken(Match('=') ? TokenType.GreaterEquals : TokenType.RightAngle);
                        break;

                    case '!':
                        AddToken(Match('=') ? TokenType.BangEquals : TokenType.Bang);
                        break;

                    case '+':
                        AddToken(TokenType.Plus);
                        break;
                    case '-':
                        AddToken(TokenType.Minus);
                        break;
                    case '*':
                        AddToken(TokenType.Star);
                        break;
                    case '/':

                        {
                            if (Match('/'))
                            {
                                // A comment goes until the end of the line.
                                while (!CheckPeep('\n') && !IsAtEnd())
                                    Advance(out char _);
                            }
                            else
                            {
                                AddToken(TokenType.Slash);
                            }
                        }
                        break;
                    case '%':
                        AddToken(TokenType.Modulus);
                        break;

                    case '=':
                        AddToken(Match('=') ? TokenType.Equality : TokenType.Assign);
                        break;

                    case ' ':
                    case '\r':
                    case '\t':
                        // Ignore whitespace.
                        break;

                    case '\n':
                        line++;
                        break;

                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        AddNumber();
                        break;

                    case '"':
                        AddString();
                        break;

                    default:
                        if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c == '_'))
                            AddIdentifier();
                        break;
                }
            }
        }

        private bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private void AddIdentifier()
        {
            int start = current;
            int length = 0;
            do
            {
                length++;
            } while (!CheckPeepIdentifierStopper());
        }

        private bool CheckPeepIdentifierStopper()
        {
            if (Peep(out char c))
                return c == ' '
                    || c == '\n'
                    || c == '\r'
                    || c == '\t'
                    || c == '('
                    || c == ')'
                    || c == '{'
                    || c == '}'
                    || c == '['
                    || c == ']'
                    || c == '<'
                    || c == '='
                    || c == '>'
                    || c == '-'
                    || c == '*'
                    || c == '/'
                    || c == '%'
                    || c == '^'
                    || c == '&'
                    || c == '|'
                    || c == '+';
            return false;
        }

        private void AddNumber()
        {
            int start = current,
                length = 0;
            while (IsDigit(Peek()))
            {
                Advance(out char _);
                length++;
            }

            // Look for a fractional part.
            bool isDecimal = false;
            if (Peek() == '.' && IsDigit(PeekNext()))
            {
                // Consume the "."
                isDecimal = true;
                length++;

                Advance(out char _);

                while (IsDigit(Peek()))
                {
                    Advance(out char _);
                    length++;
                }
            }

            tokens.Add(
                new(
                
                    isDecimal ? TokenType.DecimalLiteral : TokenType.IntegerLiteral,
                    start,
                    line = line,
                    length
                
                )
            );
        }

        private char PeekNext()
        {
            if (current + 1 >= Text.Length)
                return '\0';
            return Text[(current + 1)];
        }

        public void AddString()
        {
            int start = current;
            int length = 0;
            while (!CheckPeep('"'))
            {
                if (CheckPeep('\n'))
                    line++;
                Advance(out char _);
                length++;
            }

            if (IsAtEnd())
            {
                BedrockError.UnterminatedString(line);
                return;
            }

            Advance(out char _);

            tokens.Add(
                new Token(
                     TokenType.StringLiteral,
                     start,
                     line,
                     length
                    
                    
                )
            );
        }
    }
}
