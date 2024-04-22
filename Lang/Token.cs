namespace Bedrock
{
    public class Token
    {
        public TokenType tokenType{get;set;}
        public int startIndex {get;set;}
        public int length{get;set;}
        public int line{get;set;}
        public Token(TokenType tokenType,int startIndex,int line,int length=1){
            this.tokenType = tokenType;
            this.startIndex = startIndex;
            this.length=length;
            this.line = line;
        }
    }
}