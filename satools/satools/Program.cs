using System;
using System.IO;

using Microsoft.Extensions.Configuration;

using StockArt.Data;
using StockArt.Migrations;

namespace satools
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = configBuilder.Build();

            if (args.Length > 0 && args[0] == "migrate")
            {
                var settings = new CatalogDirectorySettings {
                    Root = Configuration["GalleryRoot"],
                    YearbookRoot = Configuration["YearbookRoot"]
                };
                var migration = new FromCatalogDirectory(settings, new StockArtDBContext());
                var result = migration.MigrateToDB((msg) => Console.WriteLine(msg));
                Console.WriteLine();
                Console.WriteLine(String.Format(
                    "Migration completed with {0} new subject(s), {1} new images set(s), and {2} updated image set(s).",
                    result.NewSubjects, result.NewImageSets, result.UpdatedImageSets));
            }

            Console.ReadLine();
        }
    }
}
