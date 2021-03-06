using MyPetStore.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPetStore.Client.Services.Interfaces
{
    public interface IProductApi
    {
        Task<List<ProductDto>> GetAllAsync(string search, string brand, string order = "asc");
    }
}
