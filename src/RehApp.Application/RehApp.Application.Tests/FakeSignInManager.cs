using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using RehApp.Domain.RelationalDatabase.Entities;

namespace RehApp.Application.Tests;

public class FakeSignInManager : SignInManager<ApplicationUser>
{
    public FakeSignInManager() : base(
        userManager: new Mock<FakeUserManager>().Object, 
        contextAccessor:  Mock.Of<IHttpContextAccessor>(), 
        claimsFactory: Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
        optionsAccessor: null!, 
        logger: null!, 
        schemes: null!, 
        confirmation: null!)
    {
    }
}
