using System.Threading.Tasks;

namespace Models.Repository.Interfaces
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        Task<Booking> GetByAccessToken(string accessToken);
    }
}