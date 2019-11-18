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

        public List<Like> Likes { get; set; } = new List<Like>();

        public List<Post> Posts { get; set; } = new List<Post>();

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public List<UserRelation> Relations { get; set; } = new List<UserRelation>();

        public List<UserRelation> RelationTargetings { get; set; } = new List<UserRelation>();
    }
}
