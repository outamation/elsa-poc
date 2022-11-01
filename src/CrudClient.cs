using RestSharp;

namespace ElsaQuickstarts.Server.DashboardAndServer
{
    public class CrudClient
    {
        readonly RestClient _client;

        class Request
        {
            public string Message { get; set; } = String.Empty;
        }

        private CrudClient()
        {
            _client = new RestClient("https://crudcrud.com/api/483c9edba37b480a9e016fa079b0c597/");
        }

        public Task SendAsync(string name, string message)
            => _client.PostJsonAsync<Request>("messages", new Request { Message = $"Hello {name}, {message}" });

        public static readonly CrudClient Instance = new CrudClient();
    }
}
