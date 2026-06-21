using AssignmentOne.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssignmentOne.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<VideoGame> VideoGames => Set<VideoGame>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VideoGame>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(2000);
            entity.Property(e => e.CreateDate).IsRequired();

            entity.HasData(
                new VideoGame { Id = new Guid("a1000000-0000-0000-0000-000000000001"), Name = "The Legend of Zelda: Breath of the Wild", Description = "An open-world action-adventure game set in Hyrule.", CreateDate = new DateTime(2017, 3, 3, 0, 0, 0, DateTimeKind.Utc) },
                new VideoGame { Id = new Guid("a1000000-0000-0000-0000-000000000002"), Name = "Red Dead Redemption 2", Description = "An epic tale of life in America at the dawn of the modern age.", CreateDate = new DateTime(2018, 10, 26, 0, 0, 0, DateTimeKind.Utc) },
                new VideoGame { Id = new Guid("a1000000-0000-0000-0000-000000000003"), Name = "The Witcher 3: Wild Hunt", Description = "A story-driven open world RPG set in a visually stunning fantasy universe.", CreateDate = new DateTime(2015, 5, 19, 0, 0, 0, DateTimeKind.Utc) },
                new VideoGame { Id = new Guid("a1000000-0000-0000-0000-000000000004"), Name = "Dark Souls III", Description = "A challenging action RPG set in a dark, dying world.", CreateDate = new DateTime(2016, 3, 24, 0, 0, 0, DateTimeKind.Utc) },
                new VideoGame { Id = new Guid("a1000000-0000-0000-0000-000000000005"), Name = "Hollow Knight", Description = "A challenging 2D action-adventure set in a beautifully hand-drawn insect kingdom.", CreateDate = new DateTime(2017, 2, 24, 0, 0, 0, DateTimeKind.Utc) },
                new VideoGame { Id = new Guid("a1000000-0000-0000-0000-000000000006"), Name = "Elden Ring", Description = "An action RPG set in the Lands Between, created with George R. R. Martin.", CreateDate = new DateTime(2022, 2, 25, 0, 0, 0, DateTimeKind.Utc) },
                new VideoGame { Id = new Guid("a1000000-0000-0000-0000-000000000007"), Name = "God of War", Description = "Kratos and his son Atreus journey through the Norse realms.", CreateDate = new DateTime(2018, 4, 20, 0, 0, 0, DateTimeKind.Utc) },
                new VideoGame { Id = new Guid("a1000000-0000-0000-0000-000000000008"), Name = "Cyberpunk 2077", Description = "An open-world RPG set in the dystopian Night City.", CreateDate = new DateTime(2020, 12, 10, 0, 0, 0, DateTimeKind.Utc) },
                new VideoGame { Id = new Guid("a1000000-0000-0000-0000-000000000009"), Name = "Hades", Description = "A roguelike dungeon crawler where you battle out of the Underworld.", CreateDate = new DateTime(2020, 9, 17, 0, 0, 0, DateTimeKind.Utc) },
                new VideoGame { Id = new Guid("a1000000-0000-0000-0000-000000000010"), Name = "Sekiro: Shadows Die Twice", Description = "A shinobi action game set in late 1500s Sengoku Japan.", CreateDate = new DateTime(2019, 3, 22, 0, 0, 0, DateTimeKind.Utc) }
            );
        });
    }
}
