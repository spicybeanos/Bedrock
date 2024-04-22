namespace Bedrock
{
    namespace Vars
    {
        public class Value
        {
            public BedrockType Type { get; set; }
            public object objectValue { get; set; }
            public int Level { get; set; }

            public Value() { }

            public Value(BedrockType type, object val, int level)
            {
                Type = type;
                objectValue = val;
                Level = level;
            }

            public static Value Parse(string text, Token token, int level = 0)
            {
                switch (token.tokenType)
                {
                    case TokenType.Bool:

                        {
                            string val = text.Substring(token.startIndex, token.length);
                            if (val == Syntax.KeywordAsString.True)
                            {
                                return new Value(BedrockType.Bool, true, level);
                            }
                            else if (val == Syntax.KeywordAsString.False)
                            {
                                return new Value(BedrockType.Bool, false, level);
                            }
                            else
                            {
                                BedrockError.InvalidNumber(token.line);
                            }
                        }
                        break;
                    case TokenType.DecimalLiteral:

                        {
                            double Val;
                            if (
                                double.TryParse(
                                    text.Substring(token.startIndex, token.length),
                                    out Val
                                )
                            )
                            {
                                return new Value(BedrockType.Float64, Val, level);
                            }
                            else
                            {
                                BedrockError.InvalidNumber(token.line);
                            }
                        }
                        break;
                        case TokenType.HexIntegerLiteral:

                        {
                            int Val;
                            if (
                                int.TryParse(
                                    text.Substring(token.startIndex, token.length),
                                    out Val
                                )
                            )
                            {
                                return new Value(BedrockType.Int32, Val, level);
                            }
                            else
                            {
                                BedrockError.InvalidNumber(token.line);
                            }
                        }
                        break;
                }
            
            
                return null;
            }
        }
    }
}
