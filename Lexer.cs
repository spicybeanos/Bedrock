




namespace Bedrock
{
    public class Lexer
    {
        public class Lex
        {
            public int startIndex;
            public LexType lexType;
        }
        public enum LexType
        {
            Keyword,
            Datatype,
            Equals,
            Identifier,
            Number,
            Symbol,
            Comment,
            Function,
            BoxBracket,
            CurlyBracket,
            Parenthesis
        }

        string buff;
        public Lexer(string buff)
        {
            this.buff = buff;
        }
    }
}