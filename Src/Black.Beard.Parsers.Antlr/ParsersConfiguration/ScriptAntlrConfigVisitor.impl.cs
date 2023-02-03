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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace Bb.Parsers
{


    public partial class ScriptAntlrConfigVisitor
    {


        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.ParserConfigurations.Antlr.AntlrConfigParser.grammarSpec" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// grammarSpec
        ///     : default_values? grammarDeclaration* EOF
        ///     ;
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AntlrConfigAstBase VisitGrammarSpec([NotNull] AntlrConfigParser.GrammarSpecContext context)
        {

            var result = new GrammarSpec(context);

            var items = context.grammarDeclaration();
            foreach (var item in items)
                result.Add((GrammarConfigDeclaration)item.Accept(this));


            var defaultValues = (GrammarSpecDefaultValues)context.default_values()?.Accept(this);
            if (defaultValues != null)
                result.Defaults = defaultValues;

            return result;

        }


        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.ParserConfigurations.Antlr.AntlrConfigParser.default_values" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// default_values
        ///     : WITH DEFAULT default_value_items+ SEMI
        ///     ;
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AntlrConfigAstBase VisitDefault_values([NotNull] AntlrConfigParser.Default_valuesContext context)
        {

            var result = new GrammarSpecDefaultValues(context);

            var items = context.default_value_item();
            foreach (var item in items)
                result.Add((GrammarSpecDefaultValue)item.Accept(this));

            return result;

        }


        /// <summary>
        /// Visits the default value item.
        /// </summary>
        /// default_value_item
        ///     : identifier COLON constant
        ///     ;
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override AntlrConfigAstBase VisitDefault_value_item([NotNull] AntlrConfigParser.Default_value_itemContext context)
        {
            var key = (IdentifierConfig)context.identifier().Accept(this);
            var value = context.constant().Accept(this);
            if (value is IdentifierConfig c)
                return new GrammarSpecDefaultValue(context, key, c.Text);

            return new GrammarSpecDefaultValue(context, key, (value as ConstantConfig).Text);

        }


        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.ParserConfigurations.Antlr.AntlrConfigParser.grammarDeclaration" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// grammarDeclaration
        ///     : RULE identifier COLON ruleConfig SEMI
        ///     ;
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AntlrConfigAstBase VisitGrammarDeclaration([NotNull] AntlrConfigParser.GrammarDeclarationContext context)
        {

            var name = (IdentifierConfig)context.identifier().Accept(this);
            var template = (GrammarRuleConfig)context.ruleConfig().Accept(this);

            return new GrammarConfigDeclaration(context, name.Text, template);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.ParserConfigurations.Antlr.AntlrConfigParser.template" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// ruleConfig
        ///     : NO? GENERATE
        ///       TEMPLATE COLON identifier
        ///       (CALCULATED TEMPLATE COLON identifier)?
        ///     ;
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AntlrConfigAstBase VisitRuleConfig([NotNull] AntlrConfigParser.RuleConfigContext context)
        {

            var generate = context.NO() == null;
            var templateIdentifier = (IdentifierConfig)context.templateIdentifier.Accept(this);
                        
            return new GrammarRuleConfig(context, generate, templateIdentifier.Text);

        }


        public override AntlrConfigAstBase VisitTerminal(ITerminalNode node)
        {
            return base.VisitTerminal(node);
        }
        
        public override AntlrConfigAstBase VisitIdentifier([NotNull] AntlrConfigParser.IdentifierContext context)
        {
            return new IdentifierConfig(context);
        }

        public override AntlrConfigAstBase VisitConstant([NotNull] AntlrConfigParser.ConstantContext context)
        {

            var result = context.identifier().Accept(this);
            if (result != null)
                return result;

            return new ConstantConfig(context.STRING_LITERAL());

        }




    }

}


