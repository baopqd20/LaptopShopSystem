using LaptopShopSystem.Models;

namespace LaptopShopSystem.Interfaces
{
    public interface IShipmentRepository
    {
        bool CreateShipment(Order order);
        bool UpdateShipment(Shipment shipment);
        bool Save();
    }
}
