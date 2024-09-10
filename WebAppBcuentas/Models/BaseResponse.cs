using Newtonsoft.Json;

namespace WebAppBcuentas.Models
{
    public class BaseResponse
    {

            public int Id { get; set; }
            public int Code { get; set; }
            public object Data { get; set; }
            public string Message { get; set; }

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
