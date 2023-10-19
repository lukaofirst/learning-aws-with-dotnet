using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Api.Interfaces;

namespace Api.Services;

public class S3Service : IS3Service
{
	private readonly IAmazonS3 _amazonS3;
	private readonly IConfiguration _configuration;
	private readonly string _s3BucketName;

	public S3Service(IAmazonS3 amazonS3, IConfiguration configuration)
	{
		_amazonS3 = amazonS3;
		_configuration = configuration;
		_s3BucketName = _configuration["AWS:S3:BucketName"]!;
	}

	public async Task<(byte[] fileBytes, string contentType)> GetFileAsync(string fileName)
	{
		var getObjectRequest = new GetObjectRequest
		{
			BucketName = _s3BucketName,
			Key = fileName
		};

		var getObjectResponse = await _amazonS3.GetObjectAsync(getObjectRequest);

		if (!getObjectResponse.HttpStatusCode.Equals(HttpStatusCode.OK))
			throw new Exception("Could not upload file to AWS S3");

		using var ms = new MemoryStream();
		await getObjectResponse.ResponseStream.CopyToAsync(ms);

		return (ms.ToArray(), getObjectResponse.Headers.ContentType);
	}

	public async Task<bool> UploadFileAsync(IFormFile formFile)
	{
		using var ms = new MemoryStream();
		await formFile.CopyToAsync(ms);

		var putObjectRequest = new PutObjectRequest
		{
			BucketName = _s3BucketName,
			Key = formFile.FileName,
			InputStream = ms
		};

		var putObjectResponse = await _amazonS3.PutObjectAsync(putObjectRequest);

		return putObjectResponse.HttpStatusCode.Equals(HttpStatusCode.OK);
	}

	public async Task<bool> DeleteFileAsync(string fileName)
	{
		var deleteObjectResponse = await _amazonS3.DeleteObjectAsync(_s3BucketName, fileName);

		return deleteObjectResponse.HttpStatusCode.Equals(HttpStatusCode.NoContent);
	}
}