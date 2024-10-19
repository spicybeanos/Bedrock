/*
    Only to be used while doing unary,binary or BedrockTypeHandler.Casting operations
    Do not use it to store data!
*/

namespace Bedrock
{
    public class Number
    {
        public BedrockNativeType type { get; set; }
        public object value { get; set; }

        public static bool IsNumber(BedrockNativeType type)
        {
            return (int)type >= (int)BedrockNativeType.UInt8
                && (int)type <= (int)BedrockNativeType.Float64;
        }

        public Number(object value)
        {
            this.value = value;
            type = BedrockTypeHandler.GetNativeType(value);
            if (!IsNumber(type))
                throw new Exception($"Object <{value}> is not number!");
        }

        public Number(object value, BedrockNativeType type)
        {
            this.value = value;
            this.type = type;
            if (!IsNumber(type))
                throw new Exception($"Object <{value}> is not number!");
        }

        public static Number operator -(Number a)
        {
            switch (a.type)
            {
                case BedrockNativeType.UInt8:
                    return new Number(-(byte)a.value);
                case BedrockNativeType.Int16:
                    return new Number(-(short)a.value);
                case BedrockNativeType.Int32:
                    return new Number(-(int)a.value);
                case BedrockNativeType.Int64:
                    return new Number(-(long)a.value);
                case BedrockNativeType.Float32:
                    return new Number(-(float)a.value);
                case BedrockNativeType.Float64:
                    return new Number(-(double)a.value);
                default:
                    throw new Exception($"Cannot do a numeric operation on type {a.type}!");
            }
        }

        public static Number operator +(Number a, Number b)
        {
            var ht = BedrockTypeHandler.GetHigherType(a.type, b.type);
            object _a = BedrockTypeHandler.Cast(a.value, ht),
                _b = BedrockTypeHandler.Cast(b.value, ht);
            switch (ht)
            {
                case BedrockNativeType.UInt8:
                    return new Number((byte)_a + (byte)_b);
                case BedrockNativeType.Int16:
                    return new Number((short)_a + (short)_b);
                case BedrockNativeType.Int32:
                    return new Number((int)_a + (int)_b);
                case BedrockNativeType.Int64:
                    return new Number((long)_a + (long)_b);
                case BedrockNativeType.Float32:
                    return new Number((float)_a + (float)_b);
                case BedrockNativeType.Float64:
                    return new Number((double)_a + (double)_b);
                default:
                    throw new Exception($"Cannot do a numeric operation on type {ht}!");
            }
        }

        public static Number operator -(Number a, Number b)
        {
            var ht = BedrockTypeHandler.GetHigherType(a.type, b.type);
            object _a = BedrockTypeHandler.Cast(a.value, ht),
                _b = BedrockTypeHandler.Cast(b.value, ht);
            switch (ht)
            {
                case BedrockNativeType.UInt8:
                    return new Number((byte)_a - (byte)_b);
                case BedrockNativeType.Int16:
                    return new Number((short)_a - (short)_b);
                case BedrockNativeType.Int32:
                    return new Number((int)_a - (int)_b);
                case BedrockNativeType.Int64:
                    return new Number((long)_a - (long)_b);
                case BedrockNativeType.Float32:
                    return new Number((float)_a - (float)_b);
                case BedrockNativeType.Float64:
                    return new Number((double)_a - (double)_b);
                default:
                    throw new Exception($"Cannot do a numeric operation on type {ht}!");
            }
        }

        public static Number operator *(Number a, Number b)
        {
            var ht = BedrockTypeHandler.GetHigherType(a.type, b.type);
            object _a = BedrockTypeHandler.Cast(a.value, ht),
                _b = BedrockTypeHandler.Cast(b.value, ht);
            switch (ht)
            {
                case BedrockNativeType.UInt8:
                    return new Number((byte)_a * (byte)_b);
                case BedrockNativeType.Int16:
                    return new Number((short)_a * (short)_b);
                case BedrockNativeType.Int32:
                    return new Number((int)_a * (int)_b);
                case BedrockNativeType.Int64:
                    return new Number((long)_a * (long)_b);
                case BedrockNativeType.Float32:
                    return new Number((float)_a * (float)_b);
                case BedrockNativeType.Float64:
                    return new Number((double)_a * (double)_b);
                default:
                    throw new Exception($"Cannot do a numeric operation on type {ht}!");
            }
        }

        public static Number operator /(Number a, Number b)
        {
            var ht = BedrockTypeHandler.GetHigherType(a.type, b.type);
            object _a = BedrockTypeHandler.Cast(a.value, ht),
                _b = BedrockTypeHandler.Cast(b.value, ht);
            switch (ht)
            {
                case BedrockNativeType.UInt8:
                    return new Number((byte)_a / (byte)_b);
                case BedrockNativeType.Int16:
                    return new Number((short)_a / (short)_b);
                case BedrockNativeType.Int32:
                    return new Number((int)_a / (int)_b);
                case BedrockNativeType.Int64:
                    return new Number((long)_a / (long)_b);
                case BedrockNativeType.Float32:
                    return new Number((float)_a / (float)_b);
                case BedrockNativeType.Float64:
                    return new Number((double)_a / (double)_b);
                default:
                    throw new Exception($"Cannot do a numeric operation on type {ht}!");
            }
        }

        public static bool operator >(Number a, Number b)
        {
            var ht = BedrockTypeHandler.GetHigherType(a.type, b.type);
            object _a = BedrockTypeHandler.Cast(a.value, ht),
                _b = BedrockTypeHandler.Cast(b.value, ht);
            switch (ht)
            {
                case BedrockNativeType.UInt8:
                    return (byte)_a > (byte)_b;
                case BedrockNativeType.Int16:
                    return (short)_a > (short)_b;
                case BedrockNativeType.Int32:
                    return (int)_a > (int)_b;
                case BedrockNativeType.Int64:
                    return (long)_a > (long)_b;
                case BedrockNativeType.Float32:
                    return (float)_a > (float)_b;
                case BedrockNativeType.Float64:
                    return (double)_a > (double)_b;
                default:
                    throw new Exception($"Cannot do a numeric operation on type {ht}!");
            }
        }

        public static bool operator <(Number a, Number b)
        {
            var ht = BedrockTypeHandler.GetHigherType(a.type, b.type);
            object _a = BedrockTypeHandler.Cast(a.value, ht),
                _b = BedrockTypeHandler.Cast(b.value, ht);
            switch (ht)
            {
                case BedrockNativeType.UInt8:
                    return (byte)_a < (byte)_b;
                case BedrockNativeType.Int16:
                    return (short)_a < (short)_b;
                case BedrockNativeType.Int32:
                    return (int)_a < (int)_b;
                case BedrockNativeType.Int64:
                    return (long)_a < (long)_b;
                case BedrockNativeType.Float32:
                    return (float)_a < (float)_b;
                case BedrockNativeType.Float64:
                    return (double)_a < (double)_b;
                default:
                    throw new Exception($"Cannot do a numeric operation on type {ht}!");
            }
        }

        public static bool operator >=(Number a, Number b)
        {
            var ht = BedrockTypeHandler.GetHigherType(a.type, b.type);
            object _a = BedrockTypeHandler.Cast(a.value, ht),
                _b = BedrockTypeHandler.Cast(b.value, ht);
            switch (ht)
            {
                case BedrockNativeType.UInt8:
                    return (byte)_a >= (byte)_b;
                case BedrockNativeType.Int16:
                    return (short)_a >= (short)_b;
                case BedrockNativeType.Int32:
                    return (int)_a >= (int)_b;
                case BedrockNativeType.Int64:
                    return (long)_a >= (long)_b;
                case BedrockNativeType.Float32:
                    return (float)_a >= (float)_b;
                case BedrockNativeType.Float64:
                    return (double)_a >= (double)_b;
                default:
                    throw new Exception($"Cannot do a numeric operation on type {ht}!");
            }
        }
        public static bool operator <=(Number a, Number b)
        {
            var ht = BedrockTypeHandler.GetHigherType(a.type, b.type);
            object _a = BedrockTypeHandler.Cast(a.value, ht),
                _b = BedrockTypeHandler.Cast(b.value, ht);
            switch (ht)
            {
                case BedrockNativeType.UInt8:
                    return (byte)_a <= (byte)_b;
                case BedrockNativeType.Int16:
                    return (short)_a <= (short)_b;
                case BedrockNativeType.Int32:
                    return (int)_a <= (int)_b;
                case BedrockNativeType.Int64:
                    return (long)_a <= (long)_b;
                case BedrockNativeType.Float32:
                    return (float)_a <= (float)_b;
                case BedrockNativeType.Float64:
                    return (double)_a <= (double)_b;
                default:
                    throw new Exception($"Cannot do a numeric operation on type {ht}!");
            }
        }
    }
}
