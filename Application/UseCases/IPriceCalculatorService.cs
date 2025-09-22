using Application.Dtos;

namespace Application.UseCases
{
    public interface IPriceCalculatorService
    {
        List<DetalhesParcela> CalcularParcelas(decimal valor, int periodo, decimal taxaJuros);
        decimal GetParcelaMensal(decimal valor, int periodo, decimal taxaJurosEfetivaMensal);
    }
}
