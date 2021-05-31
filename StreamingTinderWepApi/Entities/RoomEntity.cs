using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StreamingTinderWepApi.Entities
{
    [Table("Rooms")]
    public class RoomEntity
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RoomKey is required")]
        [StringLength(20, ErrorMessage = "RoomKey cannot be longer than 20 characters")]
        public string RoomKey { get; set; }

        [Required(ErrorMessage = "Owner_FK_Users_Id is required")]
        public int Owner_FK_Users_Id { get; set; }
    }
}