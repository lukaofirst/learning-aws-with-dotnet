namespace Api.Interfaces;

public interface IS3Service
{
	Task<(byte[] fileBytes, string contentType)> GetFileAsync(string fileName);
	Task<bool> UploadFileAsync(IFormFile formFile);
	Task<bool> DeleteFileAsync(string fileName);
}