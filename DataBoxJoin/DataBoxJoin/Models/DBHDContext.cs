using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataBoxJoin.Models
{
    public partial class DBHDContext : DbContext
    {
        public DBHDContext()
        {
        }

        public DBHDContext(DbContextOptions<DBHDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AgeRates> AgeRates { get; set; }
        public virtual DbSet<Archive> Archive { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<CompanyActivity> CompanyActivity { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<CountryActivity> CountryActivity { get; set; }
        public virtual DbSet<CustomProps> CustomProps { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Instance> Instance { get; set; }
        public virtual DbSet<Keywords> Keywords { get; set; }
        public virtual DbSet<Languages> Languages { get; set; }
        public virtual DbSet<Master> Master { get; set; }
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<MediaType> MediaType { get; set; }
        public virtual DbSet<Persons> Persons { get; set; }
        public virtual DbSet<Positions> Positions { get; set; }
        public virtual DbSet<Quality> Quality { get; set; }
        public virtual DbSet<Sequences> Sequences { get; set; }
        public virtual DbSet<Stream> Stream { get; set; }
        public virtual DbSet<StreamAudioInfo> StreamAudioInfo { get; set; }
        public virtual DbSet<StreamVideoInfo> StreamVideoInfo { get; set; }
        public virtual DbSet<Types> Types { get; set; }
        public virtual DbSet<UserGroups> UserGroups { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        // Unable to generate entity type for table 'ANNOTATIONS                    '. Please see the warning messages.
        // Unable to generate entity type for table 'COMPANIES_INV                  '. Please see the warning messages.
        // Unable to generate entity type for table 'COUNTRIES_INV                  '. Please see the warning messages.
        // Unable to generate entity type for table 'DAY_MASK                       '. Please see the warning messages.
        // Unable to generate entity type for table 'GENRES_INV                     '. Please see the warning messages.
        // Unable to generate entity type for table 'KEYWORDS_INV                   '. Please see the warning messages.
        // Unable to generate entity type for table 'LANGUAGES_INV                  '. Please see the warning messages.
        // Unable to generate entity type for table 'EDIT_ITEMS                     '. Please see the warning messages.
        // Unable to generate entity type for table 'NOTES                          '. Please see the warning messages.
        // Unable to generate entity type for table 'PERSONS_INV                    '. Please see the warning messages.
        // Unable to generate entity type for table 'SKIP_ZONES                     '. Please see the warning messages.
        // Unable to generate entity type for table 'CUSTOM_PROPS_INV               '. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseFirebird("database=127.0.0.1:databox_hd.gdb;user=sysdba;password=masterkey;Dialect=3;Charset=UTF8;ServerType=0;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgeRates>(entity =>
            {
                entity.HasKey(e => e.AgeRateid)
                    .HasName("RDB$PRIMARY1");

                entity.ToTable("AGE_RATES                      ");

                entity.HasIndex(e => e.AgeRateid)
                    .HasName("RDB$PRIMARY1");

                entity.Property(e => e.AgeRateid).HasColumnName("AGE_RATEID");

                entity.Property(e => e.AgeRateName)
                    .HasColumnName("AGE_RATE_NAME")
                    .HasMaxLength(20);

                entity.Property(e => e.Color).HasColumnName("COLOR");
            });

            modelBuilder.Entity<Archive>(entity =>
            {
                entity.ToTable("ARCHIVE                        ");

                entity.HasIndex(e => e.Archiveid)
                    .HasName("RDB$PRIMARY2");

                entity.Property(e => e.Archiveid).HasColumnName("ARCHIVEID");

                entity.Property(e => e.ArchiveName)
                    .HasColumnName("ARCHIVE_NAME")
                    .HasMaxLength(40);

                entity.Property(e => e.Color).HasColumnName("COLOR");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.Categoryid)
                    .HasName("RDB$PRIMARY3");

                entity.ToTable("CATEGORIES                     ");

                entity.HasIndex(e => e.Categoryid)
                    .HasName("RDB$PRIMARY3");

                entity.Property(e => e.Categoryid).HasColumnName("CATEGORYID");

                entity.Property(e => e.CategoryName)
                    .HasColumnName("CATEGORY_NAME")
                    .HasMaxLength(40);

                entity.Property(e => e.Color).HasColumnName("COLOR");

                entity.Property(e => e.Typeid).HasColumnName("TYPEID");
            });

            modelBuilder.Entity<Companies>(entity =>
            {
                entity.HasKey(e => e.Companyid)
                    .HasName("RDB$PRIMARY4");

                entity.ToTable("COMPANIES                      ");

                entity.HasIndex(e => e.Companyid)
                    .HasName("RDB$PRIMARY4");

                entity.Property(e => e.Companyid).HasColumnName("COMPANYID");

                entity.Property(e => e.Color).HasColumnName("COLOR");

                entity.Property(e => e.CompanyName)
                    .HasColumnName("COMPANY_NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CompanyActivity>(entity =>
            {
                entity.HasKey(e => e.Activityid)
                    .HasName("RDB$PRIMARY5");

                entity.ToTable("COMPANY_ACTIVITY               ");

                entity.HasIndex(e => e.Activityid)
                    .HasName("RDB$PRIMARY5");

                entity.Property(e => e.Activityid).HasColumnName("ACTIVITYID");

                entity.Property(e => e.ActivityName)
                    .HasColumnName("ACTIVITY_NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.Color).HasColumnName("COLOR");
            });

            modelBuilder.Entity<Countries>(entity =>
            {
                entity.HasKey(e => e.Countryid)
                    .HasName("RDB$PRIMARY6");

                entity.ToTable("COUNTRIES                      ");

                entity.HasIndex(e => e.Countryid)
                    .HasName("RDB$PRIMARY6");

                entity.Property(e => e.Countryid).HasColumnName("COUNTRYID");

                entity.Property(e => e.Ab2)
                    .HasColumnName("AB2")
                    .HasMaxLength(2);

                entity.Property(e => e.Ab3)
                    .HasColumnName("AB3")
                    .HasMaxLength(3);

                entity.Property(e => e.Color).HasColumnName("COLOR");

                entity.Property(e => e.CountryName)
                    .HasColumnName("COUNTRY_NAME")
                    .HasMaxLength(40);

                entity.Property(e => e.DialingCode).HasColumnName("DIALING_CODE");

                entity.Property(e => e.Flagid).HasColumnName("FLAGID");
            });

            modelBuilder.Entity<CountryActivity>(entity =>
            {
                entity.HasKey(e => e.Activityid)
                    .HasName("RDB$PRIMARY7");

                entity.ToTable("COUNTRY_ACTIVITY               ");

                entity.HasIndex(e => e.Activityid)
                    .HasName("RDB$PRIMARY7");

                entity.Property(e => e.Activityid).HasColumnName("ACTIVITYID");

                entity.Property(e => e.ActivityName)
                    .HasColumnName("ACTIVITY_NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.Color).HasColumnName("COLOR");
            });

            modelBuilder.Entity<CustomProps>(entity =>
            {
                entity.ToTable("CUSTOM_PROPS                   ");

                entity.HasIndex(e => e.Id)
                    .HasName("PK_CUSTOM_PROPS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Color).HasColumnName("COLOR");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(60);

                entity.Property(e => e.ParentId).HasColumnName("PARENT_ID");

                entity.Property(e => e.PropType).HasColumnName("PROP_TYPE");

                entity.Property(e => e.Res).HasColumnName("RES");
            });

            modelBuilder.Entity<Genres>(entity =>
            {
                entity.HasKey(e => e.Genreid)
                    .HasName("RDB$PRIMARY8");

                entity.ToTable("GENRES                         ");

                entity.HasIndex(e => e.Genreid)
                    .HasName("RDB$PRIMARY8");

                entity.Property(e => e.Genreid).HasColumnName("GENREID");

                entity.Property(e => e.Categoryid).HasColumnName("CATEGORYID");

                entity.Property(e => e.Color).HasColumnName("COLOR");

                entity.Property(e => e.GenreName)
                    .HasColumnName("GENRE_NAME")
                    .HasMaxLength(40);

                entity.Property(e => e.Typeid).HasColumnName("TYPEID");
            });

            modelBuilder.Entity<Groups>(entity =>
            {
                entity.HasKey(e => e.Groupid)
                    .HasName("RDB$PRIMARY9");

                entity.ToTable("GROUPS                         ");

                entity.HasIndex(e => e.Groupid)
                    .HasName("RDB$PRIMARY9");

                entity.Property(e => e.Groupid).HasColumnName("GROUPID");

                entity.Property(e => e.Color).HasColumnName("COLOR");

                entity.Property(e => e.GroupName)
                    .HasColumnName("GROUP_NAME")
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<Instance>(entity =>
            {
                entity.ToTable("INSTANCE                       ");

                entity.HasIndex(e => e.Instanceid)
                    .HasName("RDB$PRIMARY10");

                entity.Property(e => e.Instanceid).HasColumnName("INSTANCEID");

                entity.Property(e => e.Duration).HasColumnName("DURATION");

                entity.Property(e => e.InstanceName)
                    .HasColumnName("INSTANCE_NAME")
                    .HasMaxLength(40);

                entity.Property(e => e.KillDate)
                    .HasColumnName("KILL_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.Main)
                    .HasColumnName("MAIN")
                    .HasMaxLength(1);

                entity.Property(e => e.Qualityid).HasColumnName("QUALITYID");

                entity.Property(e => e.Recid).HasColumnName("RECID");

                entity.Property(e => e.Start).HasColumnName("START");

                entity.Property(e => e.Status).HasColumnName("STATUS");
            });

            modelBuilder.Entity<Keywords>(entity =>
            {
                entity.HasKey(e => e.Keywordid)
                    .HasName("RDB$PRIMARY11");

                entity.ToTable("KEYWORDS                       ");

                entity.HasIndex(e => e.Keywordid)
                    .HasName("RDB$PRIMARY11");

                entity.Property(e => e.Keywordid).HasColumnName("KEYWORDID");

                entity.Property(e => e.Color).HasColumnName("COLOR");

                entity.Property(e => e.KeywordName)
                    .HasColumnName("KEYWORD_NAME")
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<Languages>(entity =>
            {
                entity.HasKey(e => e.Languageid)
                    .HasName("RDB$PRIMARY12");

                entity.ToTable("LANGUAGES                      ");

                entity.HasIndex(e => e.Languageid)
                    .HasName("RDB$PRIMARY12");

                entity.Property(e => e.Languageid).HasColumnName("LANGUAGEID");

                entity.Property(e => e.Color).HasColumnName("COLOR");

                entity.Property(e => e.LanguageName)
                    .HasColumnName("LANGUAGE_NAME")
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<Master>(entity =>
            {
                entity.HasKey(e => e.Recid)
                    .HasName("RDB$PRIMARY14");

                entity.ToTable("MASTER                         ");

                entity.HasIndex(e => e.Clipid)
                    .HasName("RDB$13");

                entity.HasIndex(e => e.Recid)
                    .HasName("RDB$PRIMARY14");

                entity.Property(e => e.Recid).HasColumnName("RECID");

                entity.Property(e => e.AgeRate).HasColumnName("AGE_RATE");

                entity.Property(e => e.Categoryid).HasColumnName("CATEGORYID");

                entity.Property(e => e.Clipid)
                    .IsRequired()
                    .HasColumnName("CLIPID")
                    .HasMaxLength(40);

                entity.Property(e => e.Creator)
                    .HasColumnName("CREATOR")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("DEFAULT USER");

                entity.Property(e => e.Duration).HasColumnName("DURATION");

                entity.Property(e => e.Enablemask).HasColumnName("ENABLEMASK");

                entity.Property(e => e.Episodeno).HasColumnName("EPISODENO");

                entity.Property(e => e.Groupid).HasColumnName("GROUPID");

                entity.Property(e => e.Languageid).HasColumnName("LANGUAGEID");

                entity.Property(e => e.Plotoutline)
                    .HasColumnName("PLOTOUTLINE")
                    .HasMaxLength(255);

                entity.Property(e => e.Priority).HasColumnName("PRIORITY");

                entity.Property(e => e.Proddate)
                    .HasColumnName("PRODDATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.Rating).HasColumnName("RATING");

                entity.Property(e => e.Receiptdate)
                    .HasColumnName("RECEIPTDATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.ReqDay1).HasColumnName("REQ_DAY1");

                entity.Property(e => e.ReqDay2).HasColumnName("REQ_DAY2");

                entity.Property(e => e.ReqHour1).HasColumnName("REQ_HOUR1");

                entity.Property(e => e.ReqHour2).HasColumnName("REQ_HOUR2");

                entity.Property(e => e.ReqLeft).HasColumnName("REQ_LEFT");

                entity.Property(e => e.ReqMonth1).HasColumnName("REQ_MONTH1");

                entity.Property(e => e.ReqMonth2).HasColumnName("REQ_MONTH2");

                entity.Property(e => e.ReqSlot1).HasColumnName("REQ_SLOT1");

                entity.Property(e => e.ReqSlot2).HasColumnName("REQ_SLOT2");

                entity.Property(e => e.ReqTotal).HasColumnName("REQ_TOTAL");

                entity.Property(e => e.ReqWeek1).HasColumnName("REQ_WEEK1");

                entity.Property(e => e.ReqWeek2).HasColumnName("REQ_WEEK2");

                entity.Property(e => e.ReqYear1).HasColumnName("REQ_YEAR1");

                entity.Property(e => e.ReqYear2).HasColumnName("REQ_YEAR2");

                entity.Property(e => e.Season)
                    .HasColumnName("SEASON")
                    .HasMaxLength(40);

                entity.Property(e => e.Sequenceid).HasColumnName("SEQUENCEID");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.Title)
                    .HasColumnName("TITLE")
                    .HasMaxLength(40);

                entity.Property(e => e.Typeid).HasColumnName("TYPEID");

                entity.Property(e => e.ValidFrom).HasColumnName("VALID_FROM");

                entity.Property(e => e.ValidTo).HasColumnName("VALID_TO");
            });

            modelBuilder.Entity<Media>(entity =>
            {
                entity.ToTable("MEDIA                          ");

                entity.HasIndex(e => e.Mediaid)
                    .HasName("RDB$PRIMARY15");

                entity.Property(e => e.Mediaid).HasColumnName("MEDIAID");

                entity.Property(e => e.Archiveid).HasColumnName("ARCHIVEID");

                entity.Property(e => e.InP).HasColumnName("IN_P");

                entity.Property(e => e.MediaName)
                    .HasColumnName("MEDIA_NAME")
                    .HasMaxLength(40);

                entity.Property(e => e.MediaPool).HasColumnName("MEDIA_POOL");

                entity.Property(e => e.MediaTypeid).HasColumnName("MEDIA_TYPEID");

                entity.Property(e => e.OutP).HasColumnName("OUT_P");

                entity.Property(e => e.Pool)
                    .HasColumnName("POOL")
                    .HasMaxLength(50);

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.Streamid).HasColumnName("STREAMID");
            });

            modelBuilder.Entity<MediaType>(entity =>
            {
                entity.ToTable("MEDIA_TYPE                     ");

                entity.HasIndex(e => e.MediaTypeid)
                    .HasName("RDB$PRIMARY17");

                entity.Property(e => e.MediaTypeid).HasColumnName("MEDIA_TYPEID");

                entity.Property(e => e.Color).HasColumnName("COLOR");

                entity.Property(e => e.MediaName)
                    .HasColumnName("MEDIA_NAME")
                    .HasMaxLength(40);

                entity.Property(e => e.PrepareTime).HasColumnName("PREPARE_TIME");

                entity.Property(e => e.Status).HasColumnName("STATUS");
            });

            modelBuilder.Entity<Persons>(entity =>
            {
                entity.HasKey(e => e.Personid)
                    .HasName("RDB$PRIMARY18");

                entity.ToTable("PERSONS                        ");

                entity.HasIndex(e => e.Personid)
                    .HasName("RDB$PRIMARY18");

                entity.Property(e => e.Personid).HasColumnName("PERSONID");

                entity.Property(e => e.Color).HasColumnName("COLOR");

                entity.Property(e => e.PersonName)
                    .HasColumnName("PERSON_NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Positions>(entity =>
            {
                entity.HasKey(e => e.Positionid)
                    .HasName("RDB$PRIMARY19");

                entity.ToTable("POSITIONS                      ");

                entity.HasIndex(e => e.Positionid)
                    .HasName("RDB$PRIMARY19");

                entity.Property(e => e.Positionid).HasColumnName("POSITIONID");

                entity.Property(e => e.Color).HasColumnName("COLOR");

                entity.Property(e => e.PositionName)
                    .HasColumnName("POSITION_NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Quality>(entity =>
            {
                entity.ToTable("QUALITY                        ");

                entity.HasIndex(e => e.Qualityid)
                    .HasName("RDB$PRIMARY20");

                entity.Property(e => e.Qualityid).HasColumnName("QUALITYID");

                entity.Property(e => e.Color).HasColumnName("COLOR");

                entity.Property(e => e.QOrder).HasColumnName("Q_ORDER");

                entity.Property(e => e.QualityName)
                    .HasColumnName("QUALITY_NAME")
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<Sequences>(entity =>
            {
                entity.HasKey(e => e.Sequenceid)
                    .HasName("RDB$PRIMARY21");

                entity.ToTable("SEQUENCES                      ");

                entity.HasIndex(e => e.Sequenceid)
                    .HasName("RDB$PRIMARY21");

                entity.Property(e => e.Sequenceid).HasColumnName("SEQUENCEID");

                entity.Property(e => e.Color).HasColumnName("COLOR");

                entity.Property(e => e.EpisodeCount).HasColumnName("EPISODE_COUNT");

                entity.Property(e => e.SequenceName)
                    .HasColumnName("SEQUENCE_NAME")
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<Stream>(entity =>
            {
                entity.ToTable("STREAM                         ");

                entity.HasIndex(e => e.Streamid)
                    .HasName("RDB$PRIMARY22");

                entity.Property(e => e.Streamid).HasColumnName("STREAMID");

                entity.Property(e => e.AudioInfoid).HasColumnName("AUDIO_INFOID");

                entity.Property(e => e.AudioLevel).HasColumnName("AUDIO_LEVEL");

                entity.Property(e => e.FileName)
                    .HasColumnName("FILE_NAME")
                    .HasMaxLength(260);

                entity.Property(e => e.FileSize).HasColumnName("FILE_SIZE");

                entity.Property(e => e.InP).HasColumnName("IN_P");

                entity.Property(e => e.Instanceid).HasColumnName("INSTANCEID");

                entity.Property(e => e.Languageid).HasColumnName("LANGUAGEID");

                entity.Property(e => e.Main)
                    .HasColumnName("MAIN")
                    .HasMaxLength(1);

                entity.Property(e => e.OutP).HasColumnName("OUT_P");

                entity.Property(e => e.Part).HasColumnName("PART");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.StreamName)
                    .HasColumnName("STREAM_NAME")
                    .HasMaxLength(40);

                entity.Property(e => e.StreamType).HasColumnName("STREAM_TYPE");

                entity.Property(e => e.VideoInfoid).HasColumnName("VIDEO_INFOID");
            });

            modelBuilder.Entity<StreamAudioInfo>(entity =>
            {
                entity.HasKey(e => e.StreamInfoid)
                    .HasName("RDB$PRIMARY23");

                entity.ToTable("STREAM_AUDIO_INFO              ");

                entity.HasIndex(e => e.StreamInfoid)
                    .HasName("RDB$PRIMARY23");

                entity.Property(e => e.StreamInfoid).HasColumnName("STREAM_INFOID");

                entity.Property(e => e.ACT)
                    .HasColumnName("A_C_T")
                    .HasMaxLength(15);

                entity.Property(e => e.AudioBitRate).HasColumnName("AUDIO_BIT_RATE");

                entity.Property(e => e.AudioChannels).HasColumnName("AUDIO_CHANNELS");

                entity.Property(e => e.AudioSampleRate).HasColumnName("AUDIO_SAMPLE_RATE");
            });

            modelBuilder.Entity<StreamVideoInfo>(entity =>
            {
                entity.HasKey(e => e.StreamInfoid)
                    .HasName("RDB$PRIMARY24");

                entity.ToTable("STREAM_VIDEO_INFO              ");

                entity.HasIndex(e => e.StreamInfoid)
                    .HasName("RDB$PRIMARY24");

                entity.Property(e => e.StreamInfoid).HasColumnName("STREAM_INFOID");

                entity.Property(e => e.FrameRate).HasColumnName("FRAME_RATE");

                entity.Property(e => e.Height).HasColumnName("HEIGHT");

                entity.Property(e => e.VCT)
                    .HasColumnName("V_C_T")
                    .HasMaxLength(15);

                entity.Property(e => e.VideoBitRate).HasColumnName("VIDEO_BIT_RATE");

                entity.Property(e => e.Width).HasColumnName("WIDTH");
            });

            modelBuilder.Entity<Types>(entity =>
            {
                entity.HasKey(e => e.Typeid)
                    .HasName("RDB$PRIMARY25");

                entity.ToTable("TYPES                          ");

                entity.HasIndex(e => e.Typeid)
                    .HasName("RDB$PRIMARY25");

                entity.Property(e => e.Typeid).HasColumnName("TYPEID");

                entity.Property(e => e.Color).HasColumnName("COLOR");

                entity.Property(e => e.TypeName)
                    .HasColumnName("TYPE_NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserGroups>(entity =>
            {
                entity.HasKey(e => e.UserGroupid)
                    .HasName("RDB$PRIMARY27");

                entity.ToTable("USER_GROUPS                    ");

                entity.HasIndex(e => e.UserGroupid)
                    .HasName("RDB$PRIMARY27");

                entity.Property(e => e.UserGroupid).HasColumnName("USER_GROUPID");

                entity.Property(e => e.Rights).HasColumnName("RIGHTS");

                entity.Property(e => e.UserGroupName)
                    .HasColumnName("USER_GROUP_NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("RDB$PRIMARY26");

                entity.ToTable("USERS                          ");

                entity.HasIndex(e => e.Userid)
                    .HasName("RDB$PRIMARY26");

                entity.Property(e => e.Userid).HasColumnName("USERID");

                entity.Property(e => e.UserGroupid).HasColumnName("USER_GROUPID");

                entity.Property(e => e.UserName)
                    .HasColumnName("USER_NAME")
                    .HasMaxLength(60);

                entity.Property(e => e.UserPassword)
                    .HasColumnName("USER_PASSWORD")
                    .HasMaxLength(60);
            });
        }
    }
}
