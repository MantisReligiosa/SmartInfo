namespace Repository.Migrations
{
    using DomainObjects;
    using DomainObjects.Blocks.Details;
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
            AddDateTimeFormats(context);

            context.SaveChanges();
        }

        private static void AddDateTimeFormats(DatabaseContext context)
        {
            context.Set<DateTimeFormat>().AddOrUpdate(new DateTimeFormat
            {
                Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1),
                Denomination = "◊24:ÃÃ:——",
                DesigntimeFormat = "HH:mm:ss",
                ShowtimeFormat = "HH:mm:ss"
            });

            context.Set<DateTimeFormat>().AddOrUpdate(new DateTimeFormat
            {
                Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2),
                Denomination = "◊12:ÃÃ:——",
                DesigntimeFormat = "hh:mm:ss",
                ShowtimeFormat = "hh:mm:ss",
                IsDateFormat = false
            });

            context.Set<DateTimeFormat>().AddOrUpdate(new DateTimeFormat
            {
                Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 3),
                Denomination = "◊24:ÃÃ",
                DesigntimeFormat = "HH:mm",
                ShowtimeFormat = "HH:mm",
                IsDateFormat = false
            });

            context.Set<DateTimeFormat>().AddOrUpdate(new DateTimeFormat
            {
                Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 4),
                Denomination = "◊12:ÃÃ",
                DesigntimeFormat = "hh:mm",
                ShowtimeFormat = "hh:mm",
                IsDateFormat = false
            });

            context.Set<DateTimeFormat>().AddOrUpdate(new DateTimeFormat
            {
                Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 1),
                Denomination = "ƒƒ.ÃÃ.√√√√",
                DesigntimeFormat = "DD.MM.YYYY",
                ShowtimeFormat = "dd.MM.yyyy",
                IsDateFormat = true
            });

                        context.Set<DateTimeFormat>().AddOrUpdate(new DateTimeFormat
            {
                Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2),
                Denomination = "ƒƒ Ã≈—ﬂ÷ √√√√",
                DesigntimeFormat = "DD MMMM YYYY",
                ShowtimeFormat = "dd MMMM yyyy",
                IsDateFormat = true
            });

            context.Set<DateTimeFormat>().AddOrUpdate(new DateTimeFormat
            {
                Id = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3),
                Denomination = "ƒ≈Õ‹Õ≈ƒ≈À», ƒƒ Ã≈—ﬂ÷ √√√√",
                DesigntimeFormat = "dddd, DD MMMM YYYY",
                ShowtimeFormat = "dddd, dd MMMM yyyy",
                IsDateFormat = true
            });
        }
    }
}
