using System;
using CloudinaryDotNet.Actions;

namespace APITON.Interface;

public interface IImageService
{
    Task<ImageUploadResult> AddImageAsync(IFormFile file);
    Task<DeletionResult> DeleteImageAsync(string publicId);
}
