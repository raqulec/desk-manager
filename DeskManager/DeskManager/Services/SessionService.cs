using DeskManager.Models;
using DeskManager.Repository;
using DeskManager.Utils;

namespace DeskManager.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtUtils _jwtUtils;

        public SessionService(IUserRepository userRepository, JwtUtils jwtUtils)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
        }

        public async Task<string> Authenticate(Login request)
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
