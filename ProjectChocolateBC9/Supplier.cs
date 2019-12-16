using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChocolateBC9
{
    class Supplier
    {
        const double minimumAmount = 50;
        const double AmountRange = 50;
        int numberOfDifferentQualities = Enum.GetValues(typeof(ItemQuality)).Length;
       
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
            
        }
        public readonly Offer offer;

        public Supplier(string firstName, string lastName, Random rand)
        {
            FirstName = firstName;
            LastName = lastName;
            // Quantity will be between [50, 100)
            double randomQuantityBetween0ANd100 =  minimumAmount +  rand.NextDouble() * AmountRange; 
            offer = new Offer(randomQuantityBetween0ANd100, (ItemQuality)rand.Next(0, numberOfDifferentQualities), rand); 
        }

    }
}
