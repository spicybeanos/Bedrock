using System;

namespace Bedrock{
    public class TextSpilter{
        public static string[] Split(string line, char separator = ' ')
        {
            List<string> ret_ = new List<string>();
            string e = "";
            char SPACE = separator;
            bool withinQuotes_ = false;
            //if bcc is > 0 then c is inside a bracket

            int bcc = 0;
            char brc_b = '\0', brc_a = '\0';
            string openbs = "({";
            string closebs = ")}";
            string mathOps = "+-*/%=";

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '\"' )
                {
                    withinQuotes_ = !withinQuotes_;
                    e += c;
                    if (withinQuotes_ == false && bcc < 1)
                    {
                        if (e != "")
                        {
                            ret_.Add(e);
                        }
                        e = "";
                    }
                }
                else if (openbs.Contains(c) && bcc < 1 && !withinQuotes_)
                {
                    brc_a = c;
                    brc_b = closebs[openbs.IndexOf(c)];
                    bcc++;
                    if (e != "")
                    {
                        ret_.Add(e);
                    }
                    e = "";
                    e += c;
                }
                else if (c == brc_a && !withinQuotes_)
                {
                    bcc++;
                    e += c;
                }
                else if (c == brc_b && !withinQuotes_)
                {
                    bcc--;
                    e += c;
                    if (bcc < 1)
                    {
                        if (e != "")
                        {
                            ret_.Add(e);
                        }
                        brc_a = '\0';
                        brc_b = '\0';
                        e = "";
                    }
                }
                else if (mathOps.Contains(c) && bcc < 1)
                {
                    if (e != "")
                    {
                        ret_.Add(e);
                    }
                    ret_.Add(c + "");
                    e = "";
                }
                else if (c == SPACE && !withinQuotes_ && bcc < 1)
                {
                    if (e != "")
                    {
                        ret_.Add(e);
                    }
                    e = "";
                }
                else
                {
                    e += c;
                }
            }
            if (e != "")
            {
                ret_.Add(e);
            }
            return ret_.ToArray();
        }
        public static string DeBrack(char brackA, char brackB, string str)
        {
            //if bcc is > 0 then c is inside a bracket
            int substrstart = -1, len = 0;
            int bcc = 0;
            char brc_b = '\0';
            string openbs = "([{";
            string closebs= ")]}";

            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];


                if (bcc > 0)
                {
                    len++;
                }
                if (openbs.Contains(c))
                {
                    bcc++;
                    if(bcc == 1)
                    {
                        substrstart = i+1;
                        brc_b = closebs[openbs.IndexOf(c)];
                    }
                }

                if (closebs.Contains(c))
                {
                    bcc--;
                    if(bcc == 0)
                    {
                        return str.Substring(substrstart, len-1);
                    }
                }

            }

            return str.Substring(substrstart, len);
        }
        public static string[][] ProccessText(string text)
        {
            const char endl = '\n', sp =' ',qt='\"',bkslash = '\\',comment = '#';
            string e = "";
            const string openb = "([{", closeb = ")]}",operators="+-*/%",compare = "<>!=";
            List<string> r = new List<string>();
            List<string[]> ret = new List<string[]>();
            char bo='\0', bc='\0';

            int bcc = 0;
            bool inqt = false,ignore_qt=false,isincomment = false;

            text = text.Replace("\r", "");
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (isincomment)
                {
                    if(c == endl)
                    {
                        isincomment = false;
                        if(!string.IsNullOrEmpty(e))
                            r.Add(e);
                        e = "";
                    }
                }
                else if (c == qt)
                {
                    if(!ignore_qt && bcc < 1)
                        inqt = !inqt;

                    e += c;
                    if (!inqt && bcc < 1 )
                    {
                        if (!string.IsNullOrEmpty(e))
                        {
                            r.Add(e);
                        }
                        e = "";
                    }
                }
                else if (c == comment)
                {
                    if(!inqt && bcc <= 0)
                        isincomment = true;
                    else
                        e+= c;
                }
                else if (openb.Contains(c) && bcc < 1 && !inqt)
                {
                    bo = c;
                    bc = closeb[openb.IndexOf(c)];
                    bcc++;
                    if (!string.IsNullOrEmpty(e))
                    {
                        r.Add(e);
                    }
                    e = "";
                    e += c;
                }
                else if (c == bo && !inqt)
                {
                    bcc++;
                    e += c;
                }
                else if (c == bc && !inqt)
                {
                    bcc--;
                    e += c;
                    if (bcc < 1)
                    {
                        if (!string.IsNullOrEmpty(e))
                        {
                            r.Add(e);
                        }
                        bo = '\0';
                        bc = '\0';
                        e = "";
                    }
                }
                else if(compare.Contains(c) && bcc < 1 && !inqt)
                {
                    e+=c;
                    if(text[i+1] != '='){
                        if (!string.IsNullOrEmpty(e))
                        {
                            r.Add(e);
                        }
                        e = "";
                    }
                }
                else if (operators.Contains(c) && bcc < 1 && !inqt)
                {
                    if(!char.IsDigit( text[i+1]) || c!= '-'){
                        if (!string.IsNullOrEmpty(e))
                        {
                            r.Add(e);
                        }
                        r.Add(c.ToString());
                        e = "";
                    }
                    else{
                        e+=c;
                    }
                }
                else if (c == sp && !inqt && bcc < 1)
                {
                    if (!string.IsNullOrEmpty(e))
                    {
                        r.Add(e);
                    }
                    e = "";
                }
                else if (c == endl && bcc < 1)
                {
                    if (!string.IsNullOrEmpty(e))
                    {
                        r.Add(e);
                    }
                    e = "";
                    if(r.Count > 0)
                    {
                        ret.Add(r.ToArray());
                    }
                    r = new List<string>();
                }
                else if (c == bkslash && inqt)
                {
                    e += c;
                    ignore_qt = true;
                }
                else
                {
                    e += c;
                    ignore_qt = false;
                }
            }
            if(!string.IsNullOrEmpty(e))
                r.Add(e);
            if(r.Count > 0)
                ret.Add(r.ToArray());

            return ret.ToArray();
        }
    }
}