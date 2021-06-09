using Blazored.LocalStorage;
using FlysBookStore_UI.WASM.Contracts;
using FlysBookStore_UI.WASM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FlysBookStore_UI.WASM.Service
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {


        private readonly IHttpClientFactory _client;
        private readonly ILocalStorageService _localStorage;

        public AuthorRepository(IHttpClientFactory client, ILocalStorageService localstorage) :base(client, localstorage)
        {
            _client = client;
            _localStorage = localstorage;
        }

         
    }
}
