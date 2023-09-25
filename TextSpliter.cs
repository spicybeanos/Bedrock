using System;
using System.Data.Common;
using System.Reflection.Emit;
using System.Text;

namespace Bedrock
{
    public class Tokenizer
    {
        private string str { get; set; }
        private List<string> lines { get; set; }
        private static readonly Dictionary<string, TokenType>
        SymbolStringToTokenType = new(){
            {Symbols.PLUS,TokenType.Operation},
            {Symbols.MINUS,TokenType.Operation},
            {Symbols.MULTIPLY,TokenType.Operation},
            {Symbols.DIVIDE,TokenType.Operation},
            {Symbols.MODULUS,TokenType.Operation},
            {Symbols.BANG,TokenType.BoolOperation},
            {Symbols.ASSIGN,TokenType.Assigment},
            {Symbols.EQUALS,TokenType.BoolOperation},
            {Symbols.NOT_EQUALS,TokenType.BoolOperation},
            {Symbols.GREATER,TokenType.BoolOperation},
            {Symbols.LESSER,TokenType.BoolOperation},
            {Symbols.GREATER_EQUAL,TokenType.BoolOperation},
            {Symbols.LESSER_EQUALS,TokenType.BoolOperation},
            {Symbols.FUNCTION_ASSIGN,TokenType.Assigment},
            {Symbols.COLON,TokenType.Symbol},
            {Symbols.AMPERSAND,TokenType.Symbol}
        };
        public List<string> KEYWORDS = new()
        {
            Keywords.INT_TOKEN,
            Keywords.FLOAT_TOKEN,
            Keywords.STRING_TOKEN,
            Keywords.CHAR_TOKEN ,
            Keywords.BYTE_TOKEN ,
            Keywords.BOOL_TOKEN ,
            Keywords.VOID_TOKEN ,

            Keywords.FUNCTION_TOKEN,
            Keywords.REF_TOKEN ,
            Keywords.STRUCT_TOKEN,
            Keywords.ARRAY_TOKEN ,
            Keywords.IMPORT_TOKEN
        };
        public List<string> DATATYPES = new(){
            Keywords.INT_TOKEN,
            Keywords.FLOAT_TOKEN ,
            Keywords.STRING_TOKEN,
            Keywords.CHAR_TOKEN ,
            Keywords.BYTE_TOKEN ,
            Keywords.BOOL_TOKEN ,
            Keywords.VOID_TOKEN
        };
        public List<string> SYMBOLS = new(){
            "+","-","*","/","%","!",
            "=",
            "==","!=",">","<",">=","<=",
            "=>",
            ":","&"
        };
        public Tokenizer(string text)
        {
            str = text;
            lines = new();
        }
        public string[][] ProccessText(char sp = ' ')
        {
            string text = str;
            const char endl = '\n', qt = '\"', bkslash = '\\', comment = '#';

            StringBuilder e = new("");
            const string openb = "([{", closeb = ")]}", operators = "+-*/%&", compare = "<>!=";
            List<string> r = new List<string>();
            List<string[]> ret = new List<string[]>();
            char bo = '\0', bc = '\0';

            int bcc = 0;
            bool inqt = false, ignore_qt = false, isincomment = false;

            text = text.Replace("\r", "");
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (isincomment)
                {
                    if (c == endl)
                    {
                        isincomment = false;
                        if (!string.IsNullOrEmpty(e.ToString()))
                            r.Add(e.ToString());
                        e.Clear();
                    }
                }
                else if (c == qt)
                {
                    if (!ignore_qt && bcc < 1)
                        inqt = !inqt;

                    e.Append(c);
                    if (!inqt && bcc < 1)
                    {
                        if (!string.IsNullOrEmpty(e.ToString()))
                        {
                            r.Add(e.ToString());
                        }
                        e.Clear();
                    }
                }
                else if (c == comment)
                {
                    if (!inqt && bcc <= 0)
                        isincomment = true;
                    else
                        e.Append(c);
                }
                else if (openb.Contains(c) && bcc < 1 && !inqt)
                {
                    bo = c;
                    bc = closeb[openb.IndexOf(c)];
                    bcc++;
                    if (!string.IsNullOrEmpty(e.ToString()))
                    {
                        r.Add(e.ToString());
                    }
                    e.Clear();
                    e.Append(c);
                }
                else if (c == bo && !inqt)
                {
                    bcc++;
                    e.Append(c);
                }
                else if (c == bc && !inqt)
                {
                    bcc--;
                    e.Append(c);
                    if (bcc < 1)
                    {
                        if (!string.IsNullOrEmpty(e.ToString()))
                        {
                            r.Add(e.ToString());
                        }
                        bo = '\0';
                        bc = '\0';
                        e.Clear();
                    }
                }
                else if (compare.Contains(c) && bcc < 1 && !inqt)
                {
                    e.Append(c);
                    if (text[i + 1] != '=')
                    {
                        if (!string.IsNullOrEmpty(e.ToString()))
                        {
                            r.Add(e.ToString());
                        }
                        e.Clear();
                    }
                }
                else if (operators.Contains(c) && bcc < 1 && !inqt)
                {
                    if (!char.IsDigit(text[i + 1]) || c != '-')
                    {
                        if (!string.IsNullOrEmpty(e.ToString()))
                        {
                            r.Add(e.ToString());
                        }
                        r.Add(c.ToString());
                        e.Clear();
                    }
                    else
                    {
                        e.Append(c);
                    }
                }
                else if (c == sp && !inqt && bcc < 1)
                {
                    if (!string.IsNullOrEmpty(e.ToString()))
                    {
                        r.Add(e.ToString());
                    }
                    e.Clear();
                }
                else if (c == endl && bcc < 1)
                {
                    if (!string.IsNullOrEmpty(e.ToString()))
                    {
                        r.Add(e.ToString());
                    }
                    e.Clear();
                    if (r.Count > 0)
                    {
                        ret.Add(r.ToArray());
                    }
                    r = new List<string>();
                }
                else if (c == bkslash && inqt)
                {
                    e.Append(c);
                    ignore_qt = true;
                }
                else
                {
                    e.Append(c);
                    ignore_qt = false;
                }
            }
            if (!string.IsNullOrEmpty(e.ToString()))
                r.Add(e.ToString());
            if (r.Count > 0)
                ret.Add(r.ToArray());

            return ret.ToArray();
        }
        public bool IsIdentifier(string s)
        {
            return char.IsLetter(s[0]) || s[0] == '_';
        }
        public bool IsNumber(string s)
        {
            try
            {
                if (char.IsDigit(s[0]))
                    return true;
                else if (s[0] == '-')
                    if (char.IsDigit(s[1]))
                        return true;

                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool IsString(string s)
        {
            try
            {
                if (s[0] == '\"' && s[s.Length - 1] == '\"')
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool IsChar(string s)
        {
            try
            {
                if (s[0] == '\'' && s[s.Length - 1] == '\'')
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool IsSymbol(string s)
        {
            return SYMBOLS.Contains(s.Trim());
        }
        public bool IsBody(string s)
        {
            try
            {
                return s[0] == '{' && s[s.Length - 1] == '}';
            }
            catch
            {
                return false;
            }
        }
        public bool IsBox(string s)
        {
            try
            {
                return s[0] == '[' && s[s.Length - 1] == ']';
            }
            catch
            {
                return false;
            }
        }
        public TokenType GetLiteralType(string s)
        {
            if (IsNumber(s))
                return TokenType.Number;
            else if (IsChar(s))
                return TokenType.Charecter;
            else if (IsString(s))
                return TokenType.String;
            else
                return TokenType.Compute;
        }
        public Token GetToken(string s)
        {
            Token tok = new(s);
            if (IsIdentifier(s))
            {
                if (KEYWORDS.Contains(s))
                {
                    tok.tokenTags.Add(TokenType.KeyWord);
                    if (DATATYPES.Contains(s))
                    {
                        tok.tokenTags.Add(TokenType.DataType);
                    }
                }
                else
                {
                    tok.tokenTags.Add(TokenType.Identifier);
                }
            }
            else if (IsSymbol(s))
            {
                tok.tokenTags.Add(SymbolStringToTokenType[s]);
            }
            else if (IsBody(s))
            {
                tok.tokenTags.Add(TokenType.Defination);
            }
            else if(IsBox(s)){
                tok.tokenTags.Add(TokenType.Box);
            }
            else{
                tok.tokenTags.Add(TokenType.Literal);
                tok.tokenTags.Add(GetLiteralType(s));
            }
            return tok;
        }
        public List<List<Token>> SingleTokenizer(string[][] lines)
        {
            List<List<Token>> ret = new();
            for (int i = 0; i < lines.Length; i++)
            {
                List<Token> k = new();
                for (int j = 0; j < lines[i].Length; j++)
                {
                    k.Add(GetToken(lines[i][j].Trim()));
                }
                ret.Add(k);
            }
            return ret;
        }
        public List<Token> Tokenize(List<List<Token>> tokens)
        {
            List<Token> ret = new();
            for (int i = 0; i < tokens.Count; i++)
            {
                for (int j = 0; j < tokens[i].Count; j++)
                {
                    
                }
            }
            return ret;
        }
    }
}