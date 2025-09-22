namespace Application.Exceptions
{
    public class ProdutoEmprestimoNotFoundException(long id) : Exception($"Produto Empréstimo com Id {id} não foi encontrado.")
    {
        public long Id { get; set; } = id;
    }
}
