namespace Bedrock
{
    public class Lexer
    {
        string buff { get; set; }
        int pointer { get; set; }
        int line { get; set; }
        List<Token> tokens { get; set; }
        public Lexer(string buff)
        {
            this.buff = buff;
            pointer = 0;
            line = 1;
            tokens = new();
        }

        private bool Next(out char c)
        {
            if (pointer < buff.Length)
            {
                c = buff[pointer++];
                return true;
            }
            c = '\0';
            return false;
        }
        private bool Peek(out char c)
        {
            if (pointer + 1 < buff.Length)
            {
                c = buff[pointer + 1];
                return true;
            }
            c = '\0';
            return false;
        }

    }
}