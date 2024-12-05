using DeskManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeskManager.Repository
{
    public interface IUserRepository
    {
        Task<User> Register(User request);
        Task<string> Login(Login request);
    }
}



//https://medium.com/@lokeshprajapat742000/asp-net-core-web-api-using-repository-pattern-37479725752a