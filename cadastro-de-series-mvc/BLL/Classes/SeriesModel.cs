using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Enum;

namespace BLL.Classes
{
    public class SeriesModel : BaseEntity
    {
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
    }
}
