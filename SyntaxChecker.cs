


namespace Bedrock
{
    public class SyntaxChecker
    {
        public Queue<Queue<Token>> AllTokens { get; private set; }
        public SyntaxChecker(List<List<Token>> tokens)
        {

            for (int i = 0; i < tokens.Count; i++)
            {
                Queue<Token> q = new();
                for (int j = 0; j < tokens[i].Count; j++)
                {
                    q.Enqueue(tokens[i][j]);
                }
                AllTokens.Enqueue(q);
            }
        }

        public SyntaxCheckResult Check()
        {
            while (AllTokens.TryDequeue(out Queue<Token> lineQ))
            {

                Stack<Token> scope = new();
                while (lineQ.TryDequeue(out Token token))
                {
                    
                }
            }
            return null;
        }
    }
    public class SyntaxCheckResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}