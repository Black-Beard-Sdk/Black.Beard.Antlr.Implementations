using Bb.Generators;
using System.CodeDom;

namespace Generate.ModelsScripts
{
    public class CodeMarker
    {

        public CodeMarker(LevelBloc bloc)
        {
            this._bloc = bloc;

            this._position = this._bloc.Code.Count;

        }


        public void Append(CodeExpression e)
        {
            this._bloc.Code.Insert(this._position++, e.AsStatement());
        }

        public void Append(CodeStatement s)
        {
            this._bloc.Code.Insert(this._position++, s);
        }


        private readonly LevelBloc _bloc;
        private int _position;

    }


}
