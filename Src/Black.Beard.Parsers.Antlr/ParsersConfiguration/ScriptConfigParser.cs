using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Asts;
using Bb.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bb.ParserConfigurations.Antlr
{

    public class ScriptConfigParser
    {

        private ScriptConfigParser(TextWriter output, TextWriter outputError)
        {

            Output = output ?? Console.Out;
            OutputError = outputError ?? Console.Error;
            _includes = new HashSet<string>();
        }

        public static ScriptConfigParser ParseString(StringBuilder source, string sourceFile = "", TextWriter output = null, TextWriter outputError = null)
        {
            ICharStream stream = CharStreams.fromString(source.ToString());

            var parser = new ScriptConfigParser(output, outputError)
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
        public static ScriptConfigParser ParsePath(string source, TextWriter output = null, TextWriter outputError = null)
        {

            var payload = source.LoadFromFile();
            ICharStream stream = CharStreams.fromString(payload.ToString());

            var parser = new ScriptConfigParser(output, outputError)
            {
                File = source,
                Content = new StringBuilder(payload),
                //Crc = payload.CalculateCrc32(),
            };

            parser.ParseCharStream(stream);

            return parser;

        }

        public static bool Trace { get; set; }

        public AntlrConfigParser.Grammar_specContext Tree { get { return _context; } }

        public IEnumerable<string> Includes { get => _includes; }

        public string File { get; set; }

        public StringBuilder Content { get; private set; }

        public TextWriter Output { get; private set; }

        public TextWriter OutputError { get; private set; }

        private readonly HashSet<string> _includes;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result Visit<Result>(IParseTreeVisitor<Result> visitor)
        {

            if (visitor is Initializing f)
                f.Initialize(this._parser, new Diagnostics(), File);

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

            ITokenSource lexer = new AntlrConfigLexer(stream, Output, OutputError);
            var token = new CommonTokenStream(lexer);
            _parser = new AntlrConfigParser(token)
            {
                BuildParseTree = true,
                //Trace = ScriptParser.Trace, // Ca plante sur un null, pourquoi ?
            };

            _context = _parser.grammar_spec();

        }

        public AntlrConfigParser Parser { get => _parser; }

        private AntlrConfigParser _parser;
        private AntlrConfigParser.Grammar_specContext _context;

        public bool IsFragment { get; private set; }
    }

}
