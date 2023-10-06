// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Models;

// Add profile data for application users by adding properties to the User class
public class User : MongoIdentityUser<int>
{
    
}
