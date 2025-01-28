namespace API_WebLocalize.Models
{                       // Isso é um modelo de resposta padrão
    public class ResponseModel<T>
    {
        public T? Dados {get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
    }
}
