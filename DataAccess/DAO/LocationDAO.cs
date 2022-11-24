using BusinessObject;
using BusinessObject.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class LocationDAO
    {
        private static LocationDAO instance = null;
        private static readonly object instanceLock = new object();
        private LocationDAO() { }

        public static LocationDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new LocationDAO();
                    }
                }
                return instance;
            }
        }

        public Location GetLocationByID(Guid id)
        {
            Location Location = null;
            try
            {
                var context = new eRecruitment_PRN221Context();
                Location = context.Locations.FirstOrDefault(item => item.LocationId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at GetLocationById: " + ex.Message);
            }
            return Location;
        }

        public async void CreateLocation(string LocationName)
        {
            try
            {

                Location Location = new Location()
                {
                    LocationId = Guid.NewGuid(),
                    LocationName = LocationName
                };
                var context = new eRecruitment_PRN221Context();
                await context.Locations.AddAsync(Location);
                if (context.SaveChanges() > 0)
                {
                    Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Created Location successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at CreateLocation: " + ex.Message);
            }
        }
        public async void DeleteLocationById(Guid id)
        {
            try
            {
                var context = new eRecruitment_PRN221Context();
                Location Location = context.Locations.FirstOrDefault(u => u.LocationId.Equals(id));
                context.Locations.Remove(Location);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at DeleteLocation: " + ex.Message);
            }
        }

        public async Task<PaginationResult<Location>> GetAllLocations(int limit = 0, int offset = 0, string keywords = "")
        {
            PaginationResult<Location> response = new PaginationResult<Location>();
            List<Location> list = new List<Location>();
            try
            {
                var context = new eRecruitment_PRN221Context();
                list = await context.Locations.Where(k => k.LocationName.ToLower().Contains(keywords))
                        .Skip(offset * limit)
                        .Take(limit)
                        .ToListAsync();
                response.limit = limit;
                response.offset = offset;
                response.totalInPage = list.Count();
                response.totalItems = context.Locations.Where(k => k.LocationName.ToLower().Contains(keywords)).Count();
                response.data = list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at GetAllLocation: " + ex.Message);
            }
            return response;
        }

        public async Task<Location> UpdateLocation(Location updateLocation)
        {
            Location tmp = new Location();
            try
            {
                var context = new eRecruitment_PRN221Context();
                tmp = await context.Locations.AsNoTracking().FirstOrDefaultAsync(u => u.LocationId == updateLocation.LocationId);
                context.Update(updateLocation);
                await context.SaveChangesAsync();
                return updateLocation;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "Error at UpdateUser: " + ex.Message);
                return tmp;
            }
        }
    }
}
