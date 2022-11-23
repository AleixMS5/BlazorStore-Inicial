using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorStore.Shared.Utils
{
    public static class FieldLenghts
    {
        public static class User
        {
            public const int Name = 30;
            public const int Email = 80;
            public const int Address = 120;
            public const int City = 30;
            public const int Country = 40;
            public const int State = 40;
            public const int ZipCode = 6;
        }

        public static class Product
        {
            public const int Name = 50;
            public const int Quantity = 3;
            public const int UnitPrice = 4;
            public const int Description = 250;
        }

        public static class Category
        {
            public const int Name = 20;
        }

        public static class Payments
        {
            public const int NameOnCard = 80;
            public const int CardNumber = 20;
            public const int CvcCode = 3;
        }
    }
}
