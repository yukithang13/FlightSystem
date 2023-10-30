﻿using System.ComponentModel.DataAnnotations;

namespace FlightSystem.Data
{
    public class GroupInfo
    {
        [Key]
        public int GroupInfoId { get; set; }
        public int DocumentId { get; set; }

        public int GroupId { get; set; }


    }
}
