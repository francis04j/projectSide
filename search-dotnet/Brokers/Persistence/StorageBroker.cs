﻿using EFxceptions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;

namespace Brokers
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; } = new();
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }

    public class Blogg
    {
        public int Id { get; set; }
        public string Term { get; set; }

        public DateTime Created { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string? LastModifiedBy { get; set; }

    }

    public class StorageBroker : DbContext
    {
            public string DbPath { get; }
            public DbSet<Blog> Blogs { get; set; }
            public DbSet<Post> Posts { get; set; }

        public DbSet<Blogg> Bloggs { get; set; }

        public StorageBroker(DbContextOptions<StorageBroker> options)
             : base(options) { 
             this.Database.Migrate();
             var folder = Environment.SpecialFolder.LocalApplicationData;
             var path = Environment.GetFolderPath(folder);
             DbPath = System.IO.Path.Join(path, "blogging.db");
         }
        

            public StorageBroker()
                {
                    var folder = Environment.SpecialFolder.LocalApplicationData;
                    var path = Environment.GetFolderPath(folder);
                    DbPath = System.IO.Path.Join(path, "blogging.db");
            }

        // protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlite($"Data Source={DbPath}", x => x.MigrationsAssembly("Brokers"));
        //    

           protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer($"Data Source=localhost,1500; Initial Catalog=dotnet-5-crud-api; User Id=sa; Password=Pass@word", x => x.MigrationsAssembly("Brokers"));          
            
        /*  {
              return await Queries.FirstOrDefaultAsync(x => x.Term == term);
          }

          public async Task<Restaurant> AddRestaurant(Restaurant restaurant)
          {
              EntityEntry<Restaurant> entityEntry = await Restaurants.AddAsync(restaurant);
              await SaveChangesAsync();

              return entityEntry.Entity;
          }
        */
    }
}
