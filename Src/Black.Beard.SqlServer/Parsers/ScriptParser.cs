using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Asts;
using Bb.Parsers.Tsql;
using Bb.Parsers.TSql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bb.Parsers
{

    public class ScriptParser
    {

        private ScriptParser(TextWriter output, TextWriter outputError)
        {

            Output = output ?? Console.Out;
            OutputError = outputError ?? Console.Error;
            _includes = new HashSet<string>();
        }

        public static ScriptParser ParseString(StringBuilder source, string sourceFile = "", TextWriter output = null, TextWriter outputError = null)
        {
            ICharStream stream = CharStreams.fromString(source.ToString());

            var parser = new ScriptParser(output, outputError)
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
        public static ScriptParser ParsePath(string source, TextWriter output = null, TextWriter outputError = null)
        {

            var payload = source.LoadFromFile();
            ICharStream stream = CharStreams.fromString(payload.ToString());

            var parser = new ScriptParser(output, outputError)
            {
                File = source,
                Content = new StringBuilder(payload),
                //Crc = payload.CalculateCrc32(),
            };

            parser.ParseCharStream(stream);

            return parser;

        }

        public static bool Trace { get; set; }

        public TSqlParser.Tsql_fileContext Tree { get { return _context; } }

        public IEnumerable<string> Includes { get => _includes; }

        public string File { get; set; }

        public StringBuilder Content { get; private set; }

        public TextWriter Output { get; private set; }

        public TextWriter OutputError { get; private set; }

        private readonly HashSet<string> _includes;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result Visit<Result>(IParseTreeVisitor<Result> visitor)
        {

            if (visitor is IFile f)
                f.Filename = File;

            if (System.Diagnostics.Debugger.IsAttached)
                System.Diagnostics.Trace.WriteLine(File);

            var context = _context;
            return visitor.Visit(context);

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

            _context = _parser.tsql_file();

        }

        public TSqlParser Parser { get => _parser; }

        private TSqlParser _parser;
        private TSqlParser.Tsql_fileContext _context;

        public bool IsFragment { get; private set; }
    }

}
