

inp = input("Enter keywords")
keywords = inp.split(' ')

clsTxt = "namespace Bedrock\n{\npublic class Syntax\n{\n"
clsTxt = clsTxt + "public static TokenType GetKeyword(string word){\n"+ "switch(word){\n"


for word in keywords:
    tok = ""
    if word[0].isupper():
        tok = word + "_type"
    else:
        tok = word[0].upper() + word[1:]
    clsTxt= clsTxt + f"case \"{word}\":\n"+f"return TokenType.{tok};\n"
    
clsTxt= clsTxt +"default:\nreturn TokenType.Identifier;\n}\n//eoswitch\n}\n//eofn\n"
clsTxt= clsTxt +"public class KeywordAsString\n{\npublic const string\n"


for word in keywords:
    tok = ""
    if word[0].isupper():
        tok = word + "_type"
    else:
        tok = word[0].upper() + word[1:]
    clsTxt= clsTxt + tok + "="+f"\"{word}\",\n"
    
clsTxt= clsTxt + ';\n}\n//eoKaS\n}\n//eosyn\n}\n//eon\n'

f = open("gen/Syntax.cs",'w')
f.write(clsTxt)
f.close()