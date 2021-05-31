using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using FunctionAppConsumoContador.HttpClients;

namespace FunctionAppConsumoContador
{
    public class TimerTriggerConsumoContadorRefit
    {
        public readonly IContagemClient _contagemClient;

        public TimerTriggerConsumoContadorRefit(IContagemClient contagemClient)
        {
            _contagemClient = contagemClient;
        }

        [Function("TimerTriggerConsumoContadorRefit")]
        public void Run([TimerTrigger("*/5 * * * * *")] FunctionContext context)
        {
            var logger = context.GetLogger("TimerTriggerConsumoContadorRefit");
            
            var resultado = _contagemClient.GetResultadoAsync().Result;
            logger.LogInformation($" ## Valor do contador = {resultado.ValorAtual}");
            logger.LogInformation($" ## Saudação = {resultado.Saudacao}");
            logger.LogInformation($" ## Aviso = {resultado.Aviso}");
            logger.LogInformation($" ## Local = {resultado.Local}");
            logger.LogInformation($" ## Kernel = {resultado.Kernel}");
            logger.LogInformation($" ## Target Framework = {resultado.TargetFramework}");
        }
    }
}