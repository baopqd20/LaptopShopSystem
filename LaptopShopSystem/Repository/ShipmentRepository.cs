using System.Globalization;
using LaptopShopSystem.Data;
using LaptopShopSystem.Interfaces;
using LaptopShopSystem.Models;
using Newtonsoft.Json.Linq;

namespace LaptopShopSystem.Repository
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly DataContext _context;
        private readonly HttpClient _httpClient;

        public ShipmentRepository(DataContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }



        public async Task<(double Latitude, double Longitude)> GetCoordinatesAsync(string address, string apiKey)
        {
            string url = $"https://api.openrouteservice.org/geocode/search?api_key={apiKey}&text={Uri.EscapeDataString(address)}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseBody);

            var features = json["features"];
            if (features != null && features.HasValues)
            {
                var coordinates = features[0]["geometry"]["coordinates"];
                if (coordinates != null && coordinates.Count() == 2)
                {
                    double longitude = (double)coordinates[0];
                    double latitude = (double)coordinates[1];
                    return (latitude, longitude);
                }
            }

            throw new Exception("Could not find coordinates for the provided address.");
        }
        public async Task<(double distance, double duration)> GetDistanceAsync(double originLat, double originLon, double destLat, double destLon, string apiKey)
        {


            // Định dạng các giá trị tọa độ với dấu thập phân là dấu chấm
            string formattedOriginLat = originLat.ToString(CultureInfo.InvariantCulture);
            string formattedOriginLon = originLon.ToString(CultureInfo.InvariantCulture);
            string formattedDestLat = destLat.ToString(CultureInfo.InvariantCulture);
            string formattedDestLon = destLon.ToString(CultureInfo.InvariantCulture);

            string url = $"https://api.openrouteservice.org/v2/directions/driving-car?api_key={apiKey}&start={formattedOriginLon},{formattedOriginLat}&end={formattedDestLon},{formattedDestLat}";

            Console.WriteLine(url);
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseBody);

            var distanceElement = json["features"]?[0]?["properties"]?["segments"]?[0]["distance"];
            var durationElement = json["features"]?[0]?["properties"]?["segments"]?[0]["duration"];
            if (distanceElement != null && durationElement != null)
            {
                var distance = (double)distanceElement / 1000;
                var duration = (double)durationElement;
                return (distance, duration); // Trả về khoảng cách bằng km
            }

            throw new Exception("Could not calculate distance.");
        }
        public async Task<Shipment> CreateShipment(Order order)
        {
            var apiKey = "5b3ce3597851110001cf6248c441b8ad1fac461ca11ddfb225f092b3";
            var end = _context.Users.Where(p => p.Id == order.UserId).FirstOrDefault().Address;
            var originCoordinates = await GetCoordinatesAsync("Ha Noi", apiKey);
            var destinationCoordinates = await GetCoordinatesAsync(end, apiKey);

            var GetInfoShipment = await GetDistanceAsync(originCoordinates.Latitude, originCoordinates.Longitude, destinationCoordinates.Latitude, destinationCoordinates.Longitude, apiKey);
            var distance = GetInfoShipment.distance;
            var duration = (int)GetInfoShipment.duration;
            var shipment = new Shipment
            {
                OrderId = order.Id,
                Start = "Ha Noi",
                End = end,
                IntentTime = duration.ToString(),
                ShipFee = SetShipFee((int)distance),
                Status = "Pending"
            };
            _context.Add(shipment);
            return shipment;

        }

        public int SetShipFee(int distance)
        {
            if (distance < 100 && distance > 0)
            {
                return 0;
            }
            else if (distance < 500)
            {
                return 100000;
            }
            else if (distance < 1000)
            {
                return 150000;
            }
            return 200000;

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
