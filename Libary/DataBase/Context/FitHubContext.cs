using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitHub.Libary.DataBase.Entities;
using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;
using Microsoft.EntityFrameworkCore;

namespace FitHub.Libary.DataBase.Context
{
    public class FitHubContext : DbContext
    {
         public FitHubContext(DbContextOptions<FitHubContext> options) : base(options)
        {


        }
        public DbSet<Customer> Customer { get; set; }
         public DbSet<Order> Order { get; set; }



    }
}