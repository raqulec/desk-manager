using DeskManager.Models;
using DeskManager.Repository;
using DeskManager.Utils;

namespace DeskManager.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtUtils _jwtUtils;

        public UserService(IUserRepository userRepository, JwtUtils jwtUtils)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
        }

        public async Task<User> RegisterUser(User request)
        {
            return await _userRepository.CreateUser(request);
        }

        public async Task<string> LoginUser(Login request)
        {
            var user = await _userRepository.AuthenticateUser(request);

            var token = _jwtUtils.GenerateJwtToken(user);
            return token;
        }
    }
}
