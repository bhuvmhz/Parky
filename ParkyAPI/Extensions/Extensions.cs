using ParkyAPI.Models;
using ParkyAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace ParkyAPI.Extensions
{
    public static class Extensions
    {
        public static string TrimAndLower(this string text)
        {
            return text?.Trim().ToLower();
        }

        public static string TrimAndUpper(this string text)
        {
            return text?.Trim().ToUpper();
        }
    }
}
