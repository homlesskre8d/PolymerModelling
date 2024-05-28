﻿using ChemModel.Data.DbTables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemModel.Data
{
    public class Context : DbContext
    {
        public string Test { get; set; } = "";
        public DbSet<Unit> Units { get; set; } = null!;
        public DbSet<UserAddMaterial> UserAddMaterials { get; set; } = null!;
        public DbSet<MaterialPropertyBind> MaterialPropertyBinds { get; set; } = null!;
        public DbSet<MaterialEmpiricBind> MaterialEmpiricBinds { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Material> Materials { get; set; } = null!;
        public DbSet<Property> Properties { get; set; } = null!;
        public DbSet<EmpiricCoefficient> EmpiricCoefficients { get; set; } = null!;

        public Context()
        {
            Database.EnsureCreated();
        }

        public Context(string test)
        {
            Test = test;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(Test != "")
                optionsBuilder.UseSqlite($"Data Source={Test}");
            else
            {
                optionsBuilder.UseSqlite($"Data Source={DBConfig.Destination}");

            }
        }
    }
}
