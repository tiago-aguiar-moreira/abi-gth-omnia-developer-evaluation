using Ambev.DeveloperEvaluation.Application.Users.GetUser;

using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

/// <summary>
/// Profile for mapping GetUser feature requests to commands
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser feature
    /// </summary>
    public GetUserProfile()
    {
        CreateMap<GetUserRequest, GetUserCommand>();

        CreateMap<GetUserResult, GetUserResponse>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new GetUserAddressResponse
            {
                City = src.City,
                Street = src.Street,
                Number = src.Number,
                Zipcode = src.Zipcode,
                Geolocation = new GetUserGeolocationResponse
                {
                    Latitude = src.Latitude,
                    Longitude = src.Longitude
                }
            }));
    }
}
