using Umniah.Backend.Data;
using Umniah.Backend.DTOs;

namespace Umniah.Backend.Interfaces;

public interface IFileLedger
{
    // Validates and writes the file, then saves a Pending row on the scoped context.
    // That save flushes everything the context tracks, so call it before changing any other entity.
    Task<ServiceResponse<StoredFile>> SaveUploadAsync(IFormFile file);

    // Both ride on the caller's next SaveChanges.
    void Confirm(StoredFile storedFile);
    void MarkForDeletion(StoredFile storedFile);
}
