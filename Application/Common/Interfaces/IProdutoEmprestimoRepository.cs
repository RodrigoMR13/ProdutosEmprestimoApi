using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IProdutoEmprestimoRepository
    {
        IEnumerable<ProdutoEmprestimo> GetAll();
        ProdutoEmprestimo? GetById(long id);
        long Add(string nome, decimal txJurosAnual, short prazoMaxMeses);
        void Update(ProdutoEmprestimo produtoEmprestimo);
        void Delete(long id);
    }
}
