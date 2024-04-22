using System.ComponentModel;

namespace Bedrock
{
    public class SourceButcher
    {
        public static List<Token> Butcher(string text)
        {
            List<Token> tokens = new();
            int line = 1;
            for (int current = 0; current < text.Length; current++)
            {
                char c = text[current];
                switch (c)
                {
                    case ' ':
                    case '\r':
                    case '\t':
                        break;

                    case '\n':
                        line++;
                        break;

                    case '(':
                        tokens.Add(new(TokenType.LeftParentesis, current, line));
                        break;
                    case ')':
                        tokens.Add(new(TokenType.RightParentesis, current, line));
                        break;
                    case '[':
                        tokens.Add(new(TokenType.LeftSquare, current, line));
                        break;
                    case ']':
                        tokens.Add(new(TokenType.RightSquare, current, line));
                        break;
                    case '{':
                        tokens.Add(new(TokenType.LeftBrace, current, line));
                        break;
                    case '}':
                        tokens.Add(new(TokenType.RightBrace, current, line));
                        break;

                    case ';':
                        tokens.Add(new Token(TokenType.EndStatement, current, line));
                        break;

                    case '#':

                        {
                            for (; text.Length > current ? text[current] != '\n' : false; current++)
                                ;
                            line++;
                        }
                        break;

                    case '+':

                        {
                            if (current + 1 < text.Length)
                            {
                                if (text[current + 1] == '=')
                                {
                                    tokens.Add(new(TokenType.PlusEquals, current, line));
                                    current++;
                                }
                                else
                                {
                                    tokens.Add(new(TokenType.Plus, current, line));
                                }
                            }
                        }
                        break;
                    case '=':

                        {
                            if (current + 1 < text.Length)
                            {
                                if (text[current + 1] == '=')
                                {
                                    tokens.Add(new(TokenType.Equality, current, line));
                                    current++;
                                }
                                else
                                {
                                    tokens.Add(new(TokenType.Assign, current, line));
                                }
                            }
                        }
                        break;
                    case ':':

                        {
                            if (current + 1 < text.Length)
                            {
                                if (text[current + 1] == '=')
                                {
                                    tokens.Add(new(TokenType.AutoAssign, current, line));
                                    current++;
                                }
                                else
                                {
                                    tokens.Add(new(TokenType.Colon, current, line));
                                }
                            }
                        }
                        break;
                    case '-':

                        {
                            if (current + 1 < text.Length)
                            {
                                if (text[current + 1] == '=')
                                {
                                    tokens.Add(new(TokenType.MinusEquals, current, line));
                                    current++;
                                }
                                else
                                {
                                    tokens.Add(new(TokenType.Minus, current, line));
                                }
                            }
                        }
                        break;
                    case '*':

                        {
                            if (current + 1 < text.Length)
                            {
                                if (text[current + 1] == '=')
                                {
                                    tokens.Add(new(TokenType.StarEquals, current, line));
                                    current++;
                                }
                                else
                                {
                                    tokens.Add(new(TokenType.Star, current, line));
                                }
                            }
                        }
                        break;
                    case '/':

                        {
                            if (current + 1 < text.Length)
                            {
                                if (text[current + 1] == '=')
                                {
                                    tokens.Add(new(TokenType.SlashEquals, current, line));
                                    current++;
                                }
                                else
                                {
                                    tokens.Add(new(TokenType.Slash, current, line));
                                }
                            }
                        }
                        break;
                    case '%':

                        {
                            if (current + 1 < text.Length)
                            {
                                if (text[current + 1] == '=')
                                {
                                    tokens.Add(new(TokenType.ModulusEquals, current, line));
                                    current++;
                                }
                                else
                                {
                                    tokens.Add(new(TokenType.ModulusEquals, current, line));
                                }
                            }
                        }
                        break;
                    case '^':
                        tokens.Add(new Token(TokenType.Carrot, current, line));
                        break;
                    case '|':
                        tokens.Add(new Token(TokenType.Pipe, current, line));
                        break;
                    case '&':
                        tokens.Add(new Token(TokenType.Ampersand, current, line));
                        break;

                    case '"':

                        {
                            current++;
                            int length = 0;
                            int start = current;
                            for (; current < text.Length ? text[current] != '"' : false; current++)
                            {
                                if (text[current] == '\n')
                                    line++;
                                length++;
                            }
                            tokens.Add(new(TokenType.StringLiteral, current, length));
                        }
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

                        {
                            int start = current,
                                length = 0;

                            for (
                                ;
                                current < text.Length ? char.IsDigit(text[current]) : false;
                                current++
                            )
                            {
                                length++;
                            }

                            if (current < text.Length ? text[current] == '.' : false)
                            {
                                length++;
                                current++;
                                for (
                                    ;
                                    current < text.Length ? char.IsDigit(text[current]) : false;
                                    current++
                                )
                                {
                                    length++;
                                }

                                tokens.Add(
                                    new Token(TokenType.DecimalLiteral, start, line, length)
                                );
                            }
                            else if (current < text.Length ? text[current] == 'x' : false)
                            {
                                length++;
                                current++;
                                for (
                                    ;
                                    current < text.Length ? char.IsDigit(text[current]) : false;
                                    current++
                                )
                                {
                                    length++;
                                }

                                tokens.Add(
                                    new Token(TokenType.HexIntegerLiteral, start, line, length)
                                );
                            }
                            else if (current < text.Length ? text[current] == 'b' : false)
                            {
                                length++;
                                current++;
                                for (
                                    ;
                                    current < text.Length ? char.IsDigit(text[current]) : false;
                                    current++
                                )
                                {
                                    length++;
                                }
                                tokens.Add(
                                    new Token(TokenType.BinIntegerLiteral, start, line, length)
                                );
                            }
                            else
                            {
                                tokens.Add(new(TokenType.IntegerLiteral, start, line, length));
                                current--;
                            }
                        }
                        break;
                }

                if (char.IsLetter(c) || c == '_')
                {
                    int start = current,length=0;
                    for (
                        ;
                        current < text.Length ? char.IsLetterOrDigit(c) || c == '_' : false;
                        current++
                    ) {
                        length++;
                     }
                    tokens.Add(new Token(TokenType.Identifier,start,line,length));
                }
            }

            return tokens;
        }
    }
}
