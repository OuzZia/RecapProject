using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTO_s
{
    public class RentalDetailDto:IDto
    {
        public int Id { get; set; }
        public DateTime? RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }


    }
}
