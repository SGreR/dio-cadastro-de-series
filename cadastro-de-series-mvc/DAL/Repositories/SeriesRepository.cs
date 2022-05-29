using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Classes;
using BLL.Interfaces;

namespace DAL.Repositories
{
    public class SeriesRepository : IRepository<SeriesModel>
    {
        private List<SeriesModel> _seriesList = new List<SeriesModel>();
        public void Delete(int id)
        {
            return;
        }

        public void Insert(SeriesModel entity)
        {
            _seriesList.Add(entity);
        }

        public List<SeriesModel> GetAll()
        {
            return _seriesList;
        }

        public int NextId()
        {
            return _seriesList.Count;
        }

        public SeriesModel GetById(int id)
        {
            return _seriesList[id];
        }

        public void Update(int id, SeriesModel entity)
        {
            _seriesList[id] = entity;
        }

        public List<SeriesModel> GetByText(string searchString)
        {
            var matches = new List<SeriesModel>();
            foreach (SeriesModel series in _seriesList)
            {
                matches = _seriesList.FindAll(s => s.Title.ToUpper().Contains(searchString) ||
                                                s.Description.ToUpper().Contains(searchString) ||
                                                s.Year.ToString().ToUpper().Contains(searchString));
            }
            return matches;
        }
    }
}
