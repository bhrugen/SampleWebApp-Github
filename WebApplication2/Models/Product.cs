﻿using Microsoft.CodeAnalysis.Elfie.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace WebApplication2.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal PriceTotal { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Weight { get; set; }
        
        //function to validate regex for email

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        //public static string FindImage(string image)
        //{
        //    //ask this in copilot rewrite this function to return true if image contains "jpg" or "png" using regex
        //    return image;
        //}


        public static bool FindImage(string image)
        {
            Regex regex = new Regex(@"\.(jpg|png)$");
            return regex.IsMatch(image);
        }

        // function that calculates working business days between two dates
        public int CalculateWorkingDays(DateTime startDate, DateTime endDate)
        {
            int count = 0;
            while (startDate <= endDate)
            {
                if (startDate.DayOfWeek != DayOfWeek.Saturday && startDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    count++;
                }
                startDate = startDate.AddDays(1);
            }
            return count;
        }

        // q: what does LINQ stand for?

        

        //type /explain in copilot


    }
}

// function that calculates days between tow dates here it will show wrong suggestion