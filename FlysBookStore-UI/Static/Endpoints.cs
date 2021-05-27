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
       // public static string BaseUrl = "http://localhost:44357";
        public static string BaseUrl = "https://localhost:44357";
        public static string AuthorsEndpoint = $"{BaseUrl}api/authors/";
        public static string BooksEndpoint = $"{BaseUrl}api/books/";
        //Bad - Invalid URI: Invalid port specified.
        //public static string RegisterEndpoint = $"{BaseUrl}api/users/register/";
        //Good -needs the / before api
        public static string RegisterEndpoint = $"{BaseUrl}/api/users/register/";


    }
}
