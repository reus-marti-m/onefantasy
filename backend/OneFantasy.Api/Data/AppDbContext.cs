using Microsoft.EntityFrameworkCore;
using OneFantasy.Api.Models.Competitions;
using OneFantasy.Api.Models.Participations;
using OneFantasy.Api.Models.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OneFantasy.Api.Models.Participations.MinigameGroups;
using OneFantasy.Api.Models.Participations.MinigameOptions;
using OneFantasy.Api.Models.Participations.Minigames;

namespace OneFantasy.Api.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Participation> Participations { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.Entity<Competition>(e =>
            {
                e.HasKey(c => c.Id);

                e.Property(c => c.Name)
                 .IsRequired()
                 .HasMaxLength(200);

                e.HasMany(c => c.Seasons)
                 .WithOne(s => s.Competition)
                 .HasForeignKey(s => s.CompetitionId)
                 .IsRequired();
            });

            mb.Entity<Season>(e =>
            {
                e.HasKey(s => s.Id);

                e.Property(s => s.Year)
                 .IsRequired();

                e.HasMany(s => s.Teams)
                 .WithOne(t => t.Season)
                 .HasForeignKey(t => t.SeasonId)
                 .IsRequired();

                e.HasMany(s => s.Participations)
                 .WithOne(p => p.Season)
                 .HasForeignKey(p => p.SeasonId)
                 .IsRequired();
            });

            mb.Entity<Team>(e =>
            {
                e.HasKey(t => t.Id);
                e.Property(t => t.Name)
                 .IsRequired()
                 .HasMaxLength(100);

                e.Property(t => t.Abbreviation)
                 .HasMaxLength(10);

                e.HasMany(t => t.Players)
                 .WithOne(p => p.Team)
                 .HasForeignKey(p => p.TeamId)
                 .IsRequired();
            });

            mb.Entity<Player>(e =>
            {
                e.HasKey(p => p.Id);

                e.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            mb.Entity<Option>(e =>
            {
                e.ToTable("MinigameOptions");
                e.HasKey(o => o.Id);

                e.Property(o => o.Price)
                 .IsRequired();

                e.Property(o => o.HasOccurred)
                 .IsRequired()
                 .HasDefaultValue(false);

                e.HasOne(o => o.Minigame)
                 .WithMany(m => m.Options)
                 .HasForeignKey(o => o.MinigameId)
                 .IsRequired();

                e.HasDiscriminator<string>("OptionType")
                 .HasValue<Option>("Base")
                 .HasValue<OptionInterval>("Interval")
                 .HasValue<OptionPlayer>("Player")
                 .HasValue<OptionScore>("Score")
                 .HasValue<OptionTeam>("Team");
            });

            mb.Entity<OptionPlayer>(e =>
            {
                e.HasOne(o => o.Player)
                 .WithMany()
                 .HasForeignKey(o => o.PlayerId)
                 .IsRequired();
            });

            mb.Entity<OptionTeam>(e =>
            {
                e.HasOne(o => o.Team)
                 .WithMany()
                 .HasForeignKey(o => o.TeamId)
                 .IsRequired();
            });

            mb.Entity<Minigame>(e =>
            {
                e.HasKey(m => m.Id);

                e.HasMany(m => m.Options)
                 .WithOne(o => o.Minigame)
                 .HasForeignKey(o => o.MinigameId)
                 .IsRequired();

                e.HasOne(m => m.Group)
                 .WithMany(g => g.Minigames)
                 .HasForeignKey(m => m.GroupId)
                 .IsRequired();

                e.Property(m => m.IsResolved)
                 .IsRequired()
                 .HasDefaultValue(false);

                e.HasDiscriminator<string>("MinigameType")
                 .HasValue<MinigameMatch>("Match")
                 .HasValue<MinigamePlayers>("Players")
                 .HasValue<MinigameResult>("Result")
                 .HasValue<MinigameScores>("Scores");
            });

            mb.Entity<MinigameMatch>(e =>
            {
                e.Property(m => m.Type)
                 .IsRequired();
            });

            mb.Entity<MinigamePlayers>(e =>
            {
                e.Property(m => m.Type)
                 .IsRequired();
            });

            mb.Entity<MinigameGroup>(e =>
            {
                e.HasKey(g => g.Id);

                e.HasMany(p => p.Minigames)
                    .WithOne(m => m.Group)
                    .HasForeignKey(p => p.GroupId)
                    .IsRequired();

                e.HasDiscriminator<string>("GroupType")
                 .HasValue<MinigameGroupMatch2A>("Match2A")
                 .HasValue<MinigameGroupMatch2B>("Match2B")
                 .HasValue<MinigameGroupMatch3>("Match3")
                 .HasValue<MinigameGroupMulti>("Multi");
            });

            mb.Entity<MinigameGroupMatch2A>(e =>
            {
                e.HasOne(g => g.HomeTeam)
                 .WithMany()
                 .HasForeignKey(g => g.HomeTeamId)
                 .IsRequired();

                e.HasOne(g => g.VisitingTeam)
                 .WithMany()
                 .HasForeignKey(g => g.VisitingTeamId)
                 .IsRequired();
            });

            mb.Entity<MinigameGroupMatch2B>(e =>
            {
                e.HasOne(g => g.HomeTeam)
                 .WithMany()
                 .HasForeignKey(g => g.HomeTeamId)
                 .IsRequired();

                e.HasOne(g => g.VisitingTeam)
                 .WithMany()
                 .HasForeignKey(g => g.VisitingTeamId)
                 .IsRequired();
            });

            mb.Entity<MinigameGroupMatch3>(e =>
            {
                e.HasOne(g => g.HomeTeam)
                 .WithMany()
                 .HasForeignKey(g => g.HomeTeamId)
                 .IsRequired();

                e.HasOne(g => g.VisitingTeam)
                 .WithMany()
                 .HasForeignKey(g => g.VisitingTeamId)
                 .IsRequired();
            });

            mb.Entity<Participation>(e =>
            {
                e.HasKey(p => p.Id);

                e.Property(p => p.Date)
                    .IsRequired();

                e.HasOne(p => p.Season)
                    .WithMany(s => s.Participations)
                    .HasForeignKey(p => p.SeasonId)
                    .IsRequired();

                e.HasMany(p => p.Groups)
                    .WithOne(g => g.Participation)
                    .HasForeignKey(g => g.ParticipationId)
                    .IsRequired();

                e.HasDiscriminator<string>("ParticipationType")
                    .HasValue<ParticipationExtra>("Extra")
                    .HasValue<ParticipationSpecial>("Special")
                    .HasValue<ParticipationStandard>("Standard");
            });
        }
    }
}
