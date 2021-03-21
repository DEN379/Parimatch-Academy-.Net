using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebConverter.Models;

namespace WebConverter.Services
{
    /// <summary>
    /// Service that register user
    /// </summary>
    public class RegisterService : IRegisterService
    {
        private readonly List<Register> users;

        /// <summary>
        /// Register service constructor
        /// </summary>
        public RegisterService()
        {
            users = new List<Register>();
        }

        /// <summary>
        /// Authentication method that checks is credentials is present
        /// </summary>
        /// <param name="login">login</param>
        /// <param name="password">password</param>
        /// <returns>Task of Register model</returns>
        public async Task<Register> Auth(string login, string password)
        {
            return users.FirstOrDefault(
                credentials =>
                    credentials.Login == login && credentials.Password == password);
        }

        /// <summary>
        /// Register an user
        /// </summary>
        /// <param name="register">Register model</param>
        /// <returns>Task</returns>
        public async Task Register(Register register)
        {
            users.Add(register);
        }
    }
}
