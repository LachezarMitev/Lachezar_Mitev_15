using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Layer;
using Microsoft.EntityFrameworkCore;

namespace Data_Layer
{
    public class InterestContext : IDb<Interest, int>
    {
        private readonly BaronaDBContext dbContext;

        public InterestContext(BaronaDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(Interest item)
        {
            try
            {
                dbContext.Interests.Add(item);
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
                Interest interestFromDb = Read(key);

                if (interestFromDb != null)
                {
                    dbContext.Interests.Remove(interestFromDb);
                    dbContext.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public Interest Read(int key, bool useNavigationalProperties = false)
        {
            try
            {
                if (useNavigationalProperties)
                {
                    return dbContext.Interests.Include(b => b.Users).Include(b => b.Regions).FirstOrDefault(b => b.ID == key);
                }
                else
                {
                    return dbContext.Interests.Find(key);
                }
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Interest> ReadAll(bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Interest> query = dbContext.Interests;

                if (useNavigationalProperties)
                {
                    query = query.Include(b => b.Users).Include(b => b.Regions);
                }

                return query.ToList();
            }
            catch
            {
                throw;
            }
        }

        public void Update(Interest item, bool useNavigationalProperties = false)
        {
            try
            {
                Interest interestFromDb = Read(item.ID, useNavigationalProperties);

                if (interestFromDb == null)
                {
                    Create(item);
                    return;
                }

                interestFromDb.ID = item.ID;
                interestFromDb.Name = item.Name;

                if (useNavigationalProperties)
                {
                    List<User> users = new List<User>();
                    List<Region> regions = new List<Region>();

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
                    foreach (Region p in item.Regions)
                    {
                        Region regionFromDb = dbContext.Regions.Find(p.ID);

                        if (regionFromDb != null)
                        {
                            regions.Add(regionFromDb);
                        }
                        else
                        {
                            regions.Add(p);
                        }

                    }

                    interestFromDb.Users = users;
                    interestFromDb.Regions = regions;
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
