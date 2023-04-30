using GameStoreApp.Data.Enums;
using GameStoreApp.Data.Static;
using GameStoreApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System;

namespace GameStoreApp.Data
{
    public class GameAppDbInitialiser
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<GameStoreAppDbContext>();

                //checks to see if the database is created
                context.Database.EnsureCreated();

                //VoiceActors
                //checks if context VoiceActors has any elements
                if (!context.VoiceActors.Any())
                {
                    //inserts a range of elements into the database table
                    context.VoiceActors.AddRange(new List<VoiceActor>()
                    {
                        //The following with create a new VoiceActors with the values provided
                        new VoiceActor()
                        {
                            //Name of the VoiceActor
                            FullName = "Nolan North",
                            //VoiceActors Bio
                            Bio = "Nolan North is an American actor and voice actor from Connecticut. He is best known for his voice acting roles as Nathan Drake in the Uncharted series, Desmond Miles in the Assassin’s Creed series, and Ghost in Destiny after replacing Peter Dinklage. He has also voiced Deadpool in many Marvel cartoons and video games, Penguin from Batman: Arkham City, N. Gin from Crash Bandicoot and the Space Core from Portal 2.",
						    //URL path
                            PictureURL = "/Images/VoiceActorImages/Nolan-North-Picture.jpg"
                        },
                        new VoiceActor()
                        {
                            //Name of the VoiceActor
                            FullName = "Steve Blum",
                            //VoiceActors Bio
                            Bio = "Steve Blum is an American voice actor known for his distinctively deep voice and has performed countless voice-over roles for video games and animation since 1981. He is most often associated with his turn as edgy bounty hunter Spike in the cult-favorite anime series “Cowboy Bebop,” and for voicing many incarnations of the Marvel hero Wolverine. He has also voiced Zeb Orrelios and dozens of other characters from Star Wars: Rebels, Orochimaru, Zabuza, and others from Naruto, Green Goblin from the Spectacular Spiderman series, Heatblast, Vilgax and Ghostfreak from Ben 10, Grayson Hunt (Bulletstorm), Grunt (Mass Effect 2 and 3), Zoltan Kulle from Diablo 3, Abathur from Starcraft 2:Heart of the Swarm, Tank Dempsey (Call of Duty), Killer Croc from Arkham Asylum, Oghren (DragonAge), Vincent Valentine (Final Fantasy VII), Leeron (Guren Lagann), Jamie from Megas XLR, Storm Troopers and many others in most of the Star Wars games.",
						    //URL path
                            PictureURL = "/Images/VoiceActorImages/Steve-Blum.jpg"
                        },
                        new VoiceActor()
                        {
                            //Name of the VoiceActor
                            FullName = "Troy Baker",
                            //VoiceActors Bio
                            Bio = "Troy Baker is an American voice actor and musician. He is known for his video game roles, including Joel Miller in The Last of Us franchise, Booker DeWitt in BioShock Infinite (2013), Samuel “Sam” Drake in Uncharted 4: A Thief’s End (2016) and Uncharted: The Lost Legacy (2017), Rhys in Tales from the Borderlands (2014), and many more.",
						    //URL path
                            PictureURL = "/Images/VoiceActorImages/Troy-Baker.jpg"
                        },
                        new VoiceActor()
                        {
                            //Name of the VoiceActor
                            FullName = "Laura Bailey",
                            //VoiceActors Bio
                            Bio = "Laura Bailey (born May 28, 1981) is an American voice actress known for her roles in animation and video games. She made her anime debut as Kid Trunks in the Funimation dub of Dragon Ball Z. Her other anime credits include Emily / Glitter Lucky in Glitter Force, Tohru Honda in Fruits Basket, Lust in Fullmetal Alchemist and Fullmetal Alchemist: Brotherhood, the title character in the English version of Shin Chan1, Maka Albarn in Soul Eater2, and Jaina Proudmoore in World of Warcraft. She has also lent her voice to many video game characters including Kait Diaz in Gears of War 4 and Gears 5, Nadine Ross in Uncharted 4: A Thief’s End and Uncharted: The Lost Legacy, Mary Jane Watson in Marvel’s Spider-Man, and Black Widow in Marvel’s Avengers.",
						    //URL path
                            PictureURL = "/Images/VoiceActorImages/Laura-Bailey.jpg"
                        },
                        new VoiceActor()
                        {
                            //Name of the VoiceActor
                            FullName = "Christopher Judge",
                            //VoiceActors Bio
                            Bio = "Christopher Judge is an American actor best known for playing Teal’c in the Canadian-American military science fiction television series Stargate SG-1, and Kratos in the 2018 video game God of War and its sequel God of War Ragnarök (2022). He attended the University of Oregon on a football scholarship and was a Pacific-10 Conference player.",
						    //URL path
                            PictureURL = "/Images/VoiceActorImages/Christopher-Judge.jpg"
                        },
                        new VoiceActor()
                        {
                            //Name of the VoiceActor
                            FullName = "Cameron Monaghan",
                            //VoiceActors Bio
                            Bio = "Cameron Riley Monaghan is an American actor and model known for his role as Ian Gallagher on the Showtime comedy-drama series Shameless and as twins Jerome and Jeremiah Valeska, who serve as origins for the Joker, on the DC Comics-based TV series Gotham. He was born in Santa Monica, California. He began his acting career at 5 years old in commercials and at age 7, he began appearing on stage as Stuart Little in “Stuart Little” and as Piglet in \"Winnie the Pooh\".",
						    //URL path
                            PictureURL = "/Images/VoiceActorImages/Cameron-Monaghan.jpg"
                        },
                        new VoiceActor()
                        {
                            //Name of the VoiceActor
                            FullName = "N/A",
                            //VoiceActors Bio
                            Bio = "Placeholder. There is currently no Voice Actors Listed",
						    //URL path
                            PictureURL = "/Images/VoiceActorImages/place-holder-image.png"
                        },
                    });
                    //Saves all changes to the context to the database 
                    context.SaveChanges();
                }

                //GameRatings
                //checks if context GameRatings has any elements
                if (!context.GameRatings.Any())
                {
                    //inserts a range of elements into the database table
                    context.GameRatings.AddRange(new List<GameRating>()
                    {
                        //The following with create a new GameRating with the values provided
                        new GameRating()
                        {
                            //Name of the rating
                            Name = "3",
                            //Rating description
                            Description = "The content of games with a PEGI 3 rating is considered suitable for all age groups. The game should not contain any sounds or pictures that are likely to frighten young children. A very mild form of violence (in a comical context or a childlike setting) is acceptable. No bad language should be heard.",
						    //URL path
                            Logo = "/Images/GameRatingsLogos/PEGI_3.svg"
                        },
                        new GameRating()
                        {
                            //Name of the rating
                            Name = "7",
                            //Rating description
                            Description = "Game content with scenes or sounds that can possibly be frightening to younger children should fall in this category. Very mild forms of violence (implied, non-detailed, or non-realistic violence) are acceptable for a game with a PEGI 7 rating.",
						    //URL path
                            Logo = "/Images/GameRatingsLogos/PEGI_7.svg"
                        },
                        new GameRating()
                        {
                            //Name of the rating
                            Name = "12",
                            //Rating description
                            Description = "Video games that show violence of a slightly more graphic nature towards fantasy characters or non-realistic violence towards human-like characters would fall in this age category. Sexual innuendo or sexual posturing can be present, while any bad language in this category must be mild.",
						    //URL path
                            Logo = "/Images/GameRatingsLogos/PEGI_12.svg"
                        },
                        new GameRating()
                        {
                            //Name of the rating
                            Name = "16",
                            //Rating description
                            Description = "This rating is applied once the depiction of violence (or sexual activity) reaches a stage that looks the same as would be expected in real life. The use of bad language in games with a PEGI 16 rating can be more extreme, while the use of tobacco, alcohol or illegal drugs can also be present.",
						    //URL path
                            Logo = "/Images/GameRatingsLogos/PEGI_16.svg"
                        },
                        new GameRating()
                        {
                            //Name of the rating
                            Name = "18",
                            //Rating description
                            Description = "The 18 rating, which indicates content suitable only for adults, is applied when the level of violence reaches a stage where it becomes a depiction of gross violence, apparently motiveless killing, or violence towards defenceless characters. The glamorisation of the use of illegal drugs, explicit sexual activity, and gambling should also fall into this age category.",
						    //URL path
                            Logo = "/Images/GameRatingsLogos/PEGI_18.svg"
                        },
                        new GameRating()
                        {
                            //Name of the rating
                            Name = "!",
                            //Rating description
                            Description = "In addition to age ratings, there is a special rating represented by an exclamation point labeled \"Parental Guidance Recommended\". These contents are available for all ages, but it is recommended that parents (mostly with children who are under the age of 18) supervise activities within the program.",
						    //URL path
                            Logo = "/Images/GameRatingsLogos/PEGI_PARENTAL.svg"
                        }

                    });
                    //Saves all changes to the context to the database 
                    context.SaveChanges();
                }

                //Platforms
                //checks if context Platforms has any elements
                if (!context.Platforms.Any())
                {
                    //inserts a range of elements into the database table
                    context.Platforms.AddRange(new List<Platform>()
                    {
                        //The following with create a new Platform with the values provided
                        new Platform()
                        {
                            //Name of the Platform
                            Name = "PlayStation 5",
                            //Platform description
                            Description = "The PlayStation 5 (PS5) is a home video game console developed by Sony Interactive Entertainment. It was announced as the successor to the PlayStation 4 in April 2019, was launched on November 12, 2020, in Australia, Japan, New Zealand, North America, and South Korea, and was released worldwide one week later. The PS5 is part of the ninth generation of video game consoles, along with Microsoft's Xbox Series X/S consoles, which were released in the same month.",
                            ReleaseDate = new DateTime(2020, 11, 19),
                            Price = 479.99,
                            PlatformDeveloper = "Sony Interactive Entertainment",
						    //URL path
                            ImageURL = "/Images/PlatformImages/PlayStation_5_logo_and_wordmark.svg"
                        },
                        new Platform()
                        {
                            //Name of the Platform
                            Name = "Xbox Series X and Series S",
                            //Platform description
                            Description = "The Xbox Series X and Series S are the fourth generation of the Xbox series of home video game consoles developed and sold by Microsoft. Released on November 10, 2020, the higher-end Xbox Series X and lower-end Xbox Series S are part of the ninth generation of video game consoles, which also includes Sony's PlayStation 5, released the same month. They superseded the Xbox One.",
                            ReleaseDate = new DateTime(2020, 11, 10),
                            Price = 479.99,
                            PlatformDeveloper = "Microsoft",
						    //URL path
                            ImageURL = "/Images/PlatformImages/Xbox_Series_X_S_black.svg"
                        }
                    });
                    //Saves all changes to the context to the database 
                    context.SaveChanges();
                }

                //GameDeveloper
                //checks if context GameDevelopers has any elements
                if (!context.GameDevelopers.Any())
                {
                    //inserts a range of elements into the database table
                    context.GameDevelopers.AddRange(new List<GameDeveloper>()
                    {
                        //The following with create a new GameDeveloper with the values provided
                        new GameDeveloper()
                        {
                            //Name of the Game Developer
                            Name = "Santa Monica Studio",
                            //Game Developer description
                            Description = "Santa Monica Studio is an American video game developer based in Los Angeles. A first-party studio for Sony Interactive Entertainment, it is best known for developing the God of War series. The studio was founded in 1999 by Allan Becker and was located in Santa Monica, California, until relocating to Playa Vista in 2014.",
						    //URL path
                            Logo = "/Images/GameDeveloperLogos/Santa-Monica-Studio.png"
                        },
                        new GameDeveloper()
                        {
                            //Name of the Game Developer
                            Name = "Paradox Development Studio",
                            //Game Developer description
                            Description = "Paradox Development Studio (PDS) is a Swedish video game developer founded in 1995. It is closely associated with its parent company and video game publisher, Paradox Interactive. It is best known for its grand strategy wargame series Europa Universalis, Hearts of Iron, Victoria, Crusader Kings, Stellaris, and Imperator.",
						    //URL path
                            Logo = "/Images/GameDeveloperLogos/Paradox-Development-Studio.png"
                        },
                        new GameDeveloper()
                        {
                            //Name of the Game Developer
                            Name = "Ubisoft Montreal",
                            //Game Developer description
                            Description = "Ubisoft Montreal is a Canadian video game developer and a studio of Ubisoft based in Montreal. The studio was founded in April 1997 as part of Ubisoft’s growth into worldwide markets. Ubisoft Montreal has developed many popular games such as Assassin’s Creed, Far Cry, Rainbow Six, and Watch Dogs. Ubisoft Montreal is committed to success and enriching the lives of players by creating the best AAA games together year after year.",
						    //URL path
                            Logo = "/Images/GameDeveloperLogos/Ubisoft-Montreal.png"
                        },
                        new GameDeveloper()
                        {
                            //Name of the Game Developer
                            Name = "Nintendo EPD",
                            //Game Developer description
                            Description = "Nintendo Entertainment Planning & Development Division,[a] commonly abbreviated as Nintendo EPD, is the largest division within the Japanese video game company Nintendo. The division focuses on developing and producing video games, mobile apps, and other related entertainment software for the company. EPD was created after merging their Entertainment Analysis & Development (EAD) and Software Planning & Development (SPD) divisions in September 2015.",
						    //URL path
                            Logo = "/Images/GameDeveloperLogos/Nintendo.png"
                        },
                        new GameDeveloper()
                        {
                            //Name of the Game Developer
                            Name = "DICE",
                            //Game Developer description
                            Description = "EA Digital Illusions CE AB (trade name: DICE) is a Swedish video game developer based in Stockholm. The company was founded in 1992 and has been a subsidiary of Electronic Arts since 2006. Its releases include the Battlefield, Mirror's Edge and Star Wars: Battlefront series. Through their Frostbite Labs division, the company also develops the Frostbite game engine.",
						    //URL path
                            Logo = "/Images/GameDeveloperLogos/DICE.png"
                        },
                        new GameDeveloper()
                        {
                            //Name of the Game Developer
                            Name = "EA Vancouver",
                            //Game Developer description
                            Description = "EA Vancouver (formerly known as EA Burnaby, then EA Canada) is a Canadian video game developer located in Burnaby, British Columbia. The development studio opened as Distinctive Software in January 1983, and is also Electronic Arts's largest and oldest studio. EA Vancouver employs approximately 1,300 people, and houses the world's largest video game test operation.",
						    //URL path
                            Logo = "/Images/GameDeveloperLogos/EA-Vancouver.png"
                        },
                    });
                    //Saves all changes to the context to the database 
                    context.SaveChanges();
                }

                //GamePublisher
                //checks if context GamePublisher has any elements
                if (!context.GamePublishers.Any())
                {
                    //inserts a range of elements into the database table
                    context.GamePublishers.AddRange(new List<GamePublisher>()
                    {
                        //The following with create a new GamePublisher with the values provided
                        new GamePublisher()
                        {
                            //URL path
                            Logo = "/Images/GamePublisherLogos/EA_logo_black.png",
                            //Name of the publisher
                            Name = "Electronic Arts",
                            //Publisher description
                            Description = "Electronic Arts (EA) is an American video game company that was founded in 1982 by Apple employee Trip Hawkins. It is one of the biggest game publishers in the world, acting as a developer, publisher, and distributor of video games. EA has published numerous games and some productivity software for personal computers. Some of its most popular games include Madden NFL, FIFA, The Sims, and Apex Legends."
                        },
                        //The following with create a new GamePublisher with the values provided
                        new GamePublisher()
                        {
                            //URL path
                            Logo = "/Images/GamePublisherLogos/tencent-games-logo.png",
                            //Name of the publisher
                            Name = "Tencent Games",
                            //Publisher description
                            Description = "Tencent Games (Chinese: 腾讯游戏; pinyin: Téngxùn Yóuxì) is the video game publishing division of Tencent Interactive Entertainment, itself a division of Tencent Holdings. It has five internal studio groups, including TiMi Studio Group. Tencent Games was founded in 2003 to focus on online games. In 2021, it launched its international Level Infinite brand, which is stated to be operated by TiMi Studios."
                        },
                        //The following with create a new GamePublisher with the values provided
                        new GamePublisher()
                        {
                            //URL path
                            Logo = "/Images/GamePublisherLogos/Ubisoft-Logo.png",
                            //Name of the publisher
                            Name = "Ubisoft",
                            //Publisher description
                            Description = "Ubisoft Entertainment is a French video game publisher headquartered in Saint-Mandé with development studios across the world. Its video game franchises include Assassin’s Creed, Far Cry, For Honor, Just Dance, Prince of Persia, Rabbids, Rayman, Tom Clancy’s, and Watch Dogs. Ubisoft was founded in 1986 by five brothers."
                        },
                        //The following with create a new GamePublisher with the values provided
                        new GamePublisher()
                        {
                            //URL path
                            Logo = "/Images/GamePublisherLogos/Nintendo-Logo.png",
                            //Name of the publisher
                            Name = "Nintendo",
                            //Publisher description
                            Description = "Nintendo Co Ltd. is a Japanese multinational video game company headquartered in Kyoto. It develops and releases both video games and video game consoles. Nintendo was founded in 1889 as Nintendo Karuta by craftsman Fusajiro Yamauchi and originally produced handmade hanafuda playing cards. The company isn’t only a video game publisher but also a game and console developer. Nintendo is one of the most well-known video game publishers in the world, having played a pivotal role in the industry since its founding. The company is best known for developing iconic gaming franchises like Super Mario Bros, Zelda, and Pokémon – all of which have captured the hearts and imaginations of gamers around the globe."
                        },
                        //The following with create a new GamePublisher with the values provided
                        new GamePublisher()
                        {
                            //URL path
                            Logo = "/Images/GamePublisherLogos/Paradox_Interactive_logo.png",
                            //Name of the publisher
                            Name = "Paradox Interactive",
                            //Publisher description
                            Description = "Paradox Interactive AB is a video game publisher based in Stockholm, Sweden. The company started out as the video game division of Target Games and then Paradox Entertainment (now Cabinet Entertainment) before being spun out into an independent company in 2004. Through a combination of expanding internal studios, founding new studios and purchasing independent developers, the company has grown to become one of the largest publishers of strategy games for PC and console. Paradox Interactive is known for producing historical strategy computer games."
                        },
                        new GamePublisher()
                        {
                            //URL path
                            Logo = "/Images/GamePublisherLogos/Paradox_Interactive_logo.png",
                            //Name of the publisher
                            Name = "Sony Interactive Entertainment",
                            //Publisher description
                            Description = "Sony Interactive Entertainment (SIE) is a multinational video game and digital entertainment company owned by Sony. SIE handles the research and development, production, and sales of both hardware and software for the PlayStation video game systems. SIE is also a developer and publisher of video game titles, and operates several subsidiaries in Sony’s largest markets: North America, Europe and Asia."
                        },
                    });
                    //Saves all changes to the context to the database 
                    context.SaveChanges();
                }

                //Game
                //checks if context Games has any elements
                if (!context.Games.Any())
                {
                    //inserts a range of elements into the database table
                    context.Games.AddRange(new List<Game>()
                    {
                        //The following with create a new VoiceActors with the values provided
                        new Game()
                        {
                            //Name of the game
                            Name = "FIFA 23",
                            //Description of the game
                            Description = "FIFA 23 brings The World’s Game to the pitch, with HyperMotion2 Technology, men’s and women’s FIFA World Cup™coming during the season, women’s club teams, cross-play features*, and more.",
                            //Release date of the game
                            ReleaseDate = new DateTime(2022, 09, 30),
                            //Price of the game
                            Price = 59.99,
                            //Genre of the game
                            GameGenre = GameGenre.SimulationSport,
						    //URL path
                            ImageURL = "/Images/GameImage/fifa-23.jpg",
                            GameRatingId = 1,
                            GameDeveloperId = 6,
                            GamePublisherId = 1
                        },
                        new Game()
                        {
                            //Name of the game
                            Name = "FIFA 22",
                            //Description of the game
                            Description = "Powered by Football™, EA SPORTS™ FIFA 22 brings the game even closer to the real thing with fundamental gameplay advances and a new season of innovation across every mode.",
                            //Release date of the game
                            ReleaseDate = new DateTime(2021, 10, 01),
                            //Price of the game
                            Price = 49.99,
                            //Genre of the game
                            GameGenre = GameGenre.SimulationSport,
						    //URL path
                            ImageURL = "/Images/GameImage/FIFA-22.jpg",
                            GameRatingId = 1,
                            GameDeveloperId = 6,
                            GamePublisherId = 1
                        },
                        new Game()
                        {
                            //Name of the game
                            Name = "Europa Universalis IV",
                            //Description of the game
                            Description = "Europa Universalis IV gives you control of a nation through four dramatic centuries. Rule your land and dominate the world with unparalleled freedom, depth and historical accuracy. Write a new history of the world and build an empire for the ages.",
                            //Release date of the game
                            ReleaseDate = new DateTime(2013, 07, 13),
                            //Price of the game
                            Price = 34.99,
                            //Genre of the game
                            GameGenre = GameGenre.GrandStrategy,
						    //URL path
                            ImageURL = "/Images/GameImage/Europa-Universalis-IV.jpg",
                            GameRatingId = 3,
                            GameDeveloperId = 2,
                            GamePublisherId = 5
                        },
                        new Game()
                        {
                            //Name of the game
                            Name = "Crusader Kings III",
                            //Description of the game
                            Description = "Love, fight, scheme, and claim greatness. Determine your noble house’s legacy in the sprawling grand strategy of Crusader Kings III. Death is only the beginning as you guide your dynasty’s bloodline in the rich and larger-than-life simulation of the Middle Ages.",
                            //Release date of the game
                            ReleaseDate = new DateTime(2020, 09, 01),
                            //Price of the game
                            Price = 41.99,
                            //Genre of the game
                            GameGenre = GameGenre.GrandStrategy,
						    //URL path
                            ImageURL = "/Images/GameImage/Crusader-Kings-III.jpg",
                            GameRatingId = 3,
                            GameDeveloperId = 2,
                            GamePublisherId = 5
                        },
                        new Game()
                        {
                            //Name of the game
                            Name = "Stellaris ",
                            //Description of the game
                            Description = "Explore a galaxy full of wonders in this sci-fi grand strategy game from Paradox Development Studios. Interact with diverse alien races, discover strange new worlds with unexpected events and expand the reach of your empire. Each new adventure holds almost limitless possibilities.",
                            //Release date of the game
                            ReleaseDate = new DateTime(2016, 05, 09),
                            //Price of the game
                            Price = 34.99,
                            //Genre of the game
                            GameGenre = GameGenre.GrandStrategy,
						    //URL path
                            ImageURL = "/Images/GameImage/Stellaris.jpg",
                            GameRatingId = 2,
                            GameDeveloperId = 2,
                            GamePublisherId = 5
                        },
                        new Game()
                        {
                            //Name of the game
                            Name = "God of War Ragnarök",
                            //Description of the game
                            Description = "Kratos and Atreus must journey to each of the Nine Realms in search of answers as Asgardian forces prepare for a prophesied battle that will end the world. Along the way they will explore stunning, mythical landscapes, and face fearsome enemies in the form of Norse gods and monsters. The threat of Ragnarök grows ever closer. Kratos and Atreus must choose between their own safety and the safety of the realms.",
                            //Release date of the game
                            ReleaseDate = new DateTime(2022, 11, 09),
                            //Price of the game
                            Price = 59.99,
                            //Genre of the game
                            GameGenre = GameGenre.ActionAdventure,
						    //URL path
                            ImageURL = "/Images/GameImage/God-of-War-Ragnarök.jpg",
                            GameRatingId = 5,
                            GameDeveloperId = 1,
                            GamePublisherId = 6
                        },
                        new Game()
                        {
                            //Name of the game
                            Name = "God of War",
                            //Description of the game
                            Description = "His vengeance against the Gods of Olympus years behind him, Kratos now lives as a man in the realm of Norse Gods and monsters. It is in this harsh, unforgiving world that he must fight to survive… and teach his son to do the same.",
                            //Release date of the game
                            ReleaseDate = new DateTime(2018, 04, 20),
                            //Price of the game
                            Price = 39.99,
                            //Genre of the game
                            GameGenre = GameGenre.ActionAdventure,
						    //URL path
                            ImageURL = "/Images/GameImage/God-of-War-4.jpg",
                            GameRatingId = 5,
                            GameDeveloperId = 1,
                            GamePublisherId = 6
                        },
                        new Game()
                        {
                            //Name of the game
                            Name = "Battlefield 2042",
                            //Description of the game
                            Description = "Be a class above in Season 4: Eleventh Hour. Battlefield™ 2042 is a first-person shooter that marks the return to the iconic all-out warfare of the franchise.",
                            //Release date of the game
                            ReleaseDate = new DateTime(2021, 11, 19),
                            //Price of the game
                            Price = 59.99,
                            //Genre of the game
                            GameGenre = GameGenre.Shooter,
						    //URL path
                            ImageURL = "/Images/GameImage/Battlefield-2042.jpg",
                            GameRatingId = 4,
                            GameDeveloperId = 5,
                            GamePublisherId = 1
                        },
                        new Game()
                        {
                            //Name of the game
                            Name = "Battlefield V",
                            //Description of the game
                            Description = "This is the ultimate Battlefield V experience. Enter mankind’s greatest conflict with the complete arsenal of weapons, vehicles, and gadgets plus the best customization content of Year 1 and 2.",
                            //Release date of the game
                            ReleaseDate = new DateTime(2018, 11, 09),
                            //Price of the game
                            Price = 44.99,
                            //Genre of the game
                            GameGenre = GameGenre.Shooter,
						    //URL path
                            ImageURL = "/Images/GameImage/Battlefield-V.jpg",
                            GameRatingId = 4,
                            GameDeveloperId = 5,
                            GamePublisherId = 1
                        },
                        new Game()
                        {
                            //Name of the game
                            Name = "Star Wars Battlefront II",
                            //Description of the game
                            Description = "Be the hero in the ultimate STAR WARS™ battle fantasy with STAR WARS™ Battlefront™ II: Celebration Edition!",
                            //Release date of the game
                            ReleaseDate = new DateTime(2017, 11, 16),
                            //Price of the game
                            Price = 34.99,
                            //Genre of the game
                            GameGenre = GameGenre.Shooter,
						    //URL path
                            ImageURL = "/Images/GameImage/SWBF2.jpg",
                            GameRatingId = 4,
                            GameDeveloperId = 5,
                            GamePublisherId = 1
                        },
                        new Game()
                        {
                            //Name of the game
                            Name = "Battlefield 1",
                            //Description of the game
                            Description = "Battlefield™ 1 takes you back to The Great War, WW1, where new technology and worldwide conflict changed the face of warfare forever.",
                            //Release date of the game
                            ReleaseDate = new DateTime(2016, 10, 20),
                            //Price of the game
                            Price = 34.99,
                            //Genre of the game
                            GameGenre = GameGenre.Shooter,
						    //URL path
                            ImageURL = "/Images/GameImage/Battlefield-1.jpg",
                            GameRatingId = 5,
                            GameDeveloperId = 5,
                            GamePublisherId = 1
                        },
                        new Game()
                        {
                            //Name of the game
                            Name = "Mirror's Edge Catalyst",
                            //Description of the game
                            Description = "Mirror's Edge™ Catalyst raises the action-adventure bar through fluid, first person action and immerses players in Faith's story as she fights for freedom.",
                            //Release date of the game
                            ReleaseDate = new DateTime(2016, 10, 20),
                            //Price of the game
                            Price = 34.99,
                            //Genre of the game
                            GameGenre = GameGenre.ActionAdventure,
						    //URL path
                            ImageURL = "/Images/GameImage/Mirror's-Edge-Catalyst.jpg",
                            GameRatingId = 4,
                            GameDeveloperId = 5,
                            GamePublisherId = 1
                        },
                        new Game()
                        {
                            //Name of the game
                            Name = "Animal Crossing: New Horizons",
                            //Description of the game
                            Description = "Animal Crossing: New Horizons is a social simulation game developed and published by Nintendo in 2020 for the Nintendo Switch; it is the fifth main entry in the Animal Crossing series. In New Horizons, the player controls a character who moves to a deserted island after purchasing a getaway package from Tom Nook, accomplishes assigned tasks, and develops the island as they choose.",
                            //Release date of the game
                            ReleaseDate = new DateTime(2022, 09, 30),
                            //Price of the game
                            Price = 59.99,
                            //Genre of the game
                            GameGenre = GameGenre.SocialSimulation,
						    //URL path
                            ImageURL = "/Images/GameImage/Animal-Crossing-New-Horizons.jpg",
                            GameRatingId = 1,
                            GameDeveloperId = 4,
                            GamePublisherId = 4
                        },
                        new Game()
                        {
                            //Name of the game
                            Name = "For Honor",
                            //Description of the game
                            Description = "Carve a path of destruction through an intense, believable battlefield in For Honor.",
                            //Release date of the game
                            ReleaseDate = new DateTime(2017, 02, 14),
                            //Price of the game
                            Price = 25.99,
                            //Genre of the game
                            GameGenre = GameGenre.Action,
						    //URL path
                            ImageURL = "/Images/GameImage/For-Honor.jpg",
                            GameRatingId = 5,
                            GameDeveloperId = 3,
                            GamePublisherId = 3
                        },
                        new Game()
                        {
                            //Name of the game
                            Name = "Assassin's Creed Valhalla",
                            //Description of the game
                            Description = "Become a legendary Viking on a quest for glory. Raid your enemies, grow your settlement, and build your political power.",
                            //Release date of the game
                            ReleaseDate = new DateTime(2020, 11, 10),
                            //Price of the game
                            Price = 49.99,
                            //Genre of the game
                            GameGenre = GameGenre.Action,
						    //URL path
                            ImageURL = "/Images/GameImage/Assassin's-Creed-Valhalla.jpg",
                            GameRatingId = 5,
                            GameDeveloperId = 3,
                            GamePublisherId = 3
                        },
                        new Game()
                        {
                            //Name of the game
                            Name = "Assassin's Creed Odyssey",
                            //Description of the game
                            Description = "Choose your fate in Assassin's Creed® Odyssey. From outcast to living legend, embark on an odyssey to uncover the secrets of your past and change the fate of Ancient Greece.",
                            //Release date of the game
                            ReleaseDate = new DateTime(2018, 10, 05),
                            //Price of the game
                            Price = 49.99,
                            //Genre of the game
                            GameGenre = GameGenre.Action,
						    //URL path
                            ImageURL = "/Images/GameImage/Assassin's-Creed-Odyssey.jpg",
                            GameRatingId = 5,
                            GameDeveloperId = 3,
                            GamePublisherId = 3
                        },
                        new Game()
                        {
                            //Name of the game
                            Name = "Assassin's Creed IV: Black Flag",
                            //Description of the game
                            Description = "The year is 1715. Pirates rule the Caribbean and have established their own lawless Republic where corruption, greediness and cruelty are commonplace.Among these outlaws is a brash young captain named Edward Kenway.",
                            //Release date of the game
                            ReleaseDate = new DateTime(2013, 10, 29),
                            //Price of the game
                            Price = 28.99,
                            //Genre of the game
                            GameGenre = GameGenre.Action,
						    //URL path
                            ImageURL = "/Images/GameImage/Assassin's-Creed-IV-Black-Flag.jpg",
                            GameRatingId = 5,
                            GameDeveloperId = 3,
                            GamePublisherId = 3
                        },
                    });
                    //Saves all changes to the context to the database 
                    context.SaveChanges();
                }


                //VoiceActors and games
                //checks if context VoiceActors_Games has any elements
                if (!context.VoiceActors_Games.Any())
                {
                    //inserts a range of elements into the database table
                    context.VoiceActors_Games.AddRange(new List<VoiceActor_Game>()
                    {
                        new VoiceActor_Game()
                        {
                            GameId = 7,
                            VoiceActorId = 5
                        },
                        new VoiceActor_Game()
                        {
                            GameId = 6,
                            VoiceActorId = 5
                        },
                        new VoiceActor_Game()
                        {
                            GameId = 11,
                            VoiceActorId = 2
                        },
                    });
                    //Saves all changes to the context to the database 
                    context.SaveChanges();
                }

                //Platforms and games
                //checks if context Platforms_Games has any elements
                if (!context.Platforms_Games.Any())
                {
                    //inserts a range of elements into the database table
                    context.Platforms_Games.AddRange(new List<Platform_Game>()
                    {
                        new Platform_Game()
                        {
                            GameId = 5,
                            PlatformId = 1
                        },
                        new Platform_Game()
                        {
                            GameId = 5,
                            PlatformId = 2
                        },
                        new Platform_Game()
                        {
                            GameId = 2,
                            PlatformId = 1
                        },
                        new Platform_Game()
                        {
                            GameId = 2,
                            PlatformId = 2
                        },
                         new Platform_Game()
                        {
                            GameId = 3,
                            PlatformId = 1
                        },
                        new Platform_Game()
                        {
                            GameId = 3,
                            PlatformId = 2
                        }
                    });
                    //Saves all changes to the context to the database 
                    context.SaveChanges();
                }
            }


        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Creating roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                }

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                }

                //added users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<GameStoreUser>>();

                var adminUser = await userManager.FindByEmailAsync("admin@gamestoreapp.com");
                if (adminUser == null)
                {
                    var newAdminUser = new GameStoreUser()
                    {
                        firstName = "Admin",
                        lastName = "User",
                        UserName = "admin",
                        Email = "admin@gamestoreapp.com",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Admin@123");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                var customerUser = await userManager.FindByEmailAsync("testcustomer@gamestoreapp.com");
                if (customerUser == null)
                {
                    var newCustomerUser = new GameStoreUser()
                    {
                        firstName = "John",
                        lastName = "Smith",
                        UserName = "TestUser",
                        Email = "test@gamestoreapp.com",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newCustomerUser, "Test@123");
                    await userManager.AddToRoleAsync(newCustomerUser, UserRoles.User);
                }
            }
        }
    }
}
