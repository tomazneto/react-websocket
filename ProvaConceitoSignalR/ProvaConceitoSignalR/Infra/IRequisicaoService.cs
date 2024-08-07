using ProvaConceitoSignalR.Model;

namespace ProvaConceitoSignalR.Infra
{
    public interface IRequisicaoService
    {
        Task Inserir(Requisicao requisicao);
    }
}
