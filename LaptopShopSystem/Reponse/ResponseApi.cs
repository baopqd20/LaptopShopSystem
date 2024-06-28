namespace LaptopShopSystem.Reponse
{
    public class ResponseApi<T>
    {


        public int statusCode { get; set; }
        public string status { get; set; }
        public T data { get; set; }
        public ResponseApi(int statusCode, string status, T data)
        {
            this.statusCode = statusCode;
            this.status = status;
            this.data = data;
        }
    }
}
