
using Bedrock;
using System.IO;
using System.Text.Json;

class Program
{
    const string outputPath = @"TestProj\tokenizer_output.txt";
    static void Main(string[] args)
    {
        try
        {
            if(args.Length < 1)
            {
                Console.WriteLine("No file path provided. Terminating.");
                return;
            }

            var text = Tokenizer.ProccessText(File.ReadAllText(args[0]));
            JsonSerializerOptions op = new(){WriteIndented = true};
            File.WriteAllText(outputPath,JsonSerializer.Serialize(text,op));
            Console.WriteLine("Done.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not execute file: {ex}");
        }
    }
}