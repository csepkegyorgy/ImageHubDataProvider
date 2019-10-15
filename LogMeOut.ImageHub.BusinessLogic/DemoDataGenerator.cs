namespace LogMeOut.ImageHub.BusinessLogic
{
    using LogMeOut.ImageHub.Interfaces.Entity;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public static class DemoDataGenerator
    {
        private static int generateUserCount = 20;
        private static int generatePostPerUserCountMin = 3;
        private static int generatePostPerUserCountMax = 8;
        private static int generateCommentPerPostCountMin = 0;
        private static int generateCommentPerPostCountMax = 4;

        static DemoDataGenerator()
        {
            GenerateDemoUsers();
            GenerateDemoPosts();
            GenerateDemoComments();
        }

        private static Random Random = new Random();
        private static List<string> MaleFirstNames = new List<string>()
        {
            "Isaias",
            "Demetrius",
            "Elisha",
            "Jensen",
            "Caden",
            "Hugo",
            "Talon",
            "Nasir",
            "Calvin",
            "Blake",
            "Santos",
            "Alexis"
        };
        private static List<string> FemaleFirstNames = new List<string>()
        {
            "Rihanna",
            "Natalia",
            "Madisyn",
            "Luz",
            "Kaylyn",
            "Nina",
            "Kiera",
            "Dahlia",
            "Amelia",
            "Nicole",
            "Keyla",
            "Priscilla"
        };
        private static List<string> LastNames = new List<string>()
        {
            "Stein", 
            "Bowers",
            "Kemp",
            "Blackburn",
            "Jenkins",
            "Church",
            "Patel",
            "Baxter",
            "Kelley",
            "Mcdowell",
            "Gentry",
            "Morris"
        };
        private static List<string> ImageIds = new List<string>()
        {
            "almostrihanna.jpg",
            "cooltank.jpg",
            "grossfood.jpg",
            "oldcomputer.jpg",
            "shipstuffagain.jpg",
            "slenderwoman.jpg",
            "someshipstuff.jpg",
            "thisdetective.jpg",
            "tokiohotelconcert.jpg",
            "weirdpaintingclass.jpg",
            "piranhaplant.jpg",
            "testimage.jpg",
            "cicike.jpg"
        };
        private static List<string> PostDescriptions = new List<string>()
        {
            "Yo this pic is lit af",
            "Check this out",
            "Them reel deelz",
            "Weird flex ik",
            "Early bird gets the worm",
            "#hellojonap",
            "Please share and vote",
            "VOTE ON THE LINK TO HELP ME BECOME THE NEXT MAXIM COVER GIRL",
            "ENGLISH AND HUNGARIAN BELOW",
            "She bought it from home depot",
            "Kerlek osszatok! Verseny vege nov 21 #kl",
            "Felt cute might delete later",
            "Kerlek tovabbra is szavazzatok ram! #kl",
            "nude #nude",
            "My mom always said that life was like a box of chocolates"
        };
        private static List<string> CommentTexts = new List<string>()
        {
            "Come on, that's not appropriate",
            "Livin the life",
            "#litaf",
            "are you making fun of me?",
            "You gone full jerry springer",
            "Thats bollocks",
            "Stop",
            "Reported",
            "free shipping 5$ off ur first order visit my page",
            "check out these free healthy snacks that are affordable",
            ":)) kys",
            "8===D",
            "happy sunday babes",
            "love ur outfit",
            "love this dresss, girly ;)))))))))))))))))))))))))))))))))))))))))))",
            "every damn day",
            "Such a beautiful detail",
            "please visit my hub we should follow each other",
            "omg amazing, like for real",
            "hubtastic!",
            "#brokebicchprobs",
            "so me",
            "welcome to greece",
            "Welcome to romania",
            "Hahaha",
            "u ugly",
            "ur art is my everyday inspiration",
            "thank you",
            "what is this?",
            "love you to the moon xxx",
            "still cant believe this just happened",
            "huge nostalgia",
            "u dont play u slayy"
        };

        private static List<UserEntity> users = new List<UserEntity>();
        private static List<PostEntity> posts = new List<PostEntity>();
        private static List<CommentEntity> comments = new List<CommentEntity>();
        public static List<PostEntity> Posts
        {
            get { return posts.ToList(); }
        }
        public static List<UserEntity> Users
        {
            get { return users.ToList(); }
        }
        public static List<CommentEntity> Comments
        {
            get { return comments.ToList(); }
        }

        public static void AddUser(UserEntity user)
        {
            users.Add(user);
        }

        public static void AddPost(PostEntity post)
        {
            posts.Add(post);
        }

        private static void GenerateDemoUsers()
        {
            for (int i = 0; i < generateUserCount; i++)
            {
                users.Add(new UserEntity()
                {
                    Name = (Random.Next(0, 2) == 0 ? MaleFirstNames.GetRandomItem(Random) : FemaleFirstNames.GetRandomItem(Random)) + " " + LastNames.GetRandomItem(Random),
                    UserId = Guid.NewGuid(),
                    ProfileImageId = ImageIds.GetRandomItem(Random)
                });
            }
        }
        private static void GenerateDemoPosts()
        {
            foreach (UserEntity item in users)
            {
                int generatePosts = Random.Next(generatePostPerUserCountMin, generatePostPerUserCountMax + 1);

                for (int i = 0; i < generatePosts; i++)
                {
                    DateTime randomDate = DateTime.Now
                        .AddYears(Random.Next(-6, 0))
                        .AddMonths(Random.Next(0, 12))
                        .AddDays(Random.Next(0, 30));

                    posts.Add(new PostEntity()
                    {
                        Date = randomDate,
                        ImageId = ImageIds.GetRandomItem(Random),
                        HubtasticCount = Random.Next(0, 500),
                        IsHubbedByCurrentUser = Random.Next(0, 5) == 0,
                        PostDescription = PostDescriptions.GetRandomItem(Random),
                        PosterId = item.UserId,
                        PosterName = item.Name,
                        PosterProfileIconId = "thisdoggo.jpg",//item.ProfileImageId,
                        PostId = Guid.NewGuid()
                    });
                }
            }
        }
        private static void GenerateDemoComments()
        {
            foreach (PostEntity item in posts)
            {
                int generateComments = Random.Next(generateCommentPerPostCountMin, generateCommentPerPostCountMax + 1);

                for (int i = 0; i < generateComments; i++)
                {
                    DateTime randomDate = item.Date
                        .AddHours(Random.Next(0, 3))
                        .AddMinutes(Random.Next(0, 60))
                        .AddSeconds(Random.Next(0, 60));

                    UserEntity commenter = users.GetRandomItem(Random);
                    comments.Add(new CommentEntity()
                    {
                        PostId = item.PostId,
                        CommenterId = commenter.UserId,
                        CommenterName = commenter.Name,
                        CommenterProfileIconId = commenter.ProfileImageId,
                        Date = randomDate,
                        Text = CommentTexts.GetRandomItem(Random)
                    });
                }
            }
        }
    }
}
