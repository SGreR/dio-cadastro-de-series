using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BLL.Classes;

namespace Presentation.Data
{
    public class SeriesCatalogueContext : DbContext
    {
        public DbSet<SeriesModel> SeriesModel { get; set; }
        public DbSet<CategoryModel> CategoryModel { get; set; }

        public SeriesCatalogueContext (DbContextOptions<SeriesCatalogueContext> options)
            : base(options)
        {
            
        }
    }
}
