﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Profile for mapping between User entity and UpdateUserResponse
/// </summary>
public class UpdateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateUser operation
    /// </summary>
    public UpdateUserProfile()
    {
        CreateMap<UpdateUserCommand, User>();
        CreateMap<User, UpdateUserResult>();
    }
}
