using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Umniah.Backend.Data.Enums;
using Umniah.Backend.DTOs.Input;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Controllers;

[DisplayName("Site Settings Management")]
[ApiController]
[Route("api/[controller]")]
public class SiteSettingsController(ISiteSettingsService siteSettingsService) : ControllerBase
{
    [HttpGet("gallery-theme")]
    [DisplayName("Get Gallery Theme")]
    public async Task<IActionResult> GetGalleryTheme()
    {
        var response = await siteSettingsService.GetGalleryTheme();
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpGet("gallery")]
    [DisplayName("Get Gallery Settings")]
    public async Task<IActionResult> GetGallerySettings()
    {
        var response = await siteSettingsService.GetGallerySettings();
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpGet("gallery-text")]
    [DisplayName("Get Gallery Text")]
    public async Task<IActionResult> GetGalleryText()
    {
        var response = await siteSettingsService.GetGalleryText();
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpPut("gallery-text")]
    [DisplayName("Set Gallery Text")]
    public async Task<IActionResult> SetGalleryText([FromBody] InputGalleryText input)
    {
        var response = await siteSettingsService.SetGalleryText(input);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpGet("theme-hero-texts")]
    [DisplayName("Get Theme Hero Texts")]
    public async Task<IActionResult> GetThemeHeroTexts()
    {
        var response = await siteSettingsService.GetThemeHeroTexts();
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpPut("theme-hero-texts/{theme}")]
    [DisplayName("Set Theme Hero Text")]
    public async Task<IActionResult> SetThemeHeroText(GalleryTheme theme, [FromBody] InputThemeHeroText input)
    {
        var response = await siteSettingsService.SetThemeHeroText(theme, input);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpPut("gallery-theme")]
    [DisplayName("Set Gallery Theme")]
    public async Task<IActionResult> SetGalleryTheme([FromBody] InputGalleryTheme input)
    {
        var response = await siteSettingsService.SetGalleryTheme(input);
        if (response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }
}
