﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlysBookStore_UI.Static
{
    public static class Endpoints
    {

        public static string BaseUrl = "http://localhost:63225";
        public static string AuthorsEndpoint = $"{BaseUrl}api/authors/";
        public static string BooksEndpoint = $"{BaseUrl}api/books/";
        public static string RegisterEndpoint = $"{BaseUrl }api/users/reqister";


    }
}
