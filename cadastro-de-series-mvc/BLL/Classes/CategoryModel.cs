using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Enum;

namespace BLL.Classes
{
    public class CategoryModel : BaseEntity
    {
        public Genre Genre { get; set; }
        public IEnumerable<SeriesModel>? Series { get; set; }
    }
}
