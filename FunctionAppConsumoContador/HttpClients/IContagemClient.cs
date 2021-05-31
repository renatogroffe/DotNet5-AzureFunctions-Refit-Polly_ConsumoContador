using System.Threading.Tasks;
using Refit;
using FunctionAppConsumoContador.Models;

namespace FunctionAppConsumoContador.HttpClients
{
    public interface IContagemClient
    {
         [Get("")]
         Task<ResultadoContador> GetResultadoAsync();
    }
}