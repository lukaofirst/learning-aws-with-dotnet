using Amazon.DynamoDBv2.DataModel;
using Domain.Constants;

namespace Domain.Entities;

[DynamoDBTable(DynamoDBTableNames.PERSON)]
public class Person
{
	[DynamoDBHashKey]
	public string? PartitionKey { get; private set; }

	[DynamoDBRangeKey]
	public string? SortingKey { get; private set; }

	[DynamoDBProperty]
	public string? Name { get; set; }

	[DynamoDBProperty]
	public int Age { get; set; }

	[DynamoDBProperty]
	public string? Email { get; set; }

	public Person() { }

	public Person(string partitionKey, string sortingKey)
	{
		PartitionKey = partitionKey;
		SortingKey = sortingKey;
	}

	public void SetPartitionKey(string partitionKey)
	{
		PartitionKey = partitionKey;
	}

	public void SetSortingKey(string sortingKey)
	{
		SortingKey = sortingKey;
	}
}