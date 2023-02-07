﻿using Bb.Asts;
using Bb.Generators;
using Bb.Parsers;
using System.CodeDom;

namespace Generate.Scripts
{
    public class ScriptClassWithProperties : ScriptBase
    {

        public override string GetInherit(AstRule ast, Context context)
        {
            return "AstRoot";
        }

        protected override bool Generate(AstRule ast, Context context)
        {
            return ast.Strategy == "ClassWithProperties";
        }

        protected override void ConfigureTemplate(Context ctx, CodeGeneratorVisitor generator)
        {

            generator.Add(this.Name, template =>
            {

                template.Namespace("Bb.Asts", ns =>
                {
                    ns.Using("System")
                      .Using("Antlr4.Runtime")
                      .Using("System.Collections")
                      .Using("Antlr4.Runtime.Tree")
                      .Using("Bb.Parsers")

                      .CreateTypeFrom<AstRule>((ast, type) =>
                      {

                          type.AddTemplateSelector(() => TemplateSelector(ast, ctx))
                              .GenerateIf(() => Generate(ast, ctx))
                              .Comment(() => ast.ToString())
                              .Name(() => "Ast" + CodeHelper.FormatCsharp(ast.Name))
                              .Inherit(() => GetInherit(ast, ctx))



                              .Ctor((f) =>
                              {
                                  f.Argument(() => "Position", "p")
                                   .Argument(() => "List<AstRoot>", "list")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("p")
                                   .Body(b =>
                                   {

                                       b.Statements.ForEach("AstRoot".AsType(), "item", "list", stm =>
                                       {

                                           foreach (var item in ast.GetListAlternatives())
                                           {
                                               foreach (var item2 in item.Where(c => c.IsRule).ToList())
                                               {

                                                   var name = item2.ResolveName();

                                                   var type = "Ast" + CodeHelper.FormatCsharp(name);
                                                   var ty = new CodeTypeReference(type);
                                                   stm.If(CodeHelper.Var("enumerator.Current").Call("Is", new CodeTypeReference[] { ty }), t =>
                                                   {
                                                       t.Assign(CodeHelper.This().Field(CodeHelper.FormatCsharpField(name)), CodeHelper.Var("enumerator.Current").Cast(ty));
                                                   });
                                               }

                                           }

                                       });

                                   })
                                   ;
                              })
                              .Ctor((f) =>
                              {
                                  f.Argument(() => "ParserRuleContext", "ctx")
                                   .Argument(() => "List<AstRoot>", "list")
                                   .Attribute(MemberAttributes.Public)
                                   .CallBase("ctx")
                                   .Body(b =>
                                   {

                                       b.Statements.ForEach("AstRoot".AsType(), "item", "list", stm =>
                                       {

                                           foreach (var item in ast.GetListAlternatives())
                                           {
                                               foreach (var item2 in item.Where(c => c.IsRule).ToList())
                                               {

                                                   var name = item2.ResolveName();

                                                   var type = "Ast" + CodeHelper.FormatCsharp(name);
                                                   var ty = new CodeTypeReference(type);
                                                   stm.If(CodeHelper.Var("enumerator.Current").Call("Is", new CodeTypeReference[] { ty }), t =>
                                                   {
                                                       t.Assign(CodeHelper.This().Field(CodeHelper.FormatCsharpField(name)), CodeHelper.Var("enumerator.Current").Cast(ty));
                                                   });
                                               }

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
                                           "Visit" + CodeHelper.FormatCsharp(ast.Name),
                                           CodeHelper.This()
                                       );
                                   });
                              })

                              .Properties(() =>
                                  {
                                      List<object> _properties = new List<object>();
                                      foreach (var item in ast.GetListAlternatives())
                                          foreach (AstBase item2 in item.Where(c => c.IsRule).ToList())
                                              _properties.Add(item2);
                                      return _properties;
                                  },
                                  (property, model) =>
                                  {
                                      property.Name(m => CodeHelper.FormatCsharp((model as AstBase).ResolveName()))
                                      .Type(() => "Ast" + CodeHelper.FormatCsharp((model as AstBase).ResolveName()))
                                      .Attribute(MemberAttributes.Public)
                                      .Get((stm) =>
                                      {
                                          stm.Return(CodeHelper.This().Field(CodeHelper.FormatCsharpField((model as AstBase).ResolveName())));
                                      })
                                      .HasSet(false)
                                      ;
                                  }
                              )

                              .Fields(() =>
                                  {
                                      List<object> _properties = new List<object>();
                                      foreach (var item in ast.GetListAlternatives())
                                          foreach (AstBase item2 in item.Where(c => c.IsRule).ToList())
                                              _properties.Add(item2);
                                      return _properties;
                                  },
                                  (field, model) =>
                                  {
                                      field.Name(m => CodeHelper.FormatCsharpField((model as AstBase).ResolveName()))
                                      .Type(() => "Ast" + CodeHelper.FormatCsharp((model as AstBase).ResolveName()))
                                      .Attribute(MemberAttributes.Private)
                                      ;
                                  }
                              )

                              ;
                      });
                      
                });

            });

        }

    }


}