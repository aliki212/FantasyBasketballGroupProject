using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectChocolateBC9
{
    class Company
    {
        public string BrandName {get; private set;}
        private List<Factory> factories;
        private List<Store> stores;

        public Company(string brandName)
        {
            BrandName = brandName;
            factories = new List<Factory>();
            stores = new List<Store>();
        }

        public List<Factory> AddFactories(int numberOfFactories)
        {
            Factory factory = null;
            for(int i = 0; i < numberOfFactories; i++)
            {
                factory = new Factory(100);
                factories.Add(factory);
            }
            return factories;
        }

        public List<Store> AddStores(int numberOfStores)
        {
            Store store = null;
            for (int i = 0; i < numberOfStores; i++)
            {
                store = new Store();
                stores.Add(store);
            }
            return stores;
        }

    }
}
