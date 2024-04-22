
namespace Bedrock{
    public interface IFunction
    {
        public abstract string GetName();  
        public abstract string GetPrototypedName();
        public abstract BedrockType GetReturnType();
        public abstract BedrockType[] GetParamsTypes();
        public abstract string[] GetParamsName() ;

        //if the return type is void, return (Int32)0
        public abstract object ExecuteFunction(object[] args);
    }
    
    public interface IGenericFunction : IFunction{
        public abstract BedrockType GetGenericType();
        public abstract void SetGenericType(BedrockType type);
    }
}