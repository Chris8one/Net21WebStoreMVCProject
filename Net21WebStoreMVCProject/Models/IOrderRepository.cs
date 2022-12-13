using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net21WebStoreMVCProject.Models
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
