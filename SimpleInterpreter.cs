using System;
using Bedrock;

namespace Bedrock
{
    public class SimpleInterpreter
    {
        public VariableHandler variableHandler {get;set;}
        public Statement[] statements {get;set;}

        public SimpleInterpreter(IEnumerable<Statement> _statements){
            statements = _statements.ToArray();
        }
        public bool IsPrimitiveType(Keyword k){
            return (k <= Keyword.Bool && k >= Keyword.Int);
        }
        public void Run()
        {
            for (int line = 0; line < statements.Length; line++)
            {
                if(statements[line][0].TokenValue is Keyword)
                {
                    Keyword k = (Keyword)statements[line][0].TokenValue;
                    switch (k)
                    {
                        case Keyword.Int:
                        case Keyword.Float:
                        case Keyword.String:
                        case Keyword.Byte:
                        case Keyword.Bool:
                        case Keyword.Var:
                            //create new variable
                            VariableDeclearation(line,0);
                            break;
                        case Keyword.If:
                            //handle if statements
                            break;
                        case Keyword.WhileLoop:
                            //handle while loops
                            break;
                        case Keyword.Function:
                            //handle function definations
                            break;
                        case Keyword.Struct:
                            // handle structure definations
                            break;
                    }
                }
            }
        }
    
    
        public void VariableDeclearation(int line,int index){
            if(statements[line].Length < 2)
                throw new Exception($"No name given to identifier line:{line}");
            Keyword type = (Keyword)statements[line][index].TokenValue;
            BedIdentifier name = (BedIdentifier)statements[line][index+1].TokenValue;
            if(statements[line].Length >= 3){
                
            }
            else{

            }
        }
    }
}


/*


Keyword k = (Keyword)statements[line][0].TokenValue;
switch (k)
{
    case Keyword.Int:
    case Keyword.Float:
    case Keyword.String:
    case Keyword.Byte:
    case Keyword.Bool:
    case Keyword.Var:
        //create new variable
        break;
    case Keyword.If:
        //handle if statements
        break;
    case Keyword.WhileLoop:
        //handle while loops
        break;
    case Keyword.Function:
        //handle function definations
        break;
    case Keyword.Struct:
        // handle structure definations
        break;
}
*/