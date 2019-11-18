namespace LogMeOut.ImageHub.Repository.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserRelation
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public User TargetUser { get; set; }

        [Required]
        public UserRelationType RelationType { get; set; }
    }
}