using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Bb.Asts;
using Bb.Parsers;
using Bb.Parsers.Antlr;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace Bb.Parsers
{

    public partial class ScriptAntlrVisitor
    {

        public ScriptAntlrVisitor()
        {
            _lexerList = new HashSet<string>();
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.Parsers.Antlr.ANTLRv4Parser.lexerRuleSpec" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// lexerRuleSpec : FRAGMENT? TOKEN_REF optionsSpec? COLON lexerRuleBlock SEMI
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AstBase VisitLexerRuleSpec([NotNull] ANTLRv4Parser.LexerRuleSpecContext context)
        {

            var name = (AstTerminalText)context.TOKEN_REF().Accept(this);

            AstOptionList optionList = null;
                        
            AstLexerAlternativeList value = null;
            var options_spec = context.optionsSpec();
            if (options_spec != null)
                optionList = (AstOptionList)VisitOptionsSpec(options_spec);

            var lexerRuleBlock = context.lexerRuleBlock();
            if (lexerRuleBlock != null)
                value = (AstLexerAlternativeList)VisitLexerRuleBlock(context.lexerRuleBlock());

            var fragment = context.FRAGMENT() != null;

            return new AstLexerRule(context, name, value) { Options = optionList, IsFragment = fragment };

        }


        //public override AstBase VisitGrammarType([NotNull] ANTLRv4Parser.GrammarTypeContext context)
        //{
        //    var lexer = context.LEXER();
        //    if (lexer != null)
        //        return GrammarType.Lexer;
        //    var parser = context.PARSER();
        //    if (parser != null)
        //        return GrammarType.Parser;
        //    return GrammarType.None;
        //}


        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.Parsers.Antlr.ANTLRv4Parser.lexerElement" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// lexerElement
        ///     : labeledLexerElement ebnfSuffix?
        ///     | lexerAtom ebnfSuffix?
        ///     | lexerBlock ebnfSuffix?
        ///     | actionBlock QUESTION?
        ///     ;
        // but preds can be anywhere
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AstBase VisitLexerElement([NotNull] ANTLRv4Parser.LexerElementContext context)
        {

            var ebnf = (AstEbnfSuffix)VisitEbnfSuffix(context.ebnfSuffix());

            var result = (AstLexerLabeledElement)VisitLabeledLexerElement(context.labeledLexerElement());
            if (result != null)
            {
                if (ebnf != null)
                    result.Occurence = ebnf.Occurence;
                return result;
            }

            var f = (AstAtom)VisitLexerAtom(context.lexerAtom());
            if (f != null)
            {
                if (ebnf != null)
                    f.Occurence = ebnf.Occurence;
                return f;
            }

            var g = (AstLexerBlock)VisitLexerBlock(context.lexerBlock());
            if (g != null)
            {
                return g;
            }
            var e = (AstArgActionBlock)VisitActionBlock(context.actionBlock());
            e.Occurence = new Occurence(OccurenceEnum.One, context.QUESTION() != null);

            return e;

        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.Parsers.Antlr.ANTLRv4Parser.lexerAtom" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// lexerAtom
        ///     : characterRange
        ///     | terminal
        ///     | notSet
        ///     | LEXER_CHAR_SET
        ///     | DOT elementOptions?
        ///     ;
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AstBase VisitLexerAtom([NotNull] ANTLRv4Parser.LexerAtomContext context)
        {

            AstBase value = null;

            if (context != null)
            {

                var t2 = context.LEXER_CHAR_SET();
                if (t2 != null)
                    value = VisitTerminal(t2);

                else
                {

                    var t0 = context.characterRange();
                    if (t0 != null)
                        value = VisitCharacterRange(t0);

                    else
                    {


                        var t1 = context.terminal();
                        if (t1 != null)
                        {
                            value = (AstTerminal)VisitTerminal(t1);

                           

                        }
                        else
                        {

                            var t3 = context.notSet();
                            if (t3 != null)
                            {
                                value = VisitNotSet(t3);
                            }

                            else
                            {
                                var t4 = context.elementOptions();
                                if (t4 != null)
                                {
                                    Pause();
                                    value = VisitElementOptions(t4);
                                }
                            }

                        }

                    }
                }

                if (value == null)
                    value = AstTerminalText.EmptyRule(context);

                return new AstAtom(context, value);

            }

            return null;

        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.Parsers.Antlr.ANTLRv4Parser.labeledLexerElement" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// identifier (ASSIGN | PLUS_ASSIGN) (lexerAtom | lexerBlock)
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AstBase VisitLabeledLexerElement([NotNull] ANTLRv4Parser.LabeledLexerElementContext context)
        {
            if (context != null)
            {
                Pause();

                AstBase value = null;
                var atom = context.lexerAtom();
                if (atom != null)
                    value = VisitLexerAtom(atom);
                else
                    value = VisitLexerBlock(context.lexerBlock());

                LabeledElementAssignEnum mode = LabeledElementAssignEnum.Assign;
                if (context.PLUS_ASSIGN() != null)
                    mode = LabeledElementAssignEnum.PlusAssign;

                return new AstLexerLabeledElement(context, (AstIdentifier)VisitIdentifier(context.identifier()), mode, value);

            }

            return null;

        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.Parsers.Antlr.ANTLRv4Parser.lexerAlt" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// lexerElements lexerCommands?
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AstBase VisitLexerAlt([NotNull] ANTLRv4Parser.LexerAltContext context)
        {

            var result = new AstLexerAlternative(context);
            result.Rule = (AstLexerElementList)VisitLexerElements(context.lexerElements());

            var lexerCommands = context.lexerCommands();
            if (lexerCommands != null)
                result.Commands =(AstLexerCommandList) VisitLexerCommands(lexerCommands);
            
            return result;

        }


        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.Parsers.Antlr.ANTLRv4Parser.lexerAltList" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// lexerAlt (OR lexerAlt)*
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AstBase VisitLexerAltList([NotNull] ANTLRv4Parser.LexerAltListContext context)
        {

            var lexerAltList = context.lexerAlt();
            var items = new AstLexerAlternativeList(context, lexerAltList.Length);

            foreach (var lexerAlt in lexerAltList)
                items.Add((AstLexerAlternative)VisitLexerAlt(lexerAlt));

            return items;
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.Parsers.Antlr.ANTLRv4Parser.lexerBlock" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// LPAREN lexerAltList RPAREN
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AstBase VisitLexerBlock([NotNull] ANTLRv4Parser.LexerBlockContext context)
        {

            AstLexerAlternativeList _c = null;
            var c = context.lexerAltList();
            if (c != null)
                _c = (AstLexerAlternativeList)VisitLexerAltList(c);

            return new AstLexerBlock(context, _c);

        }


        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.Parsers.Antlr.ANTLRv4Parser.lexerCommand" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        ///       lexerCommandName LPAREN lexerCommandExpr RPAREN
        ///     | lexerCommandName
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AstBase VisitLexerCommand([NotNull] ANTLRv4Parser.LexerCommandContext context)
        {

            if (context != null)
            {

                var l = context.lexerCommandName();
                if (l != null)
                {

                    var lexerCommandName = VisitLexerCommandName(l);

                    var m = context.lexerCommandExpr();
                    if (m != null)
                    {

                        var o = VisitLexerCommandExpr(m); // Result is Identifier or INT
                        return new AstLexerCommand(context, lexerCommandName.ResolveName(), o.ResolveName());

                    }

                    return new AstLexerCommand(context, lexerCommandName.ResolveName(), null);

                }

            }

            return null;

        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.Parsers.Antlr.ANTLRv4Parser.lexerCommandName" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AstBase VisitLexerCommandName([NotNull] ANTLRv4Parser.LexerCommandNameContext context)
        {

            var i = context.identifier().Accept(this);
            if (i != null)
                return i;

            Pause();

            return VisitTerminal(context.MODE());

        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.Parsers.Antlr.ANTLRv4Parser.lexerCommandExpr" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AstBase VisitLexerCommandExpr([NotNull] ANTLRv4Parser.LexerCommandExprContext context)
        {

            var i = context.identifier().Accept(this);
            if (i != null)
                return i;

            Pause();

            return VisitTerminal(context.INT());

        }

        public override AstBase VisitTerminal(ITerminalNode node)
        {
            return new AstTerminalText(node, node.Symbol.Type.ToString(), node.GetText());
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.Parsers.Antlr.ANTLRv4Parser.lexerCommands" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// RARROW lexerCommand (COMMA lexerCommand)*
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AstBase VisitLexerCommands([NotNull] ANTLRv4Parser.LexerCommandsContext context)
        {

            var lexerCommands = context.lexerCommand();
            var list = new AstLexerCommandList(context, lexerCommands.Length);

            foreach (var lexerCommand in lexerCommands)
                list.Add((AstLexerCommand)VisitLexerCommand(lexerCommand));
 
            return list;

        }



        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.Parsers.Antlr.ANTLRv4Parser.lexerElements" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AstBase VisitLexerElements([NotNull] ANTLRv4Parser.LexerElementsContext context)
        {

            var lexerElements = context.lexerElement();
            var result = new AstLexerElementList(context, lexerElements.Length);
            foreach (var lexerElement in lexerElements)
                result.Add(VisitLexerElement(lexerElement));

            return result;

        }

        public override AstBase VisitCharacterRange([NotNull] ANTLRv4Parser.CharacterRangeContext context)
        {
            var str = context.STRING_LITERAL();
            return new AstRange(context, (AstTerminalText)VisitTerminal(str[0]), (AstTerminalText)VisitTerminal(str[1]));
        }

        public override AstBase VisitChannelsSpec([NotNull] ANTLRv4Parser.ChannelsSpecContext context)
        {

            Pause();
            return (AstBase)base.VisitChannelsSpec(context);
        }

        public override AstBase VisitBlockSet([NotNull] ANTLRv4Parser.BlockSetContext context)
        {

            Pause();
            return (AstBase)base.VisitBlockSet(context);
        }

        //public override AstBase VisitLexerRuleBlock([NotNull] ANTLRv4Parser.LexerRuleBlockContext context)
        //{
        //    Pause();
        //    return (AstBase)base.VisitLexerRuleBlock(context);
        //}


        public IEnumerable<string> Lexers => _lexerList;

        private readonly HashSet<string> _lexerList;


    }

}


