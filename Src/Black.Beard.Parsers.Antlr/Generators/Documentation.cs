using Bb.Parsers;
using Newtonsoft.Json.Linq;
using System;
using System.CodeDom;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

namespace Bb.Generators
{



    public class Documentation
    {

        public Documentation()
        {
            _comments = new Dictionary<string, DocumentationItem>();
        }


        public void Summary(Func<string> value)
        {
            Append(_summary, value);
        }

        public void Remark(Func<string> value)
        {
            Append(_remarks, value);
        }

        public void Returns(Func<string> value)
        {
            Append(_returns, value);
        }

        public void GenerateDocumentation(CodeTypeMember member, Context context)
        {
            Generate(_summary, null, member);
            Generate(_remarks, null, member);
        }

        private void Generate(string key, string subKey, CodeTypeMember member)
        {

            if (_comments.TryGetValue(key, out var value))
            {
                value.Generate(key, subKey, member);
            }
        }

        private void Append(string key, Func<string> value)
        {

            if (!_comments.TryGetValue(key, out DocumentationItem list))
                _comments.Add(key, list = new DocumentationItem());

            list.Add(value);

        }

        private const string _summary = "summary";
        private const string _remarks = "remarks";
        private const string _returns = "returns";

        // exception cref
        // param name
        private Dictionary<string, DocumentationItem> _comments { get; set; }


        private class DocumentationItem
        {

            public DocumentationItem()
            {
                _list = new List<(Func<string>, Func<string>)>();
            }

            internal void Add(Func<string> action1, Func<string> action2)
            {
                _list.Add((action1, action2));
            }

            internal void Add(Func<string> action)
            {
                _list.Add((null, action));
            }

            internal void Generate(string key, string subKey, CodeTypeMember member)
            {


                if (string.IsNullOrEmpty(subKey))
                {

                    List<string> comments = new List<string>();

                    foreach (var comment in _list)
                    {

                        var txt = comment.Item2();

                        foreach (var item in txt.Split('\r'))
                            if (!string.IsNullOrEmpty(item.Trim()) && !string.IsNullOrEmpty(item.Trim()))
                                comments.Add(item);

                    }

                    member.Comments.Add(new CodeCommentStatement("<" + key + ">", true));
                    foreach (var item in comments)
                        if (!string.IsNullOrEmpty(item.Trim()) && !string.IsNullOrEmpty(item.Trim()))
                            member.Comments.Add(new CodeCommentStatement(item.Trim('\n'), true));
                    member.Comments.Add(new CodeCommentStatement("</" + key + ">", true));
                
                }
                else
                {

                    foreach (var comment in _list)
                    {

                        List<string> comments = new List<string>();

                        var txt1 = comment.Item1();
                        var txt2 = comment.Item2();

                        foreach (var item in txt2.Split('\r'))
                            if (!string.IsNullOrEmpty(item.Trim()) && !string.IsNullOrEmpty(item.Trim()))
                                comments.Add(item);


                        member.Comments.Add(new CodeCommentStatement($"<{key} {subKey}=\"{txt1}\">", true));                       
                        foreach (var item in comments)
                            if (!string.IsNullOrEmpty(item.Trim()) && !string.IsNullOrEmpty(item.Trim()))
                                member.Comments.Add(new CodeCommentStatement(item.Trim('\n'), true));
                        member.Comments.Add(new CodeCommentStatement("</" + key + ">", true));


                    }                  

                }
            }

            private readonly List<(Func<string>, Func<string>)> _list;

        }

    }

}
