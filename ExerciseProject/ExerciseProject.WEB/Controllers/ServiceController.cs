using ExerciseProject.Core.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReference1;

namespace ExerciseProject.WEB.Controllers
{
    [Authorize]
    public class ServiceController : Controller
    {
        public async Task<ServiceContragentViewModel> GetByVAT([FromQuery] string vat)
        {
            ServiceContragentViewModel contragent = null;

            try
            {
                var serviceClient = new checkVatPortTypeClient();
                var request = new checkVatRequest(vat.Substring(0, 2), vat.Substring(2, vat.Length - 2));
                var response = await serviceClient.checkVatAsync(request);

                if (response.valid)
                {
                    contragent = new ServiceContragentViewModel
                    {
                        IsValid = response.valid,
                        Name = response.name,
                        Address = response.address,
                        VAT = $"{response.countryCode}{response.vatNumber}"
                    };
                }
            }
            catch (Exception)
            {
            }

            return contragent;
        }
    }
}