﻿using System.Formats.Asn1;
using System.IO;
using System.Text.Json;
using Bedrock;

class Program
{
    static void Main(string[] args)
    {
        if (!File.Exists(args[0]))
            throw new Exception("File does not exist!");
        string text = File.ReadAllText(args[0]);
        var tokens = SourceButcher.Butcher(text);
        
        IFunction mathfunc;
        mathfunc = new Bedrock.SystemFunctions.Math.Add(BedrockType.Int32);
        var result = mathfunc.ExecuteFunction(new object[] { 1, 2 });

        //Console.WriteLine(result);
        Convert.ToInt32("0x31", 16);
        foreach (var t in tokens)
        {
            //" -> "+ t.tokenType+
            Console.Write($"`{text.Substring(t.startIndex, t.length)}`"+" -> "+ t.tokenType+" ,");
            if (t.tokenType == TokenType.EndStatement)
                Console.WriteLine();
        }
    }
}
