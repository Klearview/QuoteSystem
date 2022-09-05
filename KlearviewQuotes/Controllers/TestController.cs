using KlearviewQuotes.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KlearviewQuotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly IPDFService _pdfSerivce;
        private readonly IEmailService _emailService;

        public TestController(IPDFService pdfService, IEmailService emailService)
        {
            _pdfSerivce = pdfService;
            _emailService = emailService;
        }

        [HttpGet("PDF")]
        public async Task<IActionResult> PDF()
        {
            var pdf = await _pdfSerivce.ConvertPreviewToPDF(1);

            if (pdf == null || pdf.Attachment == null)
                return NotFound();

            var email = _emailService.SendEmailWithPDF(
                new("1tomkinsnoa@gmail.com"),
                "Estimate - Test",
                "",
                pdf
                );

            if (email)
                return Ok(email);
            return BadRequest();
        }

    }
}
