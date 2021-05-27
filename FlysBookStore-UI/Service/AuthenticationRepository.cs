using FlysBookStore_UI.Contracts;
using FlysBookStore_UI.Models;
using FlysBookStore_UI.Static;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FlysBookStore_UI.Service
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IHttpClientFactory _client;
        




        public AuthenticationRepository(IHttpClientFactory client)
        {
            _client = client;
        }


        public async Task<bool> Register(RegistrationModel user)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Endpoints.RegisterEndpoint);
            request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var client = _client.CreateClient();
            HttpResponseMessage responce = await client.SendAsync(request);

            return responce.IsSuccessStatusCode;

        }
    }
}
