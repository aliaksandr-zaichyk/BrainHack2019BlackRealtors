using System;

namespace BlackRealtors.Core.Constants
{
    public static class OrganizationType
    {
        public const string Pharmacy = "pharmacy";
        public const string Hospital = "hospital";
        public const string School = "school";
        public const string Kindergarten = "kindergarten";
        public const string GroceryStore = "grocery store";
        public const string Atm = "atm";
        public const string BeautySalon = "beauty salon";
        public const string ShoppingMall = "shopping mall";
        public const string VeterinaryClinic = "veterinary clinic";
        public const string Fitness = "fitness";

        public static bool ValidOrganizationType(string organizationType)
        {
            if (Pharmacy.Equals(organizationType, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (Hospital.Equals(organizationType, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (School.Equals(organizationType, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (Kindergarten.Equals(organizationType, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (GroceryStore.Equals(organizationType, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (Atm.Equals(organizationType, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (BeautySalon.Equals(organizationType, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (ShoppingMall.Equals(organizationType, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (VeterinaryClinic.Equals(organizationType, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (Fitness.Equals(organizationType, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}
