using System;
using System.ComponentModel.DataAnnotations;

namespace ReceiptsAPI.Models
{
	public class Receipt
    {

        [Required]
        [RegularExpression(@"^[\w\s\-\&]+$", ErrorMessage = "Retailer name is not valid.")]
        public string? retailer { get; set; }

        [Required]
        public string? purchaseDate { get; set; }

        [Required]
        public string? purchaseTime { get; set; }

        [Required]
        [RegularExpression(@"^\d+\.\d{2}$", ErrorMessage = "Total price is not valid.")]
        public string? total { get; set; }

        [Required]
        public List<item>? items { get; set; }

        public string? id { get; set; }

        public int points { get; set; }



        public int GetPoints()
        {

            points += CheckRetailerName(); // Check Retailer Name
            points += CheckRoundDollar(); // Check Whole Dollar
            points += CheckMultipleQuarter(); //Check if total is a multiple of 25
            points += CheckItems(); //Check items for required metrics
            points += CheckDateTime(); //Check date time metrics


            return points;
        }

        public int CheckRetailerName()
        {
            int NamePoints = 0;

            if(retailer == null || retailer == string.Empty)
            {

                return 0;
            }

            foreach(char c in retailer)
            {

                if (char.IsLetterOrDigit(c))
                {
                    NamePoints += 1;


                }
            }

            return NamePoints;
        }


        public int CheckRoundDollar()
        {
            
            if (decimal.TryParse(total, out decimal amount))
            {
                bool isWholeDollar = amount % 1 == 0;

                if (isWholeDollar)
                {
                    return 50;
                }
                
            }

            return 0;
        }


        public int CheckMultipleQuarter()
        {

            if (decimal.TryParse(total, out decimal amount))
            {
                bool isMultipleOfQuarter = amount % 0.25m == 0;

                if (isMultipleOfQuarter)
                {
                    return 25;
                }
            }

            return 0;
        }

        public int CheckItems()
        {

            if(items == null || items.Count == 0)
            {

                return 0;
            }

            int ItemPoints = 0;
            int ItemCount = 0;


            foreach(var item in items)
            {
                ItemCount += 1;

                if(item.shortDescription?.Trim().Length % 3 == 0 && decimal.TryParse(item.price, out decimal itemPrice))
                {

                    ItemPoints += (int)Math.Ceiling(itemPrice * 0.2m);
                }

            }


            ItemPoints += (ItemCount / 2) * 5;
            return ItemPoints;

        }

        public int CheckDateTime()
        {
            int ItemPoints = 0;

            if (DateTime.TryParse(purchaseDate, out DateTime parsedDate))
            {
                bool isOddDay = parsedDate.Day % 2 != 0;
                if (isOddDay)
                {

                    ItemPoints += 6;
                }
            }


            if (TimeSpan.TryParse(purchaseTime, out TimeSpan time))
            {
                var start = new TimeSpan(14, 0, 0); // 2:00 PM
                var end = new TimeSpan(16, 0, 0);   // 4:00 PM

                bool isWithinRange = time > start && time < end;

                if (isWithinRange)
                {

                    ItemPoints += 10;
                }
            }

            return ItemPoints;
        }
    }

    
    public class item
    {
        [Required]
        [RegularExpression(@"^[\w\s\-]+$", ErrorMessage = "Item Description is not valid.")]
        public string? shortDescription { get; set; }

        [Required]
        [RegularExpression(@"^\d+\.\d{2}$", ErrorMessage = "Price for item is not valid.")]
        public string? price { get; set; }


    }


  
}

