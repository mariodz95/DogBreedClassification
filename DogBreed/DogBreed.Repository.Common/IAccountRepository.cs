using DogBreed.Model.Common;
using System.Threading.Tasks;

namespace DogBreed.Repository.Common
{
    public interface IAccountRepository
    {
        Task<IUser> RegisterAsync(string name, string password);
        Task<IUser> LoginAsync(string name, string password);    
        Task<bool> CheckIfUserExist(string email);
    }
}
