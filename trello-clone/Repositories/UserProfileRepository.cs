using Microsoft.Extensions.Configuration;
using trello_clone.Models.Data;
using trello_clone.Utils;

namespace trello_clone.Repositories { 
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

        public UserProfile GetByFirebaseId(string firebaseId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, FirebaseId, Email, FirstName, LastName
                        FROM [User]                    
                        WHERE FirebaseId = @FirebaseId";

                    DbUtils.AddParameter(cmd, "@FirebaseId", firebaseId);

                    UserProfile user = null;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                FirebaseId = DbUtils.GetString(reader, "FirebaseId"),
                                Email = DbUtils.GetString(reader, "Email"),
                                FirstName = DbUtils.GetString(reader, "FirstName"),
                                LastName = DbUtils.GetString(reader, "LastName"),
                            };
                        }

                        return user;
                    }
                }
            }
        }

        public void Add(UserProfile user)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO [User] (FirebaseId, Email, FirstName, LastName)
                                        OUTPUT INSERTED.ID
                                        VALUES (@FirebaseId, @Email, @FirstName, @LastName)";

                    DbUtils.AddParameter(cmd, "@FirebaseId", user.FirebaseId);
                    DbUtils.AddParameter(cmd, "@Email", user.Email);
                    DbUtils.AddParameter(cmd, "@FirstName", user.FirstName);
                    DbUtils.AddParameter(cmd, "@LastName", user.LastName);

                    user.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
