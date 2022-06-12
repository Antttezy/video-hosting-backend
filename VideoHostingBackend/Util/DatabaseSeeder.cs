using VideoHostingBackend.Core.Models;
using VideoHostingBackend.Data;

namespace VideoHostingBackend.Util;

internal class DatabaseSeeder
{
    private readonly VideoContext _context;

    public DatabaseSeeder(VideoContext context)
    {
        _context = context;
    }

    public void SeedDatabase()
    {
        if (!_context.Videos.Any())
        {
            UserData user = new()
            {
                User = "Admin",
                Username = "Admin",
                Sex = "Male",
                Credentials = new()
                {
                    Password = PasswordHasher.HashPassword("AdminPassword")
                },
                Avatar = "aya.jpg",
                BackgroundImage = "prof_logo.jpg"
            };

            Country country = new()
            {
                Name = "Не указано"
            };

            Category mammals = new()
            {
                Name = "mammals",
                LocalizedName = "Млекопитающие"
            };

            Category fishes = new()
            {
                Name = "fishes",
                LocalizedName = "Рыбы"
            };

            Category amphibians = new()
            {
                Name = "amphibians",
                LocalizedName = "Земноводные"
            };

            Category birds = new()
            {
                Name = "birds",
                LocalizedName = "Птицы"
            };

            List<Video> videos = new()
            {
                new()
                {
                    Category = mammals,
                    Country = country,
                    Uploader = user,
                    VideoId = "lions1.mp4",
                    VideoName = "Львы в вольере",
                    CoverImg = "lions1.png"
                },
                // new()
                // {
                //     Category = mammals,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "lions2.mp4",
                //     VideoName = "Львы и гиены",
                //     CoverImg = "lions2.png"
                // },
                // new()
                // {
                //     Category = mammals,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "lions3.mp4",
                //     VideoName = "Общение со львами",
                //     CoverImg = "lions3.png"
                // },
                // new()
                // {
                //     Category = mammals,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "gepard1.mp4",
                //     VideoName = "Гепард",
                //     CoverImg = "gepard1.png"
                // },
                new()
                {
                    Category = birds,
                    Country = country,
                    Uploader = user,
                    VideoId = "eagle.mp4",
                    VideoName = "Знакомство с орлами",
                    CoverImg = "eagle.png"
                },
                // new()
                // {
                //     Category = mammals,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "nature.mp4",
                //     VideoName = "Чудеса природы",
                //     CoverImg = "nature.png",
                // },
                // new()
                // {
                //     Category = mammals,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "wolves2.mp4",
                //     VideoName = "Общение с волками",
                //     CoverImg = "wolves2.png",
                // },
                // new()
                // {
                //     Category = mammals,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "bear.mp4",
                //     VideoName = "Белый медведь",
                //     CoverImg = "bear.png",
                // },
                // new()
                // {
                //     Category = mammals,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "nature2.mp4",
                //     VideoName = "Секреты природы",
                //     CoverImg = "nature2.png",
                // },
                // new()
                // {
                //     Category = mammals,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "gorilla.mp4",
                //     VideoName = "Горилла",
                //     CoverImg = "gorilla.png",
                // },
                // new()
                // {
                //     Category = mammals,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "safari.mp4",
                //     VideoName = "Поездка на сафари",
                //     CoverImg = "safari.png",
                // },
                // new()
                // {
                //     Category = mammals,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "tigers.mp4",
                //     VideoName = "Тигрята",
                //     CoverImg = "tigers.png",
                // },
                // new()
                // {
                //     Category = mammals,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "seal1.mp4",
                //     VideoName = "Тюлени охотятся на рыбу",
                //     CoverImg = "seal1.png",
                // },
                // new()
                // {
                //     Category = mammals,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "seal2.mp4",
                //     VideoName = "Тюлени на берегу",
                //     CoverImg = "seal2.png",
                // },
                // new()
                // {
                //     Category = birds,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "bird1.mp4",
                //     VideoName = "Совы и голуби",
                //     CoverImg = "bird1.png"
                // },
                // new()
                // {
                //     Category = birds,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "bird2.mp4",
                //     VideoName = "Орел",
                //     CoverImg = "bird2.jpg"
                // },
                // new()
                // {
                //     Category = birds,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "bird3.mp4",
                //     VideoName = "Ворон",
                //     CoverImg = "bird3.jpg"
                // },
                // new()
                // {
                //     Category = birds,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "bird4.mp4",
                //     VideoName = "Пеликан",
                //     CoverImg = "bird4.jpg"
                // },
                // new()
                // {
                //     Category = birds,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "bird5.mp4",
                //     VideoName = "Воробей",
                //     CoverImg = "bird5.jpg"
                // },
                // new()
                // {
                //     Category = birds,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "bird5.mp4",
                //     VideoName = "Воробей",
                //     CoverImg = "bird5.jpg"
                // },
                // new()
                // {
                //     Category = fishes,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "fish1.mp4",
                //     VideoName = "Вымирание рыб",
                //     CoverImg = "fish1.png"
                // },
                new()
                {
                    Category = fishes,
                    Country = country,
                    Uploader = user,
                    VideoId = "fish2.mp4",
                    VideoName = "Рыбы",
                    CoverImg = "fish2.png"
                },
                // new()
                // {
                //     Category = fishes,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "fish3.mp4",
                //     VideoName = "Какая-то рыба",
                //     CoverImg = "fish3.png"
                // },
                // new()
                // {
                //     Category = fishes,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "fish4.mp4",
                //     VideoName = "Акула",
                //     CoverImg = "fish4.png"
                // },
                // new()
                // {
                //     Category = amphibians,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "am1.mp4",
                //     VideoName = "Земноводные",
                //     CoverImg = "am1.jpg"
                // },
                // new()
                // {
                //     Category = amphibians,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "am2.mp4",
                //     VideoName = "Рептилии",
                //     CoverImg = "am2.jpg"
                // },
                new()
                {
                    Category = amphibians,
                    Country = country,
                    Uploader = user,
                    VideoId = "am3.mp4",
                    VideoName = "Головастики",
                    CoverImg = "am3.jpg"
                },
                // new()
                // {
                //     Category = amphibians,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "am5.mp4",
                //     VideoName = "Все про земноводных",
                //     CoverImg = "am5.jpg"
                // },
                // new()
                // {
                //     Category = amphibians,
                //     Country = country,
                //     Uploader = user,
                //     VideoId = "am6.mp4",
                //     VideoName = "10 земноводных",
                //     CoverImg = "am6.jpg"
                // },
            };

            _context.Videos.AddRange(videos);
            _context.SaveChanges();
        }
    }
}