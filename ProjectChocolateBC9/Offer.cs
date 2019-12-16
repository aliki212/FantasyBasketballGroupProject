using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChocolateBC9
{
    
    enum ItemQuality
    {
        Excellent,
        Good,
        Poor
    }

    class Offer
    {
        const double minimumBaseCost = 100;
        const double baseCostRange = 20;
        public double Quantity { get;  private set; }
        public ItemQuality Quality { get; private set; }

        public double TotalPrice { get; }
         
        private double pricePerKilo; 
        public double PricePerKilo
        {
            get
            {
                return pricePerKilo;
            }

            set
            {
                switch (Quality)
                {
                    case ItemQuality.Excellent:
                        pricePerKilo = value * 1.20;
                        break;

                    case ItemQuality.Good:
                        pricePerKilo = value *  1.10;
                        break;

                    case ItemQuality.Poor:
                        pricePerKilo = value * 1.05;
                        break;
                }
            }
        }

        public Offer(double quantity, ItemQuality quality, Random rand)
        {
            // Maybe rand inside???????

            // Quantity will be between [50, 100)
            Quantity = quantity; 
            Quality = quality;
            // Price Per Kilo will be between [105, 144)
            PricePerKilo = minimumBaseCost + rand.NextDouble() * baseCostRange; 
            TotalPrice = PricePerKilo * Quantity;
        }

    }
}
