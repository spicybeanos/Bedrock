using System;
using System.Data.Common;
using System.Reflection.Emit;
using System.Text;

namespace Bedrock
{
    public class Tokenizer
    {
        public static string[][] ProccessText_old(string str, char separator = ' ')
        {
            string text = str;
            const char endl = '\n', quote = '\"', bkslash = '\\', comment = '#';

            StringBuilder e = new("");
            const string openb = "([", closeb = ")]", operators = "+-*/%&:{}", compare = "<>!=";
            List<string> r = new List<string>();
            List<string[]> ret = new List<string[]>();
            char bo = '\0', bc = '\0';

            int bcc = 0;
            bool inqt = false, ignore_qt = false, isInComment = false;

            text = text.Replace("\r", "");
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (isInComment)
                {
                    if (c == endl)
                    {
                        isInComment = false;
                        if (!string.IsNullOrEmpty(e.ToString()))
                            r.Add(e.ToString());
                        e.Clear();
                        ret.Add(r.ToArray());
                        r.Clear();
                    }
                }
                else if (c == quote)
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
                        isInComment = true;
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
                else if (c == separator && !inqt && bcc < 1)
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

        public static string[][] ProccessText(string text, char s = ' ')
        {
            List<string[]> ret = new();

            return ret.ToArray();
        }
        public static List<List<Token>> Tokenize(string[][] lines)
        {
            List<List<Token>> ret = new();
            for (int i = 0; i < lines.Length; i++)
            {
                List<Token> k = new();
                for (int j = 0; j < lines[i].Length; j++)
                {
                    k.Add(Token.GetToken(lines[i][j].Trim()));
                }
                ret.Add(k);
            }
            return ret;
        }
    }
    public class TokenizerRules
    {
        public char Separator{get;set;}
        public char EndStatement{get;set;}
    }
}