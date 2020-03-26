using DogBreed.Model.Common;
using DogBreed.Repository.Common;
using DogBreed.Service.Common;
using System.Threading.Tasks;

namespace DogBreed.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IUser> RegisterAsync(string email, string password)
        {
            return await _accountRepository.RegisterAsync(email, password);
        }

        public async Task<IUser> LoginAsync(string email, string password)
        {
            return await _accountRepository.LoginAsync(email, password);
        }
    }
}
