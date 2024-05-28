using TuyenDung.Data.Dto;
using TuyenDung.Data.Model;

namespace TuyenDung.Service.Services.InterfaceIServices
{
    public interface IUserIServices
    {
        public User RegisterUserAdmin(UserDto userDto);
        public User RegisterUser(UserDto userDto);
        User Login(RequestDto requestDto);
        bool Logout(int Id);
        string ResetPassword(int Id);
    }
}
