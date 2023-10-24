
using Bedrock;
using System.IO;
using System.Text.Json;

class Program
{
    const string outputPath = @"TestProj\tokenizer_output.json";
    static void Main(string[] args)
    {
        try
        {
            if (args.Length < 1)
            {
                Console.WriteLine("No file path provided. Terminating.");
                return;
            }

            var text = Tokenizer.ProccessText_old(File.ReadAllText(args[0]));
            JsonSerializerOptions op = new() { WriteIndented = true };

            var tok = Tokenizer.Tokenize(text);
            SyntaxChecker checker = new(tok);
            var res = checker.Check();
            Console.WriteLine($"Result:{res.Success}");
            if (!res.Success)
            {
                Console.WriteLine(res.Message);
            }
            File.WriteAllText(outputPath, JsonSerializer.Serialize(tok, op));
            Console.WriteLine("Done");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not execute file: {ex}");
        }
    }
}