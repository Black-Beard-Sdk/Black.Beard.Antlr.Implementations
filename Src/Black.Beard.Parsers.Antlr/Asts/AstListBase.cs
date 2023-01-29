﻿using Antlr4.Runtime;
using System.Collections;
using System.Text;

namespace Bb.Asts
{


    public abstract class AstListBase<T> : AstBase, IEnumerable<T>
        where T : AstBase
    {

        public AstListBase(ParserRuleContext ctx, int capacity)
            : base(ctx)
        {
            this._list = new List<T>(capacity);
            this.IsList = true;
        }



        public T this[int item] { get => _list[item]; }


        public AstListBase(ParserRuleContext ctx)
            : base(ctx)
        {
            this._list = new List<T>();
        }

        public void Add(T item)
        {
            item.Parent = this;
            this._list.Add(item);
        }

        public void AddRange(params T[] items)
        {
            foreach (var item in items)
                Add(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this._list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._list.GetEnumerator();
        }

        public override bool ContainsOneTerminal
        {
            get
            {
                if (this.Count == 1)
                    return this[0].IsTerminal;
                return false;
            }
        }

        public override bool ContainsOneRule
        {
            get
            {
                if (this.Count == 1)
                    return this[0].IsRuleReference;
                return false;
            }
        }

        public override bool ContainsOnlyTerminals
        {
            get
            {

                if (this.Count == 0)
                    return false;

                foreach (var item in this)
                {
                    if (!item.ContainsOnlyTerminals)
                        return false;
                }

                return true;

            }
        }

        public override bool ContainsTerminals
        {
            get
            {
                if (this.Count == 0)
                    return false;

                foreach (var item in this)
                {
                    if (item.ContainsTerminals)
                        return true;
                }

                return false;

            }
        }

        public override bool ContainsOnlyRuleReferences
        {
            get
            {
                if (this.Count == 0)
                    return false;

                foreach (var item in this)
                    if (!item.ContainsOnlyRuleReferences)
                        return false;

                return true;

            }
        }

        public override AstTerminalText GetTerminal()
        {
            if (this._list.Count == 1)
                if (this._list[0].IsTerminal)
                    return this._list[0].GetTerminal();

            return null;

        }

        public override void ToString(Writer wrt)
        {

            var strategy = wrt.Strategy.GetStrategyFrom(this);

            string comma = string.Empty;

            foreach (T item in _list)
            {


                wrt.Append(comma);

                item.ToString(wrt);

                if (strategy.ReturnLineAfterItems)
                    wrt.AppendEndLine();

                comma = _charSplit;

            }

        }

        public OccurenceEnum Occurence { get; set; }

        public int Count => _list.Count;

        private readonly List<T> _list;

        protected string _charSplit = " ";

    }

    public enum OccurenceEnum
    {
        One,
        OneOptional,
        OneOrMore,
        OneOrMoreOptional,
        Any,
        AnyOptional
    }


}
