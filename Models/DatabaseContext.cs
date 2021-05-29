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
        //public DbSet<Job> Jobs { get; set; }
    }

    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            base.Seed(context);
            User user = new User();
            user.Name = "pansamochod";
            user.Password = "123456";

            List<Job> jobs = new List<Job>();

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

            jobs.Add(job);

            job = new Models.Job();
            job.CarManufacturer = "Opel";
            job.CarModel = "Astra GTC";
            job.ClientName = "Piotr Fronczewski";
            job.ClientPhoneNumber = "997213769";

            defects = new List<Defect>();
            defects.Add(new Defect("Hamulce do naprawy"));
            defects.Add(new Defect("Kot nasikal do wentylacji"));
            job.Defects = defects;

            jobs.Add(job);
            user.Jobs = jobs;

            context.Users.Add(user);

/*            user = new User();
            user.Name = "samochodzik";
            user.Password = "12345";

            jobs = new List<Job>();

            job = new Models.Job();
            job.CarManufacturer = "Audi";
            job.CarModel = "A3";
            job.ClientName = "Marcin Musial";
            job.ClientPhoneNumber = "123456789";

            defects = new List<Defect>();
            defects.Add(new Defect("Zawieszenie kaput"));
            defects.Add(new Defect("Maslo orzechowe"));
            job.Defects = defects;

            jobs.Add(job);
            user.Jobs = jobs;

            context.Users.Add(user);*/
            /*Job job = new Models.Job();
            job.CarManufacturer = "Honda";
            job.CarModel = "Jazz";
            job.ClientName = "Jagzam Nicram";
            job.ClientPhoneNumber = "111222333";

            List<Defect> defects = new List<Defect>();
            defects.Add(new Defect("Silnik wyjebało"));
            defects.Add(new Defect("Skrzynie też"));
            defects.Add(new Defect("A zawieszenia to nigdy nie było"));
            job.Defects = defects;
            context.Jobs.Add(job);*/

        }
    }
}