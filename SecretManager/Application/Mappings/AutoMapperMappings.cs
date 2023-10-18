using Application.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
	public class AutoMapperMappings : Profile
	{
		public AutoMapperMappings()
		{
			CreateMap<Person, PersonViewModelDTO>().ReverseMap();
			CreateMap<Person, PersonInputModelDTO>().ReverseMap();
		}
	}
}