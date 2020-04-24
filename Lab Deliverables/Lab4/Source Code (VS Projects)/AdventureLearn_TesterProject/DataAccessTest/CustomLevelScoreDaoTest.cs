using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AdventureLearn_TesterProject.DataAccessTest
{
    [TestFixture]
    class CustomLevelScoreDaoTest
    {
        // input parameters for Multiple-Input test

        private int valid_studentId = 1;
        private int invalid_studentId = -1;

        private int valid_customLevelId = 1;
        private int invalid_customLevelId = -1;

        private int valid_levelScore = 100;
        private int invalid_levelScore = -1;

        private CustomLevelScoreDao testerObj = new CustomLevelScoreDao();

        // Multiple-Input test for CustomLevelScoreDao.InsertCustomLevelScore

        [Test]
        public void InsertCustomLevelScore_Successful() // this test is meant to succeed
        {
            Assert.That(() => testerObj.InsertCustomLevelScore(valid_studentId, valid_customLevelId, valid_levelScore), Throws.Nothing);
        }
        [Test]
        public void InsertCustomLevelScore_NotSuccessful_1() // this test is meant to fail
        {
            Assert.That(() => testerObj.InsertCustomLevelScore(invalid_studentId, valid_customLevelId, valid_levelScore), Throws.Nothing);
        }
        [Test]
        public void InsertCustomLevelScore_NotSuccessful_2() // this test is meant to fail
        {
            Assert.That(() => testerObj.InsertCustomLevelScore(valid_studentId, invalid_customLevelId, valid_levelScore), Throws.Nothing);
        }
        [Test]
        public void InsertCustomLevelScore_NotSuccessful_3() // this test is meant to fail
        {
            Assert.That(() => testerObj.InsertCustomLevelScore(valid_studentId, valid_customLevelId, invalid_levelScore), Throws.Nothing);
        }
    }
}
