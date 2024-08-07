namespace ProvaConceitoSignalR.Infra
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task Add(T T);
        Task Update(T T);
        void Remove(T T);
    }
}
