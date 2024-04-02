using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task1.Data;
using Task1.Model.Domain;
using Task1.Model.DTO;
using Task1.Repositories.Implimentation;
using Task1.Repositories.Interface;


namespace Task1.Controllers
{

    [Route("api/[controller]")] //
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        //We can use this private file inside the contoller
        private readonly IUserInfoRepository userInfoRepository;
        private readonly UserInfoExtractor userInfoExtractor;
        public UserInfoController(IUserInfoRepository userInfoRepository, UserInfoExtractor userInfoExtractor)
        {
            this.userInfoRepository = userInfoRepository;
            this.userInfoExtractor = userInfoExtractor;
        }

        //Action methods
        [HttpPost]
        public async Task<IActionResult> CreateUserInfo(CreateUserInfoRequestDTO request)
        {

            //Map DTO to Domain Model
            /*
                        await dbContext.userInfornation.AddAsync(userInfo);
                        await dbContext.SaveChangesAsync();*/
            //This comes from Domain model
            var (name, email) = userInfoExtractor.ExtractUserData(request.inputString);


            var userInfo = new UserInfo
            {
                inputString=request.inputString,
                userName = name,
                userEmail = email
            };

            await userInfoRepository.CreateAsync(userInfo);

            //Map Domain model to DTO

            return Ok();
        }

    }
}
