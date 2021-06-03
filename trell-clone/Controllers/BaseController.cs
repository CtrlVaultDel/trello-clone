using trell_clone.Models.Data;
using trell_clone.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace trell_clone.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IUserProfileRepository _userRepository;

        public BaseController() { }

        // Retrieves the current user object by using the provided firebaseId
        protected UserProfile GetCurrentUser()
        {
            // Get User Claims

            var firebaseId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userRepository.GetByFirebaseId(firebaseId);
        }
    }
}
