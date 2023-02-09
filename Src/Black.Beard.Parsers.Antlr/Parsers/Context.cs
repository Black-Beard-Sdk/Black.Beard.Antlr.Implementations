using Bb.Asts;
using Bb.ParsersConfiguration.Ast;

namespace Bb.Parsers
{
    public class Context
    {

        public Context()
        {

        }



        public string Namespace { get; set; }
        public string Strategy { get; internal set; }

        

        
        public string OutputPath { get; set; }



        public string GrammarFolder { get => GrammarFile.Directory.FullName; }
        public FileInfo GrammarFile { get; set; }
        public AstGrammarSpec RootAst { get; set; }



        public HashSet<string> GetGeneratedFiles()
        {

            HashSet<string> names = new HashSet<string>();

            var dir = new DirectoryInfo(this.OutputPath);
            if (!dir.Exists)
                dir.Create();

            foreach (var item in dir.GetFiles("*.generated.cs"))
                names.Add(item.FullName);

            return names;

        }
        public void RemoveFiles(HashSet<string> names)
        {
            foreach (var item in names)
                File.Delete(item);
        }



        public string ConfigurationFile { get; set; }
        public string AntlrParserRootName { get; set; }
        public GrammarSpec Configuration { get; set; }
        public GrammarConfigDeclaration CurrentConfiguration { get; internal set; }


    }
}
