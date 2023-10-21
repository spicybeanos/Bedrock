


namespace Bedrock
{

    public enum TokenType
    {
        Identifier,
        KeyWord,
        DataType,
        MathOperation,
        BoolOperation,
        Literal,
        Number,
        Character,
        String,
        Compute,
        Symbol,
        Colon, Ampersand,
        Assigment,
        Box,
        Boolean,
        Definition
    }
    public class Token
    {
        public string Text { get; private set; }
        public List<TokenType> tokenTags { get; set; }
        public List<Token> ChildTokens { get; private set; }
        public bool HasChildren
        {
            get
            {
                if (ChildTokens != null)
                {
                    return ChildTokens.Count > 0;
                }
                else
                {
                    return false;
                }
            }
        }
        public Token(string text)
        {
            Text = text;
            ChildTokens = new();
            tokenTags = new();
        }

        private static readonly Dictionary<string, TokenType>
        SymbolStringToTokenType = new(){
            {Symbols.PLUS,TokenType.MathOperation},
            {Symbols.MINUS,TokenType.MathOperation},
            {Symbols.MULTIPLY,TokenType.MathOperation},
            {Symbols.DIVIDE,TokenType.MathOperation},
            {Symbols.MODULUS,TokenType.MathOperation},
            {Symbols.BANG,TokenType.BoolOperation},
            {Symbols.ASSIGN,TokenType.Assigment},
            {Symbols.EQUALS,TokenType.BoolOperation},
            {Symbols.NOT_EQUALS,TokenType.BoolOperation},
            {Symbols.GREATER,TokenType.BoolOperation},
            {Symbols.LESSER,TokenType.BoolOperation},
            {Symbols.GREATER_EQUAL,TokenType.BoolOperation},
            {Symbols.LESSER_EQUALS,TokenType.BoolOperation},
            {Symbols.FUNCTION_ASSIGN,TokenType.Assigment},
            {Symbols.COLON,TokenType.Symbol},
            {Symbols.AMPERSAND,TokenType.Symbol}
        };
        public static readonly List<string> KEYWORDS = new()
        {
            Keywords.INT_TOKEN,
            Keywords.FLOAT_TOKEN,
            Keywords.STRING_TOKEN,
            Keywords.CHAR_TOKEN ,
            Keywords.BYTE_TOKEN ,
            Keywords.BOOL_TOKEN ,
            Keywords.VOID_TOKEN ,

            Keywords.FUNCTION_TOKEN,
            Keywords.REF_TOKEN ,
            Keywords.STRUCT_TOKEN,
            Keywords.ARRAY_TOKEN ,
            Keywords.IMPORT_TOKEN
        };
        public static readonly List<string> DATATYPES = new(){
            Keywords.INT_TOKEN,
            Keywords.FLOAT_TOKEN ,
            Keywords.STRING_TOKEN,
            Keywords.CHAR_TOKEN ,
            Keywords.BYTE_TOKEN ,
            Keywords.BOOL_TOKEN ,
            Keywords.VOID_TOKEN
        };
        public static readonly List<string> SYMBOLS = new(){
            "+","-","*","/","%","!",
            "=",
            "==","!=",">","<",">=","<=",
            "=>",
            ":","&"
        };

        static bool IsIdentifier(string s)
        {
            return char.IsLetter(s[0]) || s[0] == '_';
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
        static bool IsBody(string s)
        {
            try
            {
                return s[0] == '{' && s[s.Length - 1] == '}';
            }
            catch
            {
                return false;
            }
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
        static TokenType GetLiteralType(string s)
        {
            if (IsNumber(s))
                return TokenType.Number;
            else if (IsChar(s))
                return TokenType.Character;
            else if (IsString(s))
                return TokenType.String;
            else
                return TokenType.Compute;
        }

        public static Token GetToken(string s)
        {
            Token tok = new(s);
            if (IsIdentifier(s))
            {
                if (KEYWORDS.Contains(s))
                {
                    tok.tokenTags.Add(TokenType.KeyWord);
                    if (DATATYPES.Contains(s))
                    {
                        tok.tokenTags.Add(TokenType.DataType);
                    }
                }
                else
                {
                    tok.tokenTags.Add(TokenType.Identifier);
                }
            }
            else if (IsSymbol(s))
            {
                tok.tokenTags.Add(SymbolStringToTokenType[s]);
            }
            else if (IsBody(s))
            {
                tok.tokenTags.Add(TokenType.Definition);
            }
            else if (IsBox(s))
            {
                tok.tokenTags.Add(TokenType.Box);
            }
            else
            {
                tok.tokenTags.Add(TokenType.Literal);
                tok.tokenTags.Add(GetLiteralType(s));
            }
            return tok;
        }
    }
    public class Keywords
    {
        public const string
        INT_TOKEN = "int",
        FLOAT_TOKEN = "float",
        STRING_TOKEN = "string",
        CHAR_TOKEN = "char",
        BYTE_TOKEN = "byte",
        VOID_TOKEN = "void",
        BOOL_TOKEN = "bool",
        FUNCTION_TOKEN = "fxn",
        REF_TOKEN = "ref",
        STRUCT_TOKEN = "struct",
        ARRAY_TOKEN = "array",
        IMPORT_TOKEN = "import";
    }
    public class Symbols
    {
        public const string
            ASTERISK = "*",
            PLUS = "+", MINUS = "-", MULTIPLY = "*", DIVIDE = "/", MODULUS = "%",
            BANG = "!",
            ASSIGN = "=",
            EQUALS = "==", NOT_EQUALS = "!=",
            GREATER = ">", LESSER = "<",
            GREATER_EQUAL = ">=", LESSER_EQUALS = "<=",
            FUNCTION_ASSIGN = "=>",
            COLON = ":",
            AMPERSAND = "&";
    }
}