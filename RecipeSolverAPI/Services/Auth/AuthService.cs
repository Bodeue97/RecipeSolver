using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using RecipeSolverAPI.Data;
using RecipeSolverAPI.Data.DataModels;
using RecipeSolverAPI.Models.Auth;
using RecipeSolverAPI.Models.Email;
using RecipeSolverAPI.Models.User;
using RecipeSolverAPI.Services.Mailer;
using RecipeSolverAPI.Templates;

namespace RecipeSolverAPI.Services.Auth
{
    public class AuthService : IAuthService
    {
        private DataContext _context;
        private IConfiguration _config;
        private IHttpContextAccessor _httpContextAccessor;
        private IMailerService _mailerService;
        private ITemplate _template;

        public AuthService(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IMailerService mailerService, ITemplate template)
        {
            _context = context;
            _config = configuration;
            _httpContextAccessor = httpContextAccessor;
            _mailerService = mailerService;
            _template = template;
        }

        public async Task<string> Register(UserRegisterRequest request)
        {
            try
            {
                if (_context.Users.Any(items => items.Email == request.Email))
                {
                    throw new Exception("user already exists");
                }

                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                string randomToken = CreateRandomToken();
                string verifyToken = CreateRandomToken();

                User user = new()
                {
                    Name = request.Name,
                    Surname = request.Surname,
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    VerificationToken = randomToken,

                };

                await _context.Users.AddAsync(user);

                // Wysłanie maila z wygenerowanym już linkem do weryfikacji konta (chwilowo url backend później przekierownaie na front-a).               
                string url = $"{_config.GetSection("AppSettings:WebsiteUrl").Value!}/verify/{randomToken}";
                string template = string.Empty;
                string subject = string.Empty;



                template = _template.GenerateEmailVerifyTemplatePL(url, $"{request.Name} {request.Surname}");
                subject = "RecipeSolver, weryfikacja.";



                var email = new EmailDto
                {
                    To = request.Email,
                    Body = template,
                    Subject = subject
                };

                await Task.Run(() => _mailerService.SendEmail(email));
                await _context.SaveChangesAsync();

                return "User created";
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }
        public async Task<string> Verify(string verificationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.VerificationToken!.Equals(verificationToken)) ?? throw new Exception("Invalid token");

            user.VerifiedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return "User verified";
        }

        public async Task<UserDto> Login(UserLoginRequest request)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(user => user.Email.Equals(request.Email)) ?? throw new Exception("User not found");

                if (user.VerifiedAt == null)
                {
                    throw new Exception("Email not verified");
                }

                if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                    throw new Exception("Incorrect password");

                RefreshTokenDto tokens = SetTokens(user);

                return new UserDto
                {
                    Name = user.Name!,
                    Surname = user.Surname!,
                    Email = request.Email,
                    Token = tokens.Token,
                    RefreshToken = tokens.RefreshToken,
                    VerificationToken = user.VerificationToken!,

                };
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }
        public async Task<string> ForgotPassword(ForgotPasswordRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(items => items.Email.Equals(request.Email)) ?? throw new Exception("User not found");

            string randomToken = CreateRandomToken();
            user.PasswordResetToken = randomToken;
            await _context.SaveChangesAsync();

            //Wysłanie maila z wygenerowanym już linkem do weryfikacji konta (chwilowo url backend później przekierownaie na front-a).
            string url = $"{_config.GetSection("AppSettings:WebsiteUrl").Value!}/reset-password/{randomToken}";

            string template = string.Empty;
            string subject = string.Empty;

            
            
                    template = _template.GenerateEmailResetPasswordTemplatePL(url, user.Name!);
                    subject = "RecipeSolver, zmiana hasła.";
             

            var _email = new EmailDto
            {
                To = request.Email,
                Body = template,
                Subject = subject
            };

            await Task.Run(() => _mailerService.SendEmail(_email));

            return "Request sent";
        }

        public async Task<string> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.PasswordResetToken!.Equals(request.Token)) ?? throw new Exception("Invalid token");

            if (user.ResetTokenExpires! < DateTime.Now)
            {
                throw new Exception("Token expired");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _context.SaveChangesAsync();

            return "Password changed";
        }
         public async Task<string> ChangePassword(ChangePasswordRequest request)
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                throw new Exception("Problem with Claim in HttpContext.");
            }
            //Pobranie maila z Claim
            string email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)!;
            var user = await _context.Users.Where(items => items.Email.Equals(email)).FirstOrDefaultAsync() ?? throw new Exception("User not found");

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _context.SaveChangesAsync();

            return "Password changed";
        }


        private string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(Int32.Parse(_config.GetSection("AppSettings:TokenExpiresMin").Value!)),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
          public async Task<UserDto> RefreshToken(TokenRequest request)
        {
            try
            {
                if (_httpContextAccessor.HttpContext == null)
                {
                    throw new AuthenticationException("Problem with Claim in HttpContext");
                }
                //Pobranie maila z Claim (z token-a)
                string email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)!;
                var user = await _context.Users.FirstOrDefaultAsync(user => user.Email!.Equals(email) && user.Token.Equals(request.Token)) ?? throw new Exception("User not found");

                if (!user.RefreshToken.Equals(request.Token))
                {
                    throw new Exception("Refresh token is incorrect");
                }

                if (user.ResetTokenExpires < DateTime.Now)
                {
                    throw new Exception("Refresh token expired");
                }

                RefreshTokenDto tokens = SetTokens(user);

                return new UserDto
                {
                    Name = user.Name!,
                    Surname = user.Surname!,
                    Email = email,
                    Token = tokens.Token,
                    RefreshToken = tokens.RefreshToken
                };
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }
        private RefreshTokenDto SetTokens(User user)
        {
            try
            {
                var token = CreateToken(user);
                var reftreshToken = CreateRandomToken();

                user.Token = token;
                user.RefreshToken = reftreshToken;
                user.TokenExpires = DateTime.Now.AddDays(Int32.Parse(_config.GetSection("AppSettings:TokenExpiresMin").Value!));
                user.RefreshTokenExpires = DateTime.Now.AddDays(Int32.Parse(_config.GetSection("AppSettings:RefreshTokenExpiresDay").Value!));
                user.TokenCreated = DateTime.Now;
                user.RefreshTokenCreated = DateTime.Now;
                _context.SaveChangesAsync();

                return new RefreshTokenDto
                {
                    Token = token,
                    RefreshToken = reftreshToken
                };
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        }
        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computeHash.SequenceEqual(passwordHash);
        }

        public string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
        }

    }
}