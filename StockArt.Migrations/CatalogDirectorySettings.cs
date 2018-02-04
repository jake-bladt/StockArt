using System;
using System.Collections.Generic;
using System.Text;

namespace StockArt.Migrations
{
    public interface ICatalogDirectorySettings
    {
        string Root { get; set; }
        string YearbookRoot { get; set; }
    }

    public class CatalogDirectorySettings : ICatalogDirectorySettings
    {
        public string Root { get; set; }
        public string YearbookRoot { get; set; }
    }
}
