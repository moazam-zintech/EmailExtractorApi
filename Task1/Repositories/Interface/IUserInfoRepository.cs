using Task1.Model.Domain;

namespace Task1.Repositories.Interface
{
    public interface IUserInfoRepository
    {
        Task<UserInfo> CreateAsync(UserInfo userInfo);
    }
}
