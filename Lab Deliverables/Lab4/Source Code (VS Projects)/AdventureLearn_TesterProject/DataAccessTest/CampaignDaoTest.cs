using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AdventureLearn_TesterProject.DataAccessTest
{
    [TestFixture]
    class CampaignDaoTest
    {
        // input parameters for Multiple-Input test

        private int valid_world_id = 1;
        private int invalid_world_id = -1;

        private int valid_section_id = 1;
        private int invalid_section_id = -1;

        private int valid_level_id = 1;
        private int invalid_level_id = -1;

        private CampaignDao testerObj = new CampaignDao();

        // Multiple-Input test for CampaignDao.GetLevel

        [Test]
        public void GetLevel_Successful() // this test is meant to succeed
        {
            Level result = new Level();
            result = testerObj.GetLevel(valid_world_id, valid_section_id, valid_level_id);
            Assert.That(result, Is.TypeOf(typeof(Level)));
        }
        [Test]
        public void GetLevel_NotSuccessful_1() // this test is meant to fail
        {
            Level result = new Level();
            Assert.That(result = testerObj.GetLevel(invalid_world_id, valid_section_id, valid_level_id), Throws.Nothing);
        }
        [Test]
        public void GetLevel_NotSuccessful_2() // this test is meant to fail
        {
            Level result = new Level();
            Assert.That(result = testerObj.GetLevel(valid_world_id, invalid_section_id, valid_level_id), Throws.Nothing);
        }
        [Test]
        public void GetLevel_NotSuccessful_3() // this test is meant to fail
        {
            Level result = new Level();
            Assert.That(result = testerObj.GetLevel(valid_world_id, valid_section_id, invalid_level_id), Throws.Nothing);
        }
    }
}
