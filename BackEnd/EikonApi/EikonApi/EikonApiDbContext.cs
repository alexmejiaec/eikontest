using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EikonApi
{
    public class EikonApiDbContext : DbContext
    {
        public EikonApiDbContext(DbContextOptions<EikonApiDbContext> options) : base(options)
        {

        }

        public DbSet<Factura> Facturas { get; set; }
        public DbSet<FacturaDetalle> FacturaDetalles { get; set; }


        /*
         MIgraciones
                add-migration Inicial
                    update-database
         */
    }
}
