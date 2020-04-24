using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AdventureLearn_TesterProject.DataAccessTest
{
    [TestFixture]
    class WorldDaoTest
    {
        // input parameters for testing

        private WorldDao testerObj = new WorldDao();

        // simple test for WorldDao.GetWorlds

        [Test]
        public void GetWorlds_Successful() // this test is meant to succeed
        {
            List<World> result = new List<World>();
            result = testerObj.GetWorlds();
            Assert.That(result, Is.TypeOf(typeof(List<World>)));
        }

        // simple test for WorldDao.GetCompletedWorldCount

        [Test]
        public void GetCompletedWorldCount_Successful() // this test is meant to succeed
        {
            int result;
            Assert.That(result = testerObj.GetCompletedWorldCount(), Throws.Nothing);
        }
    }
}
