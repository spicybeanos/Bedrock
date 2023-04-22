
using Bedrock;
using System.IO;
using System.Text.Json;

class Program
{
    const string outputPath = @"TestProj\tokenizer_output.txt";
    static void Main(string[] args)
    {
        ExecuteFile(args);
    }

    static void ComputeTimeDifference(int tests){
        List<float> td_int = new List<float>(),
        td_num = new List<float>(),
        td_float = new List<float>();

        for (int t_ = 0; t_ < tests; t_++)
        {
            int ms_now =  DateTime.Now.Millisecond,ms_later=0,_i=0;
            const int comp = 1000000;
            for (int i = 0; i < comp; i++)
            {
                _i--;
            }
            ms_later = DateTime.Now.Millisecond;
            td_int.Add((float)ms_later-(float)ms_now);

            ms_now = DateTime.Now.Millisecond;
            Number _n =0;
            for (int i = 0; i < comp; i++)
            {
                _n--;
            }
            ms_later = DateTime.Now.Millisecond;
            td_num.Add((float)ms_later-(float)ms_now);

            ms_now = DateTime.Now.Millisecond;
            float _f =0;
            for (int i = 0; i < comp; i++)
            {
                _f--;
            }
            ms_later = DateTime.Now.Millisecond;
            td_float.Add((float)ms_later-(float)ms_now);
        }
        float avg_i = sum(td_int)/tests,
        avg_num = sum(td_num)/tests,
        avg_f = sum(td_float)/tests;
        Console.WriteLine($"type\t\tavg");
        Console.WriteLine($"int\t\t{avg_i}");
        Console.WriteLine($"Number\t\t{avg_num}");
        Console.WriteLine($"float\t\t{avg_f}");
    }

    static float sum(IEnumerable<float> f){
        float r = 0;
        foreach (var f_ in f)
        {
            r+=f_;
        }
        return r;
    }

    static void ExecuteFile(string[] args)
    {
        string file = "";
        if(args.Length > 0 ){
            file = args[0];
            if(File.Exists(file)){
                string ftext = File.ReadAllText(file);
                string[][] s = TextSpilter.ProccessText(ftext);
                Statement[] statements = Tokenizer.Bake(s);
                Console.WriteLine($"Number of statements : {statements.Length}");
                string s_json = JsonSerializer.Serialize(s,new JsonSerializerOptions(){
                    WriteIndented = true
                });
                string state_json = JsonSerializer.Serialize(statements,new JsonSerializerOptions(){
                    WriteIndented = true
                });
                File.WriteAllText(outputPath,s_json +"\n\n"+ state_json);
                Console.WriteLine($"Written to {outputPath}");
            }
            else{
                Console.WriteLine("File does not exist");
            }
        }else{
            Console.WriteLine("Please provide a file path");
        }
    }
}