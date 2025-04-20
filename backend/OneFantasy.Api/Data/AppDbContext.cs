using Microsoft.EntityFrameworkCore;
using OneFantasy.Api.Models.Competitions;
using OneFantasy.Api.Models.Participations;
using OneFantasy.Api.Models.MinigameGroups;
using OneFantasy.Api.Models.Minigames;
using OneFantasy.Api.Models.MinigameOptions;
using OneFantasy.Api.Models.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace OneFantasy.Api.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Competition> Competitions { get; set; }
        public DbSet<CompetitionSeason> Seasons { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Participation> Participations { get; set; }
        public DbSet<MinigameGroup> MinigameGroups { get; set; }
        public DbSet<Minigame> Minigames { get; set; }
        public DbSet<Option> Options { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.Entity<Competition>()
                .HasMany(c => c.Seasons)
                .WithOne(s => s.Competition)
                .HasForeignKey("CompetitionId")
                .IsRequired();

            mb.Entity<CompetitionSeason>()
                .HasMany(s => s.Teams)
                .WithOne(t => t.CompetitionSeason)
                .HasForeignKey(t => t.CompetitionSeasonId)
                .IsRequired();

            mb.Entity<CompetitionSeason>()
                .HasMany(s => s.Participations)
                .WithOne(p => p.CompetitionSeason)
                .HasForeignKey("CompetitionSeasonId")
                .IsRequired();

            mb.Entity<Team>()
                .HasMany(t => t.Players)
                .WithOne(p => p.Team)
                .HasForeignKey("TeamId")
                .IsRequired();

            mb.Entity<Participation>()
                .HasDiscriminator<string>("ParticipationType")
                .HasValue<ParticipationExtraOrSpecial>("ExtraOrSpecial")
                .HasValue<ParticipationStandard>("Standard");

            mb.Entity<Participation>()
                .HasMany<MinigameGroup>()
                .WithOne(g => g.Participation)
                .HasForeignKey("ParticipationId")
                .IsRequired();

            mb.Entity<MinigameGroup>()
                .HasDiscriminator<string>("GroupType")
                .HasValue<MinigameGroupMatch2A>("Match2A")
                .HasValue<MinigameGroupMatch2B>("Match2B")
                .HasValue<MinigameGroupMatch3>("Match3")
                .HasValue<MinigameGroupMulti>("Multi");

            mb.Entity<MinigameGroupMatch2A>()
                .HasOne(g => g.MinigameScores)
                .WithOne()
                .HasForeignKey<MinigameGroupMatch2A>("MinigameScoresId")
                .IsRequired();

            mb.Entity<MinigameGroupMatch2A>()
                .HasOne(g => g.MinigamePlayers)
                .WithOne()
                .HasForeignKey<MinigameGroupMatch2A>("MinigamePlayersId")
                .IsRequired();

            mb.Entity<MinigameGroupMatch2A>()
                .HasOne(g => g.HomeTeam)
                .WithMany()
                .HasForeignKey("HomeTeamId")
                .IsRequired();

            mb.Entity<MinigameGroupMatch2A>()
                .HasOne(g => g.VisitingTeam)
                .WithMany()
                .HasForeignKey("VisitingTeamId")
                .IsRequired();

            mb.Entity<MinigameGroupMatch2B>()
                .HasOne(g => g.MinigameMatch)
                .WithOne()
                .HasForeignKey<MinigameGroupMatch2B>("MinigameMatchId")
                .IsRequired();

            mb.Entity<MinigameGroupMatch2B>()
                .HasOne(g => g.MinigamePlayers)
                .WithOne()
                .HasForeignKey<MinigameGroupMatch2B>("MinigamePlayersId")
                .IsRequired();

            mb.Entity<MinigameGroupMatch2B>()
                .HasOne(g => g.HomeTeam)
                .WithMany()
                .HasForeignKey("HomeTeamId")
                .IsRequired();

            mb.Entity<MinigameGroupMatch2B>()
                .HasOne(g => g.VisitingTeam)
                .WithMany()
                .HasForeignKey("VisitingTeamId")
                .IsRequired();

            mb.Entity<MinigameGroupMatch3>()
                .HasOne(g => g.MinigameScores)
                .WithOne()
                .HasForeignKey<MinigameGroupMatch3>("MinigameScoresId")
                .IsRequired();

            mb.Entity<MinigameGroupMatch3>()
                .HasOne(g => g.MinigamePlayers1)
                .WithOne()
                .HasForeignKey<MinigameGroupMatch3>("MinigamePlayers1Id")
                .IsRequired();

            mb.Entity<MinigameGroupMatch3>()
                .HasOne(g => g.MinigamePlayers2)
                .WithOne()
                .HasForeignKey<MinigameGroupMatch3>("MinigamePlayers2Id")
                .IsRequired();

            mb.Entity<MinigameGroupMatch3>()
                .HasOne(g => g.HomeTeam)
                .WithMany()
                .HasForeignKey("HomeTeamId")
                .IsRequired();

            mb.Entity<MinigameGroupMatch3>()
                .HasOne(g => g.VisitingTeam)
                .WithMany()
                .HasForeignKey("VisitingTeamId")
                .IsRequired();

            mb.Entity<MinigameGroupMulti>()
                .HasOne(g => g.Match1)
                .WithOne()
                .HasForeignKey<MinigameGroupMulti>("Match1Id")
                .IsRequired();

            mb.Entity<MinigameGroupMulti>()
                .HasOne(g => g.Match2)
                .WithOne()
                .HasForeignKey<MinigameGroupMulti>("Match2Id")
                .IsRequired();

            mb.Entity<MinigameGroupMulti>()
                .HasOne(g => g.Match3)
                .WithOne()
                .HasForeignKey<MinigameGroupMulti>("Match3Id")
                .IsRequired();

            mb.Entity<Minigame>()
                .HasDiscriminator<string>("MinigameType")
                .HasValue<MinigameMatch>("Match")
                .HasValue<MinigamePlayers>("Players")
                .HasValue<MinigameResult>("Result")
                .HasValue<MinigameScores>("Scores");

            mb.Entity<Option>()
                .HasDiscriminator<string>("OptionType")
                .HasValue<OptionInterval>("Interval")
                .HasValue<OptionPlayer>("Player")
                .HasValue<OptionScore>("Score")
                .HasValue<OptionTeam>("Team");

            mb.Entity<Option>()
                .HasOne(o => o.Minigame)
                .WithMany()              
                .HasForeignKey(o => o.MinigameId)
                .IsRequired();
         
            mb.Entity<MinigameResult>()
                .HasOne(m => m.Draw)
                .WithMany()
                .HasForeignKey("DrawId")
                .IsRequired();

            mb.Entity<MinigameResult>()
                .HasOne(m => m.HomeVictory)
                .WithMany()
                .HasForeignKey("HomeVictoryId")
                .IsRequired();

            mb.Entity<MinigameResult>()
                .HasOne(m => m.VisitingVictory)
                .WithMany()
                .HasForeignKey("VisitingVictoryId")
                .IsRequired();
        }
    }
}
