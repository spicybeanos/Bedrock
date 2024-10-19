


namespace Bedrock
{
    public enum TokenType : byte
    {
        EOF,
        EndStatement,
        StringLiteral,
        IntegerLiteral,
        DecimalLiteral,
        Identifier,
        Colon,
        Period,
        Comma,
        Assign,
        AutoAssign,
        

        LeftParentesis,
        RightParentesis,
        LeftSquare,
        RightSquare,
        LeftBrace,
        RightBrace,
        LeftAngle_Lesser,
        RightAngle_Greater,

        //operators
        GreaterEquals,
        LesserEquals,

        EqualEquals,

        Bang,
        BangEquals,

        Plus,
        PlusEquals,
        Minus,
        MinusEquals,
        Star,
        StarEquals,
        Slash,
        SlashEquals,
        Modulus,
        ModulusEquals,

        Ampersand,Pipe,Carrot,

        //key words
        Fxn,Fxn_type,
        Class,Self,
        Byte,Short,Int,Float,String,Void,
        Return,True,False,Null,
        
        If,Else,While,Then,Switch,Case
        
        ,Import,Ref,
        Var,
        For
    }
}