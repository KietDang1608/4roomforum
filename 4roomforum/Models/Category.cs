    using System;
    using System.ComponentModel.DataAnnotations;

    namespace _4roomforum.Models;

    public class Category
    {

       
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateOnly CreatedDate { get; set; }

    }
