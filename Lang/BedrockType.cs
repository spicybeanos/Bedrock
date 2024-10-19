namespace Bedrock
{
    public enum BedrockNativeType
    {
        Void,
        Bool,
        UInt8,
        Int16,
        Int32,
        Int64,
        Float32,
        Float64,
        String,
        Object
    }

    public class BedrockTypeHandler
    {
        public BedrockNativeType BedRockType { get; set; }
        public string ClassName { get; set; } = "";

        public static BedrockNativeType GetNativeType(object obj)
        {
            return obj switch
            {
                int => BedrockNativeType.Int32,
                bool => BedrockNativeType.Bool,
                float => BedrockNativeType.Float32,
                string => BedrockNativeType.String,
                double => BedrockNativeType.Float64,
                long => BedrockNativeType.Int64,
                short => BedrockNativeType.Int16,
                byte => BedrockNativeType.UInt8,
                _ => BedrockNativeType.Object,
            };
        }

        public string GetTypeName()
        {
            string name = "";
            switch (BedRockType)
            {
                case BedrockNativeType.Int32:
                    name = "int";
                    break;
                case BedrockNativeType.UInt8:
                    name = "byte";
                    break;
                case BedrockNativeType.Bool:
                    name = "bool";
                    break;
                case BedrockNativeType.Float32:
                    name = "float";
                    break;
                case BedrockNativeType.String:
                    name = "string";
                    break;
                default:
                    name = $"object({ClassName})";
                    break;
            }
            return name;
        }
        public static BedrockNativeType GetHigherType(BedrockNativeType a, BedrockNativeType b) =>
            (BedrockNativeType)Math.Max((int)a, (int)b);

        public static BedrockNativeType GetHigherType(object a, object b) =>
            (BedrockNativeType)Math.Max((int)GetNativeType(a), (int)GetNativeType(b));

        public static object Cast(object n, BedrockNativeType castToType)
        {
            var currentObjectType = GetNativeType(n);
            switch (castToType)
            {
                case BedrockNativeType.UInt8:
                {
                    switch (currentObjectType)
                    {
                        case BedrockNativeType.UInt8:
                            return (byte)n;
                        case BedrockNativeType.Int16:
                            return (byte)(short)n;
                        case BedrockNativeType.Int32:
                            return (byte)(int)n;
                        case BedrockNativeType.Int64:
                            return (byte)(long)n;
                        case BedrockNativeType.Float32:
                            return (byte)(float)n;
                        case BedrockNativeType.Float64:
                            return (byte)(double)n;
                        default:
                            throw new Exception(
                                $"Runtime type casting failure: Cannot convert {n} to {castToType}"
                            );
                    }
                }
                case BedrockNativeType.Int16:
                {
                    switch (currentObjectType)
                    {
                        case BedrockNativeType.UInt8:
                            return (short)(byte)n;
                        case BedrockNativeType.Int16:
                            return (short)n;
                        case BedrockNativeType.Int32:
                            return (short)(int)n;
                        case BedrockNativeType.Int64:
                            return (short)(long)n;
                        case BedrockNativeType.Float32:
                            return (short)(float)n;
                        case BedrockNativeType.Float64:
                            return (short)(double)n;
                        default:
                            throw new Exception(
                                $"Runtime type casting failure: Cannot convert {n} to {castToType}"
                            );
                    }
                }
                case BedrockNativeType.Int32:
                {
                    switch (currentObjectType)
                    {
                        case BedrockNativeType.UInt8:
                            return (int)(byte)n;
                        case BedrockNativeType.Int16:
                            return (int)(short)n;
                        case BedrockNativeType.Int32:
                            return (int)n;
                        case BedrockNativeType.Int64:
                            return (int)(long)n;
                        case BedrockNativeType.Float32:
                            return (int)(float)n;
                        case BedrockNativeType.Float64:
                            return (int)(double)n;
                        default:
                            throw new Exception(
                                $"Runtime type casting failure: Cannot convert {n} to {castToType}"
                            );
                    }
                }
                case BedrockNativeType.Int64:
                {
                    switch (currentObjectType)
                    {
                        case BedrockNativeType.UInt8:
                            return (long)(byte)n;
                        case BedrockNativeType.Int16:
                            return (long)(short)n;
                        case BedrockNativeType.Int32:
                            return (long)(int)n;
                        case BedrockNativeType.Int64:
                            return (long)n;
                        case BedrockNativeType.Float32:
                            return (long)(float)n;
                        case BedrockNativeType.Float64:
                            return (long)(double)n;
                        default:
                            throw new Exception(
                                $"Runtime type casting failure: Cannot convert {n} to {castToType}"
                            );
                    }
                }
                case BedrockNativeType.Float32:
                {
                    switch (currentObjectType)
                    {
                        case BedrockNativeType.UInt8:
                            return (float)(byte)n;
                        case BedrockNativeType.Int16:
                            return (float)(short)n;
                        case BedrockNativeType.Int32:
                            return (float)(int)n;
                        case BedrockNativeType.Int64:
                            return (float)(long)n;
                        case BedrockNativeType.Float32:
                            return (float)n;
                        case BedrockNativeType.Float64:
                            return (float)(double)n;
                        default:
                            throw new Exception(
                                $"Runtime type casting failure: Cannot convert {n} to {castToType}"
                            );
                    }
                }
                case BedrockNativeType.Float64:
                {
                    switch (currentObjectType)
                    {
                        case BedrockNativeType.UInt8:
                            return (double)(byte)n;
                        case BedrockNativeType.Int16:
                            return (double)(short)n;
                        case BedrockNativeType.Int32:
                            return (double)(int)n;
                        case BedrockNativeType.Int64:
                            return (double)(long)n;
                        case BedrockNativeType.Float32:
                            return (double)(float)n;
                        case BedrockNativeType.Float64:
                            return (double)n;
                        default:
                            throw new Exception(
                                $"Runtime type casting failure: Cannot convert {n} to {castToType}"
                            );
                    }
                }
                case BedrockNativeType.String:
                {
                    switch (currentObjectType)
                    {
                        case BedrockNativeType.UInt8:
                            return (byte)n + "";
                        case BedrockNativeType.Int16:
                            return (short)n + "";
                        case BedrockNativeType.Int32:
                            return (int)n + "";
                        case BedrockNativeType.Int64:
                            return (long)n + "";
                        case BedrockNativeType.Float32:
                            return (float)n + "";
                        case BedrockNativeType.Float64:
                            return (double)n + "";
                        case BedrockNativeType.String:
                            return (string)n;
                        default:
                            throw new Exception(
                                $"Runtime type casting failure: Cannot convert {currentObjectType} to {castToType}"
                            );
                    }
                }
                default:
                    throw new Exception(
                        $"Runtime type casting failure: Cannot convert {currentObjectType} to {castToType}"
                    );
            }
        }
    }
}
