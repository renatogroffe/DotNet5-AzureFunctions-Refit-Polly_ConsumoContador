namespace FunctionAppConsumoContador.Models
{
    public class ResultadoContador
    {
        public int ValorAtual { get; set; }
        public string Saudacao { get; set; }
        public string Aviso { get; set; }
        public string Local { get; set; }
        public string Kernel { get; set; }
        public string TargetFramework { get; set; }
    }
}