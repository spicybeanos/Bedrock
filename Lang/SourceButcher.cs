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
                        tokens.Add(new(TokenType.LeftParentesis, "(", line));
                        break;
                    case ')':
                        tokens.Add(new(TokenType.RightParentesis, ")", line));
                        break;
                    case '[':
                        tokens.Add(new(TokenType.LeftSquare, "[", line));
                        break;
                    case ']':
                        tokens.Add(new(TokenType.RightSquare, "]", line));
                        break;
                    case '{':
                        tokens.Add(new(TokenType.LeftBrace, "{", line));
                        break;
                    case '}':
                        tokens.Add(new(TokenType.RightBrace, "}", line));
                        break;

                    case ';':
                        tokens.Add(new Token(TokenType.EndStatement, ";", line));
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
                                    tokens.Add(new(TokenType.PlusEquals, "+=", line));
                                    current++;
                                }
                                else
                                {
                                    tokens.Add(new(TokenType.Plus, "+", line));
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
                                    tokens.Add(new(TokenType.EqualEquals, "==", line));
                                    current++;
                                }
                                else
                                {
                                    tokens.Add(new(TokenType.Assign, "=", line));
                                }
                            }
                        }
                        break;
                    case '>':

                        {
                            if (current + 1 < text.Length)
                            {
                                if (text[current + 1] == '=')
                                {
                                    tokens.Add(new(TokenType.GreaterEquals, ">=", line));
                                    current++;
                                }
                                else
                                {
                                    tokens.Add(new(TokenType.RightAngle_Greater, ">", line));
                                }
                            }
                        }
                        break;
                    case '<':

                        {
                            if (current + 1 < text.Length)
                            {
                                if (text[current + 1] == '=')
                                {
                                    tokens.Add(new(TokenType.LesserEquals, "<=", line));
                                    current++;
                                }
                                else
                                {
                                    tokens.Add(new(TokenType.LeftAngle_Lesser, "<", line));
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
                                    tokens.Add(new(TokenType.AutoAssign, ":=", line));
                                    current++;
                                }
                                else
                                {
                                    tokens.Add(new(TokenType.Colon, ":", line));
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
                                    tokens.Add(new(TokenType.MinusEquals, "-=", line));
                                    current++;
                                }
                                else
                                {
                                    tokens.Add(new(TokenType.Minus, "-", line));
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
                                    tokens.Add(new(TokenType.StarEquals, "*=", line));
                                    current++;
                                }
                                else
                                {
                                    tokens.Add(new(TokenType.Star, "*", line));
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
                                    tokens.Add(new(TokenType.SlashEquals, "/=", line));
                                    current++;
                                }
                                else
                                {
                                    tokens.Add(new(TokenType.Slash, "/", line));
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
                                    tokens.Add(new(TokenType.ModulusEquals, "%=", line));
                                    current++;
                                }
                                else
                                {
                                    tokens.Add(new(TokenType.ModulusEquals, "%", line));
                                }
                            }
                        }
                        break;
                    case '^':
                        tokens.Add(new Token(TokenType.Carrot, "^", line));
                        break;
                    case '|':
                        tokens.Add(new Token(TokenType.Pipe, "|", line));
                        break;
                    case '&':
                        tokens.Add(new Token(TokenType.Ampersand, "&", line));
                        break;

                    case '"':

                        {
                            current++;
                            int length = 0;
                            int start = current;
                            bool escape = false;
                            for (
                                ;
                                current < text.Length ? escape || text[current] != '"' : false;
                                current++
                            )
                            {
                                if (text[current] == '\n')
                                    line++;
                                length++;
                                escape = false;
                                if (text[current] == '\\')
                                    escape = true;
                            }
                            tokens.Add(new(TokenType.StringLiteral, text.Substring(start,length), line));
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
                                current++, length++
                            )
                                ;

                            if (current < text.Length ? text[current] == '.' : false)
                            {
                                length++;
                                current++;
                                for (
                                    ;
                                    current < text.Length
                                        ? char.IsDigit(text[current]) && text[current] != ' '
                                        : false;
                                    current++, length++
                                )
                                ;

                                tokens.Add(
                                   new Token(TokenType.DecimalLiteral, text.Substring(start,length), line)
                                );
                            }
                            else if (
                                current < text.Length
                                    ? (text[current] == 'x' || text[current] == 'X')
                                    : false
                            )
                            {
                                length++;
                                current++;
                                for (
                                    ;
                                    current < text.Length
                                        ? char.IsDigit(text[current]) && text[current] != ' '
                                        : false;
                                    current++, length++
                                )
                                    ;

                                tokens.Add(
                                    new Token(TokenType.IntegerLiteral, text.Substring(start,length), line)
                                );
                                current--;
                            }
                            else if (
                                current < text.Length
                                    ? (text[current] == 'b' || text[current] == 'B')
                                    : false
                            )
                            {
                                length++;
                                current++;
                                for (
                                    ;
                                    current < text.Length
                                        ? char.IsDigit(text[current]) && text[current] != ' '
                                        : false;
                                    current++, length++
                                )
                                    ;

                                tokens.Add(
                                    new Token(TokenType.IntegerLiteral, text.Substring(start,length), line)
                                );
                                current--;

                                
                            }
                            else
                            {
                                tokens.Add(new(TokenType.IntegerLiteral, text.Substring(start,length), line));
                                current--;
                            }
                        }
                        break;

                    default:

                        {
                            if (char.IsLetter(text[current]) || text[current] == '_')
                            {
                                int start = current,
                                    length = 0;
                                for (
                                    ;
                                    current < text.Length
                                        ? (
                                            char.IsLetterOrDigit(text[current])
                                            || text[current] == '_'
                                        )
                                            && text[current] != ' '
                                        : false;
                                    current++, length++
                                )
                                    ;
                                string word_ = text.Substring(start, length);
                                var tt = Syntax.GetKeyword(word_);
                                tokens.Add(new Token(tt, text.Substring(start,length), line));
                                current--;
                            }
                        }
                        break;
                }
            }

            tokens.Add(new Token(TokenType.EOF, "", line));

            return tokens;
        }
    }
}
