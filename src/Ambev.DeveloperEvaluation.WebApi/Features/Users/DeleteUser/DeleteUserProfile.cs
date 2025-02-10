using Ambev.DeveloperEvaluation.Application.Features.Users.DeleteUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.DeleteUser;

/// <summary>
/// Profile for mapping DeleteUser feature requests to commands
/// </summary>
public class DeleteUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteUser feature
    /// </summary>
    public DeleteUserProfile()
    {
        CreateMap<Guid, DeleteUserCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));

        CreateMap<DeleteUserResult, DeleteUserResponse>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new DeleteUserAddressResponse
            {
                City = src.City,
                Street = src.Street,
                Number = src.Number,
                Zipcode = src.Zipcode,
                Geolocation = new DeleteUserGeolocationResponse
                {
                    Latitude = src.Latitude,
                    Longitude = src.Longitude
                }
            }));
    }
}
