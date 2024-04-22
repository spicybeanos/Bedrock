namespace Bedrock
{
    public class Syntax
    {
        private readonly Dictionary<string, TokenType> m_stringToTType = new 
        Dictionary<
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
            { "char", TokenType.Char },
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
    }
}
