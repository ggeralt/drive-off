using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using RentACarApi.Model;
using RentACarShared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RentACarApi.Services
{
    public class UserService : IUserService
    {
        private UserManager<ApplicationUser> userManager; 
        private IConfiguration configuration;
        private IMailService mailService;
        public UserService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IMailService mailService)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.mailService = mailService;
        }

        public async Task<ManagerResponse> ConfirmEmail(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ManagerResponse
                {
                    Message = "User not found",
                    IsSuccess = false
                };
            }
            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string validToken = Encoding.UTF8.GetString(decodedToken);

            var result = await userManager.ConfirmEmailAsync(user, validToken);

            if (result.Succeeded)
            {
                return new ManagerResponse
                {
                    Message = "Email confirmed successfully",
                    IsSuccess = true
                };
            }

            return new ManagerResponse
            {
                Message = "Email could not be confirm",
                IsSuccess = true,
                Errors = result.Errors.Select(errors => errors.Description)
            };
        }

        public async Task<ManagerResponse> DeleteAccount(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return new ManagerResponse
                {
                    Message = "User not found",
                    IsSuccess = false
                };
            }

            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return new ManagerResponse
                {
                    Message = "The account has been deleted",
                    IsSuccess = true
                };
            }

            return new ManagerResponse
            {
                Message = "User cannot be deleted",
                IsSuccess = false,
                Errors = result.Errors.Select(errors => errors.Description)
            };
        }

        public async Task<ManagerResponse> ForgetPassword(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new ManagerResponse
                {
                    Message = "No user with that email",
                    IsSuccess = false
                };
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var passwordResetTokenEncoded = Encoding.UTF8.GetBytes(token);
            var validPasswordResetToken = WebEncoders.Base64UrlEncode(passwordResetTokenEncoded);

            string url = $"{configuration["ApplicationUrl"]}/ResetPassword?email={email}&token={validPasswordResetToken}";

            await mailService.SendEmail(email, "Reset Password", $"<p>To reset password <a href='{url}'>CLICK HERE</a></p>");

            return new ManagerResponse
            {
                Message = "Reset password  URL has been sent to the email successfully",
                IsSuccess = true
            };
        }

        public async Task<ManagerResponse> LoginUser(LoginViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            var result = await userManager.CheckPasswordAsync(user, model.Password);

            if (user == null || !result)
            {
                return new ManagerResponse
                {
                    Message = "Invalid user name of password",
                    IsSuccess = false,
                };
            }

            var claims = new[]
            {
                new Claim("Email", model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: configuration["AuthSettings:Issuer"],
                audience: configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new ManagerResponse
            {
                Message = tokenAsString,
                IsSuccess = true
            };
        }

        public async Task<ManagerResponse> RegisterUser(RegisterViewModel model)
        { 
            if (model == null)
            {
                throw new Exception("Register model is null");
            }
            else if (model.Password != model.ConfirmPassword)
            {
                return new ManagerResponse
                {
                    Message = "Confirm password doesn't match the password",
                    IsSuccess = false
                };
            }

            var identityUser = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Username
            };

            var result = await userManager.CreateAsync(identityUser, model.Password);

            if (result.Succeeded)
            {
                var emailToken = await userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                var emailTokenEncoded = Encoding.UTF8.GetBytes(emailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(emailTokenEncoded);

                string url = $"{configuration["ApplicationUrl"]}/api/auth/confirmemail?userid={identityUser.Id}&token={validEmailToken}";

                await mailService.SendEmail(identityUser.Email, "Confirm email", $"<p>Please confirm email by <a href='{url}'>CLICKING HERE</a></p>");

                return new ManagerResponse
                {
                    Message = "User created succesfully",
                    IsSuccess = true,
                };
            }

            return new ManagerResponse
            {
                Message = "User cannot be created",
                IsSuccess = false,
                Errors = result.Errors.Select(errors => errors.Description)
            }; 
        }

        public async Task<ManagerResponse> ResetPassword(ResetPasswordViewModel model)
        {
            var user = userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ManagerResponse
                {
                    Message = "No user with that email",
                    IsSuccess = false
                };
            }
            if (model.NewPassword != model.ConfirmPassword)
            {
                return new ManagerResponse
                {
                    Message = "Password doesn't match",
                    IsSuccess = false
                };
            }

            var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
            var validToken = Encoding.UTF8.GetString(decodedToken);

            var result = await userManager.ResetPasswordAsync(await user, validToken, model.NewPassword);

            if (result.Succeeded)
            {
                return new ManagerResponse
                {
                    Message = "Password has been reset successfully",
                    IsSuccess = true
                };
            }
            return new ManagerResponse
            {
                Message = "Failed to reset password",
                IsSuccess = false,
                Errors = result.Errors.Select(errors => errors.Description)
            };
        }
    }
}
