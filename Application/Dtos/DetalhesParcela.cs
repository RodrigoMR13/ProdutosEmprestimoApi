namespace Application.Dtos
{
    public class DetalhesParcela
    {
        public int NumeroParcela { get; set; }
        public decimal SaldoDevedorInicial { get; set; }
        public decimal Juros { get; set; }
        public decimal Amortizacao { get; set; }
        public decimal SaldoDevedorFinal { get; set; }

        public DetalhesParcela() { }

        public DetalhesParcela(int numParcela, decimal saldoDevedorInicial, decimal juros, decimal amortizacao,
            decimal saldoDevedorFinal)
        {
            NumeroParcela = numParcela;
            SaldoDevedorInicial = saldoDevedorInicial;
            Juros = juros;
            Amortizacao = amortizacao;
            SaldoDevedorFinal = saldoDevedorFinal;
        }
    }
}
