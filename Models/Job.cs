using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MechanicApp.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CarManufacturer { get; set; }
        public string CarModel { get; set; }
        public string ClientName { get; set; }
        public string ClientPhoneNumber { get; set; }
        public List<Defect> Defects { get; set; }
    }
}