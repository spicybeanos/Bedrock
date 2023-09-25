using System;

namespace Bedrock
{
    public class Getter
    {
        public string Name { get; private set; }
        public Guid ScopeID { get; private set; }
    }
    public class Setter
    {
        public string identifier { get; set; }
        public string type { get; set; }
        public object value { get; set; }
    }
}