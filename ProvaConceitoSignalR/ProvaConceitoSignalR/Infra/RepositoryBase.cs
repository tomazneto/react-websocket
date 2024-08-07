using Microsoft.EntityFrameworkCore;

namespace ProvaConceitoSignalR.Infra
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly Contexto Context;
        public DbSet<T> Model { get; }

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public RepositoryBase(Contexto context)
        {
            Context = context;
            Model = Context.Set<T>();
        }

        public async Task<T> GetById(int id)
        {
            return await Model.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Task.FromResult(Model.AsNoTracking());
        }

        public async Task Add(T model)
        {
            await Model.AddAsync(model);
        }

        public async Task Update(T model)
        {
            await Task.Run(() =>
            {
                Context.Entry(model).State = EntityState.Modified;
                Model.Update(model);
            });
        }

        public void Remove(T model)
        {
            Model.Remove(model);
        }

    }
}
