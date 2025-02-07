﻿using Ambev.DeveloperEvaluation.Common.Helpers;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IUserRepository using Entity Framework Core
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of UserRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public UserRepository(DefaultContext context) => _context = context;

    /// <summary>
    /// Creates a new user in the database
    /// </summary>
    /// <param name="user">The user to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user</returns>
    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    /// <summary>
    /// Retrieves a user by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(o=> o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a user by their email address
    /// </summary>
    /// <param name="email">The email address to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    /// <summary>
    /// Deletes a user from the database
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the user was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await GetByIdAsync(id, cancellationToken);
        if (user == null)
            return false;

        user.Deactivate();

        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Retrieves a list of users
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The lis of users if found, empty list if not found</returns>
    public async Task<PaginatedList<User>> ListAsync(
        int? pageNumber,
        int? pageSize,
        List<(string PropertyName, bool Ascendent)> sortingFields,
        List<(string PropertyName, object?)> filters,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Users.AsQueryable();

        foreach (var (property, value) in filters)
        {
            if (value is null) continue;

            query = property switch
            {
                "Username" => query.FilterStringField("Username", value),
                "Email" => query.FilterStringField("Email", value),
                "Phone" => query.FilterStringField("Phone", value),
                "Role" => query.Where(w => w.Role == (short)value),
                "Status" => query.Where(w => w.Status == (short)value),
                "City" => query.FilterStringField("City", value),
                "Street" => query.FilterStringField("Street", value),
                "Zipcode" => query.FilterStringField("Zipcode", value),
                "MinLatitude" => query.FilterNumberField("Latitude", (decimal)value),
                "MaxLatitude" => query.FilterNumberField("Latitude", (decimal)value),
                "MinLongitude" => query.FilterNumberField("Longitude", (decimal)value),
                "MaxLongitude" => query.FilterNumberField("Longitude", (decimal)value),
                _ => query
            };
        }

        return await PaginatedList<User>.CreateAsync(
            query.ApplyOrdering(sortingFields),
            pageNumber,
            pageSize,
            cancellationToken);
    }
        

    /// <summary>
    /// Updates a user from the database
    /// </summary>
    /// <param name="user">The user to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the user was updated, false if not found</returns>
    public async Task<bool> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        var existingUser = await GetByIdAsync(user.Id, cancellationToken);
        if (existingUser == null)
            return false;

        existingUser.Update(
            user.Username,
            user.Password,
            user.Email,
            user.Phone,
            user.Status,
            user.Role,
            user.City,
            user.Street,
            user.Number,
            user.Zipcode,
            user.Latitude,
            user.Longitude);

        _context.Users.Update(existingUser);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
