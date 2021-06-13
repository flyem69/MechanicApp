using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MechanicApp.Models
{
    public class Defect
    {
        public Defect()
        {

        }
        public Defect(string name)
        {
            Name = name;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int JobId { get; set; }
        public string Name { get; set; }
        public bool IsFinished { get; set; }
    }
}