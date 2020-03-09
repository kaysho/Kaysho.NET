using Xamarin.Essentials;

namespace Kaysho.NET.Mobile.Constants
{
    public class ApiConstants
    {
        public static string AzureBackendUrl =
            DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
        //public const string BaseApiUrl = "https://kaysho-net-api.conveyor.cloud/";
        public const string BaseApiUrl = "http://192.168.0.119:45465/";
        public const string RegisterEndpoint = "api/Authentication/register";
        public const string AuthenticateEndpoint = "api/Authentication/login";
    }
}
