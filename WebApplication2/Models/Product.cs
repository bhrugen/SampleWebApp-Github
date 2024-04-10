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