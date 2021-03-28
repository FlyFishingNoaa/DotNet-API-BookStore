using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore_Api.Data.DTOs;

namespace BookStore_Api.Data.Mappings
{
    public class Maps : Profile
    {

        public Maps()
        {
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Author, AuthorCreateDTO>().ReverseMap();
        }
    }
}
