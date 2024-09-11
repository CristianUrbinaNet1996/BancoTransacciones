using Newtonsoft.Json;

namespace WebAppBcuentas.Models
{
    public class BaseResponse
    {

        public bool? status { get; set; }
        public string? Message { get; set; }
        public string? DebugMessage { get; set; }
        public string? MapId { get; set; }
        public object? Data { get; set; }
        public T ToObjet<T>()
            {
                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    CheckAdditionalContent = true,
                    TypeNameHandling = TypeNameHandling.None,
                    MissingMemberHandling = MissingMemberHandling.Error
                };

                if (Data == null)
                    return default(T);

                return JsonConvert.DeserializeObject<T>(Data.ToString(), jsonSerializerSettings);
            }
        
    }
}
