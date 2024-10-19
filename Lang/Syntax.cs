namespace Bedrock
{
    public class Syntax
    {
        public static TokenType GetKeyword(string word)
        {
            switch (word)
            {
                case "fxn":
                    return TokenType.Fxn;
                case "Fxn":
                    return TokenType.Fxn_type;
                case "class":
                    return TokenType.Class;
                case "self":
                    return TokenType.Self;
                case "byte":
                    return TokenType.Byte;
                case "short":
                    return TokenType.Short;
                case "int":
                    return TokenType.Int;
                case "float":
                    return TokenType.Float;
                case "string":
                    return TokenType.String;
                case "void":
                    return TokenType.Void;
                case "return":
                    return TokenType.Return;
                case "true":
                    return TokenType.True;
                case "false":
                    return TokenType.False;
                case "null":
                    return TokenType.Null;
                case "if":
                    return TokenType.If;
                case "for":
                    return TokenType.For;
                case "else":
                    return TokenType.Else;
                case "while":
                    return TokenType.While;
                case "switch":
                    return TokenType.Switch;
                case "case":
                    return TokenType.Case;
                case "import":
                    return TokenType.Import;
                case "ref":
                    return TokenType.Ref;
                case "var":
                    return TokenType.Var;
                default:
                    return TokenType.Identifier;
            }
            //eoswitch
        }

        //eofn
        public class KeywordAsString
        {
            public const string Fxn = "fxn",
                Fxn_type = "Fxn",
                Class = "class",
                Self = "self",
                Byte = "byte",
                Short = "short",
                Int = "int",
                For = "for",
                Float = "float",
                String = "string",
                Void = "void",
                Return = "return",
                True = "true",
                False = "false",
                Null = "null",
                If = "if",
                Else = "else",
                While = "while",
                Switch = "switch",
                Case = "case",
                Import = "import",
                Ref = "ref",
                Var = "var";
        }
        //eoKaS
    }
    //eosyn
}
//eon
