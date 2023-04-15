using Antlr4.Runtime.Tree;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Bb.Generators
{

    public static class CodeHelper
    {

        public static CodeStatementCollection Assign(this CodeStatementCollection self, CodeExpression left, CodeExpression right)
        {
            self.Add(left.Assign(right));
            return self;
        }


        public static CodeAssignStatement Assign(this CodeExpression left, CodeExpression right)
        {
            return new CodeAssignStatement(left, right);
        }

        public static CodeCastExpression Cast(this CodeExpression self, CodeTypeReference type)
        {
            return new CodeCastExpression(type, self);
        }


        public static CodeBinaryOperatorExpression IsEqual(this CodeExpression left, CodeExpression right)
        {
            return new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.ValueEquality, right);
        }

        public static CodeBinaryOperatorExpression IsNotEqual(this CodeExpression left, CodeExpression right)
        {
            return new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.IdentityInequality, right);
        }

        public static CodePrimitiveExpression AsConstant(this object self)
        {
            return new CodePrimitiveExpression(self);
        }

        public static CodePrimitiveExpression Null()
        {
            return new CodePrimitiveExpression(null);
        }

        public static CodeStatementCollection If(this CodeStatementCollection self, CodeExpression condition, Action<CodeStatementCollection> _true, Action<CodeStatementCollection> _false = null)
        {
            var t = condition.If(_true, _false);
            self.Add(t);
            return self;
        }

        public static CodeConditionStatement If(this CodeExpression condition, Action<CodeStatementCollection> _true, Action<CodeStatementCollection> _false = null)
        {

            var result = new CodeConditionStatement(condition);

            if (_true != null)
                _true(result.TrueStatements);

            if (_false != null)
                _false(result.FalseStatements);

            return result;

        }

        public static CodeStatementCollection ForEach(this CodeStatementCollection self, CodeTypeReference variableType, string item, string list, Action<CodeStatementCollection> action)
        {

            var t = "IEnumerator".AsType();
            //t.TypeArguments.Add(variableType);
            var declare = Declare("enumerator", t, list.Var().Call("GetEnumerator"));

            var i = new CodeIterationStatement(declare, "enumerator".Var().Call("MoveNext"), new CodeSnippetStatement(""));
            i.Statements.DeclareAndInitialize(item, variableType, "enumerator".Var().Property("Current").Cast(variableType));
            action(i.Statements);

            self.Add(i);

            return self;

        }


        public static CodeExpression Property(this CodeExpression self, string name)
        {
            return new CodePropertyReferenceExpression(self, name);
        }

        public static CodeTypeReference AsType(this string type) => new CodeTypeReference(type);

        public static CodeTypeReference AsType(this Type type) => new CodeTypeReference(type);

        public static CodeStatementCollection DeclareAndCreate(this CodeStatementCollection self, string name, CodeTypeReference type, params CodeExpression[] arguments)
        {
            self.Add(DeclareAndCreate(name, type, arguments));
            return self;
        }

        public static CodeStatementCollection DeclareAndInitialize(this CodeStatementCollection self, string name, CodeTypeReference type, CodeExpression initExpression)
        {
            self.Add(DeclareAndInitialize(name, type, initExpression));
            return self;
        }


        public static CodeObjectCreateExpression Create(this CodeTypeReference type, params CodeExpression[] arguments)
        {
            return new CodeObjectCreateExpression(type, arguments);
        }

        public static CodeVariableDeclarationStatement DeclareAndCreate(this string name, CodeTypeReference type, params CodeExpression[] arguments)
        {
            return DeclareAndInitialize(name, type, new CodeObjectCreateExpression(type, arguments));
        }

        public static CodeVariableDeclarationStatement DeclareAndInitialize(this string name, CodeTypeReference type, CodeExpression initExpression)
        {
            return new CodeVariableDeclarationStatement(type, name, initExpression);
        }

        public static CodeStatementCollection Declare(this CodeStatementCollection self, string name, CodeTypeReference type, CodeExpression initExpression = null)
        {
            self.Add(Declare(name, type, initExpression));
            return self;
        }

        public static CodeVariableDeclarationStatement Declare(this string name, CodeTypeReference type, CodeExpression initExpression = null)
        {

            if (initExpression != null)
                return new CodeVariableDeclarationStatement(type, name, initExpression);

            return new CodeVariableDeclarationStatement(type, name);
        }

        public static CodeVariableReferenceExpression Var(this string name) => new CodeVariableReferenceExpression(name);

        public static CodeIndexerExpression Indexer(this string name, params CodeExpression[] indexes)
        {
             return new CodeIndexerExpression(new CodeVariableReferenceExpression(name), indexes);
        }


        public static CodeFieldReferenceExpression Field(this CodeTypeReference type, string name)
        {
            return new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(type), name);
        }

        public static CodeFieldReferenceExpression Field(this CodeExpression self, string name)
        {
            return new CodeFieldReferenceExpression(self, name);
        }

        public static CodeThisReferenceExpression This() => new CodeThisReferenceExpression();

        public static CodeStatementCollection Call(this CodeStatementCollection self, CodeExpression instance, string name, params CodeExpression[] arguments)
        {
            self.Add(instance.Call(name, arguments));
            return self;
        }

        public static CodeMethodInvokeExpression Call(this CodeExpression instance, string name, params CodeExpression[] arguments)
        {
            return new CodeMethodInvokeExpression(instance, name, arguments);
        }

        public static CodeMethodInvokeExpression Call(this string name, params CodeExpression[] arguments)
        {
            return new CodeMethodInvokeExpression(This(), name, arguments);
        }

        public static CodeMethodInvokeExpression Call(this CodeTypeReference instance, string name, params CodeExpression[] arguments)
        {
            return new CodeTypeReferenceExpression(instance).Call(name, arguments);
        }


        public static CodeMethodInvokeExpression Call(this CodeExpression instance, string name, CodeTypeReference[] types, params CodeExpression[] arguments)
        {
            var method = new CodeMethodReferenceExpression(instance, name);
            method.TypeArguments.AddRange(types);
            var result = new CodeMethodInvokeExpression(method, arguments);
            return result;
        }

        public static CodeStatementCollection Return(this CodeStatementCollection self, CodeExpression expression)
        {
            self.Add(new CodeMethodReturnStatement(expression));
            return self;
        }


        public static string FormatCsharp(this string code)
        {

            StringBuilder sb = new StringBuilder(code?.Length ?? 0);

            if (code != null)
            {

                char last = '\0';
                for (int i = 0; i < code.Length; i++)
                {
                    var s = code[i];

                    if (sb.Length == 0)
                        s = char.ToUpper(s);

                    else if (s == '_')
                    {
                        last = '_';
                        continue;
                    }

                    else if (last == '_')
                        s = char.ToUpper(s);

                    else
                        s = char.ToLower(s);

                    sb.Append(s);
                    last = s;

                }

            }
            else
            {

            }

            return sb.ToString().Trim();
        }

        public static string FormatCsharpField(this string code)
        {

            StringBuilder sb = new StringBuilder(code.Length);
            sb.Append("_");

            char last = '\0';
            for (int i = 0; i < code.Length; i++)
            {
                var s = code[i];

                if (sb.Length == 0)
                    s = char.ToLower(s);

                else if (s == '_')
                {
                    last = '_';
                    continue;
                }

                else if (last == '_')
                    s = char.ToUpper(s);

                sb.Append(s);
                last = s;

            }

            return sb.ToString();

        }

        public static string FormatCsharpArgument(this string code)
        {

            StringBuilder sb = new StringBuilder(code.Length);

            char last = '\0';
            for (int i = 0; i < code.Length; i++)
            {
                var s = code[i];

                if (sb.Length == 0)
                    s = char.ToLower(s);

                else if (s == '_')
                {
                    last = '_';
                    continue;
                }

                else if (last == '_')
                    s = char.ToUpper(s);

                sb.Append(s);
                last = s;

            }

            return sb.ToString();

        }

        public static string FormatCamelUpercase(this string code)
        {
            StringBuilder sb = new StringBuilder(code.Length);
            for (int i = 0; i < code.Length; i++)
            {
                var s = code[i];
                if (sb.Length == 0)
                    s = char.ToUpper(s);
                sb.Append(s);

            }
            return sb.ToString();
        }

        public static CodeTypeOfExpression Typeof(this string typeName)
        {
            return new CodeTypeOfExpression(typeName.AsType());
        }

        public static CodeTypeOfExpression Typeof(this CodeTypeReference codeTypeReference)
        {
            return new CodeTypeOfExpression(codeTypeReference);
        }

    }

}
