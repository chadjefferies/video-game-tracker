using System.Data;

namespace Example.VideoGameTracker.Api.DataAccess
{
    public class InMemoryTransaction : IDbTransaction
    {
        public IDbConnection? Connection => throw new NotImplementedException();

        public IsolationLevel IsolationLevel => throw new NotImplementedException();

        public void Commit() { }

        public void Dispose() { }

        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
