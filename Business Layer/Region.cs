using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class Region
    {
        [Key]
        public int ID { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }

        public List<Interest> Interests { get; set; }

        public List<User> Users { get; set; }

        private Region()
        {
            this.Interests = new List<Interest>();
            this.Users = new List<User>();
        }

        public Region(int iD, string name)
        {
            this.Interests = new List<Interest>();
            this.Users = new List<User>();
            ID = iD;
            Name = name;
        }
    }
}
