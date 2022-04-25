using Habit_Tracker_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Habit_Tracker_Web.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {

        }

        [BindProperty]
        public DrinkingWaterModel DrinkingWater { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var con = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO drinking_water(date, quantity) 
                                        VALUES(@date, @quantity)";
                    cmd.Parameters.AddWithValue("@date", DrinkingWater.Date);
                    cmd.Parameters.AddWithValue("@quantity", DrinkingWater.Quantity);
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
