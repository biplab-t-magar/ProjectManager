using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models.UtilityModels
{
    public class UserActivity
    {

        [Required]
        public int ProjectId {get; set;}

        [Required]
        public int TaskId {get; set;}

        [Required]
        public string Activity {get; set;}

        [Required]
        public DateTime Time {get; set;}
    }
}