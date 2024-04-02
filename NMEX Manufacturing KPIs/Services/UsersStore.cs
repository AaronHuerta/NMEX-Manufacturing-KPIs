using Microsoft.AspNetCore.Identity;
using NMEX_Manufacturing_KPIs.Models.Module_User;

namespace NMEX_Manufacturing_KPIs.Services
{
    public class UsersStore : IUserStore<Users>, IUserEmailStore<Users>, IUserPasswordStore<Users>
    {
        private readonly IRepositorioUsers repositorioUsers;

        public UsersStore(IRepositorioUsers repositorioUsers)
        {
            this.repositorioUsers = repositorioUsers;
        }

        //Metodo
        public async Task<IdentityResult> CreateAsync(Users user, CancellationToken cancellationToken)
        {
            user.User_id = await repositorioUsers.CreateUser(user);
            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //Se comento porque lanzaba error al ejecutar
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        //Metodo
        public async Task<Users> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return await repositorioUsers.SearchUserByEmail(normalizedEmail);
        }

        public Task<Users> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //Metodo Verificacion Login
        public async Task<Users> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            //return await repositorioUsers.SearchUserByEmail(normalizedUserName);
            return await repositorioUsers.SearchUserByIdUserNissan(normalizedUserName);
        }

        //Metodo
        public Task<string> GetEmailAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedEmailAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //Metodo
        public Task<string> GetPasswordHashAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Password);
        }

        //Metodo
        public Task<string> GetUserIdAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.User_id.ToString());
        }

        //Metodo
        public Task<string> GetUserNameAsync(Users user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> HasPasswordAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(Users user, string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(Users user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //Metodo
        public Task SetNormalizedEmailAsync(Users user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.EmailNormalized = normalizedEmail;
            return Task.CompletedTask;
        }

        //Metodo
        public Task SetNormalizedUserNameAsync(Users user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        //Metodo
        public Task SetPasswordHashAsync(Users user, string passwordHash, CancellationToken cancellationToken)
        {
            user.Password = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(Users user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(Users user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
