


namespace Bedrock
{
    namespace SystemFunctions
    {
        public class BinaryFunction : IGenericFunction
            {
                public BedrockType Type;
                public string Name;

                public virtual object ExecuteFunction(object[] args)
                {
                    if (args.Length != 2)
                        throw new ArgumentException("Only 2 Arguments!");

                    throw new NotImplementedException();
                }

                public BedrockType GetGenericType()
                {
                    return Type;
                }

                public string GetName()
                {
                    return Name;
                }

                public string[] GetParamsName()
                {
                    return new string[] { "a", "b" };
                }

                public BedrockType[] GetParamsTypes()
                {
                    return new BedrockType[] { Type, Type };
                }

                public string GetPrototypedName()
                {
                    var name = $"{GetName()}:{GetReturnType().GetTypeName()} (";
                    var pt = GetParamsTypes();
                    for (int i = 0; i < pt.Length; i++)
                    {
                        name += pt[i].GetTypeName();
                        if (i < pt.Length - 1)
                            name += ", ";
                    }
                    name += ")";
                    return name;
                }

                public BedrockType GetReturnType()
                {
                    return Type;
                }

                public void SetGenericType(BedrockType type)
                {
                    Type = type;
                }
            }
    }
}