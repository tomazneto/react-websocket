using ProvaConceitoSignalR.Model;

namespace ProvaConceitoSignalR.Infra
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryBase<Requisicao> RequisicaoRepository { get; }
        Task<int> CommitChanges();
        void BegintTransaction();
        Task CommitTransaction();
        void Rollback();
    }
}
