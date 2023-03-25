using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RehApp.Data.EntityFrameworkCore.EntityConfigurations;
using RehApp.Domain.RelationalDatabase.Entities;

namespace RehApp.Data.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public DbSet<Appeal> Appeals => Set<Appeal>();
    public DbSet<AppealDesc> AppealsDesc => Set<AppealDesc>();

    public DbSet<Diary> Diaries => Set<Diary>();
    public DbSet<DiaryDesc> DiariesDesc => Set<DiaryDesc>();
    public DbSet<DiaryEntry> DiaryEntries => Set<DiaryEntry>();

    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<ExerciseDesc> ExercisesDesc => Set<ExerciseDesc>();
    public DbSet<ExerciseHistory> ExercisesHistory => Set<ExerciseHistory>();

    public DbSet<ExerciseParams> ExercisesParams => Set<ExerciseParams>();
    public DbSet<ExerciseParamsDesc> ExercisesParamsDesc => Set<ExerciseParamsDesc>();
    public DbSet<ExerciseParamsHistory> ExercisesParamsHistory => Set<ExerciseParamsHistory>();

    public DbSet<Post> Posts => Set<Post>();
    public DbSet<PostDesc> PostsDesc => Set<PostDesc>();

    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<ReviewDesc> ReviewsDesc => Set<ReviewDesc>();

    public DbSet<Training> Trainings => Set<Training>();
    public DbSet<TrainingHistory> TrainingsHistory => Set<TrainingHistory>();

    public DbSet<DescriptionType> DescriptionTypes => Set<DescriptionType>();
    public DbSet<DescriptionValue> DescriptionValues => Set<DescriptionValue>();

    public DbSet<ApplicationUserDesc> ApplicationUsersDesc => Set<ApplicationUserDesc>();
    public DbSet<ExtAuthInfo> ExtAuthInfos => Set<ExtAuthInfo>();
    public DbSet<City> Cities => Set<City>();
    public DbSet<Invitation> Invitations => Set<Invitation>();
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<Observation> Observations => Set<Observation>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //place to apply your own fluent entity configurations
        builder.ApplyConfiguration(new DiaryEntriesConfiguration());

        base.OnModelCreating(builder);
    }
}