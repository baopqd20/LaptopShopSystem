namespace LaptopShopSystem.Interfaces
{
    public interface IJwtRepository
    {
        string GenerateJwtToken(string name, string role);
    }
}
