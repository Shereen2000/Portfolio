using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using unibooks_backend.Models;

namespace unibooks_backend.Data
{
    public class ApplicationDBContext: IdentityDbContext<APPUser>   //DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<Book> Book {get;set;}
        public DbSet<Advert> Adverts {get;set;}
        public DbSet<BookImage> BookImages {get;set;}
        public DbSet<BookMark> bookMarks{get;set;}

          protected override void  OnModelCreating(ModelBuilder builder)// seeding Roles
        {
            base.OnModelCreating(builder);

            
            builder.Entity<BookMark>(x => x.HasKey(p => new {p.AppUserId,p.AdvertId})); //
            
            builder.Entity<BookMark>().HasOne(u => u.AppUser)//
                                        .WithMany(u => u.BookMarks)
                                        .HasForeignKey(p => p.AppUserId);

            builder.Entity<BookMark>().HasOne(u => u.Advert)//
                                        .WithMany(u => u.BookMarks)
                                        .HasForeignKey(p => p.AdvertId);


            List<IdentityRole> roles = new List<IdentityRole>
            {
               new IdentityRole
               {
                Name = "Admin",
                NormalizedName = "ADMIN"
               },
               new IdentityRole
               {
                Name = "User",
                NormalizedName = "USER"
               },

            }; builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}