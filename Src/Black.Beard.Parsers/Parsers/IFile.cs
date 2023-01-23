namespace Bb.Parsers
{


    public interface Initializing : IFile
    {

        void Initialize(Antlr4.Runtime.Parser parser, Diagnostics diagnostics, string path);

    }

    public interface IFile
    {

        string Filename { get; }

    }

}
