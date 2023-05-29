using Microsoft.AspNetCore.Identity;

namespace DinnerIn.Web.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll(string searchString = null); 
    }
}
