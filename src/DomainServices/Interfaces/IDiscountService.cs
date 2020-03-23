using System.Collections.Generic;
using Models;

namespace DomainServices.Interfaces
{
    public interface IDiscountService
    {
        List<Discount> GetDiscount(Booking booking);
    }
}
