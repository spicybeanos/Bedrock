using System.Formats.Asn1;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using Bedrock;

class Program
{
    static void Main(string[] args)
    {
        if(args.Length < 1){
            Console.WriteLine("Usage: [file path]");
            return;
        }

        if (!File.Exists(args[0]))
            throw new Exception("File does not exist!");
        string text = File.ReadAllText(args[0]);
        var tokens = SourceButcher.Butcher(text);
        
        var parser = new Parser(tokens);
        var exp = parser.Parse();
        
        Console.WriteLine("value = "+new Interpreter().Evaluate(exp));
    }
}
