namespace Bedrock
{
    public class Token
    {
        public TokenType tokenType{get;set;}
        public object literalValue {get;set;}
        public string lexeme {get;set;}
        public int line{get;set;}
        public Token(TokenType tokenType,string lexeme,int line){
            this.tokenType = tokenType;
            this.lexeme = lexeme;
            this.line = line;

            if(tokenType == TokenType.IntegerLiteral){
                literalValue = LiteralParser.ParseInteger(this);
            }
            else if(tokenType == TokenType.DecimalLiteral){
                literalValue = double.Parse(lexeme);
            }
            else if(tokenType == TokenType.StringLiteral){
                literalValue = LiteralParser.ParseString(this);
            }
        }
    }
}