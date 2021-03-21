using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebConverter.Models;

namespace WebConverter.Services
{
    /// <summary>
    /// Register service interface
    /// </summary>
    public interface IRegisterService
    {
        /// <summary>
        /// Authentication method that checks is credentials is present
        /// </summary>
        /// <param name="login">login</param>
        /// <param name="password">password</param>
        /// <returns>Task of Register model</returns>
        public Task<Register> Auth(string login, string password);

        /// <summary>
        /// Register an user
        /// </summary>
        /// <param name="register">Register model</param>
        /// <returns>Task</returns>
        public Task Register(Register register);
    }
}
