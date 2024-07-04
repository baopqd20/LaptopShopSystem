using LaptopShopSystem.Models;

namespace LaptopShopSystem.Interfaces
{
    public interface IShipmentRepository
    {
        Task<Shipment> CreateShipment(Order order);
        bool UpdateShipment(Shipment shipment);
        bool Save();
    }
}
