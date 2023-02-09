using Bb;
using Bb.Asts;
using Bb.Generators;
using Bb.ParserConfigurations.Antlr;
using Bb.Parsers;
using Bb.Parsers.Antlr;
using Bb.ParsersConfiguration.Ast;
using Generate.Scripts;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generate
{

    public class Process
    {

        public void Run(FileInfo antlrParser, Context ctx)
        {

            if (antlrParser.Exists)
            {

                ctx.RootAst = LoadGrammar(ctx, antlrParser);






                ctx.Configuration = LoadConfiguration(ctx);

                new ParentVisitor().Visit(ctx.RootAst);
                new CodeVisitor().Visit(ctx.RootAst);

                new ScriptList("Models")
                    {
                        Namespace = "Bb.Asts.TSql",
                    }
                    .Using("System", "Bb.Parsers")
                    
                    .Add<ScriptClassIdentifiers>()
                    .Add<ScriptClassEnum>()
                    .Add<ScriptClassTerminals>()
                    .Add<ScriptClassLists>()
                    .Add<ScriptClassWithProperties>()
                    .Add<ScriptClassDefaults>()
                    .Add<ScriptEnums>("Enums")
                    .Add<ScriptVisitor1>("IAstTSqlVisitor1")
                    .Add<ScriptVisitor2>("IAstTSqlVisitor2")
                    .Add<ScriptTSqlVisitor1>("ScriptTSqlVisitor1", a =>
                    {
                        a.Namespace = "Bb.Parsers.TSql";
                        a.Using("Bb.Asts.TSql");
                    })
                    .Add<ScriptTSqlVisitor2>("ScriptTSqlVisitor2", a => a.Namespace = "Bb.Parsers.TSql")
                    .Add<ScriptClassToString>()

                    .Generate(ctx);

                ctx.Configuration.Save(ctx.ConfigurationFile);

            }

        }


        private AstGrammarSpec LoadGrammar(Context ctx, FileInfo antlrParser)
        {
            ctx.GrammarFile = antlrParser;
            var sb = new StringBuilder(antlrParser.LoadFromFile());
            var parser = ScriptParser.ParseString(sb, antlrParser.FullName);
            var rootAst = (AstGrammarSpec)parser.Visit(new ScriptAntlrVisitor());
            return rootAst;
        }

        private GrammarSpec LoadConfiguration(Context ctx)
        {

            if (string.IsNullOrEmpty(ctx.AntlrParserRootName))
                ctx.AntlrParserRootName = Path.GetFileNameWithoutExtension(ctx.GrammarFile.Name);

            if (string.IsNullOrEmpty(ctx.ConfigurationFile))
                ctx.ConfigurationFile = Path.Combine(ctx.GrammarFolder, ctx.GrammarFile.Name + ".conf");

            GrammarSpec result;
            if (File.Exists(ctx.ConfigurationFile))
            {
                var sb2 = new StringBuilder(ctx.ConfigurationFile.LoadFromFile());
                var config = ScriptConfigParser.ParseString(sb2, ctx.ConfigurationFile);
                result = (GrammarSpec)config.Visit(new ScriptAntlrConfigVisitor());
            }
            else
                result = new GrammarSpec(Position.Default);

            result.Append(ctx.RootAst);

            return result;

        }


    }

}
