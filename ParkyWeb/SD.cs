using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb
{
    public static class SD
    {
        public static string APIBaseUrl = "https://localhost:5001/";

        public static string NationalParkAPIPath = APIBaseUrl + "api/nationalparks/";
        public static string TrailAPIPath = APIBaseUrl + "api/trails/";
        public static string ContainerAPIPath = APIBaseUrl + "api/containers/";
        public static string AccountAPIPath = APIBaseUrl + "api/users/";
    }
}
