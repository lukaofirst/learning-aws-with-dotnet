namespace Application.DTO
{
	public record PersonViewModelDTO
	{
		public string? PartitionKey { get; set; }
		public string? SortingKey { get; set; }
		public string? Name { get; set; }
		public int Age { get; set; }
		public string? Email { get; set; }
	}
}