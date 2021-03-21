using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebConverter.Context;
using WebConverter.Models;

namespace WebConverter.Services
{
    /// <summary>
    /// Data Base Register Service
    /// </summary>
    public class DbRegisterService : IRegisterDbService
    {
        private readonly UsersContext usersContext;

        /// <summary>
        /// DbRegisterService constructor
        /// </summary>
        /// <param name="usersContext"></param>
        public DbRegisterService(UsersContext usersContext)
        {
            this.usersContext = usersContext;
        }

        /// <summary>
        /// Checks if user is authenticated.
        /// </summary>
        /// <param name="login">Login.</param>
        /// <param name="password">Password.</param>
        /// <returns>User</returns>
        public async Task<User> Auth(string login, string password)
        {
            return usersContext.Users.FirstOrDefault(
                credentials =>
                    credentials.Login == login && credentials.Password == password);
        }

        /// <summary>
        /// Register an user and add to data base.
        /// </summary>
        /// <param name="register">Register</param>
        /// <param name="token">CancellationToken</param>
        /// <returns>Task</returns>
        public async Task Register(Register register, CancellationToken token)
        {
            User userRegister = usersContext.Users.FirstOrDefault(
                credentials =>
                    credentials.Login == register.Login);

            if (userRegister == null)
            {
                userRegister = new User()
                {
                    Login = register.Login,
                    Password = register.Password
                };
                await usersContext.Users.AddAsync(userRegister);
                await usersContext.SaveChangesAsync();
            }

        }
    }
}
