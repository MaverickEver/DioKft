using System;
using MS.WebSolutions.DioKft.DataAccessLayer.Contexts;

namespace MS.WebSolutions.DioKft.DatabaseTester
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ProductContext())
            {
                var products = context.Products;
            }

            Console.ReadKey();
        }
    }
}
