using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using System.CodeDom;
using System.Text;

namespace Generate.Scripts
{
    public class ScriptClassWithProperties : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {
            var config = ast.Configuration.Config;

            if (config.Inherit == null)
                config.Inherit = new IdentifierConfig("\"AstRule\"");

            return config.Inherit.Text;

        }

        public override string StrategyTemplateKey => "ClassWithProperties";


        protected override void ConfigureTemplate(Context ctx, CodeGeneratorVisitor generator)
        {

            generator.Add(this.Name, template =>
            {

                template.Namespace(Namespace, ns =>
                {
                    ns.Using(Usings)
                      .Using("Antlr4.Runtime")
                      .Using("System.Collections")
                      .Using("Antlr4.Runtime.Tree")
                      .Using("Bb.Parsers")

                      .CreateTypeFrom<AstRule>((ast, type) =>
                      {

                          type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                              .GenerateIf(() => Generate(ast, ctx))
                              .Documentation(c => c.Summary(() => ast.ToString()))
                              .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                              .Inherit(() => GetInherit(ast, ctx))



                              .Ctor((f) =>
                              {
                                  f.Argument(() => "Position", "p")
                                   .Argument(() => "List<AstRoot>", "list")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("p", "list")
                                   .Body(b =>
                                   {

                                       var items = GetProperties(ast);

                                       b.Statements.ForEach("AstRoot".AsType(), "item", "list", stm =>
                                       {

                                           foreach (var item in items)
                                           {

                                               var fieldName = GetFieldName(item as AstBase);

                                               var type = GetType(item as AstBase);
                                               var ty = new CodeTypeReference(type);
                                               stm.If(CodeHelper.Var("enumerator.Current").Call("Is", new CodeTypeReference[] { ty }), t =>
                                               {
                                                   t.Assign(CodeHelper.This().Field(fieldName), CodeHelper.Var("enumerator.Current").Cast(ty));
                                               });

                                           }

                                       });

                                   })
                                   ;
                              })

                              .Ctor((f) =>
                              {
                                  f.Argument(() => "Position", "p")
                                   .Arguments(() => GetProperties(ast), c =>
                                   {
                                       return (GetType(c as AstBase), GetParameterdName(c as AstBase));

                                   })
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("p", "null")
                                   .Body(b =>
                                   {
                                       var items = GetProperties(ast);
                                       foreach (var item in items)
                                       {
                                           var aa = item as AstBase;
                                           b.Statements.Assign(CodeHelper.This().Field(GetFieldName(aa)), CodeHelper.Var(GetParameterdName(aa)));
                                       }

                                   })
                                   ;
                              })


                              .Ctor((f) =>
                              {
                                  f.Argument(() => "ParserRuleContext", "ctx")
                                   .Argument(() => "List<AstRoot>", "list")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("ctx", "list")
                                   .Body(b =>
                                   {

                                       var items = GetProperties(ast);

                                       b.Statements.ForEach("AstRoot".AsType(), "item", "list", stm =>
                                       {

                                           foreach (var item in items)
                                           {

                                               var fieldName = GetFieldName(item as AstBase);

                                               var type = GetType(item as AstBase);
                                               var ty = new CodeTypeReference(type);
                                               stm.If(CodeHelper.Var("enumerator.Current").Call("Is", new CodeTypeReference[] { ty }), t =>
                                               {
                                                   t.Assign(CodeHelper.This().Field(fieldName), CodeHelper.Var("enumerator.Current").Cast(ty));
                                               });


                                           }

                                       });

                                   })
                                   ;
                              })


                              .Method(method =>
                              {
                                  method
                                   .Name(g => "Accept")
                                   .Argument("IAstTSqlVisitor", "visitor")
                                   .Attribute(MemberAttributes.Override | MemberAttributes.Public)
                                   .Body(b =>
                                   {
                                       b.Statements.Call
                                       (
                                           CodeHelper.Var("visitor"),
                                           "Visit" + CodeHelper.FormatCsharp(ast.Name.Text),
                                           CodeHelper.This()
                                       );
                                   });
                              })

                              .Properties(
                                  () => GetProperties(ast),
                                  (property, model) =>
                                  {
                                      property.Name(m => GetPropertyName(model as AstBase))
                                      .Type(() => GetType(model as AstBase))
                                      .Attribute(MemberAttributes.Public)
                                      .Get((stm) =>
                                      {

                                          stm.Return(CodeHelper.This().Field(GetFieldName(model as AstBase)));

                                      })
                                      .HasSet(false)
                                      ;
                                  }
                              )

                              .Fields(() => GetProperties(ast),
                                  (field, model) =>
                                  {
                                      field.Name(m => GetFieldName(m as AstBase))
                                      .Type(() => GetType(model as AstBase))
                                      .Attribute(MemberAttributes.Private)
                                      ;
                                  }
                              )

                              .Make(t =>
                              {

                                  if (ast.ResolveName() == "create_resource_pool")
                                  {

                                  }

                                  var r = ast.Root();

                                  HashSet<string> _h = new HashSet<string>();
                                  List<CodeMemberMethod> methods = new List<CodeMemberMethod>();

                                  foreach (AstLabeledAlt alternative in ast.Alternatives)
                                  {

                                      var allCombinations = alternative.ResolveAllCombinations();

                                      foreach (TreeRuleItem alt in allCombinations)
                                      {

                                          StringBuilder uniqeConstraintKeyMethod = new StringBuilder();
                                          var t1 = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text)).AsType();
                                          List<string> arguments = new List<string>();

                                          var method = new CodeMemberMethod()
                                          {
                                              Name = CodeHelper.FormatCsharp(ast.Name.Text),
                                              ReturnType = t1,
                                              Attributes = MemberAttributes.Public | MemberAttributes.Static,
                                          };

                                          method.Comments.Add(new CodeCommentStatement("<summary>", true));
                                          method.Comments.Add(new CodeCommentStatement($"{ast.Name} : ", true));
                                          method.Comments.Add(new CodeCommentStatement(alt.GenerateDoc(ctx), true));
                                          method.Comments.Add(new CodeCommentStatement("</summary>", true));

                                          Action<TreeRuleItem> act = itemAst =>
                                          {

                                              string name = null;
                                              string varName = null;

                                              var itemResult = ast.ResolveByName(itemAst.ResolveKey());
                                              if (itemResult != null && itemResult is AstRule r1 && r1?.Configuration != null)
                                              {

                                                  name = "Ast" + CodeHelper.FormatCsharp(itemAst.Name);

                                                  if (string.IsNullOrEmpty(itemAst.Label))
                                                      varName = CodeHelper.FormatCsharpArgument(itemAst.Name);
                                                  else
                                                      varName = CodeHelper.FormatCsharpArgument(itemAst.Label);

                                              }
                                              else if (itemResult != null && itemResult is AstLexerRule r2)
                                              {

                                                  switch (r2.Configuration.Config.Kind)
                                                  {
                                                      case TokenTypeEnum.Pattern:
                                                      case TokenTypeEnum.String:
                                                      case TokenTypeEnum.Identifier:
                                                          name = nameof(String);
                                                          varName = "txt";
                                                          break;
                                                      case TokenTypeEnum.Boolean:
                                                          name = nameof(Boolean);
                                                          varName = "boolean";
                                                          break;
                                                      case TokenTypeEnum.Decimal:
                                                          name = nameof(Decimal);
                                                          varName = "_decimal";
                                                          break;
                                                      case TokenTypeEnum.Int:
                                                          name = nameof(Int64);
                                                          varName = "integer";
                                                          break;
                                                      case TokenTypeEnum.Real:
                                                          name = nameof(Double);
                                                          varName = "real";
                                                          break;
                                                      case TokenTypeEnum.Hexa:
                                                          name = "";
                                                          varName = "";
                                                          break;
                                                      case TokenTypeEnum.Binary:
                                                          name = "Object";
                                                          varName = "_binary";
                                                          break;

                                                      case TokenTypeEnum.Operator:
                                                      case TokenTypeEnum.Ponctuation:
                                                      case TokenTypeEnum.Other:
                                                      case TokenTypeEnum.Comment:
                                                      case TokenTypeEnum.Constant:
                                                      default:
                                                          break;
                                                  }

                                                  if (!string.IsNullOrEmpty(itemAst.Label))
                                                      varName = CodeHelper.FormatCsharpArgument(itemAst.Label);

                                              }

                                              if (name != null)
                                              {

                                                  CodeTypeReference argumentTypeName = null;

                                                  if (itemAst.Occurence.Value == OccurenceEnum.Any)
                                                  {
                                                      argumentTypeName = new CodeTypeReference(name);
                                                      if (itemAst.Occurence.Optional)
                                                          argumentTypeName = new CodeTypeReference(typeof(IEnumerable<>).Name + "?", argumentTypeName);
                                                      else
                                                          argumentTypeName = new CodeTypeReference(typeof(IEnumerable<>).Name, argumentTypeName);
                                                  }
                                                  else
                                                  {

                                                      if (itemAst.Occurence.Optional)
                                                          name = name + "?";

                                                      argumentTypeName = new CodeTypeReference(name);

                                                  }

                                                  method.Parameters.Add(new CodeParameterDeclarationExpression(argumentTypeName, varName));
                                                  uniqeConstraintKeyMethod.Append(name);
                                                  arguments.Add(varName);
                                              }


                                          };

                                          if (alt.Count > 0)
                                              foreach (var itemAlt in alt)
                                                  act(itemAlt);
                                          else
                                              act(alt);

                                          if (method.Parameters.Count > 0)
                                          {

                                              var noDuplicateKey = uniqeConstraintKeyMethod.ToString();

                                              if (_h.Add(noDuplicateKey))
                                              {
                                                  List<CodeExpression> args = new List<CodeExpression>(arguments.Count)
                                                  {
                                                      CodeHelper.Var("Position.Default"),
                                                      "list".Var()
                                                  };

                                                  method.Statements.Add(CodeHelper.DeclareAndCreate("list", "List<AstRoot>".AsType()));
                                                  foreach (var itemArg in arguments)
                                                      method.Statements.Add("list".Var().Call("Add", itemArg.Var()));

                                                  var ret = CodeHelper.Create(t1, args.ToArray());
                                                  method.Statements.Return(ret);

                                                  methods.Add(method);


                                              }

                                          }

                                      }

                                  }

                                  foreach (var item in methods)
                                  {
                                      t.Members.Add(item);
                                  }

                              })
                              ;
                      });

                });

            });

        }

        private static string GetType(AstBase ast)
        {

            switch (ast.TerminalKind)
            {

                case TokenTypeEnum.Hexa:
                    break;

                case TokenTypeEnum.Boolean:
                    return nameof(Boolean);

                case TokenTypeEnum.Decimal:
                    return nameof(Decimal);

                case TokenTypeEnum.Int:
                    return nameof(Int64);

                case TokenTypeEnum.Real:
                    return nameof(Double);

                case TokenTypeEnum.Binary:
                case TokenTypeEnum.Identifier:
                case TokenTypeEnum.String:
                case TokenTypeEnum.Pattern:
                    return nameof(String);

                case TokenTypeEnum.Comment:
                case TokenTypeEnum.Other:
                case TokenTypeEnum.Operator:
                case TokenTypeEnum.Constant:
                case TokenTypeEnum.Ponctuation:
                default:
                    break;
            }

            string _name = (ast as AstBase).ResolveName();

            if (_name == "STRING")
            {

            }

            var result = "Ast" + CodeHelper.FormatCsharp(_name);
            return result;
        }

        private static string GetPropertyName(AstBase ast)
        {
            string _name;
            if (ast is AstRuleRef r && r.Parent is AstAtom a && a.Parent is AstLabeledElement l)
                _name = l.Left.ResolveName();

            if (ast is AstTerminal t && t.Parent is AstAtom a2 && a2.Parent is AstLabeledElement l2)
                _name = l2.Left.ResolveName();

            else
                _name = (ast as AstBase).ResolveName();

            var result = CodeHelper.FormatCsharp(_name);

            return result;
        }

        private static string GetFieldName(AstBase ast)
        {
            string _name;
            if (ast is AstRuleRef r && r.Parent is AstAtom a && a.Parent is AstLabeledElement l)
                _name = l.Left.ResolveName();

            if (ast is AstTerminal t && t.Parent is AstAtom a2 && a2.Parent is AstLabeledElement l2)
                _name = l2.Left.ResolveName();

            else
                _name = (ast as AstBase).ResolveName();

            var result = CodeHelper.FormatCsharpField(_name);

            return result;
        }

        private static string GetParameterdName(AstBase ast)
        {
            string _name;
            if (ast is AstRuleRef r && r.Parent is AstAtom a && a.Parent is AstLabeledElement l)
                _name = l.Left.ResolveName();

            if (ast is AstTerminal t && t.Parent is AstAtom a2 && a2.Parent is AstLabeledElement l2)
                _name = l2.Left.ResolveName();

            else
                _name = (ast as AstBase).ResolveName();

            var result = CodeHelper.FormatCsharpArgument(_name);

            return result;
        }

        private static List<object> GetProperties(AstBase ast)
        {

            var items = ast.GetAllItems()
            .KeepTerminal(c =>
            {

                if (c.TerminalKind == TokenTypeEnum.Comment)
                    return false;

                if (c.TerminalKind == TokenTypeEnum.Operator)
                    return false;

                if (c.TerminalKind == TokenTypeEnum.Ponctuation)
                    return false;

                if (c.TerminalKind == TokenTypeEnum.Constant)
                    return false;

                return true;

            });

            HashSet<string> names = new HashSet<string>();
            List<object> _properties = new List<object>();
            foreach (var item in items)
                if (names.Add(GetPropertyName(item)))
                    _properties.Add(item);

            return _properties;

        }

    }


}
