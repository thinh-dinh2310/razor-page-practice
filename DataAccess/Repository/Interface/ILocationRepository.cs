using BusinessObject;
using BusinessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface ILocationRepository
    {
        public Location GetLocationById(Guid id);
        void CreateLocation(Location Location);
        void DeleteLocationById(Guid id);
        Task<PaginationResult<Location>> GetAllLocation(int limit, int offset, string keywords);
        Task<Location> UpdateLocation(Location location);
    }
}
