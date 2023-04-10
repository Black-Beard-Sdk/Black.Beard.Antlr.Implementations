using System.CodeDom;

namespace Bb.Generators
{
    public class CodeClassNested : CodeSnippetTypeMember
    {

        public CodeClassNested(CodeTypeDeclarationCollection items)
        {
            this._items = items;
        }

        public CodeTypeDeclarationCollection Items => _items;

        private readonly CodeTypeDeclarationCollection _items;

    }

}
