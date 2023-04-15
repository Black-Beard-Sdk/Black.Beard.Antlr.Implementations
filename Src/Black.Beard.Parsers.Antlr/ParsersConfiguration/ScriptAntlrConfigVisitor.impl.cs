using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Bb.Asts;
using Bb.ParserConfigurations.Antlr;
using Bb.ParsersConfiguration.Ast;
using System.Collections.Generic;
using System.Data;
using static Antlr4.Runtime.Atn.SemanticContext;

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
        ///     : grammar_spec_list*
        ///       EOF
        ///     ;
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AntlrConfigAstBase VisitGrammar_spec([NotNull] AntlrConfigParser.Grammar_specContext context)
        {

            var result = new GrammarSpec(context);

            var items = context.grammar_spec_list();
            foreach (var item in items)
            {
                var t = item.Accept(this);

                if (t is DefaultTemplateSetting t1)
                {
                    if (result.Defaults == null)
                        result.Defaults = t1;

                    else
                        result.Defaults.Merge(t1);
                }
                else if (t is TemplateSelector t2)
                    result.Add(t2);

                else if (t is GrammarConfigDeclaration t3)
                    result.Add(t3);

                else if (t is ListDeclaration t4)
                    result.Add(t4);

                else if (t is GrammarConfigTermDeclaration t5)
                    result.Add(t5);

                else
                {
                    Pause();
                }

            }

            result.Prepare();

            return result;

        }


        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.ParserConfigurations.Antlr.AntlrConfigParser.template_selector" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// template_selector
        ///     : SELECT template_setting WHEN template_selector_expression SEMI
        ///     ;
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AntlrConfigAstBase VisitTemplate_selector([NotNull] AntlrConfigParser.Template_selectorContext context)
        {

            var result = new TemplateSelector(context)
            {
                Settings = (TemplateSetting)context.template_setting().Accept(this),
                SelectorExpression = (TemplateSelectorExpression)context.template_selector_expression().Accept(this)
            };

            return result;

        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.ParserConfigurations.Antlr.AntlrConfigParser.template_setting" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// : TEMPLATE identifier COLON additional_settings?
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AntlrConfigAstBase VisitTemplate_setting([NotNull] AntlrConfigParser.Template_settingContext context)
        {
            var result = new TemplateSetting(context, (context.identifier().Accept(this) as RuleTuneInherit).Text);
            result.AddtionnalSettings = (AdditionalValues)context.additional_settings()?.Accept(this);
            return result;
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.ParserConfigurations.Antlr.AntlrConfigParser.optional_template_setting" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// TEMPLATE identifier? additional_settings?
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AntlrConfigAstBase VisitOptional_template_setting([NotNull] AntlrConfigParser.Optional_template_settingContext context)
        {
            var v = (context.identifier()?.Accept(this) as RuleTuneInherit);
            var result = new TemplateSetting(context, v?.Text);
            result.AddtionnalSettings = (AdditionalValues)context.additional_settings()?.Accept(this);
            return result;
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.ParserConfigurations.Antlr.AntlrConfigParser.additional_settings" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// additional_settings
        ///     : LPAREN default_value_item* RPAREN
        ///     ;
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AntlrConfigAstBase VisitAdditional_settings([NotNull] AntlrConfigParser.Additional_settingsContext context)
        {

            var result = new AdditionalValues(context);

            var default_value_item = context.value_item();
            if (default_value_item != null)
                foreach (var item in default_value_item)
                    result.Add((AdditionalValue)item.Accept(this));

            return base.VisitAdditional_settings(context);
        }


        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.ParserConfigurations.Antlr.AntlrConfigParser.template_selector_expression" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// template_selector_expression
        ///     : template_selector_expression_item((OR|AND) template_selector_expression_item)*
        ///     ;
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AntlrConfigAstBase VisitTemplate_selector_expression([NotNull] AntlrConfigParser.Template_selector_expressionContext context)
        {

            var result = new TemplateSelectorExpression(context);
            var item1 = (SelectorExpressionItem)context.template_selector_expression_item().Accept(this);
            var item2 = (SelectorExpressionItem)context.template_selector_expression()?.Accept(this);

            if (item2 != null)
            {
                result.Filter = new SelectorExpressionItemBinary(context)
                {

                    Left = item1,

                    Operator = (context.OR() != null)
                       ? SelectorExpressionOperationEnum.Or
                       : SelectorExpressionOperationEnum.And,

                    Right = item2

                };

            }
            else
                result.Filter = item1;

            return result;

        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.ParserConfigurations.Antlr.AntlrConfigParser.item_list" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// : LIST identifier COLON identifier+ SEMI
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AntlrConfigAstBase VisitItem_list([NotNull] AntlrConfigParser.Item_listContext context)
        {

            var items = context.identifier();

            ListDeclaration result = null;
            foreach (var item in items)
            {
                if (result == null)
                    result = new ListDeclaration(context) { ListName = item.GetText() };
                else
                    result.Add(item.GetText());
            }

            return result;

        }


        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.ParserConfigurations.Antlr.AntlrConfigParser.template_selector_expression_item_1" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// template_selector_expression_item_has
        ///     : RULE HAS (ONE|ONLY|ANY|NO|MANY)   (BLOCK|RULE|TERM|ALTERNATIVE)
        ///     | RULE HAS  ONE OUTPUT              (BLOCK|RULE|TERM|ALTERNATIVE)?
        ///     ;
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AntlrConfigAstBase VisitTemplate_selector_expression_item_has([NotNull] AntlrConfigParser.Template_selector_expression_item_hasContext context)
        {

            var result = new SelectorExpressionItemHas(context);

            if (context.ONE() != null)
            {
                result.FilterCount = FilterExpressionEnum.One;
                result.IsOutput = context.OUTPUT() != null;
            }



            else if (context.ONLY() != null)
                result.FilterCount = FilterExpressionEnum.Only;

            else if (context.ANY() != null)
                result.FilterCount = FilterExpressionEnum.Any;

            else if (context.NO() != null)
                result.FilterCount = FilterExpressionEnum.No;

            else if (context.MANY() != null)
                result.FilterCount = FilterExpressionEnum.Many;



            if (context.BLOCK() != null)
                result.Target = FilterExpressionTargetEnum.Block;

            else if (context.RULE().Length == 2)
                result.Target = FilterExpressionTargetEnum.Rule;

            else if (context.TERM() != null)
                result.Target = FilterExpressionTargetEnum.Term;

            else if (context.ALTERNATIVE() != null)
                result.Target = FilterExpressionTargetEnum.Alternative;



            return result;

        }


        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.ParserConfigurations.Antlr.AntlrConfigParser.template_selector_expression_item_2" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// template_selector_expression_item_is
        ///     : RULE IS NOT? (BLOCK|RULE|TERM|ALTERNATIVE)+
        ///     | RULE IS NOT? IN identifier
        ///     ;
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AntlrConfigAstBase VisitTemplate_selector_expression_item_is([NotNull] AntlrConfigParser.Template_selector_expression_item_isContext context)
        {


            if (context.IN() != null)
            {

                var result1 = new SelectorExpressionItemIsIn(context)
                {
                    IsNot = context.NOT() != null,
                    ListName = context.identifier()?.GetText() ?? string.Empty,
                };

                return result1;

            }


            var result = new SelectorExpressionItemIs(context)
            {
                IsNot = context.NOT() != null,
                ListName = context.identifier()?.GetText() ?? string.Empty,
            };


            if (context.BLOCK() != null && context.BLOCK().Length > 0)
                result.Add(FilterExpressionTargetEnum.Block);

            if (context.RULE() != null && context.RULE().Length > 0)
                result.Add(FilterExpressionTargetEnum.Rule);

            if (context.TERM() != null && context.TERM().Length > 0)
                result.Add(FilterExpressionTargetEnum.Term);

            if (context.ALTERNATIVE() != null && context.ALTERNATIVE().Length > 0)
                result.Add(FilterExpressionTargetEnum.Alternative);

            return result;

        }


        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.ParserConfigurations.Antlr.AntlrConfigParser.default_values" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// default_values
        ///     : DEFAULT template_setting SEMI
        ///     ;
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AntlrConfigAstBase VisitDefault([NotNull] AntlrConfigParser.DefaultContext context)
        {
            return new DefaultTemplateSetting(context, (TemplateSetting)context.template_setting().Accept(this));
        }


        /// <summary>
        /// Visits the default value item.
        /// </summary>
        /// default_value_item
        ///     : identifier COLON constant
        ///     ;
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override AntlrConfigAstBase VisitValue_item([NotNull] AntlrConfigParser.Value_itemContext context)
        {
            var i = context.identifier();
            var key = (RuleTuneInherit)i[0].Accept(this);
            var value = (RuleTuneInherit)i[1].Accept(this);
            if (value is RuleTuneInherit c)
                return new AdditionalValue(context, key, c);

            return new AdditionalValue(context, key, value);

        }


        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.ParserConfigurations.Antlr.AntlrConfigParser.grammarDeclaration" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// grammarDeclaration
        ///     : RULE identifier COLON ruleConfig SEMI
        ///     | TERM identifier ruleTermConfig SEMI
        ///     ;
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>>	Generate.dll!Generate.Scripts.ScriptClassDefaults.ConfigureTemplate.AnonymousMethod__14(System.CodeDom.CodeTypeDeclaration t) Ligne 135	C#

        public override AntlrConfigAstBase VisitGrammar_declaration([NotNull] AntlrConfigParser.Grammar_declarationContext context)
        {

            var name = (RuleTuneInherit)context.identifier().Accept(this);

            if (context.RULE() != null)
            {
                var template = (GrammarRuleConfig)context.ruleConfig().Accept(this);
                return new GrammarConfigDeclaration(context, name, template);
            }

            var config = (GrammarRuleTermConfig)context.ruleTermConfig().Accept(this);
            return new GrammarConfigTermDeclaration(context, name, config);

        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.ParserConfigurations.Antlr.AntlrConfigParser.ruleTermConfig" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// ruleTermConfig
        ///   : KIND termKindEnum identifier?
        ///   ;
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AntlrConfigAstBase VisitRuleTermConfig([NotNull] AntlrConfigParser.RuleTermConfigContext context)
        {

            var extendedData = (context.identifier()?.Accept(this) as RuleTuneInherit);

            TokenTypeEnum v = TokenTypeEnum.Other; 

            var e = context.termKindEnum();
            if (e.OTHER_() != null)
                v = TokenTypeEnum.Other;

            else if (e.CONSTANT_() != null)
                v = TokenTypeEnum.Constant;

            else if (e.IDENTIFIER_() != null)
                v = TokenTypeEnum.Identifier;

            else if (e.COMMENT_() != null)
                v = TokenTypeEnum.Comment;

            else if (e.BOOLEAN_() != null)
                v = TokenTypeEnum.Boolean;

            else if (e.STRING_() != null)
                v = TokenTypeEnum.String;

            else if (e.DECIMAL_() != null)
                v = TokenTypeEnum.Decimal;

            else if (e.INTEGER_() != null)
                v = TokenTypeEnum.Int;

            else if (e.REAL_() != null)
                v = TokenTypeEnum.Real;

            else if (e.HEXA_() != null)
                v = TokenTypeEnum.Hexa;

            else if (e.BINARY_() != null)
                v = TokenTypeEnum.Binary;

            else if (e.PATTERN_() != null)
                v = TokenTypeEnum.Pattern;

            else if (e.OPERATOR_() != null)
                v = TokenTypeEnum.Operator;

            else if (e.PONCTUATION_() != null)
                v = TokenTypeEnum.Ponctuation;

            return new GrammarRuleTermConfig(context, v, extendedData);
        
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.ParserConfigurations.Antlr.AntlrConfigParser.template" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// ruleConfig
        ///     : NO? GENERATE
        ///       rule_tune_inherit?
        ///       calculated_rule_tune_inherit?
        ///       template_setting
        ///       calculated_template_setting?
        ///     ;
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AntlrConfigAstBase VisitRuleConfig([NotNull] AntlrConfigParser.RuleConfigContext context)
        {

            var generate = context.NO() == null;

            var rule_tune_inherit = (RuleTuneInherit)context.rule_tune_inherit()?.Accept(this);
            var calculated_rule_tune_inherit = (CalculatedRuleTuneInherit)context.calculated_rule_tune_inherit()?.Accept(this);

            var templateSetting = (TemplateSetting)context.optional_template_setting()?.Accept(this);
            var calculatedTemplateSetting = (CalculatedTemplateSetting)context.calculated_template_setting()?.Accept(this);

            return new GrammarRuleConfig(context, generate, rule_tune_inherit, templateSetting, calculatedTemplateSetting, calculated_rule_tune_inherit);

        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.ParserConfigurations.Antlr.AntlrConfigParser.calculated_rule_tune_inherit" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AntlrConfigAstBase VisitCalculated_rule_tune_inherit([NotNull] AntlrConfigParser.Calculated_rule_tune_inheritContext context)
        {
            if (context != null)
            {

                var rule_tune_inherit = (RuleTuneInherit)context.rule_tune_inherit()?.Accept(this);
                    return new CalculatedRuleTuneInherit(context, rule_tune_inherit);
            }

            return null;
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="M:Bb.ParserConfigurations.Antlr.AntlrConfigParser.rule_tune_inherit" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="M:Antlr4.Runtime.Tree.AbstractParseTreeVisitor`1.VisitChildren(Antlr4.Runtime.Tree.IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// rule_tune_inherit : INHERIT STRING_LITERAL?
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override AntlrConfigAstBase VisitRule_tune_inherit([NotNull] AntlrConfigParser.Rule_tune_inheritContext context)
        {

            if (context != null)
            {
                var constant = context.STRING_LITERAL();
                if (constant != null)
                    return new RuleTuneInherit(constant);
            }

            return null;

        }

        public override AntlrConfigAstBase VisitCalculated_template_setting([NotNull] AntlrConfigParser.Calculated_template_settingContext context)
        {
            return new CalculatedTemplateSetting(context, (TemplateSetting)context.optional_template_setting()?.Accept(this));
        }

        public override AntlrConfigAstBase VisitTerminal(ITerminalNode node)
        {
            return base.VisitTerminal(node);
        }

        public override AntlrConfigAstBase VisitIdentifier([NotNull] AntlrConfigParser.IdentifierContext context)
        {
            return new RuleTuneInherit(context);
        }

    }

}


