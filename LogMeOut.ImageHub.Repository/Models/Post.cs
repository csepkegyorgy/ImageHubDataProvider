namespace LogMeOut.ImageHub.Repository.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        [Key]
        public Guid PostId { get; set; }

        [Required]
        public string ImageId { get; set; }

        [Required]
        public string PostDescription { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public User Poster { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
