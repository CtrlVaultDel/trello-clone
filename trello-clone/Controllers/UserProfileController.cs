using trello_clone.Models.Data;
using trello_clone.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace trello_clone.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(
            IUserProfileRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{firebaseId}")]
        public IActionResult GetByFirebaseId(string firebaseId)
        {
            var user = _userRepository.GetByFirebaseId(firebaseId);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Register(UserProfile userProfile)
        {
            _userRepository.Add(userProfile);
            return CreatedAtAction(nameof(GetByFirebaseId), new { firebaseId = userProfile.FirebaseId }, userProfile);
        }
    }
}