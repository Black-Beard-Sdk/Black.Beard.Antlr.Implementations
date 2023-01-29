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


        public override AstBase VisitGrammarSpec([NotNull] ANTLRv4Parser.GrammarSpecContext context)
        {

            if (context != null)
            {

                var result = new AstGrammarSpec(context, (AstGrammarDecl)VisitGrammarDecl(context.grammarDecl()));

                var prequels = context.prequelConstruct();

                if (prequels != null && prequels.Length > 0)
                {

                    result.Prequels = new AstPrequelConstructList(context, prequels.Length);

                    foreach (var prequel in prequels)
                        result.Prequels.Add((AstPrequelConstruct)VisitPrequelConstruct(prequel));

                }

                result.Rules = (AstRulesList)VisitRules(context.rules());


                var ms = context.modeSpec();
                if (ms != null && ms.Length > 0)
                {
                    result.Modes = new AstModeSpecList(context, ms.Length);
                    foreach (var m in ms)
                        result.Modes.Add((AstModeSpec)VisitModeSpec(m));
                }

                return result;
            }

            return null;

        }

        public override AstBase VisitGrammarDecl([NotNull] ANTLRv4Parser.GrammarDeclContext context)
        {

            GrammarType type = GrammarType.None;

            var grammarType = context.grammarType();

            var lexer = grammarType.LEXER();
            if (lexer != null)
                type = GrammarType.Lexer;
            else
            {
                var parser = grammarType.PARSER();
                if (parser != null)
                    type = GrammarType.Parser;
                else
                    type = GrammarType.None;
            }

            return new AstGrammarDecl(context)
            {
                Type = type,
                Name = (AstIdentifier)VisitIdentifier(context.identifier()),
            };
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
        /// Visit a parse tree produced by <see cref="M:Bb.Parsers.Antlr.ANTLRv4Parser.identifier" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AstBase VisitIdentifier([NotNull] ANTLRv4Parser.IdentifierContext context)
        {

            var r1 = context.RULE_REF();
            if (r1 != null)
                return (AstBase)VisitTerminalNode(r1, "RULE_REF");

            var r = context.TOKEN_REF();

            return (AstBase)VisitTerminalNode(r, "TOKEN_REF");

        }

        public AstIdentifier VisitTerminalNode([NotNull] ITerminalNode context, string type)
        {
            if (context != null)
            {
                var txt1 = context.GetText();
                return new AstIdentifier(context, type, txt1);
            }

            return null;
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.Parsers.Antlr.ANTLRv4Parser.prequelConstruct" />.
        /// optionsSpec | delegateGrammars | tokensSpec | channelsSpec | action_
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AstBase VisitPrequelConstruct([NotNull] ANTLRv4Parser.PrequelConstructContext context)
        {
            return new AstPrequelConstruct(context, (AstBase)base.VisitPrequelConstruct(context));
        }

        public override AstBase VisitOptionsSpec([NotNull] ANTLRv4Parser.OptionsSpecContext context)
        {

            AstOptionList result = new AstOptionList(context);

            var opts = context.option();
            if (opts != null)
            {
                foreach (var option in opts)
                {
                    var o = (AstOption)VisitOption(option);
                    result.Add(o);
                }

            }

            return result;

        }

        public override AstBase VisitOption([NotNull] ANTLRv4Parser.OptionContext context)
        {

            var key = (AstIdentifier)VisitIdentifier(context.identifier());
            var value = (AstBase)VisitOptionValue(context.optionValue());

            return new AstOption(context, key, value);

        }

        public override AstBase VisitOptionValue([NotNull] ANTLRv4Parser.OptionValueContext context)
        {

            var i = context.identifier();
            if (i != null && i.Length > 0)
            {
                AstIdentifier j = null;
                foreach (var item in i)
                {
                    var k = (AstIdentifier)VisitIdentifier(item);
                    if (j == null)
                        j = k;
                    else
                        j.Add(k);
                }

                return j;

            }

            Pause();

            var l = context.STRING_LITERAL();
            if (l != null)
            {
                return (AstBase)VisitSTRING_LITERAL(l);
            }

            var m = context.INT();
            if (m != null)
            {
                VisitInt(m);

            }


            var g = context.actionBlock();
            return VisitActionBlock(g);

        }

        private AstIdentifier VisitSTRING_LITERAL(ITerminalNode l)
        {
            return VisitTerminalNode(l, "STRING_LITERAL");
        }

        private AstIdentifier VisitInt(ITerminalNode l)
        {
            return VisitTerminalNode(l, "INT");
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.Parsers.Antlr.ANTLRv4Parser.rules" />.
        /// ruleSpec*
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AstBase VisitRules([NotNull] ANTLRv4Parser.RulesContext context)
        {
            AstRulesList result = new AstRulesList(context);
            var items = context.ruleSpec();
            if (items != null)
                foreach (var item in items)
                    result.Add((AstRule)VisitRuleSpec(item));
            return result;
        }

        public override AstBase VisitRuleSpec([NotNull] ANTLRv4Parser.RuleSpecContext context)
        {
            var r = VisitParserRuleSpec(context.parserRuleSpec());
            return r;
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.Parsers.Antlr.ANTLRv4Parser.parserRuleSpec" />.
        /// ruleModifiers? RULE_REF argActionBlock? ruleReturns? throwsSpec? localsSpec? rulePrequel* COLON ruleBlock SEMI exceptionGroup
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AstBase VisitParserRuleSpec([NotNull] ANTLRv4Parser.ParserRuleSpecContext context)
        {

            AstIdentifier ruleName = VisitTerminalNode(context.RULE_REF(), "RULE_REF");
            AstRuleModifierList modifiers = (AstRuleModifierList)VisitRuleModifiers(context.ruleModifiers());

            AstPrequelList prequels = null;
            AstArgActionBlock rule = null;
            AstArgActionBlock returnRule = null;
            AstIdentifierList throwsSpec = null;
            AstArgActionBlock localSpec = null;
            AstRuleAltList ruleBlock = null;
            AstExceptionGroup exceptionGroup = null;

            var v2 = context.argActionBlock();
            if (v2 != null)
                rule = (AstArgActionBlock)VisitArgActionBlock(v2);

            var v3 = context.ruleReturns();
            if (v3 != null)
                returnRule = (AstArgActionBlock)VisitRuleReturns(v3);

            var v4 = context.throwsSpec();
            if (v4 != null)
                throwsSpec = (AstIdentifierList)VisitThrowsSpec(v4);

            var v5 = context.localsSpec();
            if (v5 != null)
                localSpec = (AstArgActionBlock)VisitLocalsSpec(v5);

            var v6 = context.ruleBlock();
            if (v6 != null)
                ruleBlock = (AstRuleAltList)VisitRuleBlock(v6);

            var v7 = context.rulePrequel();
            if (v7 != null && v7.Length > 0)
            {
                prequels = new AstPrequelList(context, v7.Length);
                foreach (var u7 in v7)
                    prequels.Add((AstPrequel)VisitRulePrequel(u7));
            }

            var v8 = context.exceptionGroup();
            if (v8 != null)
                exceptionGroup = (AstExceptionGroup)VisitExceptionGroup(v8);

            return new AstRule(context, modifiers, ruleName, rule, returnRule, throwsSpec, localSpec, prequels, ruleBlock, exceptionGroup);

        }


        /// <summary>
        /// Visits the action block.
        /// : BEGIN_ACTION ACTION_CONTENT* END_ACTION
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override AstBase VisitActionBlock([NotNull] ANTLRv4Parser.ActionBlockContext context)
        {
            if (context != null)
            {

                var l = context.ACTION_CONTENT();
                if (l != null && l.Length > 0)
                {

                    var block = new AstArgActionBlock(context, l.Length);

                    foreach (var item in l)
                        block.Add(VisitTerminalNode(item, "ACTION_CONTENT"));

                    return block; ;

                }

            }

            return null;


        }

        public override AstBase VisitArgActionBlock([NotNull] ANTLRv4Parser.ArgActionBlockContext context)
        {
            if (context != null)
            {

                var l = context.ARGUMENT_CONTENT();
                if (l != null && l.Length > 0)
                {

                    var block = new AstArgActionBlock(context, l.Length);

                    foreach (var item in l)
                        block.Add(VisitTerminalNode(item, "ARGUMENT_CONTENT"));

                    return block; ;

                }

            }

            return null;

        }

        /// <summary>
        /// Visits the rule returns.
        /// : RETURNS argActionBlock
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override AstBase VisitRuleReturns([NotNull] ANTLRv4Parser.RuleReturnsContext context)
        {
            if (context != null)
                return VisitArgActionBlock(context.argActionBlock());

            return null;

        }

        /// <summary>
        /// Visits the throws spec.
        /// : THROWS identifier (COMMA identifier)*
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override AstBase VisitThrowsSpec([NotNull] ANTLRv4Parser.ThrowsSpecContext context)
        {
            if (context != null)
            {

                var l = context.identifier();
                var list = new AstIdentifierList(context, l.Length);

                foreach (var item in l)
                    list.Add((AstIdentifier)VisitIdentifier(item));

                return list;

            }

            return null;

        }

        public override AstBase VisitActionScopeName([NotNull] ANTLRv4Parser.ActionScopeNameContext context)
        {

            Pause();
            return (AstBase)base.VisitActionScopeName(context);
        }

        /// <summary>
        /// Visits the exception group.
        /// : exceptionHandler* finallyClause?
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override AstBase VisitExceptionGroup([NotNull] ANTLRv4Parser.ExceptionGroupContext context)
        {

            var result = new AstExceptionGroup(context);

            if (context != null)
            {

                var e = context.exceptionHandler();
                if (e != null && e.Length > 0)
                    foreach (var item in e)
                        result.Add((AstExceptionHandler)VisitExceptionHandler(item));

                var f = context.finallyClause();
                if (f != null)
                    result.FinallyClause = (AstFinallyClause)VisitFinallyClause(f);

                return result;

            }

            return result;

        }

        /// <summary>
        /// Visits the locals spec.
        /// : LOCALS argActionBlock
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override AstBase VisitLocalsSpec([NotNull] ANTLRv4Parser.LocalsSpecContext context)
        {
            if (context != null)
                return VisitArgActionBlock(context.argActionBlock());

            return null;

        }

        public override AstBase VisitRuleBlock([NotNull] ANTLRv4Parser.RuleBlockContext context)
        {

            if (context != null)
                return VisitRuleAltList(context.ruleAltList());

            return null;

        }

        /// <summary>
        /// Visits the rule modifiers.
        /// : ruleModifier+
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override AstBase VisitRuleModifiers([NotNull] ANTLRv4Parser.RuleModifiersContext context)
        {

            if (context != null)
            {
                var l = context.ruleModifier();
                if (l != null && l.Length > 0)
                {
                    var list = new AstRuleModifierList(context, l.Length);
                    foreach (var item in l)
                        list.Add((AstRuleModifier)VisitRuleModifier(item));
                    return list;
                }
            }

            return null;

        }

        /// <summary>
        /// Visits the rule modifier.
        ///     : PUBLIC
        ///     | PRIVATE
        ///     | PROTECTED
        ///     | FRAGMENT
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override AstBase VisitRuleModifier([NotNull] ANTLRv4Parser.RuleModifierContext context)
        {

            if (context != null)
            {

                var txt = context.GetText().ToLower();

                switch (txt)
                {

                    case "private":
                        return new AstRuleModifier(context, RuleModifierEnum.Private);

                    case "protected":
                        return new AstRuleModifier(context, RuleModifierEnum.Protected);

                    case "fragment":
                        return new AstRuleModifier(context, RuleModifierEnum.Fragment);

                    case "public":
                    default:
                        break;

                }

            }

            return new AstRuleModifier(context, RuleModifierEnum.Public);

        }

        /// <summary>
        /// Visits the rule prequel.
        ///     : optionsSpec
        ///     | ruleAction
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override AstBase VisitRulePrequel([NotNull] ANTLRv4Parser.RulePrequelContext context)
        {
            if (context != null)
            {
                return new AstPrequel(context
                , (AstOptionList)VisitOptionsSpec(context.optionsSpec())
                    ?? VisitRuleAction(context.ruleAction())
                );
            }

            return null;

        }

        /// <summary>
        /// Visits the rule alt list.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override AstBase VisitRuleAltList([NotNull] ANTLRv4Parser.RuleAltListContext context)
        {

            if (context != null)
            {

                var list = context.labeledAlt();
                var l = new AstRuleAltList(context, list.Length);

                foreach (var item in list)
                    l.Add((AstLabeledAlt)VisitLabeledAlt(item));

                return l;

            }

            return null;

        }

        /// <summary>
        /// Visits the labeled alt.
        /// labeledAlt
        ///     : alternative(POUND identifier)?
        ///     ;
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override AstBase VisitLabeledAlt([NotNull] ANTLRv4Parser.LabeledAltContext context)
        {

            if (context != null)
            {

                var alt = (AstAlternative)VisitAlternative(context.alternative());

                if (context.POUND() != null)
                    return new AstLabeledAlt(context, alt, (AstIdentifier)VisitIdentifier(context.identifier()));

                return new AstLabeledAlt(context, alt);

            }

            return null;

        }

        /// <summary>
        /// Visits the alternative.
        /// </summary>
        /// alternative
        ///     : elementOptions? element+
        ///     |
        ///     // explicitly allow empty alts
        ///     ;
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override AstBase VisitAlternative([NotNull] ANTLRv4Parser.AlternativeContext context)
        {

            var opt = (AstElementOptionList)VisitElementOptions(context.elementOptions());

            var l1 = context.element();
            AstElementList list1 = new AstElementList(context, l1.Length);
            foreach (var item in l1)
                list1.Add(VisitElement(item));
            return new AstAlternative(context, list1, opt);

        }

        public override AstBase VisitAction_([NotNull] ANTLRv4Parser.Action_Context context)
        {

            Pause();
            return (AstBase)base.VisitAction_(context);
        }


        public override AstBase VisitAltList([NotNull] ANTLRv4Parser.AltListContext context)
        {

            if (context != null)
            {

                var l = new AstAlternativeList(context);
                var list = context.alternative();

                foreach (var item in list)
                    l.Add((AstAlternative)VisitAlternative(item));

                return l;

            }

            return null;

        }

        /// <summary>
        /// Visits the element.
        /// element
        ///     : labeledElement(ebnfSuffix |)
        ///     | atom(ebnfSuffix |)
        ///     | ebnf
        ///     | actionBlock QUESTION?
        ///     ;
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override AstBase VisitElement([NotNull] ANTLRv4Parser.ElementContext context)
        {

            var ebnf = (AstEbnfSuffix)VisitEbnfSuffix(context.ebnfSuffix());


            var result = (AstLabeledElement)VisitLabeledElement(context.labeledElement());
            if (result != null)
            {
                if (ebnf != null)
                    result.Occurence = ebnf.Occurence;
                return result;
            }

            var f = (AstAtom)VisitAtom(context.atom());
            if (f != null)
            {
                if (ebnf != null)
                    f.Occurence = ebnf.Occurence;
                return f;
            }

            var g = (AstBlock)VisitEbnf(context.ebnf());
            if (g != null)
                return g;

            var e = (AstArgActionBlock)VisitActionBlock(context.actionBlock());
            e.Occurence = context.QUESTION() != null ? OccurenceEnum.OneOptional : OccurenceEnum.One;

            return e;

        }

        public override AstBase VisitElementOptions([NotNull] ANTLRv4Parser.ElementOptionsContext context)
        {

            if (context != null)
            {
                var l = context.elementOption();
                AstElementOptionList list = new AstElementOptionList(context, l.Length);
                foreach (var item in l)
                    list.Add((AstElementOption)VisitElementOption(item));

                Pause();
                return list;
            }

            return null;

        }

        public override AstBase VisitElementOption([NotNull] ANTLRv4Parser.ElementOptionContext context)
        {

            Pause();

            AstElementOption r = null;

            var l = context.identifier();
            foreach (var item in l)
            {
                if (r == null)
                    r = new AstElementOption(context, (AstIdentifier)VisitIdentifier(item));
                else
                    r.Value = (AstBase)VisitIdentifier(item);
            }

            if (r.Value == null)
                r.Value = (AstBase)VisitSTRING_LITERAL(context.STRING_LITERAL());

            return r;

        }


        /// <summary>
        /// Visits the atom.
        /// atom
        ///     : terminal
        ///     | ruleref
        ///     | notSet
        ///     | DOT elementOptions?
        ///     ;
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override AstBase VisitAtom([NotNull] ANTLRv4Parser.AtomContext context)
        {

            if (context != null)
            {

                bool t = false;

                AstBase value = null;

                var t1 = context.terminal();
                if (t1 != null)
                    value = VisitTerminal(t1);

                else
                {

                    var t2 = context.ruleref();
                    if (t2 != null)
                        value = VisitRuleref(t2);

                    else
                    {
                        var t3 = context.notSet();
                        if (t3 != null)
                            value = VisitNotSet(t3);

                        else
                        {
                            t = context.DOT() != null;
                            if (t)
                            {
                                Pause();
                                var t4 = context.elementOptions();
                                value = VisitElementOptions(t4);
                            }
                        }
                    }

                }

                return new AstAtom(context, value) { Dot = t };

            }

            return null;

        }

        /// <summary>
        /// : LPAREN (optionsSpec? ruleAction* COLON)? altList RPAREN
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override AstBase VisitBlock([NotNull] ANTLRv4Parser.BlockContext context)
        {

            AstOptionList _a = null;
            var a = context.optionsSpec();
            if (a != null)
                _a = (AstOptionList)VisitOptionsSpec(a);

            AstRuleAction _b = null;
            var b = context.ruleAction();
            if (b != null && b.Length > 0)
                foreach (var item in b)
                    _b = (AstRuleAction)VisitRuleAction(item);

            AstAlternativeList _c = null;
            var c = context.altList();
            if (c != null)
                _c = (AstAlternativeList)VisitAltList(c);

            return new AstBlock(context, _a, _b, _c);

        }

        public override AstBase VisitBlockSet([NotNull] ANTLRv4Parser.BlockSetContext context)
        {

            Pause();
            return (AstBase)base.VisitBlockSet(context);
        }        

        public override AstBase VisitChannelsSpec([NotNull] ANTLRv4Parser.ChannelsSpecContext context)
        {

            Pause();
            return (AstBase)base.VisitChannelsSpec(context);
        }

        public override AstBase VisitCharacterRange([NotNull] ANTLRv4Parser.CharacterRangeContext context)
        {

            Pause();
            return (AstBase)base.VisitCharacterRange(context);
        }

        public override AstBase VisitDelegateGrammar([NotNull] ANTLRv4Parser.DelegateGrammarContext context)
        {

            Pause();
            return (AstBase)base.VisitDelegateGrammar(context);
        }

        public override AstBase VisitDelegateGrammars([NotNull] ANTLRv4Parser.DelegateGrammarsContext context)
        {

            Pause();
            return (AstBase)base.VisitDelegateGrammars(context);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.Parsers.Antlr.ANTLRv4Parser.ebnf" />.
        /// block blockSuffix?
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AstBase VisitEbnf([NotNull] ANTLRv4Parser.EbnfContext context)
        {

            var block = (AstBlock)VisitBlock(context.block());

            var e = VisitBlockSuffix(context.blockSuffix());
            
            if (e is AstEbnfSuffix bs)
                block.Occurence = bs.Occurence;
            return block;
        }

        public override AstBase VisitBlockSuffix([NotNull] ANTLRv4Parser.BlockSuffixContext context)
        {

            if (context != null)
                return base.VisitBlockSuffix(context);

            return null;

        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.Parsers.Antlr.ANTLRv4Parser.ebnfSuffix" />.
        /// ebnfSuffix
        ///     : QUESTION QUESTION?
        ///     | STAR QUESTION?
        ///     | PLUS QUESTION?
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AstBase VisitEbnfSuffix([NotNull] ANTLRv4Parser.EbnfSuffixContext context)
        {

            if (context != null)
            {

                var c = new AstEbnfSuffix(context);

                var o = context.QUESTION().Length;

                if (context.STAR() != null)
                {
                    if (o > 0)
                        c.Occurence = OccurenceEnum.AnyOptional;
                    else
                        c.Occurence = OccurenceEnum.Any;
                }
                else if (context.PLUS() != null)
                {
                    if (o > 0)
                        c.Occurence = OccurenceEnum.OneOrMoreOptional;
                    else
                        c.Occurence = OccurenceEnum.OneOrMore;

                }
                else
                {

                    c.Occurence = OccurenceEnum.OneOptional;

                }

                return c;

            }

            return null;

        }

        /// <summary>
        /// Visits the exception handler.
        /// : CATCH argActionBlock actionBlock
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override AstBase VisitExceptionHandler([NotNull] ANTLRv4Parser.ExceptionHandlerContext context)
        {

            if (context != null)
            {
                return new AstExceptionHandler(context
                    , (AstArgActionBlock)VisitArgActionBlock(context.argActionBlock())
                    , (AstActionBlock)VisitActionBlock(context.actionBlock())
                );

            }

            return null;

        }

        public override AstBase VisitFinallyClause([NotNull] ANTLRv4Parser.FinallyClauseContext context)
        {

            if (context != null)
                return new AstFinallyClause(context, (AstActionBlock)VisitActionBlock(context.actionBlock()));

            return null;

        }


        public override AstBase VisitIdList([NotNull] ANTLRv4Parser.IdListContext context)
        {

            Pause();
            return (AstBase)base.VisitIdList(context);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.Parsers.Antlr.ANTLRv4Parser.labeledElement" />.
        /// : identifier (ASSIGN | PLUS_ASSIGN) (atom | block)
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AstBase VisitLabeledElement([NotNull] ANTLRv4Parser.LabeledElementContext context)
        {
            if (context != null)
            {
                AstBase value = null;
                var atom = context.atom();
                if (atom != null)
                    value = VisitAtom(atom);
                else
                    value = VisitBlock(context.block());

                LabeledElementAssignEnum mode = LabeledElementAssignEnum.Assign;
                if (context.PLUS_ASSIGN() != null)
                    mode = LabeledElementAssignEnum.PlusAssign;

                return new AstLabeledElement(context, (AstIdentifier)VisitIdentifier(context.identifier()), mode, value);

            }

            return null;

        }

        public override AstBase VisitLabeledLexerElement([NotNull] ANTLRv4Parser.LabeledLexerElementContext context)
        {

            Pause();
            return (AstBase)base.VisitLabeledLexerElement(context);
        }

        public override AstBase VisitLexerAlt([NotNull] ANTLRv4Parser.LexerAltContext context)
        {

            Pause();
            return (AstBase)base.VisitLexerAlt(context);
        }

        public override AstBase VisitLexerAtom([NotNull] ANTLRv4Parser.LexerAtomContext context)
        {

            Pause();
            return (AstBase)base.VisitLexerAtom(context);
        }

        public override AstBase VisitLexerAltList([NotNull] ANTLRv4Parser.LexerAltListContext context)
        {

            Pause();
            return (AstBase)base.VisitLexerAltList(context);
        }

        public override AstBase VisitLexerBlock([NotNull] ANTLRv4Parser.LexerBlockContext context)
        {

            Pause();
            return (AstBase)base.VisitLexerBlock(context);
        }

        public override AstBase VisitLexerCommand([NotNull] ANTLRv4Parser.LexerCommandContext context)
        {

            Pause();
            return (AstBase)base.VisitLexerCommand(context);
        }

        public override AstBase VisitLexerCommandName([NotNull] ANTLRv4Parser.LexerCommandNameContext context)
        {

            Pause();
            return (AstBase)base.VisitLexerCommandName(context);
        }

        public override AstBase VisitLexerCommandExpr([NotNull] ANTLRv4Parser.LexerCommandExprContext context)
        {

            Pause();
            return (AstBase)base.VisitLexerCommandExpr(context);
        }

        public override AstBase VisitLexerCommands([NotNull] ANTLRv4Parser.LexerCommandsContext context)
        {

            Pause();
            return (AstBase)base.VisitLexerCommands(context);
        }

        public override AstBase VisitLexerElement([NotNull] ANTLRv4Parser.LexerElementContext context)
        {

            Pause();
            return (AstBase)base.VisitLexerElement(context);
        }

        public override AstBase VisitLexerElements([NotNull] ANTLRv4Parser.LexerElementsContext context)
        {

            Pause();
            return (AstBase)base.VisitLexerElements(context);
        }

        public override AstBase VisitLexerRuleBlock([NotNull] ANTLRv4Parser.LexerRuleBlockContext context)
        {

            Pause();
            return (AstBase)base.VisitLexerRuleBlock(context);
        }


        public override AstBase VisitLexerRuleSpec([NotNull] ANTLRv4Parser.LexerRuleSpecContext context)
        {

            Pause();
            return (AstBase)base.VisitLexerRuleSpec(context);
        }

        public override AstBase VisitModeSpec([NotNull] ANTLRv4Parser.ModeSpecContext context)
        {

            Pause();
            return (AstBase)base.VisitModeSpec(context);
        }

        public override AstBase VisitNotSet([NotNull] ANTLRv4Parser.NotSetContext context)
        {

            Pause();
            return (AstBase)base.VisitNotSet(context);
        }

        /// <summary>
        /// Visits the rule action.
        /// : AT identifier actionBlock
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override AstBase VisitRuleAction([NotNull] ANTLRv4Parser.RuleActionContext context)
        {

            Pause();

            if (context != null)
                return new AstRuleAction(
                      context
                    , (AstIdentifier)VisitIdentifier(context.identifier())
                    , (AstActionBlock)VisitActionBlock(context.actionBlock())
                );

            return null;

        }

        /// <summary>
        /// Visits the ruleref.
        /// : RULE_REF argActionBlock? elementOptions?
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override AstBase VisitRuleref([NotNull] ANTLRv4Parser.RulerefContext context)
        {

            if (context != null)
            {
                return new AstRuleRef(context, VisitTerminalNode(context.RULE_REF(), "RULE_REF"))
                {
                    Action = VisitArgActionBlock(context.argActionBlock()),
                    Option = VisitElementOptions(context.elementOptions())
                };
            }

            return null;

        }

        public override AstBase VisitSetElement([NotNull] ANTLRv4Parser.SetElementContext context)
        {

            Pause();
            return (AstBase)base.VisitSetElement(context);
        }

        /// <summary>
        /// Visits the terminal.
        /// terminal
        ///     : TOKEN_REF elementOptions?
        ///     | STRING_LITERAL elementOptions?
        ///     ;
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override AstBase VisitTerminal([NotNull] ANTLRv4Parser.TerminalContext context)
        {

            if (context != null)
            {

                AstIdentifier value = null;

                var v = context.TOKEN_REF();

                if (v != null)
                    value = VisitTerminalNode(v, "TOKEN_REF");

                else
                {
                    var v2 = context.STRING_LITERAL();
                    if (v2 != null)
                        value = VisitSTRING_LITERAL(v2);
                }

                AstElementOptionList options = null;
                var option = context.elementOptions();
                if (option != null)
                    options = (AstElementOptionList)VisitElementOptions(option);

                return new AstTerminal(context, value, options);

            }

            Pause();
            return (AstBase)base.VisitTerminal(context);
        }

        //public override AstBase VisitTerminal(ITerminalNode node)
        //{

        //    Pause();
        //    return (AstBase)base.VisitTerminal(node);
        //}

        public override AstBase VisitTokensSpec([NotNull] ANTLRv4Parser.TokensSpecContext context)
        {

            return (AstBase)base.VisitTokensSpec(context);
        }


        //protected override object AggregateResult(object aggregate, object nextResult)
        //{

        //    return (AstBase)base.AggregateResult(aggregate, nextResult);
        //}

        //public override bool Equals(object? obj)
        //{

        //    return (AstBase)base.Equals(obj);
        //}

        //protected override object DefaultResult => base.DefaultResult;

        //public override int GetHashCode()
        //{

        //    return (AstBase)base.GetHashCode();
        //}

        //protected override bool ShouldVisitNextChild(IRuleNode node, object currentResult)
        //{

        //    return (AstBase)base.ShouldVisitNextChild(node, currentResult);
        //}

        //public override string ToString()
        //{

        //    return (AstBase)base.ToString();
        //}

        //public override AstBase VisitChildren(IRuleNode node)
        //{

        //    return (AstBase)base.VisitChildren(node);
        //}

        public override AstBase VisitErrorNode(IErrorNode node)
        {

            Pause();
            return (AstBase)base.VisitErrorNode(node);
        }

    }

}


