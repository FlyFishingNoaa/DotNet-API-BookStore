using FlysBookStore_UI.WASM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlysBookStore_UI.WASM.Contracts
{
    public interface IBookRepository : IBaseRepository<Book>
    {
    }
}
