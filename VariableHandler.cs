using System;

namespace Bedrock
{

    public class VariableHandler
    {

        public static Dictionary<Guid, VariableHandler> AllScopes = new();
        public Guid ScopeID { get; private set; }
        public int Rank { get; private set; }
        public VariableHandler ParentScope{get;private set;}
        public List<VariableHandler> SubScopes { get; set; }
        public VariableHandler()
        {
            Rank = 0;
        }
        public VariableHandler(VariableHandler parentScope)
        {
            Rank = parentScope.Rank + 1;
            ParentScope = parentScope;
        }
    }
}