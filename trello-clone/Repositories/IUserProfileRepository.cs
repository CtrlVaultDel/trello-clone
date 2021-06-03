using trello_clone.Models.Data;

namespace trello_clone.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfile GetByFirebaseId(string firebaseId);
        void Add(UserProfile userProfile);
    }
}
