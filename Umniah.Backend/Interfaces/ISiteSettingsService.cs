using Umniah.Backend.Data.Enums;
using Umniah.Backend.DTOs;
using Umniah.Backend.DTOs.Input;
using Umniah.Backend.DTOs.Output;

namespace Umniah.Backend.Interfaces;

public interface ISiteSettingsService
{
    Task<ServiceResponse<OutputGalleryTheme>> GetGalleryTheme();
    Task<ServiceResponse<bool>> SetGalleryTheme(InputGalleryTheme input);
    Task<ServiceResponse<OutputGallerySettings>> GetGallerySettings();
    Task<ServiceResponse<OutputGalleryText>> GetGalleryText();
    Task<ServiceResponse<bool>> SetGalleryText(InputGalleryText input);
    Task<ServiceResponse<List<OutputThemeHeroText>>> GetThemeHeroTexts();
    Task<ServiceResponse<bool>> SetThemeHeroText(GalleryTheme theme, InputThemeHeroText input);
}
