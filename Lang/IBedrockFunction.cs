
namespace Bedrock{
    public interface IFunction
    {
        public abstract string GetName();  
        public abstract BedrockTypeHandler GetReturnType();
        public abstract BedrockTypeHandler[] GetParamsTypes();
        public abstract string[] GetParamsName() ;

        //if the return type is void, return (Int32)0
        public abstract object ExecuteFunction(object[] args);
    }
}