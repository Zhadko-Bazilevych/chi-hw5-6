using HW5_6.Models;
using HW5_6.ModelViews;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HW5_6.dbHelpers
{
    class EFHelper : AbstractDbHelper
    {
        HW3Context db;

        public EFHelper()
        {
            db = new HW3Context(connectionString);
        }

        public async Task<List<OrderView>> YearOrdersWithContextAsync()
        {
            var orders = await db.Orders.Include(y=>y.OrdAnNavigation)
                .Select(x => new OrderView
                {
                    OrdId = x.OrdId,
                    OrdDatetime = x.OrdDatetime,
                    AnName = x.OrdAnNavigation.AnName
                }).OrderBy(x=>x.OrdDatetime)
                .ToListAsync();

            return orders;
        }

        public async Task<bool> CreateAsync(DateTime datetime, int analysisId)
        {
            if(db.Analyses.Any(x=>x.AnId == analysisId)) 
            {
                db.Orders.Add(
                    new Order
                    {
                        OrdDatetime = datetime,
                        OrdAn = analysisId
                    });
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(int id, DateTime datetime, int analysisId)
        {
            if (db.Analyses.Any(x => x.AnId == analysisId))
            {
                var order = await db.Orders.Where(x => x.OrdId == id).FirstOrDefaultAsync();
                if (order != null)
                {
                    order.OrdDatetime = datetime;
                    order.OrdAn = analysisId;
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await db.Orders.Where(x => x.OrdId == id).FirstOrDefaultAsync();
            if(order != null)
            {
                db.Remove(order);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
