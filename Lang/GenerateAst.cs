namespace Bedrock
{
    public class GenerateAst
    {
        public static void main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Usage: generate_ast <output directory>");
            }
            string outputDir = args[0];
            defineAst(
                outputDir,
                "Expression",
                new List<string>()
                {
                    "BinaryExpression   : Expression left, Token opr, Expression right",
                    "GroupingExpression : Expression expression",
                    "LiteralExpresion  : object value",
                    "UnaryExpression    : Token opr, Expression right"
                }
            );
        }

        private static void defineAst(string outputDir, string baseName, List<string> types)
        {
            string path = outputDir + "/" + baseName + ".cs";
            if (!File.Exists(path))
                File.Create(path).Close();

            StreamWriter writer = new StreamWriter(path);

            writer.WriteLine("");
            writer.WriteLine("namespace Bedrock {");
            writer.WriteLine("abstract class " + baseName + " {");

            foreach (string type in types)
            {
                string className = type.Split(":")[0].Trim();
                string fields = type.Split(":")[1].Trim();
                defineType(writer, baseName, className, fields);
            }
            writer.WriteLine();
            writer.WriteLine("  virtual R accept<R>(Visitor<R> visitor);");

            writer.WriteLine("  }");
            writer.WriteLine("}");
            writer.Close();
        }

        private static void defineType(
            StreamWriter writer,
            string baseName,
            string className,
            string fieldList
        )
        {
            writer.WriteLine("  static class " + className + " : " + baseName + " {");

            // Constructor.
            writer.WriteLine("    " + className + "(" + fieldList + ") {");

            // Store parameters in fields.
            string[] fields = fieldList.Split(", ");
            foreach (string field in fields)
            {
                string name = field.Split(" ")[1];
                writer.WriteLine("      this." + name + " = " + name + ";");
            }

            writer.WriteLine("    }");

            // Fields.
            writer.WriteLine();
            foreach (string field in fields)
            {
                writer.WriteLine("    readonly " + field + ";");
            }

            writer.WriteLine("  }");

            writer.WriteLine();
            writer.WriteLine("    override R accept<R>(Visitor<R> visitor) {");
            writer.WriteLine("      return visitor.visit" + className + baseName + "(this);");
            writer.WriteLine("    }");
        }
        private static void defineVisitor(StreamWriter writer, string baseName, List<string> types)
        {
            writer.WriteLine("  interface Visitor<R> {");

            foreach (string type in types)
            {
                string typeName = type.Split(":")[0].Trim();
                writer.WriteLine(
                    "    R visit"
                        + typeName
                        + baseName
                        + "("
                        + typeName
                        + " "
                        + baseName.ToLower()
                        + ");"
                );
            }

            writer.WriteLine("  }");
        }
    }
}
