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



    public class ScriptCreateInterface : ScriptAstBase<CodeNamespace>
    {

        public ScriptCreateInterface()
        {
            Parents = new List<string>();
            Usings = new List<string>();
        }

        public ScriptCreateInterface Using(string u)
        {
            this.Usings.Add(u);
            return this;
        }

        public override CodeObject Get(Context ctx, CodeCompileUnit compileUnit)
        {

            CodeNamespace @namespace = new CodeNamespace(ctx.Namespace);
            compileUnit.Namespaces.Add(@namespace);

            foreach (string ns in Usings)
                @namespace.Imports.Add(new CodeNamespaceImport(ns));

            return @namespace;

        }

        public override void GenerateTo(CodeNamespace o, Context ctx, AstBase model)
        {

            CodeTypeDeclaration class1 = new CodeTypeDeclaration("ITSqlVisitor")
            {
                IsPartial = true,
                IsInterface = true,
            };

            if (this.Parents != null)
                foreach (var parent in this.Parents)
                    class1.BaseTypes.Add(parent);

            o.Types.Add(class1);

            CodeConstructor constructor = new CodeConstructor()
            {
                Attributes = MemberAttributes.Public | MemberAttributes.Final
            };                      

            // Declaring a ToString method
            CodeMemberMethod acceptMethod = new CodeMemberMethod()
            {
                Name = "Visit" + NameOfClass(model),
                Attributes = MemberAttributes.Public,
                ReturnType = new CodeTypeReference(typeof(AstBase)),
            };

            acceptMethod.Parameters.Add(new CodeParameterDeclarationExpression("Ast" + NameOfClass(model), "i"));

            class1.Members.Add(acceptMethod);

        }



        public ScriptCreateInterface Implement(string interfaceName)
        {
            this.Parents.Add(interfaceName);
            return this;
        }

        public Func<AstBase, string> NameOfClass { get; set; }

        public List<string> Usings { get; }

        public List<string> Parents { get; }
        public string Name { get; set; }
    }



    public class ScriptCreateModel : ScriptAstBase<CodeNamespace>
    {

        public ScriptCreateModel()
        {
            Parents = new List<string>();
            Usings = new List<string>();
        }

        public ScriptCreateModel Using(string u)
        {
            this.Usings.Add(u);
            return this;
        }

        public override CodeObject Get(Context ctx, CodeCompileUnit compileUnit)
        {

            CodeNamespace @namespace = new CodeNamespace(ctx.Namespace);
            compileUnit.Namespaces.Add(@namespace);

            foreach (string ns in Usings)
                @namespace.Imports.Add(new CodeNamespaceImport(ns));

            return @namespace;

        }

        public override void GenerateTo(CodeNamespace o, Context ctx, AstBase model)
        {

            var n = NameOfClass(model);
            CodeTypeDeclaration class1 = new CodeTypeDeclaration("Ast" + n)
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

            acceptMethod.Statements.Add(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("visitor"), "Visit" + n, new CodeThisReferenceExpression()));

            class1.Members.Add(acceptMethod);

        }

        public ScriptCreateModel Inherit(string parentName)
        {
            this.Parent = parentName;
            return this;
        }

        public ScriptCreateModel Implement(string interfaceName)
        {
            this.Parents.Add(interfaceName);
            return this;
        }

        public Func<AstBase, string> NameOfClass { get; set; }

        public string Parent { get; set; }

        public List<string> Usings { get; }

        public List<string> Parents { get; }

    }



}
