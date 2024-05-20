using Microsoft.AspNetCore.Mvc;
using Task1.Model.Domain;
using Task1.Model.DTO;
using Task1.Repositories.Interface;
namespace Task1.Controllers
{
    [Route("api/[controller]")] //
	[ApiController]
	public class EmailAddressController(IEmailAddressRepository emailAddressRepository) : ControllerBase
	{
		//We can use this private file inside the contoller
		private readonly IEmailAddressRepository _emailAddressRepository = emailAddressRepository;

        [HttpPost]
		public async Task<IActionResult> CreateEmail(CreateEmailRequestDTO request)
		{
			List<Dictionary<string, string>> separatedEmailLists = _emailAddressRepository.SeparateString(request.inputString);
            foreach (var entry in separatedEmailLists)
            {
				var emailAddress = new EmailAddress
				{
					FirstName = entry["firstName"],
					LastName = entry["lastName"],
					Email = entry["email"],
                };
                await _emailAddressRepository.CreateAsync(emailAddress);
            }
            return Ok();
		}
        [HttpGet]
        public async Task<IActionResult> GetEmails()
        {
             // Retrieve emails from the repository
            var emails = await _emailAddressRepository.GetAllAsync();

                // If there are no emails found
                if (emails == null)
                {
                    return NotFound("No emails found.");
                }
                // Create a DTO (Data Transfer Object) to send to the UI
             /*   var emailDTOs = new List<EmailDTO>();
                foreach (var email in emails)
                {
                emailDTOs.Add(new EmailDTO
                {
                    id = email.ID,
                    firstName = email.FirstName,
                    lastName = email.LastName,
                    email = email.Email
                }) ;*/
                //}
            // Return the list of emails to the UI

            return Ok(emails);
        }
      /*  [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteEmail(Guid id)
        {

            var emails = await _emailAddressRepository.GetByIdAsync(id);
            return Ok();
        }*/

    }
}