using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SelfBoard.Domain.Concrete;
using SelfBoard.Domain.Entities;

public class ApplicationUserManager : UserManager<ApplicationUser>
{
    public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
    {
    }

    public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
    {
        EFSelfBoardContext db = context.Get<EFSelfBoardContext>();
        ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
        return manager;
    }
}