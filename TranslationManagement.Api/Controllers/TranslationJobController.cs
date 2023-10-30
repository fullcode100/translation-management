using Microsoft.AspNetCore.Mvc;
using TranslationManagement.Api.Models;
using External.ThirdParty.Services;
using TranslationManagement.Api.FileProcessors;

namespace TranslationManagement.Api.Controllers
{
    [ApiController]
    [Route("api/jobs")]
    public class TranslationJobController : ControllerBase
    {
        private readonly IDataRepository repository;
        private readonly ILogger<TranslatorManagementController> _logger;

        public TranslationJobController(IDataRepository repo, ILogger<TranslatorManagementController> logger)
        {
            repository = repo;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetJobs() => Ok(repository.TranslationJobs.ToArray());

        [HttpPost]
        public async Task<IActionResult> CreateJob(TranslationJob job)
        {
            job.Status = JobStatuses.New.ToString();
            SetPrice(job);
            var success = await repository.CreateTranslationJobAsync(job);
            if (success)
            {
                await NotifyJobCreation(job.Id);
                _logger.LogInformation("New job notification sent");
            }

            return Ok(success);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> CreateJobWithFile([FromForm] IFormFile file, [FromForm] string? customer)
        {
            var reader = new StreamReader(file.OpenReadStream());
            string? content;

            (content, customer) = await FileProcessorManager.Process(file.FileName, reader, customer);
            if (content == null || customer == null)
            {
                return BadRequest("params cannot be null");
            }

            var newJob = new TranslationJob()
            {
                OriginalContent = content,
                TranslatedContent = "",
                CustomerName = customer,
            };
            SetPrice(newJob);

            return await CreateJob(newJob);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateJobStatus([FromForm] int jobId, [FromForm] int translatorId, [FromForm] string newStatus)
        {
            _logger.LogInformation($"Job status update request received: {newStatus} for job {jobId} by translator {translatorId}");
            if (!IsValidJobStatus(newStatus))
            {
                return BadRequest("invalid status");
            }

            try
            {
                await repository.UpdateTranslationJobStatusAsync(jobId, newStatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("updated");
        }

        private const double PricePerCharacter = 0.01;
        private void SetPrice(TranslationJob job)
        {
            job.Price = job.OriginalContent.Length * PricePerCharacter;
        }

        private async Task NotifyJobCreation(int jobId)
        {
            var notificationSvc = new UnreliableNotificationService();
            bool isSuccess = false;
            while (true)
            {
                try
                {
                    isSuccess = await notificationSvc.SendNotification($"Job created: {jobId}");
                }
                catch { }

                if (isSuccess) break;
            }
        }

        public static bool IsValidJobStatus(string status)
        {
            return Enum.IsDefined(typeof(JobStatuses), status);
        }

        public static bool IsInvalidStatusChange(string currentStatus, string newStatus)
        {
            return (currentStatus == JobStatuses.New.ToString() && newStatus == JobStatuses.Completed.ToString()) ||
                   currentStatus == JobStatuses.Completed.ToString() || newStatus == JobStatuses.New.ToString();
        }
    }
}
