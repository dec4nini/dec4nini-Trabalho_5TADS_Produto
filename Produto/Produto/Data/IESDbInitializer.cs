using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produto.Data
{
    public class IESDbInitializer
    {
        public static void Initialize(IESContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Produtos.Any())
            {
                return;
            }

            context.SaveChanges();
        }
    }
}
