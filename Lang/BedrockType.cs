namespace Bedrock
{
    public class BedrockType
    {
        public enum BedrockPrimitiveType
        {
            UInt8,
            Int32,
            Int64,
            Float32,
            Float64,
            String,
            Bool,
            Object
        }

        public static BedrockType Int32 {get;} = new BedrockType(){BedRockType=BedrockPrimitiveType.Int32,ClassName=""};
        public static BedrockType UInt8 {get;} = new BedrockType(){BedRockType=BedrockPrimitiveType.UInt8,ClassName=""};
        public static BedrockType Int64 {get;} = new BedrockType(){BedRockType=BedrockPrimitiveType.Int64,ClassName=""};
        public static BedrockType Float32 {get;} = new BedrockType(){BedRockType=BedrockPrimitiveType.Float32,ClassName=""};
        public static BedrockType Float64 {get;} = new BedrockType(){BedRockType=BedrockPrimitiveType.Float64,ClassName=""};
        public static BedrockType Bool {get;} = new BedrockType(){BedRockType=BedrockPrimitiveType.Bool,ClassName=""};
        public static BedrockType String {get;} = new BedrockType(){BedRockType=BedrockPrimitiveType.String,ClassName="string"};

        public BedrockPrimitiveType BedRockType { get;  set; }
        public string ClassName { get;  set; }

        public string GetTypeName(){
            string name = "";
            switch (BedRockType)
            {
                case BedrockPrimitiveType.Int32:
                name="int";break;
                case BedrockPrimitiveType.Int64:
                name="long";break;
                case BedrockPrimitiveType.UInt8:
                name="byte";break;
                case BedrockPrimitiveType.Float32:
                name="float";break;
                case BedrockPrimitiveType.Float64:
                name="double";break;
                case BedrockPrimitiveType.String:
                name="string";break;
                case BedrockPrimitiveType.Bool:
                name="bool";break;
                default:
                name=$"object({ClassName})";break;
            }
            return name;
        }
    }
}
