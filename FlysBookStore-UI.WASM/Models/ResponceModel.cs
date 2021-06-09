using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlysBookStore_UI.WASM.Models
{
    public class ResponceModel
    {
        public bool Success { get; set; }

        public string Error { get; set; }

        public string Content { get; set; }

    }
}
