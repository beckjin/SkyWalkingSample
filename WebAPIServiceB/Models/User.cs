using System.ComponentModel.DataAnnotations;

namespace WebAPIService2.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}
