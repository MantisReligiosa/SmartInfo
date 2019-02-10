namespace Repository.Migrations
{
    using DomainObjects;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Repository.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var user = new User
            {
                Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1),
                Login = "admin",
                PasswordHash = "7523c62abdb7628c5a9dad8f97d8d8c5c040ede36535e531a8a3748b6cae7e00"
            };
            context.Users.AddOrUpdate(user);


            var properties = context.Parameters.ToList();
            context.Parameters.RemoveRange(properties);

            var displays = context.Displays.ToList();
            context.Displays.RemoveRange(displays);
            context.SaveChanges();
        }
    }
}
