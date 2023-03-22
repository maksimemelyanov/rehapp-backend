using Microsoft.AspNetCore.Identity;
using Moq;
using RehApp.Domain.RelationalDatabase.Entities;

namespace RehApp.Application.Tests;

public class FakeUserManager : UserManager<ApplicationUser>
{
    public FakeUserManager() : base(
        store: Mock.Of<IUserStore<ApplicationUser>>(),
        optionsAccessor: null!,
        passwordHasher: null!,
        userValidators: null!,
        passwordValidators: null!,
        keyNormalizer: null!,
        errors: null!,
        services: null!, 
        logger: null!)
    {

    }
}
