namespace TypeScriptDemo.Migrations
{
    using TypeScriptDemo.Entity.POCO;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TypeScriptDemo.Entity.POCO.CodeHubsAPIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TypeScriptDemo.Entity.POCO.CodeHubsAPIContext context)
        {
            var getUsersCount = context.Users.Count();
            if (getUsersCount == 0)
            {
                for (int i = 0; i < 50; i++)
                {
                    Users users = new Users
                    {
                        Name = $"Test User {i}",
                        Email = $"testuser{i}@gmail.com",
                        Phone = $"123456789{i}",
                    };
                    context.Users.Add(users);
                }
                context.SaveChanges();
            }
        }
    }
}
