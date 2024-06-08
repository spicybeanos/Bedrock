namespace Bedrock
{
    namespace SystemFunctions
    {
        public class Boolean
        {
            public class Compare : BinaryFunction
            {

                public Compare(BedrockType type)
                {
                    Type = type;
                }

                public override object ExecuteFunction(object[] args)
                {
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
                        case BedrockType.BedrockPrimitiveType.String:
                            return ((string)args[0]).CompareTo((string)args[1]);

                        default:
                            throw new Exception($"Invalid type trying to compare:{Type.GetTypeName()}");
                    }
                }
            }


            public class Equality : BinaryFunction
            {

                public Equality(BedrockType type)
                {
                    Type = type;
                }

                public override object ExecuteFunction(object[] args)
                {
                    Compare compare = new Compare(Type);
                    int res = (int)compare.ExecuteFunction(args);

                    return res == 0;
                }
            }
            public class Greater : BinaryFunction
            {

                public Greater(BedrockType type)
                {
                    Type = type;
                }

                public override object ExecuteFunction(object[] args)
                {
                    Compare compare = new Compare(Type);
                    int res = (int)compare.ExecuteFunction(args);

                    return res > 0;
                }
            }

            public class Lesser : BinaryFunction
            {

                public Lesser(BedrockType type)
                {
                    Type = type;
                }

                public override object ExecuteFunction(object[] args)
                {
                    Compare compare = new Compare(Type);
                    int res = (int)compare.ExecuteFunction(args);

                    return res < 0;
                }
            }
            public class Inequality : BinaryFunction
            {

                public Inequality(BedrockType type)
                {
                    Type = type;
                }

                public override object ExecuteFunction(object[] args)
                {
                    Compare compare = new Compare(Type);
                    int res = (int)compare.ExecuteFunction(args);

                    return res != 0;
                }
            }
            public class GreaterEquals : BinaryFunction
            {

                public GreaterEquals(BedrockType type)
                {
                    Type = type;
                }

                public override object ExecuteFunction(object[] args)
                {
                    Compare compare = new Compare(Type);
                    int res = (int)compare.ExecuteFunction(args);

                    return res >= 0;
                }
            }

            public class LesserEquals : BinaryFunction
            {

                public LesserEquals(BedrockType type)
                {
                    Type = type;
                }

                public override object ExecuteFunction(object[] args)
                {
                    Compare compare = new Compare(Type);
                    int res = (int)compare.ExecuteFunction(args);

                    return res <= 0;
                }
            }
        }
    }
}
