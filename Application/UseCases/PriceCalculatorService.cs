using Application.Dtos;

namespace Application.UseCases
{
    public class PriceCalculatorService : IPriceCalculatorService
    {
        public List<DetalhesParcela> CalcularParcelas(decimal valor, int periodo, decimal taxaJurosEfetivaMensal)
        {
            try
            {   
                List<DetalhesParcela> parcelas = [];

                decimal saldoDevedor = valor;

                decimal parcelaMensal = GetParcelaMensal(valor, periodo, taxaJurosEfetivaMensal);

                for (int n = 1; n <= periodo; n++)
                {
                    decimal juros = saldoDevedor * taxaJurosEfetivaMensal;
                    decimal amortizacao = parcelaMensal - juros;
                    decimal saldoDevedorFinal = saldoDevedor - amortizacao;

                    parcelas.Add(new DetalhesParcela(n, Math.Round(saldoDevedor, 2), Math.Round(juros, 2),
                        Math.Round(amortizacao, 2), Math.Round(saldoDevedorFinal, 2)));

                    saldoDevedor = saldoDevedorFinal;
                }

                return parcelas;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public decimal GetParcelaMensal(decimal valor, int periodo, decimal taxaJurosEfetivaMensal)
        {
            try
            {
                decimal jurosTotais = (decimal)Math.Pow(1 + (double)taxaJurosEfetivaMensal, periodo);

                decimal parcelaMensal = valor * (jurosTotais * taxaJurosEfetivaMensal / (jurosTotais - 1));

                return parcelaMensal;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
