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


        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;

        public AuthorRepository(HttpClient client, ILocalStorageService localstorage) :base(client, localstorage)
        {
            _client = client;
            _localStorage = localstorage;
        }

         
    }
}
