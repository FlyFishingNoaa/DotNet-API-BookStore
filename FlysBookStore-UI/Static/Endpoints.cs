using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlysBookStore_UI.Static
{
    public static class Endpoints
    {
        // 44311
        //44357
        // "applicationUrl": "http://localhost:63225",
        // public static string BaseUrl = "http://localhost:63225";
        public static string BaseUrl = "http://localhost:44357";
        public static string AuthorsEndpoint = $"{BaseUrl}api/authors/";
        public static string BooksEndpoint = $"{BaseUrl}api/books/";
        //public static string RegisterEndpoint = $"{BaseUrl}api/users/register/";
        public static string RegisterEndpoint = $"{BaseUrl}api/Users/register/";


    }
}
