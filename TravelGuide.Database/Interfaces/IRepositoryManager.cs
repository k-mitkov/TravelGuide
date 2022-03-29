using System.Collections.Generic;
using System.Threading.Tasks;
using TravelGuide.Database.Entities;

namespace TravelGuide.Database.Interfaces
{
    public interface IRepositoryManager
    {
        IGenericRepository<Comment> CommentsRepository { get; set; }
        IGenericRepository<Landmark> LandmarksRepository { get; set; }
        IGenericRepository<User> UsersRepository { get; set; }

        Task<IEnumerable<TResult>> QueryAsync<TResult>(string query) where TResult : class, new();
        Task<TResult> ExecuteScalarAsync<TResult>(string query);

    }
}
