using Antlr4.Runtime;
using Bb.Asts;
using Bb.Parsers;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Generators
{


    public class ScriptAstOnNamespace : ScriptAstBase<CodeNamespace>
    {

        public ScriptAstOnNamespace()
        {
            Parents = new List<string>();
            Usings = new List<string>();
        }

        public ScriptAstOnNamespace Using(string u)
        {
            this.Using(u);
            return this;
        }

        public override CodeNamespace Get(Context ctx, CodeCompileUnit compileUnit)
        {

            CodeNamespace @namespace = new CodeNamespace(ctx.Namespace);
            compileUnit.Namespaces.Add(@namespace);

            foreach (string ns in Usings) 
                @namespace.Imports.Add(new CodeNamespaceImport(ns));

            return @namespace;

        }

        public override void GenerateTo(CodeNamespace o, Context ctx, AstBase model)
        {

            CodeTypeDeclaration class1 = new CodeTypeDeclaration(NameOfClass(model))
            {
                IsPartial = true,
            };

            if (!string.IsNullOrEmpty(this.Parent))
                class1.BaseTypes.Add(this.Parent);

            if (this.Parents != null)
                foreach (var parent in this.Parents)
                    class1.BaseTypes.Add(parent);

            o.Types.Add(class1);

            CodeConstructor constructor = new CodeConstructor()
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Final
            };

            // Add parameters.
            constructor.Parameters.Add(new CodeParameterDeclarationExpression(typeof(ParserRuleContext), "ctx"));
            constructor.BaseConstructorArgs.Add(new CodeVariableReferenceExpression("ctx"));

            class1.Members.Add(constructor);

            // Declaring a ToString method
            CodeMemberMethod acceptMethod = new CodeMemberMethod()
            {
                Name = "Accept",
                Attributes = MemberAttributes.Public | MemberAttributes.Override,
                ReturnType = new CodeTypeReference(typeof(void)),
            };

            acceptMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(IAstBaseVisitor), "visitor"));

            // acceptMethod.Statements.Add(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("visitor"), "Visit" + this.Name, new CodeThisReferenceExpression()));

            class1.Members.Add(acceptMethod);

        }

        public Func<AstBase, string> NameOfClass { get; set; }

        public string Parent { get; set; }

        public List<string> Usings { get; }

        public List<string> Parents { get; }

    }



}
