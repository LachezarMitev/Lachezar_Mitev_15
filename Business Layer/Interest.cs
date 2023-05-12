using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class Interest
    {
        [Key]
        public int ID { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }

        public List<User> Users { get; set; }

        public List<Region> Regions { get; set; }

        private Interest()
        {
            this.Users = new List<User>();
            this.Regions = new List<Region>();
        }

        public Interest(int iD, string name)
        {
            this.Users = new List<User>();
            this.Regions = new List<Region>();
            ID = iD;
            Name = name;
        }
    }
}
