using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

/// <summary>
/// Profile for mapping between User entity and ListUsersResponse
/// </summary>
public class ListUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListUsers operation
    /// </summary>
    public ListUserProfile()
    {
        CreateMap<User, ListUserResult>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new ListUserAddressResult
            {
                City = src.City,
                Street = src.Street,
                Number = src.Number,
                Zipcode = src.Zipcode,
                Geolocation = new ListUserGeolocationResult
                {
                    Latitude = src.Latitude,
                    Longitude = src.Longitude
                }
            }));
    }
}
