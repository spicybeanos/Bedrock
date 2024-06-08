using Bedrock.SystemFunctions.Math;

namespace Bedrock
{
    namespace SystemFunctions
    {
        public class Operators
        {
            public static object DoMath(object[] paras,BedrockType type,TokenType opp){
                switch(opp){
                    case TokenType.Plus:{
                        
                    }
                    break;
                    case TokenType.Minus:{

                    }
                    break;
                    case TokenType.Star:{

                    }
                    break;
                    case TokenType.Slash:{

                    }
                    break;
                    case TokenType.Modulus:{

                    }
                    break;
                }
                return 0;
            }


            private static Add operatorAdd_i08 = new Add(BedrockType.UInt8);
            private static Add operatorAdd_i32 = new Add(BedrockType.Int32);
            private static Add operatorAdd_i64 = new Add(BedrockType.Int64);
            private static Add operatorAdd_f32 = new Add(BedrockType.Float32);
            private static Add operatorAdd_f64 = new Add(BedrockType.Float64);
            private static Add operatorAdd_str = new Add(BedrockType.String);
 
            private static Minus operatorMinus_i08 = new Minus(BedrockType.UInt8);
            private static Minus operatorMinus_i32 = new Minus(BedrockType.Int32);
            private static Minus operatorMinus_i64 = new Minus(BedrockType.Int64);
            private static Minus operatorMinus_f32 = new Minus(BedrockType.Float32);
            private static Minus operatorMinus_f64 = new Minus(BedrockType.Float64);
 
            private static Multiply operatorMultiply_i08 = new Multiply(BedrockType.UInt8);
            private static Multiply operatorMultiply_i32 = new Multiply(BedrockType.Int32);
            private static Multiply operatorMultiply_i64 = new Multiply(BedrockType.Int64);
            private static Multiply operatorMultiply_f32 = new Multiply(BedrockType.Float32);
            private static Multiply operatorMultiply_f64 = new Multiply(BedrockType.Float64);
 
            private static Divide operatorDivide_i08 = new Divide(BedrockType.UInt8);
            private static Divide operatorDivide_i32 = new Divide(BedrockType.Int32);
            private static Divide operatorDivide_i64 = new Divide(BedrockType.Int64);
            private static Divide operatorDivide_f32 = new Divide(BedrockType.Float32);
            private static Divide operatorDivide_f64 = new Divide(BedrockType.Float64);
 
            private static Modulus operatorModulus_i08 = new Modulus(BedrockType.UInt8);
            private static Modulus operatorModulus_i32 = new Modulus(BedrockType.Int32);
            private static Modulus operatorModulus_i64 = new Modulus(BedrockType.Int64);
        }
    }
}