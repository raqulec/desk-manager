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

        public async Task<User> CreateUser(User request)
        {
            var existingUser = await _userRepository.GetUserByEmail(request.Email);

            if (existingUser != null)
            {
                throw new ArgumentException("Email is already taken");
            }

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            return await _userRepository.AddUser(user);
        }

        public async Task<string> AuthenticateUser(Login request)
        {
            var user = await _userRepository.GetUserByEmail(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                throw new ArgumentException("Email or password is incorrect");
            }

            var token = _jwtUtils.GenerateJwtToken(user);
            return token;
        }
    }
}
