namespace Application.Responses
{
    public class ProdutoEmprestimoResponse
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public decimal TaxaJurosAnual { get; set; }
        public short PrazoMaximoMeses { get; set; }

        public ProdutoEmprestimoResponse() { }

        public ProdutoEmprestimoResponse(long id, string nome, decimal txJurosAnual, short prazoMaxMeses)
        {
            Id = id;
            Nome = nome;
            TaxaJurosAnual = txJurosAnual;
            PrazoMaximoMeses = prazoMaxMeses;
        }
    }
}
