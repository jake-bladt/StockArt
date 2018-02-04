using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using StockArt.Data;
using StockArt.Domain;
using System.Text;

using Microsoft.EntityFrameworkCore;

namespace StockArt.Migrations
{
    public class FromCatalogDirectory
    {
        public class MigrationResult
        {
            public MigrationResult()
            {
                NewSubjects = 0;
                NewImageSets = 0;
                UpdatedImageSets = 0;
            }

            public int NewSubjects { get; internal set; }
            public int NewImageSets { get; internal set; }
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

            var dbSubjects = _dbContext.Subjects.Include(s => s.ImageSetSubjects).ThenInclude(iss => iss.ImageSet).ToList();
            Announce(String.Format("{0} subjects found in database.", dbSubjects.ToArray<Subject>().Length));

            foreach(var sdi in subjectDIs)
            {
                var iSet = new ImageSet
                {
                    Name = sdi.Name,
                    ImageCount = sdi.GetFiles().Length
                };

                var displayName = SetNameToSubjectName(iSet.Name);
                var dbSubject = dbSubjects.FirstOrDefault(s => s.DisplayName == displayName);
                if(null == dbSubject)
                {
                    // If there's no existing subject matching the name in the directory, create one
                    // and link it to the image set found.
                    dbSubject = new Subject { DisplayName = displayName };

                    // Check for a catalog image in the yearbook directory with the canonical name
                    var catalogImagePath = Path.Combine(_settings.YearbookRoot, iSet.Name + ".jpg");
                    if(File.Exists(catalogImagePath))
                    {
                        dbSubject.CatalogImagePath = catalogImagePath;
                        dbSubject.CatalogImageType = "fso";
                    }

                    dbSubject.ImageSetSubjects.Add(new ImageSetSubject { ImageSet = iSet, Subject = dbSubject });
                    _dbContext.Add(dbSubject);
                    Announce(String.Format("Adding new subject {0} with a set of {1} image(s).", dbSubject.DisplayName, iSet.ImageCount));
                    ret.NewSubjects++;
                    ret.NewImageSets++;
                }
                else
                {
                    // If the subject already exists in the database, determine if the image set is already stored.
                    // If it's not, add it.
                    var dbImgSet = dbSubject.ImageSetSubjects.FirstOrDefault(iss => iss.ImageSetName == iSet.Name)?.ImageSet;
                    if(null == dbImgSet)
                    {
                        // If this is a new image set for an existing subject, add the image set and the relationship
                        // to the subject to the DB.
                        dbSubject.ImageSetSubjects.Add(new ImageSetSubject { Subject = dbSubject, ImageSet = iSet });
                        _dbContext.Update(dbSubject);
                        Announce(String.Format("Added set with {0} new images of {1}", iSet.ImageCount, dbSubject.DisplayName));
                        ret.NewImageSets++;
                    }
                    else
                    {
                        // If this is an existing image set, but the ImageCount has changed, update the ImageCount in the
                        // database.
                        var newImageCount = iSet.ImageCount - dbImgSet.ImageCount;
                        if(newImageCount != 0)
                        {
                            dbImgSet.ImageCount = iSet.ImageCount;
                            _dbContext.Update(dbImgSet);
                            var verb = newImageCount > 0 ? "Added" : "Removed";
                            Announce(String.Format("{0} {1} image(s) of {2}.", verb, Math.Abs(newImageCount), dbSubject.DisplayName));
                            ret.UpdatedImageSets++;
                        }
                    }
                }
            }
            _dbContext.SaveChanges();
            return ret;
        }

        protected void Announce(string msg)
        {
            if (null != _messageHook) _messageHook(msg);
        }

        protected string SetNameToSubjectName(string setName)
        {
            var letters = setName.ToCharArray();
            var curr = letters[0].ToString();
            var prev = string.Empty;
            var builder = new StringBuilder(curr.ToUpper());
            for(int i = 1; i < letters.Length; i++)
            {
                prev = curr;
                curr = letters[i].ToString();
                if (curr == ".")
                {
                    builder.Append(" ");
                }
                else
                {
                    builder.Append(prev == "." ? curr.ToUpper() : curr);
                }
            }
            if (prev == ".") builder.Append(".");

            return builder.ToString();
        }
        
    }
}
