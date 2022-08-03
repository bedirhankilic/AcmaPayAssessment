using AcmePayAssessment.ApiModels;
using AcmePayAssessment.BusinessLayer.Abstract;
using AcmePayAssessment.BusinessLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AcmePayAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly ITransactionService _service;
        public AuthorizeController(ITransactionService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(_service.GetTransactionList());
        }

        [HttpPost]
        public IActionResult Post([FromBody] AuthorizeAdd value)
        {
            string err = "";
            if (value.CVV <= 0)
            {
                err = "CVV Error\n";
            }
            else if (value.ExpirationYear < DateTime.Now.Year || (value.ExpirationYear == DateTime.Now.Year && value.ExpirationMonth < DateTime.Now.Month))
            {
                err = "Expiration Date";
            }
            else if (value.Currency.Length != 3)
            {
                err = "Currency";
            }
            else if (string.IsNullOrWhiteSpace(value.CardHolder))
            {
                err = "Card Holder Empty";
            }
            else if (string.IsNullOrWhiteSpace(value.HolderName))
            {
                err = "Holder Name Empty";
            }
            if (err != "")
                return BadRequest(err);


            var result = _service.Authorize(value);
            return new JsonResult(result);
        }

        [HttpPost("/api/authorize/{id}/voids")]
        public IActionResult Voids(string id, [FromBody] RequestModel requestModel)
        {
            if (requestModel.OrderReference.Trim()?.Length > 50)
                return BadRequest("Order Reference must be less 51");

            Guid guid = Guid.Empty;
            bool result = Guid.TryParse(id, out guid);
            if (!result)
                return BadRequest("Wrong format for Id field!");

            try
            {
                return new JsonResult(_service.Void(guid, requestModel.OrderReference?.Trim()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("/api/authorize/{id}/capture")]
        public IActionResult Capture(string id, [FromBody] RequestModel requestModel)
        {
            if (requestModel.OrderReference.Trim()?.Length > 50)
                return BadRequest("Order Reference must be less 51");

            Guid guid = Guid.Empty;
            bool result = Guid.TryParse(id, out guid);
            if (!result)
                return BadRequest("Wrong format for Id field!");

            try
            {
                return new JsonResult(_service.Capture(guid, requestModel.OrderReference?.Trim()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
