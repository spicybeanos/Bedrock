namespace Bedrock
{
    namespace SystemFunctions
    {
        public class Math
        {
            public class Add : BinaryFunction
            {
                public Add(BedrockType type)
                {
                    Type = type;
                    Name = "Add";
                }

                public override object ExecuteFunction(object[] args)
                {
                    if (args.Length != 2)
                        throw new ArgumentException("Only 2 Arguments!");

                    switch (Type.BedRockType)
                    {
                        case BedrockType.BedrockPrimitiveType.UInt8:
                            return (byte)args[0] + (byte)args[1];
                        case BedrockType.BedrockPrimitiveType.Int32:
                            return (int)args[0] + (int)args[1];
                        case BedrockType.BedrockPrimitiveType.Int64:
                            return (long)args[0] + (long)args[1];
                        case BedrockType.BedrockPrimitiveType.Float32:
                            return (float)args[0] + (float)args[1];
                        case BedrockType.BedrockPrimitiveType.Float64:
                            return (double)args[0] + (double)args[1];
                        case BedrockType.BedrockPrimitiveType.String:
                            return (string)args[0] + (string)args[1];

                        default:
                            throw new Exception($"Invalid generic type : {Type.GetTypeName()}");
                    }
                }
            }

            public class Minus : BinaryFunction
            {
                public Minus(BedrockType type)
                {
                    Type = type;
                    Name = "Minus";
                }

                public override object ExecuteFunction(object[] args)
                {
                    if (args.Length != 2)
                        throw new ArgumentException("Only 2 Arguments!");

                    switch (Type.BedRockType)
                    {
                        case BedrockType.BedrockPrimitiveType.UInt8:
                            return (byte)args[0] - (byte)args[1];
                        case BedrockType.BedrockPrimitiveType.Int32:
                            return (int)args[0] - (int)args[1];
                        case BedrockType.BedrockPrimitiveType.Int64:
                            return (long)args[0] - (long)args[1];
                        case BedrockType.BedrockPrimitiveType.Float32:
                            return (float)args[0] - (float)args[1];
                        case BedrockType.BedrockPrimitiveType.Float64:
                            return (double)args[0] - (double)args[1];

                        default:
                            throw new Exception($"Invalid generic type : {Type.GetTypeName()}");
                    }
                }
            }

            public class Multiply : BinaryFunction
            {
                public Multiply(BedrockType type)
                {
                    Type = type;
                    Name = "Multiply";
                }

                public override object ExecuteFunction(object[] args)
                {
                    if (args.Length != 2)
                        throw new ArgumentException("Only 2 Arguments!");

                    switch (Type.BedRockType)
                    {
                        case BedrockType.BedrockPrimitiveType.UInt8:
                            return (byte)args[0] * (byte)args[1];
                        case BedrockType.BedrockPrimitiveType.Int32:
                            return (int)args[0] * (int)args[1];
                        case BedrockType.BedrockPrimitiveType.Int64:
                            return (long)args[0] * (long)args[1];
                        case BedrockType.BedrockPrimitiveType.Float32:
                            return (float)args[0] * (float)args[1];
                        case BedrockType.BedrockPrimitiveType.Float64:
                            return (double)args[0] * (double)args[1];

                        default:
                            throw new Exception($"Invalid generic type : {Type.GetTypeName()}");
                    }
                }
            }

            public class Divide : BinaryFunction
            {
                public Divide(BedrockType type)
                {
                    Type = type;
                    Name = "Divide";
                }

                public override object ExecuteFunction(object[] args)
                {
                    if (args.Length != 2)
                        throw new ArgumentException("Only 2 Arguments!");

                    switch (Type.BedRockType)
                    {
                        case BedrockType.BedrockPrimitiveType.UInt8:
                            return (byte)args[0] / (byte)args[1];
                        case BedrockType.BedrockPrimitiveType.Int32:
                            return (int)args[0] / (int)args[1];
                        case BedrockType.BedrockPrimitiveType.Int64:
                            return (long)args[0] / (long)args[1];
                        case BedrockType.BedrockPrimitiveType.Float32:
                            return (float)args[0] / (float)args[1];
                        case BedrockType.BedrockPrimitiveType.Float64:
                            return (double)args[0] / (double)args[1];

                        default:
                            throw new Exception($"Invalid generic type : {Type.GetTypeName()}");
                    }
                }
            }

            public class Modulus : BinaryFunction
            {
                public Modulus(BedrockType type)
                {
                    Type = type;
                    Name = "Modulus";
                }

                public override object ExecuteFunction(object[] args)
                {
                    if (args.Length != 2)
                        throw new ArgumentException("Only 2 Arguments!");

                    switch (Type.BedRockType)
                    {
                        case BedrockType.BedrockPrimitiveType.UInt8:
                            return (byte)args[0] % (byte)args[1];
                        case BedrockType.BedrockPrimitiveType.Int32:
                            return (int)args[0] % (int)args[1];
                        case BedrockType.BedrockPrimitiveType.Int64:
                            return (long)args[0] % (long)args[1];
                        default:
                            throw new Exception($"Invalid generic type : {Type.GetTypeName()}");
                    }
                }
            }
        }
    }
}
