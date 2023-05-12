using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Layer;
using Microsoft.EntityFrameworkCore;

namespace Data_Layer
{
    public class RegionContext : IDb<Region, int>
    {
        private readonly BaronaDBContext dbContext;

        public RegionContext(BaronaDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(Region item)
        {
            try
            {
                dbContext.Regions.Add(item);
                dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void Delete(int key)
        {
            try
            {
                Region regionFromDb = Read(key);

                if (regionFromDb != null)
                {
                    dbContext.Regions.Remove(regionFromDb);
                    dbContext.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public Region Read(int key, bool useNavigationalProperties = false)
        {
            try
            {
                if (useNavigationalProperties)
                {
                    return dbContext.Regions.Include(b => b.Users).Include(b => b.Interests).FirstOrDefault(b => b.ID == key);
                }
                else
                {
                    return dbContext.Regions.Find(key);
                }
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Region> ReadAll(bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Region> query = dbContext.Regions;

                if (useNavigationalProperties)
                {
                    query = query.Include(b => b.Users).Include(b => b.Interests);
                }

                return query.ToList();
            }
            catch
            {
                throw;
            }
        }

        public void Update(Region item, bool useNavigationalProperties = false)
        {
            try
            {
                Region regionFromDb = Read(item.ID, useNavigationalProperties);

                if (regionFromDb == null)
                {
                    Create(item);
                    return;
                }

                regionFromDb.ID = item.ID;
                regionFromDb.Name = item.Name;

                if (useNavigationalProperties)
                {
                    List<User> users = new List<User>();
                    List<Interest> interests = new List<Interest>();

                    foreach (User p in item.Users)
                    {
                        User userFromDb = dbContext.Users.Find(p.ID);

                        if (userFromDb != null)
                        {
                            users.Add(userFromDb);
                        }
                        else
                        {
                            users.Add(p);
                        }

                    }
                    foreach (Interest p in item.Interests)
                    {
                        Interest interestFromDb = dbContext.Interests.Find(p.ID);

                        if (interestFromDb != null)
                        {
                            interests.Add(interestFromDb);
                        }
                        else
                        {
                            interests.Add(p);
                        }

                    }

                    regionFromDb.Users = users;
                    regionFromDb.Interests = interests;
                }

                dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
