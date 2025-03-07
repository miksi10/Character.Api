﻿using CharacterApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CharacterApi.DbContext
{
    public class SeedData
    {
        public static void EnsureSeedData(WebApplication app)
        {
            try
            {
                using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<CharacterDbContext>();

                    if (context != null)
                    {
                        context.Database.Migrate();
                    }

                    var character = new Character()
                    {
                        Name = "firstCharacter",
                        Health = 2,
                        Mana = 2,
                        BaseStrength = 2,
                        BaseAgility = 2,
                        BaseIntelligence = 2,
                        BaseFaith = 2,
                        Class = new Class() { Name = "Warrior", Description = "Warrior class"},
                        Items = new List<Item>() {
                            new Item()
                            {
                                Name = "Strength boost",
                                Description = "Item for additional strength",
                                BonusStrength = 5,
                                BonusAgility = 0,
                                BonusFaith = 0,
                                BonusIntelligence = 0,
                            }
                        },
                        CreatedBy = "5af697c9-ad4f-4658-b5c9-207173613dc8"
                    };

                    context.Character.Add(character);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
