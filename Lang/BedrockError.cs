

namespace Bedrock
{
    public class BedrockError
    {
        public static void UnterminatedString(int line)
        {
            throw new Exception($"Unterminated string at line {line}");
        }
        public static void InvalidNumber(int line){
            throw new Exception($"Invalid number at line {line}");
        }
    }
}