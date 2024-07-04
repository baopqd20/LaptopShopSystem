using LaptopShopSystem.Data;
using LaptopShopSystem.Interfaces;
using LaptopShopSystem.Models;

namespace LaptopShopSystem.Repository
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly DataContext _context;

        public ShipmentRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateShipment(Order order)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool UpdateShipment(Shipment shipment)
        {
            throw new NotImplementedException();
        }
    }
}
