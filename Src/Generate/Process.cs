using Bb;
using Bb.Asts;
using Bb.Configurations;
using Bb.Generators;
using Bb.ParserConfigurations.Antlr;
using Bb.Parsers;
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

                ctx.AntlrParserRootName = Path.GetFileNameWithoutExtension(antlrParser.Name) + ".";
                this._folder = antlrParser.Directory.FullName;

                var sb = new StringBuilder(antlrParser.LoadFromFile());
                var parser = ScriptParser.ParseString(sb, antlrParser.FullName);
                var rootAst = (AstGrammarSpec)parser.Visit(new ScriptAntlrVisitor());

                var configFile = Path.Combine(this._folder, ctx.AntlrParserRootName + "conf");
                if (File.Exists(configFile))
                {
                    var sb2 = new StringBuilder(configFile.LoadFromFile());
                    var config = ScriptConfigParser.ParseString(sb2, configFile);
                    _astConfig = (GrammarSpec)config.Visit(new ScriptAntlrConfigVisitor());
                }
                else
                    _astConfig = new GrammarSpec(Position.Default);

                _astConfig.Append(rootAst);

                foreach (var item in rootAst.Rules)
                {
                    var conf = ctx.Configuration.GetConfiguration(item);
                    if (conf.Strategy != "_")
                        item.Configuration.Config.TemplateSetting.TemplateName = conf.Strategy;
                    item.Configuration.Config.Generate = conf.Generate;
                }

                var visitor2 = new ParentVisitor();
                visitor2.Visit(rootAst);

                var visitor3 = new CodeVisitor();
                visitor3.Visit(rootAst);

                List<ScriptBase> _generators = new List<ScriptBase>()
                {
                    new ScriptEnums() { Name = "Models.Enums" },
                    new ScriptClassEnum() { Name = "Models.ClassEnums" },
                    new ScriptClassTerminalAlias() { Name = "Models.Ids" },
                    new ScriptClassList() { Name = "Models.Lists" },
                    new ScriptClassWithProperties() { Name ="Models.ClassWithProperties" },
                    new ScriptModelDefault() { Name= "Models.Defaults" },
                    new ScriptVisitor1() { Name = "IAstTSqlVisitor1"},
                    new ScriptVisitor2() { Name = "IAstTSqlVisitor2"},
                    new ScriptTSqlVisitor1() { Name = "ScriptTSqlVisitor1" },
                    new ScriptTSqlVisitor2() { Name = "ScriptTSqlVisitor2" },

                    // 

                };


                var names = ctx.GetGeneratedFiles();

                foreach (var generator in _generators)
                {
                    generator.Configuration = _astConfig;
                    foreach (var name in generator.Generate(rootAst, ctx))
                        if (names.Contains(name))
                            names.Remove(name);
                }

                ctx.RemoveFiles(names);

         

                _astConfig.Save(configFile);

            }

        }

        private string _folder;
        private GrammarSpec _astConfig;

    }

}
