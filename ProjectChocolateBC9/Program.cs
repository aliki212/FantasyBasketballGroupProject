using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChocolateBC9
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test for submitting offers from the three suppliers

            Random rand = new Random();
            Supplier supplier1 = new Supplier("Giannis", "Diakidis", rand);
            Console.WriteLine($"{supplier1.FullName}, offer is: {supplier1.offer.Quantity:F2} items, of {supplier1.offer.Quality} quality:" +
                              $"\nwith cost per kilo: {supplier1.offer.PricePerKilo:F2} and Total Cost: {supplier1.offer.TotalPrice:F2}");

            Supplier supplier2 = new Supplier("George", "Papas", rand);
            Console.WriteLine($"\n{supplier2.FullName}, offer is: {supplier2.offer.Quantity:F2} items, of {supplier2.offer.Quality} quality:" +
                               $"\nwith cost per kilo: {supplier2.offer.PricePerKilo:F2} and Total Cost: {supplier2.offer.TotalPrice:F2}");

            Supplier supplier3 = new Supplier("Kostas", "Argyriou", rand);
            Console.WriteLine($"\n{supplier3.FullName}, offer is: {supplier3.offer.Quantity:F2} items, of {supplier3.offer.Quality} quality:" +
                              $"\nwith cost per kilo: {supplier3.offer.PricePerKilo:F2} and Total Cost: {supplier3.offer.TotalPrice:F2}"); ;

            List<Supplier> suppliers = new List<Supplier>() { supplier1, supplier2, supplier3 };

            //Test for creating a company with one factory and one store

            Company company = new Company("ION");
            List<Factory> factories = company.AddFactories(1);
            List<Store> stores = company.AddStores(1);

            DateTime orderDate = DateTime.Now; 
            Offer bestOffer = factories[0].SelectSupplier(suppliers, orderDate);
            Console.WriteLine($"\nBest Offer: Quantity:{bestOffer.Quantity:F2}, Quality:{bestOffer.Quality}, Total Price:{bestOffer.TotalPrice:F2} ");
            
            Console.WriteLine($"Factory's Raw Material before Order: {factories[0].RawMaterial:F2}");
            factories[0].AddRawMaterial(bestOffer);
            Console.WriteLine($"Factory's Raw Material after Order: {factories[0].RawMaterial:F2}");

            DateTime sixMonthsLater = orderDate.AddMonths(6);
            factories[0].CheckForRawMaterialAvailability(100, bestOffer, sixMonthsLater);
            Console.WriteLine($"Factory's Raw Material after Making Chocolates: {factories[0].RawMaterial:F2}");

            


        }
    }
}
