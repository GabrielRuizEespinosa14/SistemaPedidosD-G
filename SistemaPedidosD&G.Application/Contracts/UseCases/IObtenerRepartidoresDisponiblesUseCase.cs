using SistemaPedidosD_G.Application.DTO;

namespace SistemaPedidosD_G.Application.Contracts.UseCases;

public interface IObtenerRepartidoresDisponiblesUseCase
{
    Task<IEnumerable<RepartidorDTO>> EjecutarAsync();
}