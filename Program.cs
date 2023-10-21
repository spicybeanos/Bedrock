
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

            var text = Tokenizer.ProccessText(File.ReadAllText(args[0]));
            JsonSerializerOptions op = new() { WriteIndented = true };

            var tok = Tokenizer.Tokenize(text);

            File.WriteAllText(outputPath, JsonSerializer.Serialize(tok, op));
            Console.WriteLine("Done");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not execute file: {ex}");
        }
    }

    static string spit(List<List<Token>> tok)
    {
        string @out = "{";
        for (int i = 0; i < tok.Count; i++)
        {
            @out+="\n\t[";
            for (int j = 0; j < tok[i].Count; j++)
            {
                @out +="\n\t\tText:\""+tok[i][j].Text+"\"";
                @out+="\n\ttokenTag:"+tok[i][j].tokenTag.ToString();
            }
            @out += "\n\t],";
        }
        @out += "\n}";
        return @out;
    }
}