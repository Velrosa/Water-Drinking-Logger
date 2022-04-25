using Habit_Tracker_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Habit_Tracker_Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public List<DrinkingWaterModel> Records { get; set; }

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            Records = GetAllRecords();
        }

        private List<DrinkingWaterModel> GetAllRecords()
        {
            using (var con = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM drinking_water";

                    var tableData = new List<DrinkingWaterModel>();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tableData.Add(new DrinkingWaterModel
                            {
                                Id = reader.GetInt32(0),
                                Date = DateTime.Parse(reader.GetString(1),
                                    CultureInfo.CurrentUICulture.DateTimeFormat),
                                Quantity = reader.GetInt32(2),
                            });
                        }

                        return tableData;
                    }
                }
            }
        }
    }
}
