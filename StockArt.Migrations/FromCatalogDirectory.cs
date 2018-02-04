using System;
using System.IO;
using System.Collections.Generic;

using StockArt.Data;
using StockArt.Domain;
using System.Text;

namespace StockArt.Migrations
{
    public class FromCatalogDirectory
    {
        public class MigrationResult
        {
            public MigrationResult()
            {
                NewSubjects = 0;
                UpdatedImageSets = 0;
            }

            public int NewSubjects { get; internal set; }
            public int UpdatedImageSets { get; internal set; }
        }

        protected delegate void VerboseMessageHook(string msg);

        protected ICatalogDirectorySettings _settings;
        protected StockArtDBContext _dbContext;
        protected VerboseMessageHook _messageHook;

        public FromCatalogDirectory(ICatalogDirectorySettings settings, StockArtDBContext dbContext)
        {
            _settings = settings;
            _dbContext = dbContext;
        }

        public MigrationResult MigrateToDB(Action<string> onAnnounce = null)
        {
            _messageHook = new VerboseMessageHook(onAnnounce);

            var ret = new MigrationResult();

            var rootDI = new DirectoryInfo(_settings.Root);
            var subjectDIs = rootDI.GetDirectories();
            Announce(String.Format("{0} subject directories found.", subjectDIs.Length));

            foreach(var sdi in subjectDIs)
            {
                var name = sdi.Name;
                var imgCount = sdi.GetFiles().Length;

            }

            return ret;

        }

        protected void Announce(string msg)
        {
            if (null != _messageHook) _messageHook(msg);
        }

        protected string SetNameToSubjectName(string setName)
        {
            var letters = setName.ToCharArray();
            var r = letters[0].ToString();
            var builder = new StringBuilder(prev.ToUpper());
            for(int i = 1; i < letters.Length; i++)
            {
            
            }

            return builder.ToString();
        }
        
    }
}
