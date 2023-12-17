using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace K_Burns_SpitTree.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public string Name { get; set; }

        //Navigational Property Between POST & CATEGORY- ONE TO MANY
        public List<Post> Posts { get; set; } //MANY
    }
}