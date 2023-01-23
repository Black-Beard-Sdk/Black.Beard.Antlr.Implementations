using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Bb.Asts;
using Bb.Parsers;
using Bb.Parsers.Tsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace Bb.Parsers
{

    public partial class ScriptTSqlVisitor
    {

        //public override AstBase VisitTsql_file([NotNull] TSqlParser.Tsql_fileContext context)
        //{

        //    List<AstBase> list = new List<AstBase>();
            
        //    foreach (var item in context.children)
        //        list.Add(item.Accept(this));

        //    return new AstBase(context, list);

        //}


    }

}


