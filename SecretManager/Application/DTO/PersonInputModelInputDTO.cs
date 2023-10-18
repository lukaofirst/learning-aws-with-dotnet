namespace Application.DTO
{
	public record PersonInputModelDTO
	{
		public string? Name { get; init; }
		public int Age { get; init; }
		public string? Email { get; init; }
	}
}