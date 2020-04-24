using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AdventureLearn_TesterProject.DataAccessTest
{
    [TestFixture]
    class CustomLevelDaoTest
    {
        // input parameters for BVT

        private int valid_customLevelId = 1;
        private int invalid_customLevelId_1 = 0;
        private int invalid_customLevelId_2 = -1;

        private int valid_studentId = 1;
        private int invalid_studentId_1 = 0;
        private int invalid_studentId_2 = -1;

        private CustomLevelDao testerObj = new CustomLevelDao();

        // BVT for CustomLevelDao.GetCustomLevel

        [Test]
        public void GetCustomLevel_Successful() // this test is meant to succeed
        {
            CustomLevel result = new CustomLevel();
            result = testerObj.GetCustomLevel(valid_customLevelId);
            Assert.That(result, Is.TypeOf(typeof(CustomLevel)));
        }
        [Test]
        public void GetCustomLevel_NotSuccessful_1() // this test is meant to fail
        {
            CustomLevel result = new CustomLevel();
            Assert.That(result = testerObj.GetCustomLevel(invalid_customLevelId_1), Throws.Nothing);
        }
        [Test]
        public void GetCustomLevel_NotSuccessful_2() // this test is meant to fail
        {
            CustomLevel result = new CustomLevel();
            Assert.That(result = testerObj.GetCustomLevel(invalid_customLevelId_2), Throws.Nothing);
        }

        // BVT for CustomLevelDao.GetClearedCustomLevels

        [Test]
        public void GetClearedCustomLevels_Successful() // this test is meant to succeed
        {
            List<CustomLevelScore> result = new List<CustomLevelScore>();
            result = testerObj.GetClearedCustomLevels(valid_studentId);
            Assert.That(result, Is.TypeOf(typeof(List<CustomLevelScore>)));
        }
        [Test]
        public void GetClearedCustomLevels_NotSuccessful_1() // this test is meant to fail
        {
            List<CustomLevelScore> result = new List<CustomLevelScore>();
            Assert.That(result = testerObj.GetClearedCustomLevels(invalid_customLevelId_1), Throws.Nothing);
        }
        [Test]
        public void GetClearedCustomLevels_NotSuccessful_2() // this test is meant to fail
        {
            List<CustomLevelScore> result = new List<CustomLevelScore>();
            Assert.That(result = testerObj.GetClearedCustomLevels(invalid_customLevelId_2), Throws.Nothing);
        }

        // BVT for CustomLevelDao.GetStudentCustomLevels

        [Test]
        public void GetStudentCustomLevels_Successful() // this test is meant to succeed
        {
            List<CustomLevel> result = new List<CustomLevel>();
            result = testerObj.GetStudentCustomLevel(valid_studentId);
            Assert.That(result, Is.TypeOf(typeof(List<CustomLevel>)));
        }
        [Test]
        public void GetStudentCustomLevels_NotSuccessful_1() // this test is meant to fail
        {
            List<CustomLevel> result = new List<CustomLevel>();
            Assert.That(result = testerObj.GetStudentCustomLevel(invalid_customLevelId_1), Throws.Nothing);
        }
        [Test]
        public void GetStudentCustomLevels_NotSuccessful_2() // this test is meant to fail
        {
            List<CustomLevel> result = new List<CustomLevel>();
            Assert.That(result = testerObj.GetStudentCustomLevel(invalid_customLevelId_2), Throws.Nothing);
        }

        // BVT for CustomLevelDao.DeleteCustomLevel

        [Test]
        public void DeleteCustomLevel_Successful() // this test is meant to succeed
        {
            int result;
            Assert.That(result = testerObj.DeleteCustomLevel(valid_customLevelId), Throws.Nothing);            
        }
        [Test]
        public void DeleteCustomLevel_NotSuccessful_1() // this test is meant to fail
        {
            int result;
            Assert.That(result = testerObj.DeleteCustomLevel(invalid_customLevelId_1), Throws.Nothing);
        }
        [Test]
        public void DeleteCustomLevel_NotSuccessful_2() // this test is meant to fail
        {
            int result;
            Assert.That(result = testerObj.DeleteCustomLevel(invalid_customLevelId_2), Throws.Nothing);
        }

        // BVT for CustomLevelDao.GetCustomLevelMonster

        [Test]
        public void GetCustomLevelMonster_Successful() // this test is meant to succeed
        {
            Monster result = new Monster();
            Assert.That(result = testerObj.GetCustomLevelMonster(valid_customLevelId), Throws.Nothing);
        }
        public void GetCustomLevelMonster_NotSuccessful_1() // this test is meant to fail
        {
            Monster result = new Monster();
            Assert.That(result = testerObj.GetCustomLevelMonster(invalid_customLevelId_1), Throws.Nothing);
        }
        public void GetCustomLevelMonster_NotSuccessful_2() // this test is meant to fail
        {
            Monster result = new Monster();
            Assert.That(result = testerObj.GetCustomLevelMonster(invalid_customLevelId_2), Throws.Nothing);
        }
    }
}
