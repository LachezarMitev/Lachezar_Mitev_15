using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required, MaxLength(20)]
        public string FName { get; set; }

        [Required, MaxLength(20)]
        public string LName { get; set; }

        [Range(18, 81)]
        public int Age { get; set; }

        [Required, MaxLength(20)]
        public string UName { get; set; }

        [Required, MaxLength(70)]
        public string Password { get; set; }

        [Required, MaxLength(20)]
        public string Email { get; set; }

        [ForeignKey("Region")]
        public int RegionID { get; set; }

        public Region Region { get; set; }

        public List<User> Friends { get; set; }

        public List<Interest> Interests { get; set; }

        private User()
        {
            this.Friends = new List<User>();
            this.Interests = new List<Interest>();
        }

        public User(int iD, string fName, string lName, int age, string uName, string password, string email, int regionID)
        {
            this.Friends = new List<User>();
            this.Interests = new List<Interest>();
            ID = iD;
            FName = fName;
            LName = lName;
            Age = age;
            UName = uName;
            Password = password;
            Email = email;
            RegionID = regionID;
        }
    }
}
