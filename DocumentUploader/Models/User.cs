using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DocumentUploader.Models
{
    public class User
    {

        [Key]
        public string Email { get; set; }
        public string AccountId { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        int Role { get; set; }

        public List<File> Files { get; set; }

        public User()
        {
            Files = new List<File>();
        }

    }
   
}
