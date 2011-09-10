using System.Linq;
using System.Text;
using System.Web.Mvc;
using GiveCampLondon.Repositories;

namespace GiveCampLondon.Website.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class MailingsAdminController : Controller
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly INonTechVolunteerRepository _nonTechieVolunteerRepository;

        public MailingsAdminController(IVolunteerRepository volunteerRepository, INonTechVolunteerRepository nonTechVolunteerRepository)
        {
            _nonTechieVolunteerRepository = nonTechVolunteerRepository;
            _volunteerRepository = volunteerRepository;
        }

        public FileContentResult DownloadEmailList()
        {
            var users = _volunteerRepository.FindAll().Where(x => x.HasCancelled == false).Select(x => x.Email).Distinct();
            var notTechies = _nonTechieVolunteerRepository.FindAll().Where(x => x.HasCancelled == false).Select(x => x.Email).Distinct();

            var sb = new StringBuilder();
            foreach (var user in users)
            {
                sb.AppendFormat(user + ",");
            }

            foreach (var notTechy in notTechies)
            {
                sb.AppendFormat(notTechy + ",");
            }

            return File(new UTF8Encoding().GetBytes(sb.ToString()), "text/csv", "UserMailAddress.csv");
        }
    }
}
