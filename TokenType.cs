
public enum TokenType
{
    // Single-character tokens.
    LEFT_PAREN, RIGHT_PAREN, 
    LEFT_BRACE, RIGHT_BRACE,
    LEFT_BOX,RIGHT_BOX,
    COMMA, DOT, MINUS, PLUS, SEMICOLON, SLASH, STAR,
    COLON,AMPERSAND,

    // One or two character tokens.
    BANG, BANG_EQUAL,
    EQUAL, EQUAL_EQUAL,
    GREATER, GREATER_EQUAL,
    LESS, LESS_EQUAL,

    // Literals.
    IDENTIFIER, STRING, NUMBER,

    // Keywords.
    AND, ELSE, FALSE, 
    FXN, FOR, IF, NULL, OR,
    PRINT, RETURN, SUPER, 
    THIS, TRUE, WHILE,

    //Datatypes
    INT,NUM,STR,BOOL,BYTE,

    EOF
}