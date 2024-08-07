using ProvaConceitoSignalR.Model;

namespace ProvaConceitoSignalR.Infra
{
    public class RequisicaoService(IUnitOfWork unitOfWork) : IRequisicaoService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Inserir(Requisicao requisicao)
        {
            if (requisicao != null)
            {
                await _unitOfWork.RequisicaoRepository.Add(requisicao).ConfigureAwait(false);
                await _unitOfWork.CommitChanges().ConfigureAwait(false);
            }
        }
    }
}
