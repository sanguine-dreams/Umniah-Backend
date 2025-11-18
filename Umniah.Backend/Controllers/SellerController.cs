using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Umniah.Backend.DTOs.Input;
using Umniah.Backend.DTOs.Output;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[DisplayName("Seller Management")]
public class SellerController(IBulkCrudService<InputSeller, OutputSeller> sellerService) : ControllerBase
{
    [HttpGet]
    [DisplayName("Get All Sellers")]
    public async Task<IActionResult> GetAllSellers()
    {
        var response = await sellerService.GetAll();
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpPost]
    [DisplayName("Edit Sellers")]
    public async Task<IActionResult> EditSellers([FromBody] List<InputSeller> inputs)
    {
        var response = await sellerService.EditAll(inputs);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }
}