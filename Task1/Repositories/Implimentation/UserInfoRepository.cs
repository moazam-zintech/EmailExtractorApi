using Task1.Data;
using Task1.Model.Domain;
using Task1.Repositories.Interface;

namespace Task1.Repositories.Implimentation
{
    public class UserInfoRepository: IUserInfoRepository
    {
        //We can use this private fild inside the contoller
        private readonly ApplicationDbContext dbContext;

        public UserInfoRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            //inject DB class which we injected in program.cs
        }
       public async Task<UserInfo> CreateAsync(UserInfo userInfo)
        {
            //Now we use injected servises
            await dbContext.userInfornation.AddAsync(userInfo);
            await dbContext.SaveChangesAsync();
            return userInfo;
        }
    }
}
