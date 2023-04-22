using System;

namespace Bedrock
{
    public class Number{
        private object Value {get;set;}
        public Number(object number){Value = number;}
        public static implicit operator Number(int x) => new Number(x);
        public static implicit operator Number(float x) => new Number(x);
        public static implicit operator int(Number x) => (int)x.Value;
        public static implicit operator float(Number x) => (float)x.Value;
        
        private static Number PromoteMath(Number a, Number b,Operator op){
            switch(a.Value is float){
                case true:
                    return Compute.FloatMath[op]((float)a.Value,(float)b.Value);
            }
            switch(b.Value is float){
                case true :
                return Compute.FloatMath[op]((float)a.Value,(float)b.Value);
            }
            return Compute.IntMath[op]((int)a.Value,(int)b.Value);
        }
        public static Number operator +(Number a, Number b) => PromoteMath(a,b,Operator.Plus);
        public static Number operator -(Number a, Number b) => PromoteMath(a,b,Operator.Minus);
        public static Number operator *(Number a, Number b) => PromoteMath(a,b,Operator.Multiply);
        public static Number operator /(Number a, Number b) => PromoteMath(a,b,Operator.Divide);
        public static Number operator %(Number a, Number b) => PromoteMath(a,b,Operator.Modulous);
        
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}