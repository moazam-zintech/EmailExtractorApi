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
	public class EmailAddressController : ControllerBase
	{
		//We can use this private file inside the contoller
		private readonly IEmailAddressRepository _emailAddressRepository;
		private readonly EmailExtractor _email;
		public EmailAddressController(IEmailAddressRepository emailAddressRepository, EmailExtractor email)
		{
			this._emailAddressRepository = emailAddressRepository;
			this._email = email;
		}
		[HttpPost]
		public async Task<IActionResult> CreateEmail(CreateEmailRequestDTO request)
		{
			List<Dictionary<string, string>> separatedEmailLists = _email.SeparateString(request.inputString);

            var modifiedEmails = new List<EmailAddress>();
			var incorrectEmails=new List<EmailAddress>();
            foreach (var entry in separatedEmailLists)
            {
				var emailAddress = new EmailAddress
				{
					FirstName = entry["firstName"],
					LastName = entry["lastName"],
					Email = entry["email"]
				};
                modifiedEmails.Add(emailAddress);
            }
			foreach (var modifiedEmail in modifiedEmails)
			{
				await _emailAddressRepository.CreateAsync(modifiedEmail);
			}
			return Ok();
		}
	}
}