﻿using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using Bb.ParsersConfiguration.Ast;
using System.CodeDom;
using System.Text;
using System.Xml.Linq;

namespace Generate.ModelsScripts
{

    public class ScriptClassWithProperties : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {
            return GetInherit_Impl("AstBnfRule", ast, context);
        }

        public override HashSet<string> StrategyTemplateKeys => _keys;

        private HashSet<string> _keys = new HashSet<string> { "ClassWithProperties" };

        protected override void ConfigureTemplate(Context ctx, CodeGeneratorVisitor generator)
        {

            generator.Add(this.Name, template =>
            {

                template.Namespace(Namespace, ns =>
                {
                    ns.Using(Usings)
                      .Using("Antlr4.Runtime")
                      .Using("Antlr4.Runtime.Tree")
                      .Using("System.Collections")

                      .CreateTypeFrom<AstRule>(ast => Generate(ast, ctx), null, (ast, type) =>
                      {

                          type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                              .Documentation(c => c.Summary(() => ast.ToString()))
                              .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name.Text))
                              .Inherit(() => GetInherit(ast, ctx))



                              .Ctor((f) =>
                              {
                                  f.Argument(() => "Position", "p")
                                   .Argument(() => "AstRootList<AstRoot>", "list")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("p")
                                   .Body(b =>
                                   {

                                       var items = ast.GetProperties();

                                       b.Statements.ForEach("AstRoot".AsType(), "item", "list", stm =>
                                       {

                                           foreach (var item in items)
                                           {

                                               var fieldName = (item as AstBase).GetFieldName();

                                               var type = (item as AstBase).Type();
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
                                   .Arguments(() => ast.GetProperties(), c =>
                                   {
                                       var a = c as AstBase;
                                       return (a.Type(), a.GetParameterdName());
                                   })
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("p")
                                   .Body(b =>
                                   {
                                       var items = ast.GetProperties();
                                       foreach (var item in items)
                                       {
                                           var aa = item as AstBase;
                                           b.Statements.Assign(CodeHelper.This().Field(aa.GetFieldName()), CodeHelper.Var(aa.GetParameterdName()));
                                       }

                                   })
                                   ;
                              })

                              .Ctor((f) =>
                              {
                                  f.Argument(() => "ParserRuleContext", "ctx")
                                   .Argument(() => "AstRootList<AstRoot>", "list")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("ctx")
                                   .Body(b =>
                                   {

                                       var items = ast.GetProperties();

                                       b.Statements.ForEach("AstRoot".AsType(), "item", "list", stm =>
                                       {

                                           foreach (var item in items)
                                           {

                                               var fieldName = (item as AstBase).GetFieldName();

                                               var type = (item as AstBase).Type();
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
                                  () => ast.GetProperties(),
                                  (property, model) =>
                                  {
                                      property.Name(m => (model as AstBase).GetPropertyName())
                                      .Type(() =>
                                      {
                                          return (model as AstBase).Type();
                                      })
                                      .Attribute(MemberAttributes.Public)
                                      .Get((stm) =>
                                      {

                                          stm.Return(CodeHelper.This().Field((model as AstBase).GetFieldName()));

                                      })
                                      .HasSet(false)
                                      ;
                                  }
                              )

                              .Fields(() => ast.GetProperties(),
                                  (field, model) =>
                                  {
                                      field.Name(m => (m as AstBase).GetFieldName())
                                      .Type(() => (model as AstBase).Type())
                                      .Attribute(MemberAttributes.Private)
                                      ;
                                  }
                              )

                              .Field(field =>
                              {

                                  field.Name("_ruleValue")
                                  .Type(typeof(string))
                                  .Attribute(MemberAttributes.Private | MemberAttributes.Static)
                                  .Value((a) =>
                                  {
                                      return ast.Alternatives.ToString();
                                  })
                                  ;
                              })
                                .Field(field =>
                                {
                                    field.Name("_ruleName")
                                         .Type(typeof(string))
                                         .Attribute(MemberAttributes.Private | MemberAttributes.Static)
                                         .Value((a) =>
                                         {
                                             return ast.Name.Text;
                                         });
                                })
                                .Property(property =>
                                {
                                    property.Name((a) => "RuleName")
                                            .Type(() => typeof(string))
                                            .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                            .Get((a) => a.Return("_ruleName".Var()))
                                            .HasSet(false)
                                            ;
                                })
                                .Property(property =>
                                {
                                    property.Name((a) => "RuleValue")
                                            .Type(() => typeof(string))
                                            .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                            .Get((a) => a.Return("_ruleValue".Var()))
                                            .HasSet(false)
                                            ;
                                })

                              .Make(t =>
                              {

                                  HashSet<string> _h = new HashSet<string>();
                                  List<CodeMemberMethod> methods = new List<CodeMemberMethod>();

                                  foreach (AstLabeledAlt alternative in ast.Alternatives)
                                  {

                                      var allCombinations = alternative.ResolveAllCombinations();

                                      foreach (var altSource in allCombinations)
                                      {
                                          var alt = altSource.Item;
                                          StringBuilder uniqeConstraintKeyMethod = new StringBuilder();
                                          var name = CodeHelper.FormatCsharp(ast.Name.Text);
                                          var t1 = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text)).AsType();
                                          List<string> arguments = new List<string>();

                                          var method = "New".AsMethod(t1, MemberAttributes.Public | MemberAttributes.Static)
                                            .BuildDocumentation(ast.Name.Text, alt, ctx);

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

                                                  method.Statements.Add(CodeHelper.DeclareAndCreate("list", "AstRootList<AstRoot>".AsType()));
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

                              .Method(method =>
                              {
                                  var type = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));
                                  method.Name(g => "Null")
                                        .Return(() => type + "?")
                                        .Attribute(MemberAttributes.Static | MemberAttributes.Public)
                                        .Body(b => b.Statements.Return(CodeHelper.Null()))
                                   ;

                              })

                              .Method(method =>
                              {
                                  var type = ("Ast" + CodeHelper.FormatCsharp(ast.Name.Text));
                                  method.Name(g => "ToString")
                                        .Argument("Writer", "writer")
                                        .Argument("StrategySerializationItem", "strategy")
                                        .Attribute(MemberAttributes.Public | MemberAttributes.Override)
                                        .Return(() => typeof(bool))
                                        .Body(b =>
                                        {
                                            GenerateClassDefaultToString.Generate(ast, b.Statements);
                                        })
                                   ;

                              })

                              ;
                      });

                });

            });

        }


    }


}
