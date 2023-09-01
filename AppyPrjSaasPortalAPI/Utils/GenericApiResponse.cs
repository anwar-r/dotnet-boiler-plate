namespace AppyPrjSaasPortalAPI.Utils
{
    public class GenericApiResponse
    {
        public string Message { get; set; }
        public object Data { get; set; }
        private GenericApiResponse(string message, object data)
        {
            Message = message;
            Data = data;
        }

        public static GenericApiResponse Get(string message, object data)
        {
            return new GenericApiResponse(message, data);
        }
        public static GenericApiResponse Get(string message)
        {
            return new GenericApiResponse(message, new object());
        }
    }
}
