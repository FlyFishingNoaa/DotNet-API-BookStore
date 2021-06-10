using FlysBookStore_UI.WASM.Contracts;
using FlysBookStore_UI.WASM.Models;
using FlysBookStore_UI.WASM.Static;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using FlysBookStore_UI.WASM.Providers;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace FlysBookStore_UI.WASM.Service
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        //private readonly IHttpClientFactory _client;
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        

        public AuthenticationRepository(HttpClient client, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _client = client;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;

        }



        public async Task<bool> Login(LoginModel user)
        {
            var response = await _client.PostAsJsonAsync(Endpoints.LogInEndpoint, user);
            Console.WriteLine(response.StatusCode);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var content = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<TokenResponce>(content);

            //Store Token
            await _localStorage.SetItemAsync("authToken", token.Token);

            //Change auth state of app
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider)
                .LoggedIn();

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", token.Token);

            return true;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider)
                .LoggedOut();
        }

        public async Task<bool> Register(RegistrationModel user)
        {
            var response = await _client.PostAsJsonAsync(Endpoints.RegisterEndpoint
                , user);
            return response.IsSuccessStatusCode;
        }


        //public async Task<bool> Register(RegistrationModel user)
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Post, Endpoints.RegisterEndpoint);
        //    request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

        //    var client = _client.CreateClient();
        //    HttpResponseMessage responce = await client.SendAsync(request);

        //    return responce.IsSuccessStatusCode;

        //}



        //public async Task<bool> Login(LoginModel user)
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Post, Endpoints.LogInEndpoint);
        //    request.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

        //    var client = _client.CreateClient();
        //    HttpResponseMessage responce = await client.SendAsync(request);

        //    if (!responce.IsSuccessStatusCode)
        //    {
        //        return false;
        //    }

        //    var content = await responce.Content.ReadAsStringAsync();
        //    var token = JsonConvert.DeserializeObject<TokenResponce>(content);

        //    //Store Token
        //    await _localStorage.SetItemAsync("authToken", token.Token);

        //    //Change auth state of app
        //   await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();

        //    client.DefaultRequestHeaders.Authorization =
        //        new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token.Token);
        //    return true;           
        //}



        //public async Task Logout()
        //{
        //    await _localStorage.RemoveItemAsync("authToken");
        //     ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
        //}

    }
}
