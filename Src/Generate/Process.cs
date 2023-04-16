﻿using Bb;
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

                bool splitObjectOnDisk = false;

                string namespaceParser = "Bb.SqlServer.Parser";
                string namespaceModels = "Bb.SqlServer.Asts";


                ctx.RootAst = LoadGrammar(ctx, antlrParser);
                ctx.Configuration = LoadConfiguration(ctx);
                PostTreatments(ctx);

                var newConfiguration = Path.Combine(ctx.GrammarFolder, ctx.GrammarFile.Name + ".newConf");

                new ScriptList("Models")
                {
                    Namespace = namespaceModels,
                    
                }
                .Using("System")
                .Using("Bb.Asts")
                .Using("Bb.Parsers")

                //.Add<ScriptClassBases>()
                .Add<ScriptClassIdentifiers>(null, a =>
                {
                    a.SplitObjectOnDisk = splitObjectOnDisk;
                })
                //.Add<ScriptClassEnum>()
                .Add<ScriptClassTerminals>(null, a =>
                {
                    a.SplitObjectOnDisk = splitObjectOnDisk;
                })
                .Add<ScriptClassLists>(null, a =>
                {
                    a.SplitObjectOnDisk = splitObjectOnDisk;
                })
                .Add<ScriptClassIdentifiertWithProperties>(null, a =>
                {
                    a.SplitObjectOnDisk = splitObjectOnDisk;
                })
                .Add<ScriptClassWithProperties>(null, a =>
                {
                    a.SplitObjectOnDisk = splitObjectOnDisk;
                })
                .Add<ScriptClassDefaults>(null, a =>
                {
                    a.SplitObjectOnDisk = splitObjectOnDisk;
                })
                .Add<ScriptEnums>("Enums", a =>
                {
                    a.SplitObjectOnDisk = splitObjectOnDisk;
                    a.Using(namespaceParser);
                    a.Using("Antlr4.Runtime");
                    a.Using("Antlr4.Runtime.Tree");
                })
                .Add<ScriptClassVisitorEnums>("ScriptTSqlVisitor.Enums", a =>
                {
                    a.Namespace = namespaceParser;
                    a.Using(namespaceModels);
                })
                .Add<ScriptClassVisitorDefaults>("ScriptTSqlVisitor.Defaults", a =>
                {
                    a.Namespace = namespaceParser;
                    a.Using(namespaceModels);
                })
                .Add<ScriptClassVisitorIdentifier>("ScriptTSqlVisitor.Identifiers", a =>
                {
                    a.Namespace = namespaceParser;
                    a.Using(namespaceModels);
                })
                .Add<ScriptClassVisitorList>("ScriptTSqlVisitor.Lists", a =>
                {
                    a.Namespace = namespaceParser;
                    a.Using(namespaceModels);
                })
                .Add<ScriptClassVisitorTerminalAlias>("ScriptTSqlVisitor.Terminals", a =>
                {
                    a.Namespace = namespaceParser;
                    a.Using(namespaceModels);
                })
                .Add<ScriptClassVisitorWithProperties>("ScriptTSqlVisitor.WithProperties", a =>
                {
                    a.Namespace = namespaceParser;
                    a.Using(namespaceModels);
                })

                .Add<ScriptTSqlVisitor2>("ScriptTSqlVisitor2", a => a.Namespace = namespaceParser)
                //.Add<ScriptClassToString>()

                .Add<ScriptInterfaceVisitor1>("IAstTSqlVisitor1")
                //.Add<ScriptInterfaceVisitor2>("IAstTSqlVisitor2")

                .Generate(ctx);

                ctx.Configuration.Save(newConfiguration);

            }

        }

        private static void PostTreatments(Context ctx)
        {

            new ParentVisitor().Visit(ctx.RootAst);

        }

        private AstGrammarSpec LoadGrammar(Context ctx, FileInfo antlrFile)
        {

            ctx.GrammarFile = antlrFile;

            AstGrammarSpec rootAst = null;
            foreach (AstGrammarSpec grammar in LoadGrammar(antlrFile))
            {
                if (rootAst == null)
                    rootAst = grammar;
                else
                    foreach (var rule in grammar.Rules.Terminals)
                        rootAst.Rules.Terminals.Add(rule);
            }

            return rootAst;

        }

        private static IEnumerable<AstGrammarSpec> LoadGrammar(FileInfo antlrFile)
        {

            if (antlrFile.Exists)
            {
                var sb = new StringBuilder(antlrFile.LoadFromFile());
                var parser = ScriptParser.ParseString(sb, antlrFile.FullName);
                var visitor = new ScriptAntlrVisitor();
                var rootAst = (AstGrammarSpec)parser.Visit(visitor);
                yield return rootAst;

                foreach (var lexer in visitor.Lexers)
                {
                    var filename = new FileInfo(Path.Combine(antlrFile.Directory.FullName, lexer + ".g4"));
                    foreach (var rootAst2 in LoadGrammar(filename))
                        yield return rootAst2;
                }
            }

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
