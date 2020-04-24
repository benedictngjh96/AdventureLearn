using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AdventureLearn_TesterProject.DataAccessTest
{
    [TestFixture]
    class SectionDaoTest
    {
        // input parameters for BVT and Multiple-Input test

        private int valid_worldId = 1;
        private int invalid_worldId_1 = 0;
        private int invalid_worldId_2 = -1;

        private int valid_sectionId = 1;
        private int invalid_sectionId = -1;

        private int valid_levelId = 1;
        private int invalid_levelId = -1;

        private int valid_studentId = 1;
        private int invalid_studentId = -1;

        private SectionDao testerObj = new SectionDao();

        // BVT for SectionDao.GetWorldSections

        [Test]
        public void GetWorldSections_Successful() // this test is meant to succeed
        {
            List<Section> result = new List<Section>();
            result = testerObj.GetWorldSections(valid_worldId);
            Assert.That(result, Is.TypeOf(typeof(List<Section>)));
        }
        [Test]
        public void GetWorldSections_NotSuccessful_1() // this test is meant to fail
        {
            List<Section> result = new List<Section>();
            Assert.That(result = testerObj.GetWorldSections(invalid_worldId_1), Throws.Nothing);
        }
        [Test]
        public void GetWorldSections_NotSuccessful_2() // this test is meant to fail
        {
            List<Section> result = new List<Section>();
            Assert.That(result = testerObj.GetWorldSections(invalid_worldId_2), Throws.Nothing);
        }

        // Multiple-Input test for SectionDao.GetSectionLevels

        [Test]
        public void GetSectionLevels_Successful() // this test is meant to succeed
        {
            Section result = new Section();
            Assert.That(result = testerObj.GetSectionLevels(valid_worldId, valid_sectionId), Throws.Nothing);
        }
        [Test]
        public void GetSectionLevels_NotSuccessful_1() // this test is meant to fail
        {
            Section result = new Section();
            Assert.That(result = testerObj.GetSectionLevels(invalid_worldId_1, valid_sectionId), Throws.Nothing);
        }
        [Test]
        public void GetSectionLevels_NotSuccessful_2() // this test is meant to fail
        {
            Section result = new Section();
            Assert.That(result = testerObj.GetSectionLevels(valid_worldId, invalid_sectionId), Throws.Nothing);
        }

        // Multiple-Input test for SectionDao.CheckSectionCleared

        [Test]
        public void CheckSectionCleared_Successful() // this test is meant to succeed
        {
            int result;
            result = testerObj.CheckSectionCleared(valid_worldId, valid_sectionId, valid_studentId);
        }
        [Test]
        public void CheckSectionCleared_NotSuccessful_1() // this test is meant to fail
        {
            int result;
            result = testerObj.CheckSectionCleared(invalid_worldId_1, valid_sectionId, valid_studentId);
        }
        [Test]
        public void CheckSectionCleared_NotSuccessful_2() // this test is meant to fail
        {
            int result;
            result = testerObj.CheckSectionCleared(valid_worldId, invalid_sectionId, valid_studentId);
        }
        [Test]
        public void CheckSectionCleared_NotSuccessful_3() // this test is meant to fail
        {
            int result;
            result = testerObj.CheckSectionCleared(valid_worldId, valid_sectionId, invalid_studentId);
        }
    }
}
