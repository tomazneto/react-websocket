using Microsoft.EntityFrameworkCore.Storage;
using ProvaConceitoSignalR.Model;

namespace ProvaConceitoSignalR.Infra
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepositoryBase<Requisicao> RequisicaoRepository { get; }

        #region [context]
        private Contexto Context { get; set; }
        private IDbContextTransaction Transaction { get; set; }
        #endregion

        #region [methods]
        public async Task<int> CommitChanges()
        {
            try
            {
                return await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                throw new Exception($"Erro ao salvar dados. {message}");
            }
        }

        public void BegintTransaction()
        {
            Transaction = Context.Database.BeginTransaction();
        }

        public async Task CommitTransaction()
        {
            await CommitChanges();
            Context.Database.CommitTransaction();
            DisposeTransaction();
        }

        public void Rollback()
        {
            if (Transaction != null)
            {
                Context.Database.RollbackTransaction();
                Transaction.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            DisposeTransaction();
            Context = null;
        }

        private void DisposeTransaction()
        {
            if (Transaction != null)
            {
                Transaction.Dispose();
                Transaction = null;
            }
        }
        #endregion

        #region [constructor]
        public UnitOfWork(IConfiguration configuration, Contexto context)
        {
            Context = context;
            RequisicaoRepository = new RepositoryBase<Requisicao>(Context);
        }
        #endregion
    }
}
