using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AdventureLearn_TesterProject.DataAccessTest
{
    [TestFixture]
    class StudentScoreDaoTest
    {
        // input parameters for Multiple-Input tests

        private int valid_worldId = 1;
        private int invalid_worldId = -1;

        private int valid_studentId = 1;
        private int invalid_studentId = -1;

        private int valid_levelId = 1;
        private int invalid_levelId = -1;

        private int valid_sectionId = 1;
        private int invalid_sectionId = -1;

        private int valid_levelScore = 100;
        private int invalid_levelScore = -1;

        private StudentScoreDao testerObj = new StudentScoreDao();

        // Multiple-Input test for StudentScoreDao.GetStudentScore

        [Test]
        public void GetStudentScore_Successful() // this test is meant to succeed
        {
            Student result = new Student();
            result = testerObj.GetStudentScores(valid_worldId, valid_sectionId, valid_studentId);
            Assert.That(result, Is.TypeOf(typeof(Student)));
        }
        [Test]
        public void GetStudentScore_NotSuccessful_1() // this test is meant to fail
        {
            Student result = new Student();
            Assert.That(result = testerObj.GetStudentScores(invalid_worldId, valid_sectionId, valid_studentId), Throws.Nothing);
        }
        [Test]
        public void GetStudentScore_NotSuccessful_2() // this test is meant to fail
        {
            Student result = new Student();
            Assert.That(result = testerObj.GetStudentScores(valid_worldId, invalid_sectionId, valid_studentId), Throws.Nothing);
        }
        [Test]
        public void GetStudentScore_NotSuccessful_3() // this test is meant to fail
        {
            Student result = new Student();
            Assert.That(result = testerObj.GetStudentScores(valid_worldId, valid_sectionId, invalid_studentId), Throws.Nothing);
        }

        // EC test for StudentScoreDao.GetAvgWorldScores

        [Test]
        public void GetAvgWorldScores_Successful() // test is meant to succeed
        {
            StudentScore result = new StudentScore();
            result = testerObj.GetAvgWorldScores(valid_studentId);
            Assert.That(result, Is.TypeOf(typeof(StudentScore)));
        }
        [Test]
        public void GetAvgWorldScores_NotSuccessful() // test is meant to fail
        {
            StudentScore result = new StudentScore();
            Assert.That(result = testerObj.GetAvgWorldScores(invalid_studentId), Throws.Nothing);
        }

        // Multiple-Input test for StudentScoreDao.InsertStudentScore

        [Test]
        public void InsertStudentScore_Successful() // test is meant to succeed
        {
            int result;
            result = testerObj.InsertStudentScore(valid_studentId, valid_worldId, valid_sectionId, valid_levelId, valid_levelScore);
            Assert.That(result, Is.EqualTo(1));
        }
        [Test]
        public void InsertStudentScore_NotSuccessful_1() // test is meant to fail
        {
            int result;
            Assert.That(result = testerObj.InsertStudentScore(invalid_studentId, valid_worldId, valid_sectionId, valid_levelId, valid_levelScore), Throws.Nothing);
        }
        public void InsertStudentScore_NotSuccessful_2() // test is meant to fail
        {
            int result;
            Assert.That(result = testerObj.InsertStudentScore(valid_studentId, invalid_worldId, valid_sectionId, valid_levelId, valid_levelScore), Throws.Nothing);
        }
        public void InsertStudentScore_NotSuccessful_3() // test is meant to fail
        {
            int result;
            Assert.That(result = testerObj.InsertStudentScore(valid_studentId, valid_worldId, invalid_sectionId, valid_levelId, valid_levelScore), Throws.Nothing);
        }
        public void InsertStudentScore_NotSuccessful_4() // test is meant to fail
        {
            int result;
            Assert.That(result = testerObj.InsertStudentScore(valid_studentId, valid_worldId, valid_sectionId, invalid_levelId, valid_levelScore), Throws.Nothing);
        }
        public void InsertStudentScore_NotSuccessful_5() // test is meant to fail
        {
            int result;
            Assert.That(result = testerObj.InsertStudentScore(valid_studentId, valid_worldId, valid_sectionId, valid_levelId, invalid_levelScore), Throws.Nothing);
        }

        // EC test for StudentScoreDao.GetCampaignRanking

        [Test]
        public void GetCampaignRanking_Successful() // test is meant to succeed
        {
            int result;
            result = testerObj.GetCampaignRanking(valid_studentId);
            Assert.That(result, Is.GreaterThan(0));
        }
        [Test]
        public void GetCampaignRanking_NotSuccessful() // test is meant to fail
        {
            int result;
            Assert.That(result = testerObj.GetCampaignRanking(invalid_studentId), Throws.Nothing);
        }
    }
}
