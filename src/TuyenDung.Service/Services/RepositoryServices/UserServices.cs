using TuyenDung.API.Helper;
using TuyenDung.Data.DataContext;
using TuyenDung.Data.Dto;
using TuyenDung.Data.Model;
using TuyenDung.Data.Model.Enum;
using TuyenDung.Service.Repository.Interface;
using TuyenDung.Service.Services.InterfaceIServices;


namespace TuyenDung.Service.Services.RepositoryServices
{
    public class UserServices : IUserIServices
    {
        private readonly MyDb _dbContext;
        private readonly IUserInterface _userInterface;
        private readonly Token _token;
        public UserServices(MyDb dbContext, IUserInterface userInterface,Token token)
        {
            _token = token;
            _userInterface = userInterface;
            _dbContext = dbContext;
        }
        public User Login(RequestDto requestDto)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(x => x.Email == requestDto.Email);
                if (user == null)
                {
                    throw new Exception("Email not found");
                }
                if (!BCrypt.Net.BCrypt.Verify(requestDto.Password, user.Password))
                {
                    throw new Exception("Incorrect password!");
                }
                UpdateOrCreateAccessToken(user);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while logging in.", ex);
            }
        }

        public bool Logout(int Id)
        {
            try
            {
                var userToken = _userInterface.GetValidTokenByUserId(Id);
                if(userToken != null)
                {
                    var tokenValue = userToken.AcessToken;
                    var principal = _token.ValidataToken(tokenValue);
                    if (principal != null)
                    {
                        userToken.Status = StatusToken.Expired;
                    }
                }
                _dbContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error during logout for user {Id}: {ex.Message}");
                return false;
            }
        }

        public User RegisterUser(UserDto userDto)
        {
            try
            {
                if (userDto == null)
                {
                    throw new ArgumentNullException("All data fields have not been filled in");
                }
                User user = new User
                {
                    Name = userDto.Name,
                    Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                    Email = userDto.Email,
                    Phone = userDto.Phone,
                    Address = userDto.Address,
                    DateofBirth = userDto.DateofBirth,
                    roles = Roles.User,
                    DateCreate = DateTime.Now,
                };
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating user", ex);
            }
        }

        public User RegisterUserAdmin(UserDto userDto)
        {
            try
            {
                if(userDto == null)
                {
                    throw new ArgumentNullException("All data fields have not been filled in");
                }
                User user = new User
                {
                    Name = userDto.Name,
                    Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                    Email = userDto.Email,
                    Phone = userDto.Phone,
                    Address = userDto.Address,
                    DateofBirth = userDto.DateofBirth,
                    roles = Roles.Managerment,
                    DateCreate = DateTime.Now,
                };
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating user",ex);
            }
        }
        public string ResetPassword(int Id)
        {
            throw new NotImplementedException();
        }
        public void UpdateOrCreateAccessToken(User user)
        {
            var existingToken = _userInterface.GetValidTokenByUserId(user.Id);
            if (existingToken != null)
            {
                var token = _token.CreateToken(user);
                if (string.IsNullOrEmpty(token))
                    throw new Exception("Failed to create a token.");
                existingToken.AcessToken = token;
                existingToken.ExpirationDate = DateTime.Now;
            }
            else
            {
                var token = _token.CreateToken(user);
                if (string.IsNullOrEmpty(token))
                    throw new Exception("Failed to create a token.");
                var accessToken = new AccessToken
                {
                    UserId = user.Id,
                    AcessToken = token,
                    Status = StatusToken.Valid,
                    ExpirationDate = DateTime.Now,
                };
                _dbContext.AccessTokens.Add(accessToken);
            }
            _dbContext.SaveChanges();
        }
    }
}
