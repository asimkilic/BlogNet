using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogNet.Data
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedRolesAndUsers(RoleManager<IdentityRole> roleManager,
         UserManager<ApplicationUser> userManager)
        {
            await roleManager.CreateAsync(new IdentityRole("admin"));
            var user = new ApplicationUser()
            {
                Email = "admin@example.com",
                UserName = "admin@example.com",
                EmailConfirmed = true,
            };
            await userManager.CreateAsync(user, "P@ssword1");
            await userManager.AddToRoleAsync(user, "admin");
        }
        public static async Task SeedCategoriesAndPostsAsync(ApplicationDbContext context)
        {
            var author = await context.Users.FirstOrDefaultAsync(x => x.UserName == "admin@example.com");
            if (author is null || await context.Categories.AnyAsync()) return;
            var cat1 = new Category
            {
                Name = "General",
                Posts = new List<Post>
                {
                    new Post
                    {
                        Title="Welcome to My Blog",
                        Content="<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis ornare dapibus tellus, et dignissim erat semper ut. Cras ac justo blandit lacus tincidunt egestas. Donec porta cursus dui, sit amet condimentum nibh tempus in. Cras pharetra nisi augue, vel euismod purus commodo sed. Sed eget tellus scelerisque, vehicula tortor eu, molestie nunc. Donec tellus leo, commodo sed justo pulvinar, scelerisque tempus nunc. Phasellus hendrerit, magna nec facilisis pellentesque, lacus diam malesuada dui, ac elementum velit est a tellus. Duis in justo et nisl congue dignissim id in sapien. Aenean dignissim elit nulla, sit amet euismod nunc vehicula vel.</p>",
                        PhotoPath="sample-photo-1.jpg",
                        Slug="welcome-to-my-blog",
                        Author=author
                    }, new Post
                    {
                        Title="A Sunny Day",
                        Content="<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis ornare dapibus tellus, et dignissim erat semper ut. Cras ac justo blandit lacus tincidunt egestas. Donec porta cursus dui, sit amet condimentum nibh tempus in. Cras pharetra nisi augue, vel euismod purus commodo sed. Sed eget tellus scelerisque, vehicula tortor eu, molestie nunc. Donec tellus leo, commodo sed justo pulvinar, scelerisque tempus nunc. Phasellus hendrerit, magna nec facilisis pellentesque, lacus diam malesuada dui, ac elementum velit est a tellus. Duis in justo et nisl congue dignissim id in sapien. Aenean dignissim elit nulla, sit amet euismod nunc vehicula vel.</p>",
                        PhotoPath="sample-photo-2.jpg",
                        Slug="a-sunny-day",
                        Author=author
                    }
                }
            };
            context.Categories.AddRange(cat1);
            await context.SaveChangesAsync();
        }

    }
}
