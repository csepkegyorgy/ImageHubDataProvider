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

        static DemoDataGenerator()
        {
            GenerateDemoUsers();
            GenerateDemoPosts();
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
            "amandahoe.jpg",
            "bedhoe.jpg",
            "bikinihoe.jpg",
            "cooltank.jpg",
            "fathoe.jpg",
            "greenhairhoe.jpg",
            "grossfood.jpg",
            "latinahoe.jpg",
            "nakedhoe.jpg",
            "nakedhoe2.jpg",
            "oldcomputer.jpg",
            "oxindiangirl.jpg",
            "poolhoe.jpg",
            "povhoe.jpg",
            "shipstuffagain.jpg",
            "slenderwoman.jpg",
            "soldierhoe.jpg",
            "someshipstuff.jpg",
            "thisdetective.jpg",
            "tokiohotelconcert.jpg",
            "uglyasianhoe.jpg",
            "vanhoe.jpg",
            "wastedhoe.jpg",
            "weirdpaintingclass.jpg",
            "piranhaplant.jpg",
            "testimage.jpg"
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

        private static List<UserEntity> users = new List<UserEntity>();
        private static List<PostEntity> posts = new List<PostEntity>();
        public static List<PostEntity> Posts
        {
            get { return posts.ToList(); }
        }
        public static List<UserEntity> Users
        {
            get { return users.ToList(); }
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
                        PosterProfileIconId = item.ProfileImageId,
                        PostId = Guid.NewGuid()
                    });
                }
            }
        }

        
    }
}
