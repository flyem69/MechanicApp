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
            Database.SetInitializer(new DatabaseInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }
    }

    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            base.Seed(context);
            User user = new User();
            user.Name = "pansamochod";
            user.Password = "123456";
            context.Users.Add(user);

            Job job = new Models.Job();
            job.CarManufacturer = "Honda";
            job.CarModel = "Jazz";
            job.ClientName = "Jagzam Nicram";
            job.ClientPhoneNumber = "111222333";

            List<Defect> defects = new List<Defect>();
            defects.Add(new Defect("Silnik wyjebało"));
            defects.Add(new Defect("Skrzynie też"));
            defects.Add(new Defect("A zawieszenia to nigdy nie było"));
            job.Defects = defects;
            context.Jobs.Add(job);
            
        }
    }
}