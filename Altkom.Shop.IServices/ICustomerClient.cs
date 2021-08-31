using Altkom.Shop.Models;
using System.Threading.Tasks;

namespace Altkom.Shop.IServices
{
    public interface ICustomerClient
    {
        Task YouHaveGotNewCustomer(Customer customer);
        Task Pong(string message);
    }

    public interface ICustomerServer
    {
        Task SendCustomer(Customer customer);
        Task Ping(string message);
    }
}
