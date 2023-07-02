using RentACarApi.Model;
using RentACarShared;

namespace RentACarApi.Services
{
    public interface IUserService
    {
        Task<ManagerResponse> RegisterUser(RegisterViewModel model);
        Task<ManagerResponse> LoginUser(LoginViewModel model);
        Task<ManagerResponse> ConfirmEmail(string userId, string token);
        Task<ManagerResponse> ForgetPassword(string email);
        Task<ManagerResponse> ResetPassword(ResetPasswordViewModel model);
        Task<ManagerResponse> DeleteAccount(string id);
    }
}
