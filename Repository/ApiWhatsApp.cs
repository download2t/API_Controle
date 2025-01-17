namespace API_CONTROLE.Repository
{
    public class ApiWhatsApp
    {
        public string Telefone { get; set; }
        public string ApiKey { get; set; }
        public string ApiPath { get; set; }

        public ApiWhatsApp()
        {
            Telefone = string.Empty;
            ApiKey = string.Empty;
            ApiPath = string.Empty;
        }

        public ApiWhatsApp(string telefone, string apiKey, string apiPath)
        {
            Telefone = telefone;
            ApiKey = apiKey;
            ApiPath = apiPath;
        }
    }
}
