using RentACarApi.Model;
using RentACarShared;

namespace RentACarApi.Services
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterUser(RegisterViewModel model);
        Task<UserManagerResponse> LoginUser(LoginViewModel model);
        Task<UserManagerResponse> ConfirmEmail(string userId, string token);
        Task<UserManagerResponse> ForgetPassword(string email);
        Task<UserManagerResponse> ResetPassword(ResetPasswordViewModel model);
    }
}
