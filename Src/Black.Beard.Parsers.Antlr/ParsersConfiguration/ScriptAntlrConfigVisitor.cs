using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Bb.Asts;
using Bb.ParserConfigurations.Antlr;
using Bb.Parsers;
using Bb.Parsers.Antlr;
using Bb.ParsersConfiguration.Antlr;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bb.Parsers
{

    public partial class ScriptAntlrConfigVisitor : AntlrConfigParserBaseVisitor<AntlrConfigAstBase>, Initializing
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        public ScriptAntlrConfigVisitor()
        {

        }


        public void Initialize(Parser parser, Diagnostics diagnostics, string path)
        {

            _parser = parser;
            _diagnostics = diagnostics;
            _scriptPath = path;

            if (!string.IsNullOrEmpty(path))
                _scriptPathDirectory = new FileInfo(path).Directory.FullName;
            else
                _scriptPathDirectory = AppDomain.CurrentDomain.BaseDirectory;

        }


        public void EvaluateErrors(IParseTree item)
        {

            if (item != null)
            {

                if (item is ErrorNodeImpl e)
                    AddError(e);

                else if (item is ParserRuleContext r)
                {

                    if (r.exception != null)
                    {
                        AddError(r);
                    }

                }

                int c = item.ChildCount;
                for (int i = 0; i < c; i++)
                {
                    IParseTree child = item.GetChild(i);
                    EvaluateErrors(child);
                }

            }

        }

        public override AntlrConfigAstBase Visit(IParseTree tree)
        {
            EvaluateErrors(tree);
            //if (this._diagnostics.Count > 0)
            //    LocalDebug.Stop();
            var result = base.Visit(tree);

            return (AntlrConfigAstBase)result;

        }

        public IEnumerable<ErrorModel> Errors { get => _diagnostics; }

        public string Filename { get; set; }

        public uint Crc { get; set; }
        public CultureInfo Culture { get => _currentCulture; }

        

        void AddError(TokenLocation start, string txt, string message, string path = null)
        {
            _diagnostics
                .AddError(
                    path ?? Filename,
                    start.Line,
                    start.StartIndex,
                    start.Column,
                    txt,
                    message
            );
        }

        void AddWarning(TokenLocation start, string txt, string message, string path = null)
        {
            _diagnostics
                .AddWarning(
                    path ?? Filename,
                    start.Line,
                    start.StartIndex,
                    start.Column,
                    txt,
                    message
            );

        }

        void AddError(ParserRuleContext r)
        {

            int stateId = r.invokingState;

            if (stateId == -1)
                stateId = r.exception.OffendingState;

            ATNState state = _parser.Atn.states[stateId];
            string o0 = _parser.RuleNames[state.ruleIndex];
            string o1 = _parser.RuleNames[r.RuleIndex];

            _diagnostics
                .AddError
                (
                    Filename,
                    r.Start.Line,
                    r.Start.StartIndex,
                    r.Start.Column,
                    r.Start.Text,
                    $"Failed to parse script. '{o0}' expect '{o1}'"
                );

        }

        void AddError(ErrorNodeImpl e)
        {
            _diagnostics
                .AddError(
                    Filename,
                    e.Symbol.Line,
                    e.Symbol.StartIndex,
                    e.Symbol.Column,
                    e.Symbol.Text,
                    $"Failed to parse script at position {e.Symbol.StartIndex}, line {e.Symbol.Line}, col {e.Symbol.Column} '{e.Symbol.Text}'"
            );
        }

        [System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerNonUserCode]
        protected void Pause()
        {
            System.Diagnostics.Debugger.Break();
        }


        private StringBuilder _initialSource;
        private Parser _parser;
        private Diagnostics _diagnostics;
        private string _scriptPath;
        private string _scriptPathDirectory;
        private CultureInfo _currentCulture;


    }

}


