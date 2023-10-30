using System.ComponentModel.DataAnnotations;

namespace FlightSystem.Data
{
    public class GroupRole
    {
        [Key]
        public int GroupRoleId { get; set; }
        [StringLength(450)]
        public string UserId { get; set; }


        public int GroupId { get; set; }



    }
}
