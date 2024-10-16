using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TranslationManagement.Api.Mappers;
using TranslationManagement.Api.Models.Requests;
using TranslationManagement.Api.Models.Responses;
using TranslationManagement.Api.Validators;
using TranslationManagement.Business.Services.Interfaces;

namespace TranslationManagement.Api.Controllers
{
    [ApiController]
    [Route("api/jobs/[action]")]
    public class TranslationJobController : ControllerBase
    {
        private readonly ILogger<TranslationJobController> _logger;
        private readonly ITranslationJobService _service;
        private readonly TranslationJobApiMapper _mapper;
        private readonly TranslationJobUpdateRequestValidator _updateRequestValidator;

        public TranslationJobController(ILogger<TranslationJobController> logger, ITranslationJobService service, TranslationJobApiMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<TranslationJobResponse[]> GetJobs()
        {
            return Ok(_service.GetTranslationJobs().Select(_mapper.DtoToResponse).ToArray());
        }

        [HttpPost]
        public IActionResult CreateJob([FromBody] TranslationJobCreateRequest request)
        {
            _service.CreateTranslationJob(_mapper.RequestToDto(request));
            return Ok();
        }


        [HttpPut]
        public IActionResult UpdateJobStatus([FromBody] TranslationJobUpdateRequest request)
        {
            // Validation
            _logger.LogInformation("Job status update request received: " + request.Status + " for job " + request.Id.ToString());
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
            _service.UpdateTranslationJob(_mapper.RequestToDto(request));
            return Ok();
        }
    }
}