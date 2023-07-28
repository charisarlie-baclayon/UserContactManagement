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
                    new {Id = 2, Firstname = "Leonel Christie", Lastname = "Baclayon"},
                    new {Id = 3, Firstname = "Grace Christian", Lastname = "Baclayon"}
                });
        }
    }
}
