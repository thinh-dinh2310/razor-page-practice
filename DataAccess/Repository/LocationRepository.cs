using BusinessObject;
using BusinessObject.DTO;
using DataAccess.DAO;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class LocationRepository:ILocationRepository
    {
        public Location GetLocationById(Guid id)
        {
            return LocationDAO.Instance.GetLocationByID(id);
        }
        public Task<PaginationResult<Location>> GetAllLocation(int limit, int offset, string keywords) => LocationDAO.Instance.GetAllLocations(limit, offset, keywords);


        public void DeleteLocationById(Guid id)
        {
            LocationDAO.Instance.DeleteLocationById(id);
        }

        public void CreateLocation(Location Location)
        {
            LocationDAO.Instance.CreateLocation(Location.LocationName);
        }

        public Task<Location> UpdateLocation(Location location) => LocationDAO.Instance.UpdateLocation(location);
    }
}
