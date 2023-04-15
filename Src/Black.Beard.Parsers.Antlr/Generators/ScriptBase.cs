using Bb.Asts;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using Microsoft.VisualBasic;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Generators
{


    public abstract class ScriptBase
    {

        public ScriptBase()
        {
            this.Usings = new HashSet<string>();
        }


        protected string GetInherit_Impl(string classBaseName, AstRule ast, Context context)
        {

            var config = ast.Configuration.Config;

            config.CalculatedInherit = new CalculatedRuleTuneInherit("\"" + classBaseName + "\"");

            if (config.Inherit != null && !string.IsNullOrEmpty(config.Inherit.Text))
            {
                if (config.Inherit.Text != config.CalculatedInherit.RuleTuneInherit.Text)
                    return config.Inherit.Text;

                config.Inherit = null;

            }

            return classBaseName;

        }


        public bool SplitObjectOnDisk { get; set; }

        public string Namespace { get; set; }

        public abstract string StrategyTemplateKey { get; }

        public string Name
        {
            get
            {
                return Filename;
            }
        }

        public HashSet<string> Usings { get; set; }

        public string Filename { get; set; }

        public GrammarSpec Configuration { get; set; }

        public static string DefaultFilename(Type item)
        {
            var c = "ScriptClass";
            var _defaultFilename = item.Name;
            if (_defaultFilename.StartsWith(c))
                _defaultFilename = _defaultFilename.Substring(c.Length);
            return _defaultFilename;

        }

        public static string DefaultFilename<T>()
            where T : ScriptBase
        {
            return DefaultFilename(typeof(T));
        }



        public abstract string GetInherit(AstRule ast, Context context);

        public IEnumerable<string> Generate(Context context)
        {
            var visitor = new CodeGeneratorVisitor(context, this);
            ConfigureTemplate(context, visitor);
            return visitor.Visit(context.RootAst);
        }

        protected abstract void ConfigureTemplate(Context context, CodeGeneratorVisitor generator);

        protected string TemplateSelector(AstRule ast, Context context)
        {

            var result = _templateSelector(ast, context);

            if (!context.StrategyKeyExists(result))
                Console.WriteLine($"{ast.Name.Text} : {result} strategy is not found");

            return result;

        }              

        private string _templateSelector(AstRule ast, Context context)
        {

            var c1 = ast.Configuration.Config;

            var t = c1.TemplateSetting.TemplateName;
            if (t != null)
                return t;

            if (!string.IsNullOrEmpty(ast.Strategy))
                return ast.Strategy;

            var conf = ast.Configuration.Config;


            var txt = Configuration.Evaluate(ast);
            if (!string.IsNullOrEmpty(txt))
            {
                conf.CalculatedTemplateSetting = new CalculatedTemplateSetting
                (
                    Position.Default,
                    new TemplateSetting(Position.Default, txt)
                );
            }
            else
            {
                conf.CalculatedTemplateSetting = new CalculatedTemplateSetting
                (
                    Position.Default,
                    new TemplateSetting(Position.Default, TemplateSelectorCompute(ast, context))
                );
            }

            if (conf.TemplateSetting != null && !string.IsNullOrEmpty(conf.TemplateSetting.TemplateName))
                ast.Strategy = conf.TemplateSetting.TemplateName;
            else
                ast.Strategy = conf.CalculatedTemplateSetting.Setting.TemplateName;

            return ast.Strategy;

        }

        private static string TemplateSelectorCompute(AstRule ast, Context context)
        {

            /*
            
                a:rule ('COMMA' {a}).any
                a:rule {a}.any
             
             */

            if (ast.Alternatives.Count == 1)
            {

                var r = ast.Alternatives[0].Rule.Rule;
                if (r.Count == 1)
                {
                    var o = r[0];
                    switch (o.Type)
                    {

                        case "AstAtom":

                            var oc = o.ResolveOccurence();

                            var i = o as AstAtom;
                            var p = i.Occurence;
                            switch (p.Value)
                            {
                                case OccurenceEnum.Any:
                                    return "ClassList";

                                case OccurenceEnum.One:
                                    if (i.IsTerminal)
                                        return "ClassTerminalAlias";
                                    break;

                                default:
                                    break;
                            }

                            break;

                        case "AstlabeledElement":
                            break;

                        case "AstBlock":
                            break;

                        case "AstArgActionBlock":
                            break;

                        default:
                            break;
                    }

                }
                if (r.Count == 2)
                {

                    var items = r.GetAllItems().ToList();
                    var rules = items.Where(c => c.Type == "AstRuleRef").ToList();
                    if (rules.Count == 2)
                    {
                        if (rules[0].ResolveName() == rules[1].ResolveName())
                        {
                            var parent = rules[1].Parent as AstAtom;
                            if (parent.Occurence.Value == OccurenceEnum.Any)
                                return "ClassList";
                            else
                            {
                                var parent2 = parent.Ancestor<AstBlock>();
                                if (parent2?.Occurence.Value == OccurenceEnum.Any)
                                    return "ClassList";

                            }
                        }

                    }
                    
                }
                else
                {

                }

            }

            if (ast.ContainsJustOneAlternative)
            {

                var itemRules = ast.GetRules().GroupBy(c => c.ResolveName()).ToList();
                if (itemRules.Count == 1)
                {

                    var itemTerms = ast.GetTerminals().GroupBy(c => c.ResolveName()).ToList();

                    if (itemTerms.Count == 0)
                    {

                        var i = itemRules[0].ToList();

                        if (i.Count == 1)
                            if (i[0].ResolveOccurence() <= (int)OccurenceEnum.One)
                                if (i[0].ResolveName() == "id_")
                                    return "ClassTerminalAlias";

                        if (i.Count > 1)
                            return "ClassList";

                        foreach (var item in i)
                            if (item.ResolveOccurence() >= (int)OccurenceEnum.Any)
                                return "ClassList";

                    }
                    else if (itemTerms.Count == 1)
                    {
                        var o = itemTerms[0].First();
                        var splitChar = new HashSet<string>() { "COMMA" };
                        if (splitChar.Contains(o.ResolveName()))
                        {
                            var oc = ast.GetRules().First().ResolveOccurence();
                            if (oc > (int)OccurenceEnum.One)
                                return "ClassList";
                        }
                    }
                    else
                    {

                    }
                }

                foreach (var item in ast.GetListAlternatives())
                    if (item.Where(c => c.IsRule).Any())
                        return "ClassWithProperties";

            }

            return "_";

        }

        protected virtual bool Generate(AstRule ast, Context context)
        {
            return ast.Configuration.Config.Generate
                && TemplateSelector(ast, context) == this.StrategyTemplateKey;
        }

        public void Using(params string[] usings)
        {
            foreach (var us in usings)
                this.Usings.Add(us);
        }

    }

}
