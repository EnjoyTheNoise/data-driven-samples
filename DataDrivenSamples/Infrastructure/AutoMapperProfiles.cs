using AutoMapper;
using DataDrivenSamples.Data.Shared.Dtos.Create;
using DataDrivenSamples.Data.Shared.Dtos.Delete;
using DataDrivenSamples.Data.Shared.Dtos.Get;
using DataDrivenSamples.Data.Shared.Dtos.Update;
using DataDrivenSamples.Data.Shared.Models;

namespace DataDrivenSamples.Infrastructure
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Item, UpdateItemResponseDto>();
            CreateMap<Item, GetItemResponseDto>();
            CreateMap<Item, CreateItemResponseDto>();

            CreateMap<GetItemRequestDto, Item>();
            CreateMap<CreateItemRequestDto, Item>();
            CreateMap<UpdateItemRequestDto, Item>();
            CreateMap<DeleteItemRequestDto, Item>();
        }
    }
}
