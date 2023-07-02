using RentACarApi.Model;
using RentACarShared;

namespace RentACarApi.Services
{
    public class FileService : IFileService
    {
        private readonly ApplicationDbContext context;

        public FileService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<ManagerResponse> DeletePictureAsync(int pictureId)
        {
            var picture = await context.Pictures.FindAsync(pictureId);

            if (picture == null)
            {
                return new ManagerResponse
                {
                    Message = "Failed to fiend picture",
                    IsSuccess = false
                };
            }
            context.Pictures.Remove(picture);

            var result = await context.SaveChangesAsync();
            if (result > 0)
            {
                return new ManagerResponse
                {
                    Message = "Picture deleted",
                    IsSuccess = true
                };
            }
            else
            {
                return new ManagerResponse
                {
                    Message = "Failed to delete picture",
                    IsSuccess = false
                };
            }

        }
    }
}
