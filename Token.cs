


using System.Linq.Expressions;

namespace Bedrock
{

    public enum TokenType
    {
        Null,
        Identifier,
        KeyWord,
        DataType,
        BinaryMathOperation,
        BinaryBoolOperation,

        Number,
        Integer,
        Character,
        String,
        Boolean,

        Parenthesis,
        Symbol,
        Colon, Ampersand,
        Assigment,
        FunctionLocationAssign,

        Box,
        CurlyBracketOpen,
        CurlyBracketClose,
        EndOfLine
    }
    public class Token
    {
        public string Text { get; private set; }
        public string tokenTagText {get{return tokenTag.ToString();}}
        public TokenType tokenTag { get; set; }
        public Token(string text)
        {
            Text = text;
            tokenTag = TokenType.Null;
        }
        public Token(string text,TokenType tokenType)
        {
            Text = text;
            tokenTag = tokenType;
        }

        internal static readonly Dictionary<string, TokenType>
        SymbolStringToTokenType = new()
        {
            {Symbols.PLUS,TokenType.BinaryMathOperation},
            {Symbols.MINUS,TokenType.BinaryMathOperation},
            {Symbols.ASTERISK,TokenType.BinaryMathOperation},
            {Symbols.DIVIDE,TokenType.BinaryMathOperation},
            {Symbols.MODULUS,TokenType.BinaryMathOperation},
            {Symbols.BANG,TokenType.BinaryBoolOperation},
            {Symbols.ASSIGN,TokenType.Assigment},
            {Symbols.EQUALS,TokenType.BinaryBoolOperation},
            {Symbols.NOT_EQUALS,TokenType.BinaryBoolOperation},
            {Symbols.GREATER,TokenType.BinaryBoolOperation},
            {Symbols.LESSER,TokenType.BinaryBoolOperation},
            {Symbols.GREATER_EQUAL,TokenType.BinaryBoolOperation},
            {Symbols.LESSER_EQUALS,TokenType.BinaryBoolOperation},
            {Symbols.FUNCTION_ASSIGN,TokenType.FunctionLocationAssign},
            {Symbols.COLON,TokenType.Colon},
            {Symbols.AMPERSAND,TokenType.Symbol},
            {Symbols.PIPE,TokenType.Symbol}
        };
        internal static readonly List<string> KEYWORDS = new()
        {
            Keywords.INT_TOKEN,
            Keywords.NUMBER_TOKEN,
            Keywords.STRING_TOKEN,
            Keywords.CHAR_TOKEN ,
            Keywords.BYTE_TOKEN ,
            Keywords.BOOL_TOKEN ,
            Keywords.VOID_TOKEN ,
            Keywords.FUNCTION_DATATYPE_TOKEN,

            Keywords.IF_TOKEN,
            Keywords.ELSE_TOKEN,
            Keywords.WHILE_TOKEN,

            Keywords.FUNCTION_TOKEN,
            Keywords.REF_TOKEN ,
            Keywords.STRUCT_TOKEN,
            Keywords.ARRAY_TOKEN ,
            Keywords.IMPORT_TOKEN,

            Keywords.BOOL_FALSE,
            Keywords.BOOL_TRUE,
            Keywords.BOOL_OP_AND,
            Keywords.BOOL_OP_OR,

            Keywords.RETURN
        };
        internal static readonly List<string> DATATYPES = new()
        {
            Keywords.INT_TOKEN,
            Keywords.NUMBER_TOKEN,
            Keywords.STRING_TOKEN,
            Keywords.CHAR_TOKEN ,
            Keywords.BYTE_TOKEN ,
            Keywords.BOOL_TOKEN ,
            Keywords.VOID_TOKEN,
            Keywords.FUNCTION_DATATYPE_TOKEN
        };
        internal static readonly List<string> SYMBOLS = new()
        {
            Symbols.PLUS,
            Symbols.MINUS ,
            Symbols.ASTERISK ,
            Symbols.DIVIDE,
            Symbols.MODULUS ,
            Symbols.BANG ,
            Symbols.ASSIGN ,
            Symbols.EQUALS ,
            Symbols.NOT_EQUALS ,
            Symbols.GREATER,
            Symbols.LESSER ,
            Symbols.GREATER_EQUAL ,
            Symbols.LESSER_EQUALS ,
            Symbols.FUNCTION_ASSIGN,
            Symbols.COLON ,
            Symbols.AMPERSAND,
            Symbols.PIPE
        };


        public static Token GetToken(string s)
        {
            Token tok = new(s);
            if (IsIdentifier(s))
            {
                if (KEYWORDS.Contains(s))
                {
                    if (DATATYPES.Contains(s))
                    {
                        tok.tokenTag = TokenType.DataType;
                    }
                    else
                    {
                        tok.tokenTag = TokenType.KeyWord;
                    }
                }
                else
                {
                    tok.tokenTag = TokenType.Identifier;
                }
            }
            else if (IsSymbol(s))
            {
                tok.tokenTag = SymbolStringToTokenType[s];
            }
            else if (s == Symbols.CURLY_OPEN)
            {
                tok.tokenTag = TokenType.CurlyBracketOpen;
            }
            else if (s == Symbols.CURLY_CLOSE)
            {
                tok.tokenTag = TokenType.CurlyBracketClose;
            }
            else if (IsBox(s))
            {
                tok.tokenTag = TokenType.Box;
            }
            else
            {
                tok.tokenTag = GetLiteralType(s);
            }
            return tok;
        }

        static bool IsIdentifier(string s)
        {
            try
            {
                return (char.IsLetter(s[0]) || s[0] == '_') && !s.Contains(Symbols.COMMA);
            }
            catch
            {
                return false;
            }
        }
        static bool IsNumber(string s)
        {
            try
            {
                if (char.IsDigit(s[0]))
                    return true;
                else if (s[0] == '-')
                    if (char.IsDigit(s[1]))
                        return true;

                return false;
            }
            catch
            {
                return false;
            }
        }
        static bool IsInteger(string s)
        {
            try
            {
                if (IsNumber(s))
                {
                    return !s.Contains(Symbols.DOT);
                }
                return false;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        static bool IsString(string s)
        {
            try
            {
                if (s[0] == '\"' && s[s.Length - 1] == '\"')
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        static bool IsChar(string s)
        {
            try
            {
                if (s[0] == '\'' && s[s.Length - 1] == '\'')
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        static bool IsSymbol(string s)
        {
            return SYMBOLS.Contains(s.Trim());
        }
        static bool IsBox(string s)
        {
            try
            {
                return s[0] == '[' && s[s.Length - 1] == ']';
            }
            catch
            {
                return false;
            }
        }
        static bool IsBoolean(string value)
        {
            return value == Keywords.BOOL_TRUE || value == Keywords.BOOL_FALSE;
        }
        static TokenType GetLiteralType(string s)
        {
            if (IsNumber(s))
                if(IsInteger(s))
                    return TokenType.Integer;
                else
                    return TokenType.Number;
            else if (IsChar(s))
                return TokenType.Character;
            else if (IsString(s))
                return TokenType.String;
            else if (IsBoolean(s))
                return TokenType.Boolean;
            else
                return TokenType.Parenthesis;
        }
    }
    public class Keywords
    {
        public const string
        INT_TOKEN = "int",
        NUMBER_TOKEN = "num",
        STRING_TOKEN = "string",
        CHAR_TOKEN = "char",
        BYTE_TOKEN = "byte",
        VOID_TOKEN = "void",
        BOOL_TOKEN = "bool",
        FUNCTION_TOKEN = "fxn",
        FUNCTION_DATATYPE_TOKEN = "Fxn",
        REF_TOKEN = "ref",
        STRUCT_TOKEN = "struct",
        WHILE_TOKEN = "while",
        IF_TOKEN = "if",
        ELSE_TOKEN = "else",
        ARRAY_TOKEN = "array",
        IMPORT_TOKEN = "import",
        BOOL_TRUE = "true",
        BOOL_FALSE = "false",
        BOOL_OP_AND = "and",
        BOOL_OP_OR = "or",
        RETURN = "return";
    }
    public class Symbols
    {
        public const string
            PLUS = "+", MINUS = "-", ASTERISK = "*", DIVIDE = "/", MODULUS = "%",
            BANG = "!",
            ASSIGN = "=",
            EQUALS = "==", NOT_EQUALS = "!=",
            GREATER = ">", LESSER = "<",
            GREATER_EQUAL = ">=", LESSER_EQUALS = "<=",
            FUNCTION_ASSIGN = "=>",
            COLON = ":",
            AMPERSAND = "&", PIPE = "|",
            
            
            
            
            DOT = ".",CURLY_OPEN = "{",CURLY_CLOSE = "}",COMMA = ",";
    }
}