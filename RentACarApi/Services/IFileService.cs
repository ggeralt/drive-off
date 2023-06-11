using RentACarApi.Model;
using RentACarShared;

namespace RentACarApi.Services
{
    public interface IFileService
    {
        Task<ManagerResponse> DeletePictureAsync(int pictureId);
    }
}
