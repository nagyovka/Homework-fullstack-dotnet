using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using External.ThirdParty.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TranslationManagement.Api.Controlers;
using TranslationManagement.Api.Models.Requests;
using TranslationManagement.Api.Models.Responses;

namespace TranslationManagement.Api.Controllers
{
    [ApiController]
    [Route("api/jobs/[action]")]
    public class TranslationJobController : ControllerBase
    {
        private readonly ILogger<TranslationJobController> _logger;

        public TranslationJobController(ILogger<TranslationJobController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<TranslationJobResponse[]> GetJobs()
        {
            // create request/response classes
            return _context.TranslationJobs.ToArray();
        }

        [HttpPost]
        public bool CreateJob(TranslationJobCreateRequest request)
        {
            job.Status = "New";
            SetPrice(job);
            //_context.TranslationJobs.Add(job);
            bool success = _context.SaveChanges() > 0;
            if (success)
            {

                var notificationSvc = new UnreliableNotificationService();
                //while (!notificationSvc.SendNotification("Job created: " + job.Id).Result)
                {
                }

                _logger.LogInformation("New job notification sent");
            }

            return success;
        }

        [HttpPost]
        public bool CreateJobWithFile(IFormFile file, string customer)
        {
            // move everything to business layer
            var reader = new StreamReader(file.OpenReadStream());
            string content;

            if (file.FileName.EndsWith(".txt"))
            {
                content = reader.ReadToEnd();
            }
            else if (file.FileName.EndsWith(".xml"))
            {
                var xdoc = XDocument.Parse(reader.ReadToEnd());
                content = xdoc.Root.Element("Content").Value;
                customer = xdoc.Root.Element("Customer").Value.Trim();
            }
            else
            {
                throw new NotSupportedException("unsupported file");
            }

            var newJob = new TranslationJob()
            {
                OriginalContent = content,
                TranslatedContent = "",
                CustomerName = customer,
            };

            SetPrice(newJob);

            return CreateJob(newJob);
        }

        [HttpPost]
        // change to Put
        // introduce request
        // add validation (fluentValidation)
        public string UpdateJob([FromBody] TranslationJobUpdateRequest request)
        {
            _logger.LogInformation("Job status update request received: " + newStatus + " for job " + jobId.ToString() + " by translator " + translatorId);
            if (typeof(JobStatuses).GetProperties().Count(prop => prop.Name == newStatus) == 0)
            {
                return "invalid status";
            }

            var job = _context.TranslationJobs.Single(j => j.Id == jobId);

            bool isInvalidStatusChange = (job.Status == JobStatuses.New && newStatus == JobStatuses.Completed) ||
                                         job.Status == JobStatuses.Completed || newStatus == JobStatuses.New;
            if (isInvalidStatusChange)
            {
                return "invalid status change";
            }

            job.Status = newStatus;
            _context.SaveChanges();
            return "updated";
        }
    }
}