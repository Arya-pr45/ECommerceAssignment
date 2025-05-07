using ECommerce.Models;

namespace ECommerce.Services
{
    public interface IAddressService
    {
        Task<List<Address>> GetAddressesByUserIdAsync(int userId);
        Task<Address> GetDefaultAddressAsync(int userId);
        Task AddAddressAsync(Address address);
        Task SetDefaultAddressAsync(int addressId, int userId);
    }
}