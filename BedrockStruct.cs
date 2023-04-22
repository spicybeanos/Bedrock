using System;


namespace Bedrock
{
    public class BedrockStruct
    {
        public Dictionary<StructMemberPair,object> Members = new Dictionary<StructMemberPair,object>();
        public const BedInt defaultMemberValue = 0;
        public void Define(string defination)
        {
            defination = defination.Substring(1,defination.Length-2);
            string[][] tokens = TextSpilter.ProccessText(defination);
            for (int line = 0; line < tokens.Length; line++)
            {
                switch(Tokenizer.Keywords.ContainsKey(tokens[line][0])){
                    case true:
                        Members.Add(new StructMemberPair(
                            Tokenizer.Keywords[tokens[line][0]],
                            new BedIdentifier(tokens[line][1])
                        ),defaultMemberValue);
                    break;
                    case false:
                        throw new Exception($"Identifier type not a primitive type {tokens[line][0]}");
                }
            }
        }
    }

    public struct StructMemberPair{
        public Keyword Type;
        public BedIdentifier Name;
        public StructMemberPair(Keyword type ,BedIdentifier name){
            Type = type;Name = name;
        }
    }
}