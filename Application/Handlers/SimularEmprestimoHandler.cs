using Application.Commands;
using Application.Responses;
using MediatR;
using Domain.Entities;
using Application.UseCases;
using Application.Dtos;
using Application.Exceptions;
using Application.Common.Interfaces;
using System.Text.Json;

namespace Application.Handlers
{
    public class SimularEmprestimoHandler :
        IRequestHandler<SimularEmprestimoCommand, SimulacaoEmprestimoResponse>
    {
        private readonly IProdutoEmprestimoRepository _produtoEmprestimoRepository;
        private readonly IPriceCalculatorService _priceCalculatorUseCase;
        private readonly ICacheService _cache;

        public SimularEmprestimoHandler(
            IProdutoEmprestimoRepository produtoEmprestimoRepository,
            IPriceCalculatorService priceCalculatorUseCase,
            ICacheService cache)
        {
            _produtoEmprestimoRepository = produtoEmprestimoRepository;
            _priceCalculatorUseCase = priceCalculatorUseCase;
            _cache = cache;
        }

        public async Task<SimulacaoEmprestimoResponse> Handle(
            SimularEmprestimoCommand command, CancellationToken cancellationToken)
        {
            var cacheKey = $"simulacao:{command.IdProduto}:{command.ValorSolicitado}:{command.PrazoMeses}";

            var cached = await _cache.GetAsync(cacheKey);
            if (!string.IsNullOrEmpty(cached))
            {
                return JsonSerializer.Deserialize<SimulacaoEmprestimoResponse>(cached)!;
            }


            ProdutoEmprestimo produtoEmprestimo = _produtoEmprestimoRepository.GetById(command.IdProduto)
                ?? throw new ProdutoEmprestimoNotFoundException(command.IdProduto);

            var produtoResponse = new ProdutoEmprestimoResponse
            {
                Id = produtoEmprestimo.Id,
                Nome = produtoEmprestimo.Nome,
                TaxaJurosAnual = produtoEmprestimo.TaxaJurosAnual,
                PrazoMaximoMeses = produtoEmprestimo.PrazoMaximoMeses
            };

            decimal txJurosEfetivaMensal = CalcularTxJurosEfetivaMensal(produtoEmprestimo.TaxaJurosAnual);

            List<DetalhesParcela> parcelas = _priceCalculatorUseCase.CalcularParcelas(
                            command.ValorSolicitado,
                            command.PrazoMeses,
                            txJurosEfetivaMensal);

            decimal parcelaMensal = _priceCalculatorUseCase.GetParcelaMensal(command.ValorSolicitado,
                            command.PrazoMeses,
                            txJurosEfetivaMensal);

            SimulacaoEmprestimoResponse response = new(
                produtoResponse,
                Math.Round(command.ValorSolicitado, 2),
                command.PrazoMeses,
                txJurosEfetivaMensal,
                parcelas.Sum(p => p.Juros + p.Amortizacao),
                Math.Round(parcelaMensal, 2),
                parcelas
            );

            await _cache.SetAsync(cacheKey, JsonSerializer.Serialize(response), TimeSpan.FromMinutes(5));

            return response;
        }

        private static decimal CalcularTxJurosEfetivaMensal(decimal txJurosAnual)
        {
            decimal txJurosEfetivaMensal = (decimal)Math.Pow((1 + (double)txJurosAnual), 1.0 / 12) - 1;
            return Math.Round(txJurosEfetivaMensal, 6);
        }
    }
}
