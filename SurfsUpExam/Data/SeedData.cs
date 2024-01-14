using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SurfsUpExam.Models;

namespace SurfsUpExam.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SurfsUpExamContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SurfsUpExamContext>>()))

            { 
                if(context.SurfsBoard.Any())
                {
                    return;
                }

                context.SurfsBoard.Add(
                    new SurfsBoard
                    {
                        Name = "The Minilog",
                        Length = 6,
                        Width = 21,
                        Thickness = 2.75,
                        Volume = 38.8,
                        BoardType = "Shortboard",
                        Price = 565,
                        Equipment = null,
                    });

                context.SurfsBoard.Add(
                    new SurfsBoard
                    {
                        Name = "Naish Maliko",
                        Length = 14,
                        Width = 25,
                        Thickness = 6,
                        Volume = 330,
                        BoardType = "SUP",
                        Price = 1304,
                        Equipment = "Fin, Paddle, Pump, Leash",
                    });
                context.SaveChanges();
            }
        }
    }
}
