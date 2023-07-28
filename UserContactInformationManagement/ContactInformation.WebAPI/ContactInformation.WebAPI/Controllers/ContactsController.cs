using Microsoft.AspNetCore.Mvc;

namespace ContactInformation.WebAPI.Controllers
{
    [ApiController]
    public class ContactsController : ControllerBase
    {
        public JsonResult GetContacts()
        {
            return new JsonResult(
                new List<object>
                {
                    new {Id = 1, Firstname = "Charis Arlie", Lastname = "Baclayon"},
                    new {Id = 1, Firstname = "Leonel Christie", Lastname = "Baclayon"},
                });
        }
    }
}
