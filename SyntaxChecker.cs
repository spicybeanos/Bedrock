


using System.Net;
using System.Reflection.Metadata;

namespace Bedrock
{
    public class SyntaxChecker
    {
        public Queue<Queue<Token>> AllTokens { get; private set; }
        public SyntaxChecker(List<List<Token>> tokens)
        {
            AllTokens = new();
            for (int i = 0; i < tokens.Count; i++)
            {
                Queue<Token> q = new();
                for (int j = 0; j < tokens[i].Count; j++)
                {
                    q.Enqueue(tokens[i][j]);
                }
                AllTokens.Enqueue(q);
            }
        }

        public static SyntaxCheckResult InvalidSymbol(int line, string symbol, string found, string expected, string token)
        {
            return new SyntaxCheckResult
            {
                Line = line,
                Symbol = symbol,
                Success = false,
                Message = $"Invalid symbol \"{symbol}\" found at line {line}.\nExpected a {expected} after {token}, found a {found}"
            };
        }
        public static SyntaxCheckResult InvalidSymbol(int line, string symbol, string found, string expected, Token checkingToken)
        {
            return new SyntaxCheckResult
            {
                Line = line,
                Symbol = symbol,
                Success = false,
                Message = $"Invalid symbol \"{symbol}\" found at line {line}.\nExpected a {expected} after {checkingToken.tokenTag} \"{checkingToken.Text}\", found a {found}"
            };
        }
        public static SyntaxCheckResult IncompleteStatement(int line, string symbol, string message = "")
        {
            return new SyntaxCheckResult()
            {
                Line = line,
                Symbol = symbol,
                Success = false,
                Message = $"Incomplete statement at line {line}, symbol \'{symbol}\'\nExpected a function call ,assignment or declearation.\n{message}"
            };
        }
        public static SyntaxCheckResult IncompleteAssignment(int line, string symbol)
        {
            return new SyntaxCheckResult()
            {
                Line = line,
                Symbol = symbol,
                Success = false,
                Message = $"Incomplete assignment at line {line}, symbol \'{symbol}\'\nRight hand side empty or incomplete."
            };
        }
        public static SyntaxCheckResult InvalidSymbol(int line, string symbol, string found)
        {
            return new SyntaxCheckResult
            {
                Line = line,
                Symbol = symbol,
                Success = false,
                Message = $"Invalid symbol \"{symbol}\" found at line {line}.A {found} was unexpected"
            };
        }
        public static SyntaxCheckResult IllegalDeclearation(int line, string symbol, string found)
        {
            return new SyntaxCheckResult
            {
                Line = line,
                Symbol = symbol,
                Success = false,
                Message = $"Illegal variable declearation.\nFormat is name:type = value\nExpected a data type found {found}"
            };
        }
        public static SyntaxCheckResult IllegalEnding(int line, string symbol)
        {
            return new SyntaxCheckResult
            {
                Line = line,
                Symbol = symbol,
                Success = false,
                Message = $"Illegal ending of statement with symbol \'{symbol}\',line {line}.\nExpected a function call ,assignment or declearation.\n"
            };
        }
        public static SyntaxCheckResult IllegalOperation(int line, string symbol, string value, string operation)
        {
            return new SyntaxCheckResult()
            {
                Success = false,
                Line = line,
                Symbol = symbol,
                Message = $"IllegalOperation in line {line}, symbol {symbol}. {operation} is invalid on {value}"
            };
        }

        public SyntaxCheckResult Check()
        {
            int line = 1;
            while (AllTokens.TryDequeue(out Queue<Token> lineQ))
            {
                Stack<Token> scope = new();

                while (lineQ.TryDequeue(out Token token))
                {
                    //only look whats ahead.
                    //only look back if there's nothing ahead!!
                    switch (token.tokenTag)
                    {
                        case TokenType.Identifier:
                            {
                                if (lineQ.TryPeek(out Token tok))
                                {
                                    switch (tok.tokenTag)
                                    {
                                        case TokenType.Colon:
                                        case TokenType.Parenthesis:
                                        case TokenType.Assigment:
                                        case TokenType.CurlyBracketOpen:
                                            break;
                                        default:
                                            return InvalidSymbol(line, tok.Text, tok.tokenTag.ToString(),
                                            $"{TokenType.Colon}, {TokenType.Parenthesis}, {TokenType.Assigment} or {TokenType.CurlyBracketOpen}"
                                            , $"\'{token.Text}\', a {token.tokenTag}");
                                    }
                                }
                                else
                                {
                                    if (!scope.TryPeek(out Token t))
                                    {
                                        return IncompleteStatement(line, token.Text);
                                    }
                                }
                            }
                            break;
                        case TokenType.KeyWord:
                            {
                                if (lineQ.TryPeek(out Token tok))
                                {
                                    switch (tok.tokenTag)
                                    {
                                        case TokenType.Identifier:
                                        case TokenType.Box:
                                        case TokenType.Number:
                                        case TokenType.Integer:
                                        case TokenType.String:
                                        case TokenType.Boolean:
                                        case TokenType.Parenthesis:
                                            break;
                                        default:
                                            return
                                            InvalidSymbol(line, tok.Text, tok.tokenTag.ToString(),
                                            $"{TokenType.Identifier}, {TokenType.Box} or {TokenType.Parenthesis}"
                                            , token);
                                    }
                                }
                            }
                            break;
                        case TokenType.DataType:
                            {
                                if (lineQ.TryPeek(out Token tok))
                                {
                                    switch (tok.tokenTag)
                                    {
                                        case TokenType.Assigment:
                                            break;
                                        case TokenType.Box:
                                            if (token.Text != Keywords.FUNCTION_DATATYPE_TOKEN)
                                                return InvalidSymbol(line, tok.Text,
                                                tok.tokenTag.ToString(),
                                                TokenType.Assigment.ToString(), token);
                                            break;
                                        default:
                                            return InvalidSymbol(line, tok.Text,
                                             tok.tokenTag.ToString(),
                                             TokenType.Assigment.ToString(), token);
                                    }
                                }
                            }
                            break;
                        case TokenType.Colon:
                            {
                                if (lineQ.TryPeek(out Token tok))
                                {
                                    switch (tok.tokenTag)
                                    {
                                        case TokenType.DataType:
                                            break;
                                        default:
                                            return InvalidSymbol(line, tok.Text, tok.tokenTag.ToString()
                                            , TokenType.DataType.ToString(), token);
                                    }
                                }
                                else
                                {
                                    return IncompleteStatement(line, token.Text, "Expected a data type");
                                }
                            }
                            break;
                        case TokenType.Assigment:
                            {
                                if (lineQ.TryPeek(out Token tok))
                                {

                                }
                                else
                                {
                                    return IncompleteAssignment(line, token.Text);
                                }
                            }
                            break;
                        case TokenType.Number:
                        case TokenType.Integer:
                            {
                                if (lineQ.TryPeek(out Token tok))
                                {
                                    switch (tok.tokenTag)
                                    {
                                        case TokenType.BinaryMathOperation:
                                            break;
                                        case TokenType.BinaryBoolOperation:
                                            return IllegalOperation(line, tok.Text,
                                            token.Text, tok.Text);
                                        default:
                                            return InvalidSymbol(line, tok.Text, tok.tokenTag.ToString());
                                    }
                                }
                            }
                            break;
                        case TokenType.Parenthesis:
                            {
                                if (lineQ.TryPeek(out Token tok))
                                {
                                    switch (tok.tokenTag)
                                    {
                                        case TokenType.BinaryMathOperation:
                                        case TokenType.BinaryBoolOperation:
                                        break;
                                        default:
                                            return InvalidSymbol(line, tok.Text, tok.tokenTag.ToString());
                                    }
                                }
                            }
                            break;
                        case TokenType.String:
                            {
                                if (lineQ.TryPeek(out Token tok))
                                {
                                    switch (tok.tokenTag)
                                    {
                                        case TokenType.BinaryMathOperation:
                                            if (tok.Text != Symbols.PLUS)
                                                return IllegalOperation(line, tok.Text,
                                                token.Text, tok.Text);
                                            break;
                                        case TokenType.BinaryBoolOperation:
                                            return IllegalOperation(line, tok.Text,
                                            token.Text, tok.Text);
                                        default:
                                            return InvalidSymbol(line, tok.Text, tok.tokenTag.ToString());
                                    }
                                }
                            }
                            break;
                        case TokenType.Boolean:
                            {
                                if (lineQ.TryPeek(out Token tok))
                                {
                                    switch (tok.tokenTag)
                                    {
                                        case TokenType.BinaryBoolOperation:
                                            break;
                                        case TokenType.BinaryMathOperation:
                                            return IllegalOperation(line, tok.Text,
                                            token.Text, tok.Text);
                                        default:
                                            return InvalidSymbol(line, tok.Text, tok.tokenTag.ToString());
                                    }
                                }
                            }
                            break;
                        case TokenType.BinaryMathOperation:
                            {
                                if (lineQ.TryPeek(out Token tok))
                                {
                                    switch (tok.tokenTag)
                                    {
                                        case TokenType.Number:
                                        case TokenType.Integer:
                                        case TokenType.Identifier:
                                            break;
                                        default:
                                            return InvalidSymbol(line, tok.Text, tok.tokenTag.ToString(),
                                            $"{TokenType.Number},{TokenType.Integer} or {TokenType.Identifier}", token);
                                    }
                                }
                                else
                                {
                                    return IncompleteAssignment(line, token.Text);
                                }
                            }
                            break;
                        case TokenType.BinaryBoolOperation:
                            {
                                if (lineQ.TryPeek(out Token tok))
                                {
                                    switch (tok.tokenTag)
                                    {
                                        case TokenType.Boolean:
                                        case TokenType.Identifier:
                                            break;
                                        default:
                                            return InvalidSymbol(line, tok.Text, tok.tokenTag.ToString(),
                                            $"{TokenType.Boolean} or {TokenType.Identifier}", token);
                                    }
                                }
                                else
                                {
                                    return IncompleteAssignment(line, token.Text);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    scope.Push(token);
                }
                line++;
            }
            return new SyntaxCheckResult()
            {
                Success = true
            };
        }
    }
    public class SyntaxCheckResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Line { get; set; }
        public string Symbol { get; set; }


    }
}