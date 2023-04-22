using System;


namespace Bedrock
{
    public class Tokenizer
    {
        public static readonly Dictionary<string,Keyword> Keywords = new Dictionary<string,Keyword>()
        {
            {"struct",Keyword.Struct},
            {"var",Keyword.Var},
            {"int",Keyword.Int},
            {"string",Keyword.String},
            {"byte",Keyword.Byte},
            {"float",Keyword.Float},
            {"bool",Keyword.Bool},
            {"fxn",Keyword.Function},
            {"Fxn",Keyword.FunctionObject},
            {"if",Keyword.If},
            {"while",Keyword.WhileLoop},
            {"loop",Keyword.Loop},
            {"for",Keyword.ForeachLoop},
            {"or",Keyword.Or},
            {"and",Keyword.And},
            {"null",Keyword.Null}
        };
        public static readonly Dictionary<string,Operator> Operators = new Dictionary<string, Operator>()
        {
            {"+",Operator.Plus},
            {"-",Operator.Minus},
            {"*",Operator.Multiply},
            {"/",Operator.Divide},
            {"%",Operator.Modulous},
            {">",Operator.GreaterThan},
            {"<",Operator.LessThan},
            {"=",Operator.Equals},
            {"!",Operator.BoolInvert}
        };

        public static bool IsIdentifier(string s){
            if(char.IsDigit(s[0])){
                return false;
            }
            if(char.IsPunctuation(s[0])){
                return false;
            }
            if(char.IsSymbol(s[0])){
                return false;
            }
            return true;
        }

        public static Statement[] Bake(string[][] tokens){
            List<Statement> statements = new List<Statement>();
            for (int line = 0; line < tokens.Length; line++)
            {
                Statement s_ = new Statement();
                for (int i = 0; i < tokens[line].Length; i++)
                {
                    s_ += Token.Parse(tokens[line][i]);
                }
                statements.Add(s_);
            }
            return statements.ToArray();
        }
    }

    public class Token
    {
        public object TokenValue {get;set;}
        public string TokenType {get {return TokenValue.GetType().ToString();}}
        public Token(string value)
        {
            if(Tokenizer.Keywords.ContainsKey(value))
            {
                TokenValue = Tokenizer.Keywords[value];
            }else if(Tokenizer.Operators.ContainsKey(value)){
                TokenValue = Tokenizer.Operators[value];
            }else if(IsIdentifier(value)){
                TokenValue = (BedIdentifier)value;
            }else{
                TokenValue = LiteralParser.Parse(value);
            }
        }
        public static Token Parse(string value){
            Token t = new Token();
            if(Tokenizer.Keywords.ContainsKey(value))
            {
                t.TokenValue = Tokenizer.Keywords[value];
            }else if(Tokenizer.Operators.ContainsKey(value)){
                t.TokenValue = Tokenizer.Operators[value];
            }else if(IsIdentifier(value)){
                t.TokenValue = (BedIdentifier)value;
            }else if(value[0] == '(' && value[value.Length-1] == ')'){
                t.TokenValue = (BedComputeValue)value;
            }else if(value[0] == '{' && value[value.Length-1] == '}'){
                t.TokenValue = (BedDefination)value;
            }else{
                t.TokenValue = LiteralParser.Parse(value);
            }
            return t;
        }
        public Token (){}
        public override string ToString()
        {
            return TokenValue.ToString();
        }
        public static implicit operator BedString(Token t) => (BedString)t.TokenValue;
        public static implicit operator BedInt(Token t) => (BedInt)t.TokenValue;
        public static implicit operator BedFloat(Token t) => (BedFloat)t.TokenValue;
        public static implicit operator BedByte(Token t) => (BedByte)t.TokenValue;
        public static implicit operator Keyword(Token t) => (Keyword)t.TokenValue;
        public static implicit operator Operator(Token t) => (Operator)t.TokenValue;
        
        public static implicit operator Token(BedString x) => new Token(){TokenValue = x};
        public static implicit operator Token(BedInt x) => new Token(){TokenValue = x};
        public static implicit operator Token(BedFloat x) => new Token(){TokenValue = x};
        public static implicit operator Token(BedByte x) => new Token(){TokenValue = x};
        public static implicit operator Token(Keyword x) => new Token(){TokenValue = x};
        public static implicit operator Token(Operator x) => new Token(){TokenValue = x};

        public static explicit operator BedIdentifier(Token x) => new BedIdentifier(x);

        public static bool IsIdentifier(string s)
        {
            if(char.IsDigit(s[0]))
                return false;
            if(char.IsPunctuation(s[0]))
                return false;
            if(char.IsSymbol(s[0]) && s[0] != '&')
                return false;
            return true;
        }
    }
    public class BedIdentifier
    {
        const char ReferenceChar = '&';
        public string IdentifierName {get;set;}
        public bool IsReference {
            get {
                return IdentifierName.StartsWith(ReferenceChar);
            }
        }
        public BedIdentifier(string s = ""){IdentifierName = s;}
        public static explicit operator string(BedIdentifier b) => b.IdentifierName;
        public static explicit operator BedIdentifier(string s) => new BedIdentifier(s);
    }
    public class BedComputeValue{
        public string ComputeString {get;set;}
        public BedComputeValue(string s = ""){ComputeString = s;}
        public static explicit operator string(BedComputeValue b) => b.ComputeString;
        public static explicit operator BedComputeValue(string s) => new BedComputeValue(s);
    }
    public class BedDefination{
        public string DefinationString {get;set;}
        public BedDefination(string s = ""){DefinationString = s;}
        public static explicit operator string(BedDefination b) => b.DefinationString;
        public static explicit operator BedDefination(string s) => new BedDefination(s);
    }
    public class Statement
    {
        public List<Token> tokens {get;set;}
        public int Length{
            get {
                return tokens.Count;
            }
        }
        public Token this[int index]{ get{return tokens[index];}set{tokens[index] = value;}}
        public Statement(Token[] tokens_)
        { 
            tokens = new List<Token>();
            tokens.AddRange(tokens_);
        }
        public Statement(List<Token> tokens_){ tokens = tokens_;}
        public Statement (){tokens = new List<Token>();}
        public Statement SubStatement(int startIndex,int length){
            Statement ret = new Statement();
            for (int i = startIndex; i < length + startIndex; i++)
            {
                ret += tokens[i];
            }
            return ret;
        }
        public Statement SubStatement(int startIndex){
            Statement ret = new Statement();
            for (int i = startIndex; i < tokens.Count; i++)
            {
                ret += tokens[i];
            }
            return ret;
        }
        public static implicit operator Statement(Token[] tokens) => new Statement(tokens);
        public static implicit operator Token[] (Statement s) => s.tokens.ToArray();
        public static Statement operator +(Statement s, Token t) 
        {
            s.tokens.Add(t);
            return s;
        }
        public static Statement operator +(Statement s1, Statement s2) 
        {
            s1.tokens.AddRange(s2.tokens);
            return s1;
        }
    }
    
    public enum Keyword
    {
        Struct,Var,
        Int,Float,String,Byte,Bool,
        FunctionObject,Function,
        If,WhileLoop,ForeachLoop,Loop,
        Or,And,
        Null
    }
    public enum Operator{
        Plus,Minus,Multiply,Divide,Modulous
        ,BoolInvert,GreaterThan,LessThan,Equals
    }
}