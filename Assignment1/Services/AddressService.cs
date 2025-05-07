using ECommerce.Data;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services
{
    public class AddressService : IAddressService
    {
        private readonly ApplicationDbContext _context;

        public AddressService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Address>> GetAddressesByUserIdAsync(int userId)
        {
            return await _context.Addresses
                .Where(a => a.CustomerId == userId)
                .OrderByDescending(a => a.IsDefault)
                .ToListAsync();
        }

        public async Task<Address> GetDefaultAddressAsync(int userId)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.CustomerId == userId && a.IsDefault);
        }

        public async Task AddAddressAsync(Address address)
        {
            if (address.IsDefault)
            {
                var userAddresses = await _context.Addresses.Where(a => a.CustomerId == address.CustomerId).ToListAsync();
                userAddresses.ForEach(a => a.IsDefault = false);
            }
            _context.Add(address);
            await _context.SaveChangesAsync();
        }

        public async Task SetDefaultAddressAsync(int addressId, int userId)
        {
            var userAddresses = await _context.Addresses.Where(a => a.CustomerId == userId).ToListAsync();
            userAddresses.ForEach(a => a.IsDefault = false);

            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == addressId && a.CustomerId == userId);
            if (address != null)
            {
                address.IsDefault = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
