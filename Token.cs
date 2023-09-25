


namespace Bedrock
{

    public enum TokenType
    {
        Identifier,
        KeyWord,
        DataType,
        ReferenceType,
        FunctionTag,
        Operation,
        BoolOperation,
        Literal,
        Number,
        Charecter,
        String,
        Compute,
        Symbol,
        Colon, Ampersand,
        Assigment,
        Initialization,
        Box,
        Boolean,
        Defination
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