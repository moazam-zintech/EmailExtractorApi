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
		[HttpPost]
		public async Task<IActionResult> CreateUserInfo(CreateUserInfoRequestDTO request)
		{
			List<Dictionary<string, string>> separatedLists = userInfoExtractor.SeparateString(request.inputString);

            var modifiedUserInfos = new List<UserInfo>();

            foreach (var entry in separatedLists)
            {
				var userInfo = new UserInfo
				{
					userName = entry["name"],
					userEmail = entry["email"]
				};
               
                modifiedUserInfos.Add(userInfo);
            }
		
			foreach (var modifiedUserInfo in modifiedUserInfos)
			{
				await userInfoRepository.CreateAsync(modifiedUserInfo);
			}
			//Map DTO to Domain Model
			/*
						await dbContext.userInfornation.AddAsync(userInfo);
						await dbContext.SaveChangesAsync();*/
			//This comes from Domain model
			//Map Domain model to DTO
			return Ok();
		}
	}
}
