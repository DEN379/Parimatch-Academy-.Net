using System.Threading;
using System.Threading.Tasks;
using WebConverter.Context;
using WebConverter.Models;

namespace WebConverter.Services
{
    /// <summary>
    /// IRegisterDbService
    /// </summary>
    public interface IRegisterDbService
    {
        /// <summary>
        /// Authentication method that checks is credentials is present
        /// </summary>
        /// <param name="login">login</param>
        /// <param name="password">password</param>
        /// <returns>Task of Register model</returns>
        public Task<User> Auth(string login, string password);

        /// <summary>
        /// Register an user
        /// </summary>
        /// <param name="register">Register model</param>
        /// <param name="token">CancellationToken</param>
        /// <returns>Task</returns>
        public Task Register(Register register, CancellationToken token);
    }
}