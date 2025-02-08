using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;

/// <summary>
/// Profile for mapping between Application and API UpdateUser responses
/// </summary>
public class UpdateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateUser feature
    /// </summary>
    public UpdateUserProfile()
    {
        CreateMap<UpdateUserRequest, UpdateUserCommand>()
            .ConstructUsing((src, ctx) => new UpdateUserCommand
            {
                Id = ctx.Items["Id"] as Guid? ?? Guid.Empty,
                City = src.Address.City,
                Street = src.Address.Street,
                Number = src.Address.Number,
                Zipcode = src.Address.Zipcode,
                Latitude = src.Address.Geolocation.Latitude,
                Longitude = src.Address.Geolocation.Longitude
            });

        CreateMap<UpdateUserResult, UpdateUserResponse>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src));

        CreateMap<UpdateUserResult, UpdateUserAddressResponse>()
            .ForMember(dest => dest.Geolocation, opt => opt.MapFrom(src => src));

        CreateMap<UpdateUserResult, UpdateUserGeolocationResponse>();
    }
}
