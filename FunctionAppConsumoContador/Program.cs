using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Refit;
using FunctionAppConsumoContador.HttpClients;

namespace FunctionAppConsumoContador
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices((services) =>
                {
                    var retryPolicy = Policy
                        .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                        .RetryAsync(2, onRetry: (message, retryCount) =>
                        {
                            Console.Out.WriteLine($" ** RequestMessage: {message.Result.RequestMessage}");
                            Console.Out.WriteLine($" ** Content: {message.Result.Content.ReadAsStringAsync().Result}");
                            Console.Out.WriteLine($" ** ReasonPhrase: {message.Result.ReasonPhrase}");
                            Console.Out.WriteLine($" ** Retentativa: {retryCount}");
                        });

                    services.AddRefitClient<IContagemClient>()
                        .ConfigureHttpClient(
                            c => c.BaseAddress = new Uri(Environment.GetEnvironmentVariable("EndpointContador")))
                        .AddPolicyHandler(retryPolicy);
                })
                .Build();

            host.Run();
        }
    }
}