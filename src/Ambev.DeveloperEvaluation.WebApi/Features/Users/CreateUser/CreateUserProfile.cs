using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Profile for mapping between Application and API CreateUser responses
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser feature
    /// </summary>
    public CreateUserProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Address.Number))
            .ForMember(dest => dest.Zipcode, opt => opt.MapFrom(src => src.Address.Zipcode))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Address.Geolocation.Latitude))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Address.Geolocation.Longitude));

        CreateMap<CreateUserResult, CreateUserResponse>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new CreateUserAddressResponse
            {
                City = src.City,
                Street = src.Street,
                Number = src.Number,
                Zipcode = src.Zipcode,
                Geolocation = new CreateUserGeolocationResponse
                {
                    Latitude = src.Latitude,
                    Longitude = src.Longitude
                }
            }));
    }
}