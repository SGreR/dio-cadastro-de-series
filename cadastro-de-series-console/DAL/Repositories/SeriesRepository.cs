using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Classes;
using BLL.Interfaces;

namespace DAL.Repositories
{
    public class SeriesRepository : IRepository<Series>
    {
        private List<Series> _seriesList = new List<Series>();
        public void Delete(int id)
        {
            _seriesList[id].Delete();
        }

        public void Insert(Series entity)
        {
            _seriesList.Add(entity);
        }

        public List<Series> GetAll()
        {
            return _seriesList;
        }

        public int NextId()
        {
            return _seriesList.Count;
        }

        public Series GetById(int id)
        {
            return _seriesList[id];
        }

        public void Update(int id, Series entity)
        {
            _seriesList[id] = entity;
        }

        public List<Series> GetByText(string searchString)
        {
            var matches = new List<Series>();
            foreach (Series series in _seriesList)
            {
                matches = _seriesList.FindAll(s => s.GetTitle().ToUpper().Contains(searchString) ||
                                                s.GetDescription().ToUpper().Contains(searchString) ||
                                                s.GetYear().ToUpper().Contains(searchString));
            }
            return matches;
        }
    }
}
