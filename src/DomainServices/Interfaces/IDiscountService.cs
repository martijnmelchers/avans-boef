using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DomainServices.Interfaces
{
    public interface IDiscountService
    {
        List<Discount> GetDiscount(Booking booking);
    }
}
