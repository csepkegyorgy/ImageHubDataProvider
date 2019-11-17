namespace LogMeOut.ImageHub.Repository.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public User Commenter { get; set; }

        [Required]
        public Post Post { get; set; }
    }
}
