using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ProjetoBomNegocio.Models;

namespace ProjetoBomNegocio.Contexto2
{
    public class DB_BomSuporteContext : DbContext
    {
        public DB_BomSuporteContext()
            : base("DB_BomSuporteContext")
        {
        }

        public DbSet<Tab_Suporte> Suportes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Tab_Suporte>()
                .Property(f => f.data_abertura)
                .HasColumnType("DateTime");*/
        }

        public static DB_BomSuporteContext Create()
        {
            return new DB_BomSuporteContext();
        }

    }
}