using System;


namespace Bedrock{

    public class VariableHandler{
        
        public Dictionary<BedIdentifier,object> Variables = new Dictionary<BedIdentifier, object>();
        public static Dictionary<string,BedrockStruct> Structs = new Dictionary<string, BedrockStruct>();
        public void AddVariable(BedIdentifier name,object value){
            if(!Variables.ContainsKey(name)){
                Variables.Add(name,value);
            }else{
                throw new Exception($"Variable {name} already decleared!");
            }
        }
        public void Set(BedIdentifier id, object value){
            if(Variables.ContainsKey(id)){
                Variables[id] = value;
            }else{
                throw new Exception($"Variable '{id}' not initialized!");
            }
        }
        public void SetVariable(BedIdentifier setee,BedIdentifier getee){
            if(Variables.ContainsKey(setee) && Variables.ContainsKey(setee)){
                if(setee.IsReference){
                    Variables[setee] = getee;
                }else{
                    Variables[setee] = Variables[getee];
                }
            }
        }
        public void SetVariableZero(BedIdentifier var,Keyword type)
        {
            switch (type)
            {
                case Keyword.Int:
                    Set(var,0);
                    break;
                case Keyword.Float:
                    Set(var,0f);
                    break;
                case Keyword.Byte:
                    Set(var,(byte)0);
                    break;
                case Keyword.Bool:
                    Set(var,false);
                    break;
                case Keyword.String:
                    Set(var,"");
                    break;
                default:
                    throw new Exception($"Invalid primitive type '{type}'");
            }
        }
    }
}