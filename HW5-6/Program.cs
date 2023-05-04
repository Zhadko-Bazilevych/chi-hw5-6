using HW5_6.dbHelpers;
using HW5_6.Models;
using HW5_6.ModelViews;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using System.Text;

namespace HW5_6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;

            AdoTasks().Wait();
            EFTasks().Wait();
        }

        static async Task AdoTasks()
        {
            AdoHelper ado = new AdoHelper();

            Console.WriteLine("Task 1: select orders with reader");
            PrintList(await ado.YearOrdersWithReaderAsync());

            Console.WriteLine("Task 2: select orders with adapter");
            PrintList(await ado.YearOrdersWithAdapterAsync());

            Console.WriteLine("Task 4: create order");
            if (await ado.CreateAsync(DateTime.Now, 5))
            {
                Console.WriteLine("Замовлення додано успішно");
            }
            else
            {
                Console.WriteLine("Помилка додавання замовлення");
            };

            Console.WriteLine("Task 5: update order");
            if (await ado.UpdateAsync(1001, DateTime.Now.AddDays(-3), 5))
            {
                Console.WriteLine("Замовлення відкореговано успішно");
            }
            else
            {
                Console.WriteLine("Помилка корегування замовлення");
            };

            Console.WriteLine("Task 6: delete order");
            if (await ado.DeleteAsync(1001))
            {
                Console.WriteLine("Замовлення видалено успішно");
            }
            else
            {
                Console.WriteLine("Замовлення не знайдено");
            };
        }

        static async Task EFTasks()
        {
            EFHelper ef = new EFHelper();

            Console.WriteLine("Task 3: select orders with dbContext"); 
            PrintList(await ef.YearOrdersWithContextAsync());

            Console.WriteLine("Task 7.4: create order");
            if (await ef.CreateAsync(DateTime.Now, 5))
            {
                Console.WriteLine("Замовлення додано успішно");
            }
            else
            {
                Console.WriteLine("Помилка додавання замовлення");
            };

            Console.WriteLine("Task 7.5: update order");
            if (await ef.UpdateAsync(1002, DateTime.Now.AddDays(-2), 3))
            {
                Console.WriteLine("Замовлення відкореговано успішно");
            }
            else
            {
                Console.WriteLine("Помилка корегування замовлення");
            };

            Console.WriteLine("Task 7.6: delete order");
            if (await ef.DeleteAsync(1002))
            {
                Console.WriteLine("Замовлення видалено успішно");
            }
            else
            {
                Console.WriteLine("Замовлення не знайдено");
            };
        }

        static void PrintList(List<OrderView> list)
        {
            Console.WriteLine("Id\tDatetime\t\tAnalysis");
            foreach (OrderView order in list)
            {
                Console.WriteLine(order.ToString());
            }
            Console.WriteLine();
        }
    }
}
