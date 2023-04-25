using Microsoft.Extensions.Primitives;

namespace Bb.SqlServer.Client
{

    public interface ISqlServerWatcher : IDisposable
    {
        IChangeToken Watch();

    }

}