using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Umniah.Backend.DTOs.Input;
using Umniah.Backend.DTOs.Output;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Controllers;

[DisplayName("Gallery Image Management")]
[ApiController]
[Route("api/[controller]")]
public class GalleryImageController(ICrudService<InputGalleryImage, OutputGalleryImage> galleryImageService) : ControllerBase
{
    [HttpGet]
    [DisplayName("Get All Gallery Images")]
    public async Task<IActionResult> GetAllGalleryImages()
    {
        var response = await galleryImageService.GetAll();
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpPost]
    [DisplayName("Create Gallery Image")]
    public async Task<IActionResult> CreateGalleryImage([FromBody] InputGalleryImage input)
    {
        var response = await galleryImageService.Create(input);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpPut("{id}")]
    [DisplayName("Edit Gallery Image")]
    public async Task<IActionResult> EditGalleryImage(Guid id, [FromBody] InputGalleryImage input)
    {
        var response = await galleryImageService.Edit(id, input);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpDelete("{id}")]
    [DisplayName("Delete Gallery Image")]
    public async Task<IActionResult> DeleteGalleryImage(Guid id)
    {
        var response = await galleryImageService.Delete(id);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpGet("{id}")]
    [DisplayName("Get Gallery Image By ID")]
    public async Task<IActionResult> GetGalleryImageById(Guid id)
    {
        var response = await galleryImageService.GetById(id);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    
}