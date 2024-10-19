using System.Text;

namespace Bedrock
{
    public class LiteralParser
    {
        public static int ParseInteger(Token token)
        {
            string str = token.lexeme;
            if (str.Length > 2)
            {
                if (str[1] == 'x' || str[1] == 'X')
                {
                    int xn = 0;
                    for (int i = 2; i < str.Length; i++)
                    {
                        xn *= 16;
                        xn += hexToInt(str[i], token);
                    }
                    return xn;
                }
                else if (str[1] == 'b' || str[1] == 'B')
                {
                    int bn = 0;
                    for (int i = 2; i < str.Length; i++)
                    {
                        bn <<= 1;
                        bn += binToInt(str[i],token);
                    }
                    return bn;
                }
                else
                {
                    int nn = 0;
                    for (int i = 2; i < str.Length; i++)
                    {
                        nn *= 10;
                        nn += decToInt(str[i], token);
                    }
                    return nn;
                }
            }
            int n = 0;
            for (int i = 0; i < str.Length; i++)
            {
                n *= 10;
                n += decToInt(str[i], token);
            }
            return n;
        }
        public static string ParseString(Token token)
        {
            return token.lexeme;
        }
        private static int binToInt(char c, Token token)
        {
            switch (c)
            {
                case '0':
                    return 0;
                case '1':
                    return 1;
                default:
                    BedrockError.Error(token, $"Unexpected symbol '{c}' in binary integer literal.");
                    throw new BedrockError.ParseError();
            }
        }

        private static int decToInt(char c, Token token)
        {
            switch (c)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return (int)(c - '0');
                default:
                    BedrockError.Error(token, $"Unexpected symbol '{c}' in integer literal.");
                    throw new BedrockError.ParseError();
            }
        }

        private static int hexToInt(char c, Token token)
        {
            switch (c)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return (int)(c - '0');
                case 'a':
                case 'A':
                    return 10;
                case 'b':
                case 'B':
                    return 11;
                case 'c':
                case 'C':
                    return 12;
                case 'd':
                case 'D':
                    return 13;
                case 'e':
                case 'E':
                    return 14;
                case 'f':
                case 'F':
                    return 15;
                default:
                    BedrockError.Error(
                        token,
                        $"Unexpected symbol '{c}' in hexadecimal integer literal."
                    );
                    throw new BedrockError.ParseError();
            }
        }
    }
}
