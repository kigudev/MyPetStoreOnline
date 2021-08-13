using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MyPetStore.Web.Services.Abstractions
{
    public interface IFileService
    {
        Task<string> SaveImageAsync(IFormFile image);
    }
}