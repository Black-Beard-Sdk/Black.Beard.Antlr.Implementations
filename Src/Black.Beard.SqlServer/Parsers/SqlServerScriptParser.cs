﻿using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Parsers;
using Bb.SqlServer.Asts;

using System.Runtime.CompilerServices;
using System.Text;

namespace Bb.SqlServer.Parser
{

    public class SqlServerScriptParser
    {

        private SqlServerScriptParser(TextWriter output, TextWriter outputError)
        {

            Output = output ?? Console.Out;
            OutputError = outputError ?? Console.Error;
            _includes = new HashSet<string>();
        }

        public static SqlServerScriptParser ParseString(StringBuilder source, string sourceFile = "", TextWriter output = null, TextWriter outputError = null)
        {
            ICharStream stream = CharStreams.fromString(source.ToString());

            var parser = new SqlServerScriptParser(output, outputError)
            {
                File = sourceFile ?? string.Empty,
                Content = source,
                //Crc = source.CalculateCrc32(),
            };
            parser.ParseCharStream(stream);
            return parser;

        }

        /// <summary>
        /// Load specified document in a dedicated parser
        /// </summary>
        /// <param name="source"></param>
        /// <param name="output"></param>
        /// <param name="outputError"></param>
        /// <returns></returns>
        public static SqlServerScriptParser ParsePath(string source, TextWriter output = null, TextWriter outputError = null)
        {

            var payload = source.LoadFromFile();
            ICharStream stream = CharStreams.fromString(payload.ToString());

            var parser = new SqlServerScriptParser(output, outputError)
            {
                File = source,
                Content = new StringBuilder(payload),
                //Crc = payload.CalculateCrc32(),
            };

            parser.ParseCharStream(stream);

            return parser;

        }

        public static bool Trace { get; set; }

        public TSqlParser.T_rootContext Tree { get { return _context; } }

        public IEnumerable<string> Includes { get => _includes; }

        public string File { get; set; }

        public StringBuilder Content { get; private set; }

        public TextWriter Output { get; private set; }

        public TextWriter OutputError { get; private set; }

        private readonly HashSet<string> _includes;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result Visit<Result>(IParseTreeVisitor<Result> visitor)
        {

            if (visitor is Initializing g)
                g.Initialize(Parser, new Diagnostics(), File);

            if (System.Diagnostics.Debugger.IsAttached)
                System.Diagnostics.Trace.WriteLine(File);

            var context = _context;
            var result = visitor.Visit(context);
            return result;
        }

        public AstRoot GetModel()
        {
            var visitor = new ScriptTSqlVisitor();
            var result = (AstRoot)Visit(visitor);
            return result;
        }

        public bool InError { get => _parser.ErrorListeners.Count > 0; }
        public uint Crc { get; private set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ParseCharStream(ICharStream stream)
        {

            var lexer = new TSqlLexer(stream, Output, OutputError);
            var token = new CommonTokenStream(lexer);
            _parser = new TSqlParser(token)
            {
                BuildParseTree = true,
                //Trace = ScriptParser.Trace, // Ca plante sur un null, pourquoi ?
            };

            _context = _parser.t_root();

        }

        public TSqlParser Parser { get => _parser; }

        private TSqlParser _parser;
        private TSqlParser.T_rootContext _context;

        public bool IsFragment { get; private set; }
    }

}
