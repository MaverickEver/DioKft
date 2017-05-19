using System;
using System.IO;
using System.Linq;
using MS.WebSolutions.DioKft.DataAccessLayer.Entities;
using MS.WebSolutions.DioKft.DataAccessLayer.Contexts;
using System.Data.Entity;
using System.Text;

namespace MS.WebSolutions.DioKft.DatabaseTester
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1 && !string.IsNullOrEmpty(args[0]))
            {
                Console.WriteLine("Expected argument is the path of a CSV file.");
                return;
            }

            Console.WriteLine(ExecuteImportLogic(args[0]));

            Console.ReadKey();
        }

        private static string ExecuteImportLogic(string filePath)
        {
            var fileContent = File.ReadAllText(filePath);

            var rows = fileContent.Split('\n').Skip(1);
            var missedRows = new StringBuilder("Missed items:");


            using (var context = new ProductContext())
            {
                foreach (var row in rows)
                {
                    var cells = row.Split('|');

                    if (cells.Length != 4)
                    {
                        missedRows.AppendLine($"{string.Join("|",cells)}");
                        continue;
                    }

                    var product = new Product
                    {
                        Name = cells[1],
                        ManufacturerId = CreateEntity(cells[0], context, context.Manufacturers),
                        CategoryId = CreateEntity(cells[2], context, context.Categories),
                        UnitId = CreateEntity(cells[3], context, context.Units)
                    };

                    context.Products.Add(product);
                    context.SaveChanges();
                }                
            }

            return missedRows.ToString();
        }

        private static int CreateEntity<TEntity>(string name, DbContext context, DbSet<TEntity> list) where TEntity : EntityBase, new()
        {
            int id = (from i in list
                       where i.Name == name
                       select i.Id).FirstOrDefault();

            if (id != 0) return id;
            
            var item = new TEntity { Name = name };                                  
            list.Add(item);
            context.SaveChanges();

            return item.Id;
        }
    }
}
