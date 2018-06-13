using Parking.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Parking.DatabaseConfiguration
{
    public class DbConfig : DbContext
    {
        //public DbConfig(DbContextOptions<SchoolContext> options) : base(options)
        //{
        //}

        public DbSet<ParkingEntity> Parkers{ get; set; }
    }
}