using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MechanicApp.Models
{
    [DbConfigurationType(typeof(MySql.Data.EntityFramework.MySqlEFConfiguration))]
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DatabaseContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DatabaseContext>());
        }

        public DbSet<Job> Jobs { get; set; }
    }
}