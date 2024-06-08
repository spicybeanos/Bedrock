


namespace Bedrock
{
    public enum TokenType
    {
        EndStatement,
        StringLiteral,
        IntegerLiteral,
        HexIntegerLiteral,
        BinIntegerLiteral,
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
        LeftAngle,
        RightAngle,

        //operators
        GreaterEquals,
        LesserEquals,

        Equality,

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
        Function,
        Byte,Int,Long,Float,Double,String,UShort,Bool,Void,
        Return,True,False,Null,
        
        If,Else,While,Then,Switch,Case
        
        ,Import,Ref,
        
    }
}