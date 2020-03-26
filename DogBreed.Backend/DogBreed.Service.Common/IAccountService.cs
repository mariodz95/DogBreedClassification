using DogBreed.Model.Common;
using System.Threading.Tasks;

namespace DogBreed.Service.Common
{
    public interface IAccountService
    {
        Task<IUser> RegisterAsync(string name, string password);
        Task<IUser> LoginAsync(string name, string password);
    }
}
