using Mapster;
using Umniah.Backend.Data;
using Umniah.Backend.DTOs.Input;

namespace Umniah.Backend.Config;

public class MapsterConfigurations
{
    public static void Configure()
    {
        TypeAdapterConfig.GlobalSettings.Default
            .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);

        // Complex mapping example
        TypeAdapterConfig<InputGalleryImage, GalleryImage>
            .NewConfig()
            .Map(dest => dest.StoragePath, 
                src => $"/uploads/{Guid.NewGuid()}{Path.GetExtension(src.imageFile)}")
            .AfterMapping((src, dest) => dest.CreatedAt = DateTime.UtcNow);
    }
}