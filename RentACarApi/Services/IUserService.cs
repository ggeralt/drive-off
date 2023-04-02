using RentACarShared;

namespace RentACarApi.Services
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterUser(RegisterViewModel model);
    }
}
