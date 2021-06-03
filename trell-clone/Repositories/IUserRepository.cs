using trell_clone.Models.Data;

namespace trell_clone.Repositories
{
    public interface IUserRepository
    {
        UserProfile GetByFirebaseId(string firebaseId);
        void Add(UserProfile userProfile);
    }
}
