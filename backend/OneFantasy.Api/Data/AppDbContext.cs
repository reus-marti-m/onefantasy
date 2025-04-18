using Microsoft.EntityFrameworkCore;
using OneFantasy.Api.Models.Competitions;
using OneFantasy.Api.Models.Participations;
using OneFantasy.Api.Models.Seasons;
using OneFantasy.Api.Models.MinigameGroups;
using OneFantasy.Api.Models.Minigames;
using OneFantasy.Api.Models.MinigameOptions;

namespace OneFantasy.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // DbSet per a cada arrel d’entitat
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<CompetitionSeason> CompetitionSeasons { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }

        public DbSet<Participation> Participations { get; set; }
        public DbSet<MinigameGroup> MinigameGroups { get; set; }
        public DbSet<Minigame> Minigames { get; set; }
        public DbSet<Option> Options { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            //
            // —— Competition ⟷ CompetitionSeason (1:N)
            //
            mb.Entity<Competition>()
                .HasMany(c => c.Seasons)
                .WithOne(s => s.Competition)
                .HasForeignKey("CompetitionId")
                .IsRequired();

            //
            // —— CompetitionSeason ⟷ Team (1:N) and ⟷ Participation (1:N)
            //
            mb.Entity<CompetitionSeason>()
                .HasMany(s => s.Teams)
                .WithOne()
                .HasForeignKey("CompetitionSeasonId")
                .IsRequired();

            mb.Entity<CompetitionSeason>()
                .HasMany(s => s.Participations)
                .WithOne(p => p.CompetitionSeason)
                .HasForeignKey("CompetitionSeasonId")
                .IsRequired();

            //
            // —— Team ⟷ Player (1:N)
            //
            mb.Entity<Team>()
                .HasMany(t => t.Players)
                .WithOne(p => p.Team)
                .HasForeignKey("TeamId")
                .IsRequired();

            //
            // —— Participation inheritance TPH: ExtraOrSpecial, Standard
            //
            mb.Entity<Participation>()
                .HasDiscriminator<string>("ParticipationType")
                .HasValue<ParticipationExtraOrSpecial>("ExtraOrSpecial")
                .HasValue<ParticipationStandard>("Standard");

            //
            // —— Participation ⟷ MinigameGroup (1:N)
            //
            mb.Entity<Participation>()
                .HasMany<MinigameGroup>()
                .WithOne(g => g.Participation)
                .HasForeignKey("ParticipationId")
                .IsRequired();

            //
            // —— MinigameGroup inheritance TPH
            //
            mb.Entity<MinigameGroup>()
                .HasDiscriminator<string>("GroupType")
                .HasValue<MinigameGroupMatch2A>("Match2A")
                .HasValue<MinigameGroupMatch2B>("Match2B")
                .HasValue<MinigameGroupMatch3>("Match3")
                .HasValue<MinigameGroupMulti>("Multi");

            //
            // —— Group → Minigame relations (1:1) 
            //     cada subtipo té nav propi, FK automàtic (shadow) 
            //
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

            //
            // —— Minigame inheritance TPH
            //
            mb.Entity<Minigame>()
                .HasDiscriminator<string>("MinigameType")
                .HasValue<MinigameMatch>("Match")
                .HasValue<MinigamePlayers>("Players")
                .HasValue<MinigameResult>("Result")
                .HasValue<MinigameScores>("Scores");

            //
            // —— Option inheritance TPH
            //
            mb.Entity<Option>()
                .HasDiscriminator<string>("OptionType")
                .HasValue<OptionInterval>("Interval")
                .HasValue<OptionPlayer>("Player")
                .HasValue<OptionScore>("Score")
                .HasValue<OptionTeam>("Team");

            //
            // —— MinigameMatch/Players/Scores → Options (1:N) com a entitats normals
            //
            mb.Entity<Option>()
                .HasOne(o => o.Minigame)
                .WithMany()               // sense llista a la punta “1”, ja que no la tens al base
                .HasForeignKey(o => o.MinigameId)
                .IsRequired();
            //
            // —— MinigameResult → Draw, HomeVictory, VisitingVictory (1:1)
            //
            // (suposant que has afegit DrawId, HomeVictoryId i VisitingVictoryId com a FK a MinigameResult)
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
