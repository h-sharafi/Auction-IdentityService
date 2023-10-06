using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using IdentityService.Application.Interfaces;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

namespace IdentityService.Infrastructure.Services;

public class ProfileService : IProfileService
{
    private readonly ILogger<ProfileService> _logger;
    private readonly UserManager<User> _userManager;

    private readonly IMongoCollectionService _mongoCollectionService;

    public ProfileService(ILogger<ProfileService> logger, UserManager<User> userManager, IMongoCollectionService mongoCollectionService)
    {
        _logger = logger;
        _userManager = userManager;
        _mongoCollectionService = mongoCollectionService;
    }

    public async virtual Task GetProfileDataAsync(ProfileDataRequestContext context)
    {

        // var requestedClaimTypes = context.RequestedClaimTypes;
        // var user = context.Subject;

        // your implementation to retrieve the requested information

        var subjectId = int.Parse(context.Subject.GetSubjectId());
        var user = await _userManager.FindByIdAsync(subjectId.ToString());
        var filter = Builders<Role>.Filter.In(c => c.Id, user.Roles);
        var roles = await _mongoCollectionService.Find(filter)
             .Project(c => new { c.Id, c.Name })
             .ToListAsync();

        var claims = new List<Claim>
        {
            new Claim(JwtClaimTypes.PreferredUserName, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };
        if (roles.Any())
            claims.Add(new Claim("Role", string.Join(",", roles.Select(c => c.Name))));

        context.IssuedClaims.AddRange(claims);
        // throw new Exception("kkkkkkkkkkkkkkkkkk");
        // context.IssuedClaims = claims;


    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var subjectId = context.Subject.GetSubjectId();

        var user = await _userManager.FindByIdAsync(subjectId);

        context.IsActive = user != null;
    }
}
