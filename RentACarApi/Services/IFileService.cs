using RentACarApi.Model;

namespace RentACarApi.Services
{
    public interface IFileService
    {
        Task<Picture> GetPictureAsync(int pictureId);
    }
}
