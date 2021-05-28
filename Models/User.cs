using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MechanicApp.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string HashedPassword { get; set; }
        public string Password { set
            {
                SHA512 hasher = new SHA512Managed();
                byte[] pass = Encoding.Unicode.GetBytes(value);
                byte[] res = hasher.ComputeHash(pass);
                HashedPassword = Convert.ToBase64String(res);
            } }
        public List<Job> Jobs { get; set; }
    }
}