using System;

namespace BlackRealtors.Core.Constants
{
    public static class Cities
    {
        public const string Hrodna = "hrodna";
        public const string Minsk = "minsk";
        public const string Gomel = "gomel";
        public const string Brest = "brest";
        public const string Viciebsk = "viciebsk";
        public const string Mahiliou = "mahilioŭ";

        public static bool ValidCity(string city)
        {
            if (Hrodna.Equals(city, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (Minsk.Equals(city, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (Gomel.Equals(city, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (Brest.Equals(city, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (Viciebsk.Equals(city, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (Mahiliou.Equals(city, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}
