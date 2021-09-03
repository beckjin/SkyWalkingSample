using System.ComponentModel.DataAnnotations;

namespace GrpcService.Repositories.EF.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}
