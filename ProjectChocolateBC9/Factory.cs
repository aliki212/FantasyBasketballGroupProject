using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChocolateBC9
{
    enum ChocolateType
    { 
        WhiteChocolate,
        DarkChocolate,
        MilkChocolate,
        MilkChocolateWithAlmonds,
        MilkChocolateWithHazelnuts
    }

    class Factory
    {

        public int NumOfEmployees { get; private set; }
        public double RawMaterial { get; private set; }
        private DateTime orderDate;
        public double MinimumRawMaterial { get; private set; }

        public Factory(double rawMaterial)
        {
            RawMaterial = rawMaterial;
            MinimumRawMaterial = 0.1 * RawMaterial;
        }

        //Select the best Supplier Between 3 For the current Year...
        public Offer SelectSupplier(List<Supplier> suppliers, DateTime orderDate)
        {

            this.orderDate = orderDate;
            double lowestCost = double.MaxValue;
            Supplier bestSupplier = null;

            foreach (var supplier in suppliers)
            {
                double offerCost = supplier.offer.TotalPrice;
                lowestCost = Math.Min(offerCost, lowestCost);

                if (lowestCost == offerCost)
                    bestSupplier = supplier;
            }
            return bestSupplier.offer;       
        }

        public void AddRawMaterial(Offer selectedOffer)
        {
            RawMaterial += selectedOffer.Quantity;
        }

        public void CheckForRawMaterialAvailability(double requestedItems, Offer selectedOffer, DateTime date)
        {

            if (RawMaterial >= MinimumRawMaterial)
            {
                ProduceChocolates(requestedItems);
            }
            else if (date < orderDate.AddYears(1))
            {
                this.AddRawMaterial(selectedOffer);
                Console.WriteLine("An extra order was done!");
            }
            else
            {
                Console.WriteLine("We should find a new supplier!");
            }
        }

        public void ProduceChocolates(double requestedItems)
        {
            if (RawMaterial > requestedItems)
            {
                RawMaterial -= requestedItems;
                // To be completed....

            }
                
            else
                Console.WriteLine("The requsted amount of Chocolate cannot be produced! ");
        }
    }
}
