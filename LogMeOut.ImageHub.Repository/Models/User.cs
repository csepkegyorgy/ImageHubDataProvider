namespace LogMeOut.ImageHub.Repository.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ProfileImageId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FacebookUserId { get; set; }

        [Required]
        public string Email { get; set; }

        public ICollection<Like> Likes { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
