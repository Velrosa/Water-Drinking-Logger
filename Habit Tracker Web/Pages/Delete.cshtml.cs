using Habit_Tracker_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;

namespace Habit_Tracker_Web.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public DrinkingWaterModel DrinkingWater { get; set; }

        public DeleteModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public IActionResult OnGet(int id)
        {
            DrinkingWater = GetById(id);

            return Page();
        }

        private DrinkingWaterModel GetById(int id)
        {
            var drinkingWaterRecord = new DrinkingWaterModel();

            using(var con = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                con.Open();
                using(var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM drinking_water
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("id", id);

                    using(var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            drinkingWaterRecord.Id = reader.GetInt32(0);
                            drinkingWaterRecord.Date = DateTime.Parse(reader.GetString(1),
                                CultureInfo.CurrentUICulture.DateTimeFormat);
                            drinkingWaterRecord.Quantity = reader.GetInt32(2);
                        }

                        return drinkingWaterRecord;
                    }
                }
            }
        }

        public IActionResult OnPost(int id)
        {
            using(var con = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                con.Open();
                using(var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"DELETE from drinking_water WHERE Id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                }

                return RedirectToPage("./Index");
            }
        }
    }
}
