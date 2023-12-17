using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace K_Burns_SpitTree.Models
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<SpitTreeDbContext>
    {
        protected override void Seed(SpitTreeDbContext context)
        {

            if(!context.Users.Any())
            {
                //************************************************************
                //Creating a few roles and storing them in AspNetRole tables
                //************************************************************

                //create a roleManager object that will allow us to create roles and store them in the database
                RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                //if the Admin role doesn't exist
                if (!roleManager.RoleExists("Admin"))
                {
                    //create an Admin role
                    roleManager.Create(new IdentityRole("Admin"));
                }

                //if the Member role doesn't exist
                if (!roleManager.RoleExists("Member"))
                {
                    //create a Member role
                    roleManager.Create(new IdentityRole("Member"));
                }

                //save the new roles to the database
                context.SaveChanges();

                //************************************************************
                //Creating some users now and assign them to different roles
                //************************************************************

                //the userManager object allows creating users and store them in the database
                UserManager<User> userManager = new UserManager<User>(new UserStore<User>(context));

                //if users with the admin@spittree.com username doesn't exist then
                if (userManager.FindByName("admin@spittree.com") == null)
                {
                    //super relaxed password validator
                    userManager.PasswordValidator = new PasswordValidator()
                    {
                        RequireDigit = false,
                        RequiredLength = 1,
                        RequireLowercase = false,
                        RequireNonLetterOrDigit = false,
                        RequireUppercase = false
                    };

                    //create a user admin
                    var admin = new User()
                    {
                        UserName = "admin@spittree.com",
                        Email = "admin@spittree.com",
                        FirstName = "Jim",
                        LastName = "Smith",
                        Street = "56 High Street",
                        City = "Glasgow",
                        PostCode = "G1 67AD",
                        EmailConfirmed = true,
                        PhoneNumber = "00447869145567"
                    };

                    //add the hashed password to user
                    userManager.Create(admin, "admin123");

                    //add the user to the role Admin
                    userManager.AddToRole(admin.Id, "Admin");

                    //create a few members
                    var member1 = new User()
                    {
                        UserName = "member1@gmail.com",
                        Email = "member1@gmail.com",
                        FirstName = "Paul",
                        LastName = "Goat",
                        Street = "5 Merry Street",
                        City = "Coatbridge",
                        PostCode = "ML1 67AD",
                        EmailConfirmed = true,
                        PhoneNumber = "00447979164499"
                    };

                    if (userManager.FindByName("member1@gmail.com") == null)
                    {
                        userManager.Create(member1, "password1");
                        userManager.AddToRole(member1.Id, "Member");
                    }

                    var member2 = new User()
                    {
                        UserName = "member2@yahoo.com",
                        Email = "member2@yahoo.com",
                        FirstName = "Luigi",
                        LastName = "Musolini",
                        Street = "15 Confused Street",
                        City = "Rutherglen",
                        PostCode = "G1 7HO",
                        EmailConfirmed = true,
                        PhoneNumber = "00447779163399"
                    };

                    if (userManager.FindByName("member2@yahoo.com") == null)
                    {
                        userManager.Create(member2, "password2");
                        userManager.AddToRole(member2.Id, "Member");
                    }

                    //save users to the database
                    context.SaveChanges();

                    //************************************************************
                    //Seeding the Categories Table
                    //************************************************************

                    //create a few categories
                    var cat1 = new Category() { Name = "Motors" };
                    var cat2 = new Category() { Name = "Property" };
                    var cat3 = new Category() { Name = "Jobs" };
                    var cat4 = new Category() { Name = "Services" };
                    var cat5 = new Category() { Name = "Pets" };
                    var cat6 = new Category() { Name = "For Sale" };

                    //add each category to the Categories table
                    context.Categories.Add(cat1);
                    context.Categories.Add(cat2);
                    context.Categories.Add(cat3);
                    context.Categories.Add(cat4);
                    context.Categories.Add(cat5);
                    context.Categories.Add(cat6);

                    //save the changes to the database
                    context.SaveChanges();

                    //************************************************************
                    //Seeding the Posts Table
                    //************************************************************

                    //create a post
                    var post1 = new Post()
                    {
                        Title = "House For Sale",
                        Description = "Beautiful 5 bedroom detached house",
                        Location = "Glasgow",
                        Price = 145000m,
                        DatePosted = new DateTime(2019, 1, 1, 8, 0, 15), //this is the date when the post/ad was created
                        DateExpired = new DateTime(2019, 1, 1, 8, 0, 15).AddDays(14), //the post will expire after 14 days
                        User = member2,
                        Category = cat2
                    };

                    //add the post1 to the Posts table
                    context.Posts.Add(post1);

                    var post2 = new Post()
                    {
                        Title = "Hyunday Tucson",
                        Description = "Beautiful 2016 Hyunday 5Dr",
                        Location = "Edinbrugh",
                        Price = 14000m,
                        DatePosted = new DateTime(2019, 5, 25, 8, 0, 15),
                        DateExpired = new DateTime(2019, 5, 25, 8, 0, 15).AddDays(14),
                        User = member2,
                        Category = cat1
                    };
                    
                    //add the post2 to the Posts table
                    context.Posts.Add(post2);

                    var post3 = new Post()
                    {
                        Title = "Audi Q5",
                        Description = "Beautiful 2019 Audi Q5",
                        Location = "Aberdeen",
                        Price = 56000m,
                        DatePosted = new DateTime(2019, 1, 25, 6, 0, 15),
                        DateExpired = new DateTime(2019, 1, 25, 6, 0, 15).AddDays(14),
                        User = member1,
                        Category = cat1
                    };

                    //add the post3 to the Posts table
                    context.Posts.Add(post3);

                    var post4 = new Post()
                    {
                        Title = "Lhasso Apso",
                        Description = "Beautiful 2 years old Lhasso Apso",
                        Location = "Glasgow",
                        Price = 500m,
                        DatePosted = new DateTime(2019, 3, 5, 8, 0, 15),
                        DateExpired = new DateTime(2019, 3, 5, 8, 0, 15).AddDays(14),
                        User = member2,
                        Category = cat5
                    };

                    //add the post4 to the Posts table
                    context.Posts.Add(post4);

                    var post5 = new Post()
                    {
                        Title = "Mercedes Benz A180",
                        Description = "Beautiful 2018 Mercedes Benz class A180",
                        Location = "Edinbrugh",
                        Price = 34000m,
                        DatePosted = new DateTime(2019, 4, 5, 5, 0, 15),
                        DateExpired = new DateTime(2019, 4, 5, 5, 0, 15).AddDays(14),
                        User = member2,
                        Category = cat1
                    };

                    //add the post5 to the Posts table
                    context.Posts.Add(post5);

                    var post6 = new Post()
                    {
                        Title = "Hyunday Tucson",
                        Description = "Beautiful 2017 Hyunday 5Dr",
                        Location = "Edinbrugh",
                        Price = 14000m,
                        DatePosted = new DateTime(2018, 5, 25, 8, 0, 15),
                        DateExpired = new DateTime(2018, 5, 25, 8, 0, 15).AddDays(14),
                        User = member2,
                        Category = cat1
                    };

                    //add the post6 to the Posts table
                    context.Posts.Add(post6);

                    //save the changes to the database
                    context.SaveChanges();

                }//end if
            }//end if  
        }//end seed method
    }//end class
}//end namespace