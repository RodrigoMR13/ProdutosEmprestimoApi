namespace Domain.Entities
{
    public class ProdutoEmprestimo
    {
        public long Id { get; private set; }
        public string Nome { get; private set; }
        public decimal TaxaJurosAnual { get; private set; }
        public short PrazoMaximoMeses { get; private set; }

        public ProdutoEmprestimo(long id, string nome, decimal txJurosAnual, short prazoMaximoMeses)
        {
            Id = id;
            Nome = nome;
            TaxaJurosAnual = txJurosAnual;
            PrazoMaximoMeses = prazoMaximoMeses;
        }
    }
}
