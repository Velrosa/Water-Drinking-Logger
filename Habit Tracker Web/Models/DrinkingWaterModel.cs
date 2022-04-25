﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Habit_Tracker_Web.Models
{
    public class DrinkingWaterModel
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yy}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "Value for {0} must be positive.")]
        public int Quantity { get; set; }
    }
}
