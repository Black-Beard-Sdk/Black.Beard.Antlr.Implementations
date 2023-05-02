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


        public static CodeStatementCollection If(this CodeStatementCollection self, CodeExpression condition, Action<CodeStatementCollection> _true, Action<CodeStatementCollection> _false = null)
        {
            var t = condition.If(_true, _false);
            self.Add(t);
            return self;
        }


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

        public static CodeStatementCollection Declare(this CodeStatementCollection self, string name, CodeTypeReference type, CodeExpression initExpression = null)
        {
            self.Add(Declare(name, type, initExpression));
            return self;
        }

        public static CodeStatementCollection Call(this CodeStatementCollection self, CodeExpression instance, string name, params CodeExpression[] arguments)
        {
            self.Add(instance.Call(name, arguments));
            return self;
        }

        public static CodeStatementCollection Return(this CodeStatementCollection self, CodeExpression expression)
        {
            self.Add(new CodeMethodReturnStatement(expression));
            return self;
        }

        public static CodeStatementCollection ForEach(this CodeStatementCollection self, CodeTypeReference variableType, string item, string list, Action<CodeStatementCollection> action)
        {
            self.Add(ForEach(variableType, item, list, action));
            return self;
        }

        public static CodeStatementCollection ForEach(this CodeStatementCollection self, CodeTypeReference variableType, string item, CodeExpression list, Action<CodeStatementCollection> action)
        {
            self.Add(variableType.ForEach(item, list, action));
            return self;
        }

        public static CodeStatementCollection Assign(this CodeStatementCollection self, CodeExpression left, CodeExpression right)
        {
            self.Add(left.Assign(right));
            return self;
        }

        public static CodeStatementCollection Using(this CodeStatementCollection self, string variableToDispose, CodeExpression initExpression, Action<CodeStatementCollection> _block, Action<CodeStatementCollection> _finally = null)
        {
            self.Add(DeclareAndInitialize(variableToDispose, initExpression));
            self.Add(Using(variableToDispose, _block, _finally));
            return self;
        }

        public static CodeStatementCollection TryFinally(this CodeStatementCollection self, Action<CodeStatementCollection> _block, Action<CodeStatementCollection> _finally)
        {
            self.Add(TryFinally(_block, _finally));
            return self;
        }

        public static CodeStatementCollection TryCatch(this CodeStatementCollection self, Action<CodeStatementCollection> _block, params (CodeTypeReference, string, Action<CodeStatementCollection> act)[] catchBlock)
        {
            self.Add(TryCatch(_block, catchBlock));
            return self;
        }

        public static CodeStatementCollection TryCatchFinally(this CodeStatementCollection self, Action<CodeStatementCollection> _block, Action<CodeStatementCollection> _finally, params (CodeTypeReference, string, Action<CodeStatementCollection> act)[] catchBlock)
        {
            self.Add(TryCatchFinally(_block, _finally, catchBlock));
            return self;
        }


    }

}
