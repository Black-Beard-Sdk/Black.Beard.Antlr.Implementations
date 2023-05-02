using Antlr4.Runtime.Tree;
using Newtonsoft.Json.Linq;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Bb.Generators
{

    public static partial class CodeHelper
    {


        public static CodeExpression PreIncrement(this string name)
        {
            return new CodeSnippetExpression("++" + name);
        }

        public static CodeExpression PostIncrement(this string name, int i = 1)
        {
            if (i == 1)
                return new CodeSnippetExpression(name + "++");
            return new CodeSnippetExpression(name + "+=" + i.ToString());
        }

        public static CodeExpression PreDecrement(this string name)
        {
            return new CodeSnippetExpression("--" + name);
        }

        public static CodeExpression PostDecrement(this string name, int i = 1)
        {
            if (i == 1)
                return new CodeSnippetExpression(name + "--");
            return new CodeSnippetExpression(name + "-=" + i.ToString());
        }

        public static CodeAssignStatement Assign(this CodeExpression left, CodeExpression right)
        {
            return new CodeAssignStatement(left, right);
        }



        public static CodeCastExpression Cast(this CodeExpression self, CodeTypeReference type)
        {
            return new CodeCastExpression(type, self);
        }

        #region Operators

        public static CodeBinaryOperatorExpression LessThan(this CodeExpression left, CodeExpression right)
        {
            return new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.LessThan, right);
        }

        public static CodeBinaryOperatorExpression LessThanOrEqual(this CodeExpression left, CodeExpression right)
        {
            return new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.LessThanOrEqual, right);
        }

        public static CodeBinaryOperatorExpression GreaterThanOrEqual(this CodeExpression left, CodeExpression right)
        {
            return new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.GreaterThanOrEqual, right);
        }

        public static CodeBinaryOperatorExpression GreaterThan(this CodeExpression left, CodeExpression right)
        {
            return new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.GreaterThan, right);
        }

        public static CodeBinaryOperatorExpression Add(this CodeExpression left, CodeExpression right)
        {
            return new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Add, right);
        }

        public static CodeBinaryOperatorExpression Substract(this CodeExpression left, CodeExpression right)
        {
            return new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Subtract, right);
        }

        public static CodeBinaryOperatorExpression Divide(this CodeExpression left, CodeExpression right)
        {
            return new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Divide, right);
        }

        public static CodeBinaryOperatorExpression Multiply(this CodeExpression left, CodeExpression right)
        {
            return new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Multiply, right);
        }

        public static CodeBinaryOperatorExpression Modulus(this CodeExpression left, CodeExpression right)
        {
            return new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.Modulus, right);
        }

        public static CodeBinaryOperatorExpression IsEqual(this CodeExpression left, CodeExpression right)
        {
            return new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.ValueEquality, right);
        }

        public static CodeBinaryOperatorExpression IsNotEqual(this CodeExpression left, CodeExpression right)
        {
            return new CodeBinaryOperatorExpression(left, CodeBinaryOperatorType.IdentityInequality, right);
        }

        #endregion Operators

        public static CodePrimitiveExpression AsConstant(this char self) => new CodePrimitiveExpression(self);

        public static CodePrimitiveExpression AsConstant(this int self)
        {
            return new CodePrimitiveExpression(self);
        }

        public static CodePrimitiveExpression AsConstant(this object self)
        {
            return new CodePrimitiveExpression(self);
        }

        public static CodePrimitiveExpression Null()
        {
            return new CodePrimitiveExpression(null);
        }


        public static CodeTryCatchFinallyStatement Using(this string variableToDispose, Action<CodeStatementCollection> _block, Action<CodeStatementCollection> _finally)
        {

            CodeTryCatchFinallyStatement try1 = new CodeTryCatchFinallyStatement();
            
            if (_block != null)
                _block(try1.TryStatements);

            try1.FinallyStatements.Add(variableToDispose.Var().Call("Dispose"));
            if (_finally != null)
                _finally(try1.FinallyStatements);

            return try1;

        }

        public static CodeTryCatchFinallyStatement TryFinally(this Action<CodeStatementCollection> _block, Action<CodeStatementCollection> _finally)
        {

            CodeTryCatchFinallyStatement try1 = new CodeTryCatchFinallyStatement();

            if (_block != null)
                _block(try1.TryStatements);

            if (_finally != null)
                _finally(try1.FinallyStatements);

            return try1;

        }

        public static CodeTryCatchFinallyStatement TryCatch(this Action<CodeStatementCollection> _block, params (CodeTypeReference, string, Action<CodeStatementCollection> act)[] catchBlock)
        {

            CodeTryCatchFinallyStatement try1 = new CodeTryCatchFinallyStatement();

            if (_block != null)
                _block(try1.TryStatements);

            foreach (var item in catchBlock)
            {
                // Defines a catch clause for exceptions of type ApplicationException.
                CodeCatchClause catch1 = new CodeCatchClause(item.Item2, item.Item1);
                if (item.act != null)
                    item.act(catch1.Statements);
                else
                    catch1.Statements.Add(new CodeCommentStatement("Handle any System.ApplicationException here."));
                try1.CatchClauses.Add(catch1);
            }


            // Defines a finally block by adding to the FinallyStatements collection.

            //if (_finally != null)
            //    _finally(try1.FinallyStatements);

            return try1;

        }

        public static CodeTryCatchFinallyStatement TryCatchFinally(this Action<CodeStatementCollection> _block, Action<CodeStatementCollection> _finally, params (CodeTypeReference, string, Action<CodeStatementCollection> act)[] catchBlock)
        {

            CodeTryCatchFinallyStatement try1 = new CodeTryCatchFinallyStatement();

            if (_block != null)
                _block(try1.TryStatements);

            foreach (var item in catchBlock)
            {
                // Defines a catch clause for exceptions of type ApplicationException.
                CodeCatchClause catch1 = new CodeCatchClause(item.Item2, item.Item1);
                if (item.act != null)
                    item.act(catch1.Statements);
                else
                    catch1.Statements.Add(new CodeCommentStatement("Handle any System.ApplicationException here."));
                try1.CatchClauses.Add(catch1);
            }

            // Defines a finally block by adding to the FinallyStatements collection.

            if (_finally != null)
                _finally(try1.FinallyStatements);

            return try1;


        }

        # region CodeConditionStatement

        public static CodeConditionStatement If(this CodeExpression condition, Action<CodeStatementCollection> _true, Action<CodeStatementCollection> _false = null)
        {

            var result = new CodeConditionStatement(condition);

            if (_true != null)
                _true(result.TrueStatements);

            if (_false != null)
                _false(result.FalseStatements);

            return result;

        }

        public static CodeIterationStatement For(this CodeStatement init, CodeExpression test, CodeStatement incrementStatement, Action<CodeStatementCollection> action)
        {
            var i = new CodeIterationStatement(init, test, incrementStatement);
            action(i.Statements);
            return i;
        }

        public static CodeIterationStatement For(this CodeTypeReference type, string name, CodeExpression test, Action<CodeStatementCollection> action)
        {
            CodeStatement incrementStatement = name.PostIncrement().AsStatement();
            return For(type, name, (0).AsConstant(), test, incrementStatement, action);

        }

        public static CodeIterationStatement For(this CodeTypeReference type, string name, int init, CodeExpression test, Action<CodeStatementCollection> action)
        {
            var incrementStatement = new CodeAssignStatement(name.Var(), name.Var().Add((1).AsConstant()));
            return For(type, name, init.AsConstant(), test, incrementStatement, action);
        }

        public static CodeIterationStatement For(this CodeTypeReference type, string name, CodeExpression init, CodeExpression test, CodeStatement incrementStatement, Action<CodeStatementCollection> action)
        {

            var declare = Declare(name, type, init);
            var i = new CodeIterationStatement(declare, test, incrementStatement);
            action(i.Statements);
            return i;

        }

        public static CodeIterationStatement ForEach(this CodeTypeReference variableType, string item, string list, Action<CodeStatementCollection> action)
        {

            var t = "IEnumerator".AsType();
            //t.TypeArguments.Add(variableType);
            var declare = Declare("enumerator", t, list.Var().Call("GetEnumerator"));

            var i = new CodeIterationStatement(declare, "enumerator".Var().Call("MoveNext"), new CodeSnippetStatement(""));
            i.Statements.DeclareAndInitialize(item, variableType, "enumerator".Var().Property("Current").Cast(variableType));
            action(i.Statements);

            return i;

        }

        public static CodeIterationStatement ForEach(this CodeTypeReference variableType, string item, CodeExpression list, Action<CodeStatementCollection> action)
        {

            var t = "IEnumerator".AsType();
            //t.TypeArguments.Add(variableType);
            var declare = Declare("enumerator", t, list.Call("GetEnumerator"));

            var i = new CodeIterationStatement(declare, "enumerator".Var().Call("MoveNext"), new CodeSnippetStatement(""));
            i.Statements.DeclareAndInitialize(item, variableType, "enumerator".Var().Property("Current").Cast(variableType));
            action(i.Statements);

            return i;

        }

        # endregion CodeConditionStatement


        public static CodeExpression Property(this CodeExpression self, string name)
        {
            return new CodePropertyReferenceExpression(self, name);
        }

        public static CodeTypeReference AsType(this string type) => new CodeTypeReference(type);

        public static CodeTypeReference AsType(this Type type) => new CodeTypeReference(type);


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

        public static CodeVariableDeclarationStatement Declare(this string name, CodeTypeReference type, CodeExpression initExpression = null)
        {

            if (initExpression != null)
                return new CodeVariableDeclarationStatement(type, name, initExpression);

            return new CodeVariableDeclarationStatement(type, name);
        }

        public static CodeVariableDeclarationStatement DeclareAndInitialize(this string name, CodeExpression initExpression = null)
        {
            var type = "var".AsType();
            if (initExpression != null)
                return new CodeVariableDeclarationStatement(type, name, initExpression);

            return new CodeVariableDeclarationStatement(type, name);
        }



        public static CodeVariableReferenceExpression Var(this string name) => new CodeVariableReferenceExpression(name);

        public static CodeIndexerExpression Indexer(this string name, params CodeExpression[] indexes)
        {
            return new CodeIndexerExpression(new CodeVariableReferenceExpression(name), indexes);
        }

        public static CodeIndexerExpression Indexer(this CodeExpression self, params CodeExpression[] indexes)
        {
            return new CodeIndexerExpression(self, indexes);
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




        public static CodeExpressionStatement AsStatement(this CodeExpression self)
        {
            return new CodeExpressionStatement(self);
        }

        public static CodeMethodInvokeExpression Call(this CodeExpression instance, string name, params CodeExpression[] arguments)
        {
            return new CodeMethodInvokeExpression(instance, name, arguments);
        }

        public static CodeMethodInvokeExpression Call(this CodeExpression instance, string name, string[] genericParameters, params CodeExpression[] arguments)
        {

            StringBuilder builder = new StringBuilder(name);

            if (genericParameters.Length > 0)
            {
                string comma = string.Empty;
                builder.Append("<");
                foreach (var parameter in genericParameters)
                {
                    builder.Append(comma);
                    builder.Append(parameter);
                    comma = ", ";
                }
                builder.Append(">");

            }

            return new CodeMethodInvokeExpression(instance, builder.ToString(), arguments);

        }

        public static CodeMethodInvokeExpression Call(this string name, params CodeExpression[] arguments)
        {
            return new CodeMethodInvokeExpression(This(), name, arguments);
        }

        public static CodeMethodInvokeExpression Call(this string name, string[] genericParameters, params CodeExpression[] arguments)
        {

            StringBuilder builder = new StringBuilder(name);

            if (genericParameters.Length > 0)
            {
                string comma = string.Empty;
                builder.Append("<");
                foreach (var parameter in genericParameters)
                {
                    builder.Append(comma);
                    builder.Append(parameter);
                    comma = ", ";
                }
                builder.Append(">");

            }
            var m = new CodeMethodInvokeExpression(This(), builder.ToString(), arguments);
            return m;

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

            if (codeTypeReference.BaseType.EndsWith("?"))
                codeTypeReference.BaseType = codeTypeReference.BaseType.Substring(0, codeTypeReference.BaseType.Length - 1);

            return new CodeTypeOfExpression(codeTypeReference);
        }

    }

}
