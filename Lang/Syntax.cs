namespace Bedrock
{
    public class Syntax
    {
        private readonly Dictionary<string, TokenType> m_stringToTType = new Dictionary<
            string,
            TokenType
        >()
        {
            { "fxn", TokenType.Function },
            { "byte", TokenType.Byte },
            { "int", TokenType.Int },
            { "long", TokenType.Long },
            { "float", TokenType.Float },
            { "double", TokenType.Double },
            { "string", TokenType.String },
            { "ushort", TokenType.UShort },
            { "bool", TokenType.Bool },
            { "void", TokenType.Void },
            { "return", TokenType.Return },
            { "true", TokenType.True },
            { "false", TokenType.False },
            { "null", TokenType.Null },
            { "if", TokenType.If },
            { "else", TokenType.Else },
            { "while", TokenType.While },
            { "then", TokenType.Then },
            { "switch", TokenType.Switch },
            { "case", TokenType.Case },
            { "import", TokenType.Import },
            { "ref", TokenType.Ref }
        };

        public TokenType GetKeyword(string word)
        {
            if (m_stringToTType.ContainsKey(word))
                return m_stringToTType[word];
            throw new Exception($"not a key word : [{word}]");
        }

        public class KeywordAsString
        {
            public const string Function = "fxn",
                Byte = "byte",
                Int = "int",
                UShort = "ushort",
                Long = "long",
                Float = "float",
                Double = "double",
                String = "string",
                Bool = "bool",
                Void = "void",
                Return = "return",
                True = "true",
                False = "false",
                Null = "null",
                If = "if",
                Else = "else",
                While = "while",
                Then = "then",
                Switch = "switch",
                Case = "case",
                Import = "import",
                Ref = "ref",
                Unique = "unique";
        }
    }
}
