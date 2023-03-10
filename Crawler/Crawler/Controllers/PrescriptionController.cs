using Crawler.Services;
using Microsoft.AspNetCore.Mvc;

namespace Crawler.Controllers;

[Route("api/prescriptions")]
[ApiController]
public class PrescriptionController : ControllerBase
{

    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPrescription(int id)
    {
        var prescription = await _prescriptionService.GetPrescription(id);
        return Ok(prescription);
    }
}