namespace FA.JustBlog.Data.Migrations
{
    using FA.JustBlog.Models.Common;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FA.JustBlog.Data.JustBlogDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FA.JustBlog.Data.JustBlogDbContext context)
        {
            var elementCategory = new Category[]
              {
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Michael Jackson",
                    UrlSlug =   "MJdfgdf",
                    Description ="Michael Joseph Jackson was an American singer, songwriter, " +
                    "and dancerm, he also is regarded as one of the most significant cultural figures of the 20th century",
                    IsDeleted = false
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Taylor Swift",
                    UrlSlug =   "TSgfdgdf",
                    Description ="Taylor Alison Swift is an American singer-songwriter. Her narrative songwriting, which is often inspired by her personal experiences," +
                    " has received widespread media coverage and critical praise",
                    IsDeleted = false
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Anna Faris",
                    UrlSlug =   "AFgdfg",
                    Description ="Anna Kay Faris is an American actress, comedian, producer, podcaster, and author. Faris is known for portraying distinctive comedic roles across film and television, " +
                    "particularly in slapstick and dark comedy.",
                    IsDeleted = false
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Alexander the Great",
                    UrlSlug =   "ATGgfdg",
                    Description ="Alexander III of Macedon, commonly known as Alexander the Great, was a king of the ancient Greek kingdom of Macedon." +
                    " A member of the Argead dynasty, he was born in Pella—a city in Ancient Greece—in 356 BC",
                    IsDeleted = false
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Charlie Puth",
                    UrlSlug =   "CPgfdg",
                    Description ="Charles Otto Puth Jr. is an American singer, songwriter, and record producer. " +
                    "His initial exposure came through the viral success of his song videos uploaded to YouTube",
                    IsDeleted = false
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Roberto Calos",
                    UrlSlug =   "RCghgfhgfgh",
                    Description ="Roberto Carlos da Silva Rocha, commonly known as Roberto Carlos," +
                    " is a Brazilian former professional footballer who now works as a football ambassador.",
                    IsDeleted = false
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Frank Lampard",
                    UrlSlug =   "FLfdsfsd",
                    Description ="Frank James Lampard OBE is an English professional football manager and former player who was the head coach of Premier League club Chelsea from July 2019 until January 2021." +
                    " He is widely considered to be one of Chelsea's greatest ever players, and one of the greatest midfielders of his generation",
                    IsDeleted = false
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Arena Of Valor",
                    UrlSlug =   "AOVfff",
                    Description ="Arena of Valor, formerly Strike of Kings, is an international adaptation of Honor of Kings, a multiplayer online battle arena developed by TiMi Studio Group and published by Tencent Games for Android, " +
                    "iOS and Nintendo Switch for markets outside Mainland China",
                    IsDeleted = false
                },  new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Thomas Cruise Mapother",
                    UrlSlug =   "TCMfff",
                    Description ="Thomas Cruise Mapother IV is an American actor and producer. He has received various accolades for his work, including three Golden Globe Awards and three nominations for Academy Awards. " +
                    "He is one of the highest-paid actors in the world",
                    IsDeleted = false
                },  new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Dwayne Douglas Johnson",
                    UrlSlug =   "DDJf",
                    Description ="Dwayne Douglas Johnson, also known by his ring name The Rock, is an American actor, producer, businessman and retired professional wrestler. Regarded as one of the greatest professional wrestlers of all time," +
                    " he wrestled for the World Wrestling Federation for eight years prior to pursuing an acting career.",
                    IsDeleted = false
                },  new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Dom Toretto",
                    UrlSlug =   "DThgf",
                    Description ="Dom Toretto is living the quiet life off the grid with Letty and his son, but they know that danger always lurks just over the peaceful horizon. This time, that threat forces Dom to confront the sins of his past to save those he loves most. His crew soon comes together to stop a world-shattering plot by the most skilled assassin and " +
                    "high-performance driver they've ever encountered -- Dom's forsaken brother.",
                    IsDeleted = false
                },




              };

            var t1 = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "Datuk Lee Chong Wei",
                UrlSlug = "DLCW",
                Description = "Datuk Lee Chong Wei DB PJN AMN DCSM DSPN is a Malaysian former badminton player. As a singles player, Lee was ranked first worldwide for 349 weeks, " +
                "including a 199-week streak from 21 August 2008 to 14 June 2012.",
                Count = 5,
                IsDeleted = false
            };

            var t2 = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "Lin Dan",
                UrlSlug = "LDgdfgf",
                Description = "Lin Dan (born 14 October 1983)[1] is a Chinese former professional badminton player. He is a two-time Olympic champion, " +
                "five-time World champion, as well as a six-time All England champion.",
                Count = 3,
                IsDeleted = false
            };

            var t3 = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "Chen Long",
                UrlSlug = "CLfdg",
                Description = "Automatically earn a total of 5% back on all Zappos purchases when using your Amazon Rewards Visa Card.*",
                Count = 10,
                IsDeleted = false
            };

            var t4 = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "Viktor Axelsen",
                UrlSlug = "VAfff",
                Description = "Viktor Axelsen is a Danish badminton player. He is the 2017 World Champion and the gold medalist at the 2020 Summer Olympics. He won the 2010 World Junior Championships, " +
                "beating Korea's Kang Ji-wook in the final to become the first ever European player to hold the title",
                Count = 3,
                IsDeleted = false
            };

            var t5 = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "Cai Yun",
                UrlSlug = "CfffY",
                Description = "Cai Yun (born 19 January 1980) is a former professional badminton player representing China. He is the 2012 London Olympic gold medallist and a four-time World Champion in men's doubles. " +
                "He is regarded as one of the greatest men's doubles player in his era",
                Count = 9,
                IsDeleted = false
            };

            var t6 = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "Fu Hai Fung",
                UrlSlug = "FHFfff",
                Description = "Fu Haifeng (born 23 August 1983) is a former professional badminton player representing China." +
                " He is considered to be one of the greatest men's doubles players in badminton history.",
                Count = 2,
                IsDeleted = false
            };

            var t7 = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "Zhang Nam",
                UrlSlug = "ZNfdgdfdf",
                Description = "Zhang Nan (born 1 March 1990) is a Chinese badminton player who specializes in both men's and mixed doubles. He found much success in mixed doubles with his former partner Zhao Yunlei. They won gold in 2012 Summer Olympics, 3 golds in BWF World Championships in 2011, 2014 and 2015 and a gold at the 2014 Asian Games. Having won all major events as a pair, " +
                "they are considered one of the most successful mixed doubles pairs of all time.",
                Count = 5,
                IsDeleted = false
            };

            var t8 = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "Badminton ",
                UrlSlug = "Badmin",
                Description = "Badminton is a racquet sport played using racquets to hit a shuttlecock across a net. Although it may be played with larger teams, the most common forms of the game (with one player per side) and (with two players per side). Badminton is often played as a casual outdoor activity in a yard or on a beach; formal games are played on a rectangular indoor court. Points are scored by striking the shuttlecock with the racquet and landing it within the opposing side's half of the court",
                Count = 6,
                IsDeleted = false
            };

            var t9 = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "Football",
                UrlSlug = "Football",
                Description = "The association football tournament at the 2020 Summer Olympics is held from 21 July to 7 August 2021 in Japan. In addition to the Olympic host city of Tokyo, matches are also being played in Kashima, Saitama, Sapporo, Rifu and Yokohama",
                Count = 9,
                IsDeleted = false
            };

            var t10 = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "Volleyball",
                UrlSlug = "VBggg",
                Description = "The volleyball tournaments at the 2020 Summer Olympics in Tokyo is played between 24 July and 8 August 2021." +
                " 24 volleyball teams and 48 beach volleyball teams participate in the tournament.",
                Count = 6,
                IsDeleted = false
            };

            var listPosts = new List<Post>
            {
                new Post
                {
                    Id = Guid.NewGuid(),
                    Title = "Post 01",
                    UrlSlug = "post 01",
                    ShortDescription = "A car (or automobile) is a wheeled motor vehicle used for transportation.",
                    ImageUrl = "car.jpg",
                    PostContent = "Cars came into global use during the 20th century, " +
                    "and developed economies depend on them. The year 1886 is regarded as " +
                    "the birth year of the car when German inventor Karl Benz patented" +
                    " his Benz Patent-Motorwagen.[1][4][5] Cars became widely available" +
                    " in the early 20th century. One of the first cars accessible to the masses was the 1908" +
                    " Model T, an American car manufactured by the Ford Motor Company. " +
                    "Cars were rapidly adopted in the US, where they replaced animal-drawn carriages and carts," +
                    " but took much longer to be accepted in Western Europe and other parts of the world.",
                    PublishedDate = DateTime.Now,
                    IsDeleted = false,
                    Published = true,
                    ViewCount = 5,
                    Category = elementCategory.Single(category => category.Name == elementCategory[0].Name),
                    Tags = new List<Tag>{t3, t5,t7}
                },new Post
                {
                    Id = Guid.NewGuid(),
                    Title = "Post 02",
                    UrlSlug = "post 02",
                    ShortDescription = "A background light is used to illuminate the background area of a set.",
                    ImageUrl = "images.jpg",
                    PostContent = "The background light will also provide separation between the subject and the background. " +
                    "Many lighting setups follow a three-point lighting or four-point lighting setup. " +
                    "Four-point lighting is the same as three-point lighting with the addition of a background light. " +
                    "In a four-point lighting, the background light is placed last and is usually placed directly behind the subject and" +
                    " pointed at the background. By adding a background light to a set, filmmakers can add a sense of depth to shots." +
                    "In film, the background light is usually of lower intensity. More than one light could be used to light uniformly a background or alternatively to highlight points of interest",
                    PublishedDate = DateTime.Now,
                    IsDeleted = false,
                    Published = true,
                    ViewCount = 7,
                    Category = elementCategory.Single(category => category.Name == elementCategory[2].Name),
                    Tags = new List<Tag>{t2, t3,t5}
                },new Post
                {
                    Id = Guid.NewGuid(),
                    Title = "Post 03",
                    UrlSlug = "post 03",
                    ShortDescription = "A motorcycle, often called a motorbike, bike, or cycle, " +
                    "is a two- or three-wheeled motor vehicle.",
                    ImageUrl = "motor.jpg",
                    PostContent = "Motorcycle design varies greatly to suit a range of different purposes: long-distance travel, commuting, cruising, " +
                    "sport (including racing), and off-road riding. Motorcycling is riding a motorcycle and being involved in other related social activity " +
                    "such as joining a motorcycle club and attending motorcycle rallies." +
                    "The 1885 Daimler Reitwagen made by Gottlieb Daimler and Wilhelm Maybach in Germany was the first internal combustion, " +
                    "petroleum-fueled motorcycle.In 1894, Hildebrand & Wolfmüller became the first series production motorcycle.[4][5]",
                    PublishedDate = DateTime.Now,
                    IsDeleted = false,
                    Published = true,
                    ViewCount = 4,
                    Category = elementCategory.Single(category => category.Name == elementCategory[1].Name),
                    Tags = new List<Tag>{t1, t6,t8}
                },new Post
                {
                    Id = Guid.NewGuid(),
                    Title = "Post 04",
                    UrlSlug = "post 04",
                    ShortDescription = "A helicopter is a type of rotorcraft in which lift and thrust are supplied by horizontally-spinning rotors.",
                    ImageUrl = "helicopter.jpg",
                    PostContent = "This allows the helicopter to take off and land vertically, to hover, and to fly forward, backward and laterally." +
                    " These attributes allow helicopters to be used in congested or isolated areas where fixed-wing aircraft and many forms of VTOL " +
                    "(Vertical TakeOff and Landing) aircraft cannot perform.In 1942 the Sikorsky R-4 became the first helicopter to reach full-scale " +
                    "production.[1][2]",
                    PublishedDate = DateTime.Now,
                    IsDeleted = false,
                    Published = true,
                    ViewCount = 7,
                    Category = elementCategory.Single(category => category.Name == elementCategory[3].Name),
                    Tags = new List<Tag>{t2, t4,t5}
                },new Post
                {
                    Id = Guid.NewGuid(),
                    Title = "Post 05",
                    UrlSlug = "post 05",
                    ShortDescription = "A tank is an armored fighting vehicle intended as a primary offensive weapon in front-line ground combat.",
                    ImageUrl = "tank.JPG",
                    PostContent = "Modern tanks are versatile mobile land weapons platforms whose main armament is a large-caliber tank gun mounted" +
                    " in a rotating gun turret, supplemented by machine guns or other ranged weapons such as anti-tank guided missiles or " +
                    "rocket launchers. They have heavy vehicle armor which provides protection for the crew, the vehicle's munition storage, " +
                    "fuel tank and propulsion systems. The use of tracks rather than wheels provides improved operational mobility which " +
                    "allows the tank to overcome rugged terrain and adverse conditions such as mud and ice/snow better than wheeled vehicles, " +
                    "and thus be more flexibly positioned at advantageous locations on the battlefield",
                    PublishedDate = DateTime.Now,
                    IsDeleted = false,
                    Published = true,
                    ViewCount = 2,
                    Category = elementCategory.Single(category => category.Name == elementCategory[5].Name),
                    Tags = new List<Tag>{t3, t4,t6}
                },

            };
            context.Categories.AddRange(elementCategory);
            context.Posts.AddRange(listPosts);
            context.SaveChanges();
        }
    }
}
