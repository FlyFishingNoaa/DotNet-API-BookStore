using BookStore_Api.Data;
using BookStore_Api.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_Api.Contracts
{
    public interface IAuthorRepository : IRepositoryBase<Author>
    {
       // Task Create(AuthorCreateDTO author);
    }
}
