global using BedInt = System.Int32;
global using BedByte = System.Byte;
global using BedString = System.String;
global using BedFloat = System.Single;
using System;

namespace Bedrock{
    using BedInt = System.Int32;
    using BedString = String;
    using BedFloat = System.Single;
    public class LiteralParser
    {
        public static object Parse(string literal)
        {
            switch(IsNumber(literal)){
                case true:
                    if(literal.Contains("."))
                    {
                        return (BedFloat)float.Parse(literal);
                    }
                    else
                    {
                        return (BedInt)int.Parse(literal);
                    }
                    
                    default:
                    return (BedString)ProcessString(literal);
                    
            }
        }
        public static string ProcessString(string literal){
            string ret_ = "";
            for (int i = 1; i < literal.Length-1; i++)
            {
                switch(literal[i]){
                    case '\\':
                        switch(literal[++i]){
                            case 't':
                                ret_ += '\t';
                                break;
                            case 'n':
                                ret_ += '\n';
                                break;
                            case 'r':
                                ret_ += '\r';
                                break;
                            case '\\':
                                ret_ += '\\';
                                break;
                        }
                        break;
                    default:
                    ret_ += literal[i];
                    break;
                }
            }
            return ret_;
        }
        public static bool IsNumber(string s){
            for (int i = 0; i < s.Length; i++)
            {
                if(!char.IsDigit(s[i])){
                    switch(s[i]){
                        case '-':
                            if(i!=0){return false;}
                            break;
                        case '.':
                            if(i+1 >= s.Length){return false;}
                            break;
                        default:
                            return false;
                    }
                }
            }
            return true;
        }
    }
}