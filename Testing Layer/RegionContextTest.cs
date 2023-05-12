using Business_Layer;
using Data_Layer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_Layer
{
    [SetUpFixture]
    public class RegionContextTest
    {
        private RegionContext context = new(SetupFixture.dbContext);
        private Region region;
        private User user;
        private Interest interest;

        [SetUp]
        public void Setup()
        {
            region = new(1111, "ColetoLand");

            user = new(1212, "Georgi", "Colov", 18, "Colaja", "Kokti", "coleto@coleto.com", 1111);
            interest = new(2222, "kotki");

            region.Users.Add(user);
            region.Interests.Add(interest);

            context.Create(region);
        }

        [TearDown]
        public void DropCustomer()
        {
            foreach (Region item in SetupFixture.dbContext.Regions.ToList())
            {
                SetupFixture.dbContext.Regions.Remove(item);
            }

            SetupFixture.dbContext.SaveChanges();
        }


        [Test]
        public void Create()
        {
            Region testRegion = new(1234, "topLane");

            int regionsBefore = SetupFixture.dbContext.Regions.Count();

            context.Create(testRegion);

            int regionsAfter = SetupFixture.dbContext.Regions.Count();
            Assert.That(regionsBefore + 1 == regionsAfter, "Create() does not work!");
        }

        [Test]
        public void Read()
        {
            Region readRegion = context.Read(region.ID);

            Assert.AreEqual(region, readRegion, "Read does not return the same object!");
        }

        [Test]
        public void ReadWithNavigationalProperties()
        {
            Region readRegion = context.Read(region.ID);
            Assert.That(readRegion.Users.Contains(user)
                && readRegion.Interests.Contains(interest),
                "user and interest are not in the Users and Interests lists!");
        }

        [Test]
        public void ReadAll()
        {
            List<Region> regions = (List<Region>)context.ReadAll();

            Assert.That(regions.Count != 0, "ReadAll() does not work!");
        }

        [Test]
        public void ReadAllWithNavigationalProperties()
        {
            Region readRegion = new(2468, "botLane");
            User user = new(7890, "Pesho", "Baronski", 23, "Barona", "kotkite", "barona@barona.com", readRegion.ID);
            SetupFixture.dbContext.Users.Add(user);
            SetupFixture.dbContext.Regions.Add(readRegion);
            context.Create(readRegion);

            List<Region> regions = (List<Region>)context.ReadAll();

            Assert.That(regions.Count != 0 && context.Read(readRegion.ID).Users.Count == 1, "ReadAll() does not return regions!");
        }

        [Test]
        public void Update()
        {
            Region changedRegion = context.Read(region.ID);

            changedRegion.Name = "Coleto";

            context.Update(changedRegion);

            Assert.AreEqual(changedRegion, region, "Update() does not work!");
        }

        [Test]
        public void Delete()
        {
            int cusotmersBefore = SetupFixture.dbContext.Regions.Count();

            context.Delete(region.ID);
            int salonsAfter = SetupFixture.dbContext.Regions.Count();

            Assert.IsTrue(cusotmersBefore - 1 == salonsAfter, "Delete() does not work!");
        }
    }
}
