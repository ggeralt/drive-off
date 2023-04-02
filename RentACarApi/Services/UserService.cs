using Microsoft.AspNetCore.Identity;
using RentACarShared;

namespace RentACarApi.Services
{
    public class UserService : IUserService
    {
        private UserManager<IdentityUser> userManager;
        public UserService(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<UserManagerResponse> RegisterUser(RegisterViewModel model)
        {
            if (model == null)
            {
                throw new Exception("Register model is null");
            }
            else if (model.Password != model.ConfirmPassword)
            {
                return new UserManagerResponse
                {
                    Message = "Confirm password doesn't match the password",
                    IsSuccess = false
                };
            }

            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Username
            };

            var result = await userManager.CreateAsync(identityUser, model.Password);

            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "User created succesfully",
                    IsSuccess = true,
                };
            }

            return new UserManagerResponse
            {
                Message = "User cannot be created",
                IsSuccess = false,
                Errors = result.Errors.Select(errors => errors.Description)
            }; 
        }
    }
}
