using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TranslationManagement.Api.Mappers;
using TranslationManagement.Api.Models.Requests;
using TranslationManagement.Api.Models.Responses;
using TranslationManagement.Api.Validators;
using TranslationManagement.Business.Services.Interfaces;

namespace TranslationManagement.Api.Controlers
{
    [ApiController]
    [Route("api/TranslatorsManagement/[action]")]
    public class TranslatorManagementController : ControllerBase
    {

        private readonly ILogger<TranslatorManagementController> _logger;
        private readonly ITranslatorService _service;
        private readonly TranslatorApiMapper _mapper;
        private readonly TranslatorUpdateRequestValidator _updateRequestValidator;

        public TranslatorManagementController(ILogger<TranslatorManagementController> logger, ITranslatorService service, TranslatorApiMapper mapper, TranslatorUpdateRequestValidator updateRequestValidator)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
            _updateRequestValidator = updateRequestValidator;
        }

        [HttpGet]
        public ActionResult<TranslatorResponse[]> GetTranslators()
        {
            return Ok(_service.GetTranslators().Select(_mapper.DtoToResponse));
        }

        [HttpGet]
        public ActionResult<TranslatorResponse[]> GetTranslatorsByName(string name)
        {
            // inefficient, rewrite
            return Ok(_service.GetTranslators().Select(_mapper.DtoToResponse).Where(x => x.Name == name));
        }

        [HttpPost]
        public IActionResult AddTranslator([FromBody] TranslatorCreateRequest request)
        {
            _service.CreateTranslator(_mapper.RequestToDto(request));
            return Ok();
        }
        
        [HttpPost]
        public IActionResult UpdateTranslatorStatus([FromBody] TranslatorUpdateRequest request)
        {
            _logger.LogInformation("User status update request: " + request.Status + " for user " + request.Id.ToString());
            var validationResult = _updateRequestValidator.Validate(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    Errors = validationResult.Errors.Select(e => new
                    {
                        e.PropertyName,
                        e.ErrorMessage
                    })
                });
            }
            _service.UpdateTranslator(_mapper.RequestToDto(request));
            return Ok();
        }
    }
}