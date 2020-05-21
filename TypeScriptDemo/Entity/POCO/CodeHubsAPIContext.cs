using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TypeScriptDemo.Entity.POCO
{
    public class CodeHubsAPIContext : DbContext
    {
        public CodeHubsAPIContext() : base("DefaultConnection") { }
        public DbSet<Users> Users { get; set; }
    }
}