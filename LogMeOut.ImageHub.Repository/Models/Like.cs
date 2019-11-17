namespace LogMeOut.ImageHub.Repository.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Like
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Post Post { get; set; }
    }
}
