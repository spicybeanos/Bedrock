

namespace Bedrock
{
    public class VariableHandler 
    {
        public static ulong globalCounter = 1;
        public static Dictionary<ulong,object> referenceValues {get;set;} = new();
        public class Scope{
            public Dictionary<string,ulong> variables {get;set;} = new();

        }
        
    }
}