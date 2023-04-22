using System;

namespace Bedrock
{
    public class Compute
    {
        public delegate int _opp_int(int a,int b);
        public delegate float _opp_float(float a,float b);
        public static Dictionary<Operator,_opp_int> IntMath = new Dictionary<Operator, _opp_int>(){
            {Operator.Plus,_add},
            {Operator.Minus,_sub},
            {Operator.Multiply,_mlt},
            {Operator.Divide,_div},
            {Operator.Modulous,_mod},
        };
        public static Dictionary<Operator,_opp_float> FloatMath = new Dictionary<Operator, _opp_float>(){
            {Operator.Plus,_add},
            {Operator.Minus,_sub},
            {Operator.Multiply,_mlt},
            {Operator.Divide,_div},
            {Operator.Modulous,_mod},
        };

        private static int _add(int a,int b) => a+b;
        private static int _sub(int a,int b) => a-b;
        private static int _mlt(int a,int b) => a*b;
        private static int _div(int a,int b) => a/b;
        private static int _mod(int a,int b) => a%b;

        private static float _add(float a,float b) => a+b;
        private static float _sub(float a,float b) => a-b;
        private static float _mlt(float a,float b) => a*b;
        private static float _div(float a,float b) => a/b;
        private static float _mod(float a,float b) => a%b;
    }
}