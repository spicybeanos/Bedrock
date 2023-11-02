public class Token
{
    public TokenType tokenType { get; set; }
    public string tokenText { get; set; }
    public object tokenLiteral { get; set; }
    public int line { get; set; }
    public Token(TokenType tokenType,string tokenText,
    object tokenLiteral,int line)
    {
        this.tokenType = tokenType;
        this.tokenText=tokenText;
        this.tokenLiteral=tokenLiteral;
        this.line=line;
    }
}