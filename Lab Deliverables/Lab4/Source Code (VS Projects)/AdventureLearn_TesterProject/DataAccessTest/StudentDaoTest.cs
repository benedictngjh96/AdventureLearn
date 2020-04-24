using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AdventureLearn_TesterProject.DataAccessTest
{
    [TestFixture]
    class StudentDaoTest
    {
        // input parameters for Multiple-Input test

        private string valid_studentName = "Lorem ipsum";
        private string invalid_studentName = "";

        private string valid_studentEmail = "Lorem@mail.com";
        private string invalid_studentEmail = "";

        private string valid_fbId = "102292528066576";
        private string invalid_fbId = "";

        private string valid_googleId = "102908015136726471008";
        private string invalid_googleId = "";

        private int valid_charId = 1;
        private int invalid_charId = -1;

        private int valid_studentId = 1;
        private int existing_studentId = 35;
        private int invalid_studentId_1 = 0;
        private int invalid_studentId_2 = -1;

        private StudentDao testerObj = new StudentDao();

        // EC test for StudentDao.InsertGoogleStudent

        [Test]
        public void InsertGoogleStudent_Successful() // this test is meant to succeed
        {
            int result;
            result = testerObj.InsertGoogleStudent(valid_studentName, valid_studentEmail, valid_googleId);
            Assert.That(result, Is.EqualTo(1));
        }
        [Test]
        public void InsertGoogleStudent_NotSuccessful_1() // this test is meant to fail
        {
            int result;
            Assert.That(result = testerObj.InsertGoogleStudent(invalid_studentName, valid_studentEmail, valid_googleId), Throws.Nothing);
        }
        [Test]
        public void InsertGoogleStudent_NotSuccessful_2() // this test is meant to fail
        {
            int result;
            Assert.That(result = testerObj.InsertGoogleStudent(valid_studentName, invalid_studentEmail, valid_googleId), Throws.Nothing);
            
        }
        [Test]
        public void InsertGoogleStudent_NotSuccessful_3() // this test is meant to fail
        {
            int result;
            Assert.That(result = testerObj.InsertGoogleStudent(valid_studentName, valid_studentEmail, invalid_googleId), Throws.Nothing);
        }

        // EC test for StudentDao.InsertFacebookStudent

        [Test]
        public void InsertFacebookStudent_Successful() // this test is meant to succeed
        {
            int result;
            result = testerObj.InsertFacebookStudent(valid_studentName, valid_studentEmail, valid_fbId);
            Assert.That(result, Is.EqualTo(1));
        }
        [Test]
        public void InsertFacebookStudent_NotSuccessful_1() // this test is meant to fail
        {
            int result;
            Assert.That(result = testerObj.InsertFacebookStudent(invalid_studentName, valid_studentEmail, valid_fbId), Throws.Nothing);
        }
        [Test]
        public void InsertFacebookStudent_NotSuccessful_2() // this test is meant to fail
        {
            int result;
            Assert.That(result = testerObj.InsertFacebookStudent(valid_studentName, invalid_studentEmail, valid_fbId), Throws.Nothing);

        }
        [Test]
        public void InsertFacebookStudent_NotSuccessful_3() // this test is meant to fail
        {
            int result;
            Assert.That(result = testerObj.InsertFacebookStudent(valid_studentName, valid_studentEmail, invalid_fbId), Throws.Nothing);
        }

        // BVT for StudentDao.CheckStudentExist

        [Test]
        public void CheckStudentExist_Successful() // this test is meant to succeed
        {
            bool result;
            result = testerObj.CheckStudentExist(existing_studentId);
            Assert.That(result, Is.EqualTo(true));
        }
        [Test]
        public void CheckStudentExist_NotSuccessful_1() // this test is meant to fail
        {
            bool result;
            result = testerObj.CheckStudentExist(invalid_studentId_1);
            Assert.That(result, Is.EqualTo(true));
        }
        public void CheckStudentExist_NotSuccessful_2() // this test is meant to fail
        {
            bool result;
            result = testerObj.CheckStudentExist(invalid_studentId_2);
            Assert.That(result, Is.EqualTo(true));
        }

        // EC test for StudentDao.CheckGoogleCharExist

        [Test]
        public void CheckGoogleCharExist_Successful() // this test is meant to succeed
        {
            bool result;
            result = testerObj.CheckGoogleCharExist(valid_googleId);
            Assert.That(result, Is.EqualTo(true));
        }
        [Test]
        public void CheckGoogleCharExist_NotSuccessful() // this test is meant to fail
        {
            bool result;
            result = testerObj.CheckGoogleCharExist(invalid_googleId);
            Assert.That(result, Is.EqualTo(true));
        }

        // EC test for StudentDao.CheckFacebookCharExist

        [Test]
        public void CheckFacebookCharExist_Successful() // this test is meant to succeed
        {
            bool result;
            result = testerObj.CheckFacebookCharExist(valid_fbId);
            Assert.That(result, Is.EqualTo(true));
        }
        [Test]
        public void CheckFacebookCharExist_NotSuccessful() // this test is meant to fail
        {
            bool result;
            result = testerObj.CheckFacebookCharExist(invalid_fbId);
            Assert.That(result, Is.EqualTo(true));
        }

        // EC test for StudentDao.CheckGoogleExist

        [Test]
        public void CheckGoogleExist_Successful() // this test is meant to succeed
        {
            bool result;
            result = testerObj.CheckGoogleExist(valid_googleId);
            Assert.That(result, Is.EqualTo(true));
        }
        [Test]
        public void CheckGoogleExist_NotSuccessful() // this test is meant to fail
        {
            bool result;
            result = testerObj.CheckGoogleExist(invalid_googleId);
            Assert.That(result, Is.EqualTo(true));
        }

        // EC test for StudentDao.CheckFacebookExist

        [Test]
        public void CheckFacebookExist_Successful() // this test is meant to succeed
        {
            bool result;
            result = testerObj.CheckFacebookExist(valid_fbId);
            Assert.That(result, Is.EqualTo(true));
        }
        [Test]
        public void CheckFacebookExist_NotSuccessful() // this test is meant to fail
        {
            bool result;
            result = testerObj.CheckFacebookExist(invalid_fbId);
            Assert.That(result, Is.EqualTo(true));
        }

        // EC test for StudentDao.GetFacebookStudent

        [Test]
        public void GetFacebookStudent_Successful() // this test is meant to succeed
        {
            Student result = new Student();
            result = testerObj.GetFacebookStudent(valid_fbId);
            Assert.That(result, Is.TypeOf(typeof(Student)));
        }
        [Test]
        public void GetFacebookStudent_NotSuccessful() // this test is meant to fail
        {
            Student result = new Student();
            Assert.That(result = testerObj.GetFacebookStudent(invalid_fbId), Throws.Nothing);
        }

        // EC test for StudentDao.GetGoogleStudent

        [Test]
        public void GetGoogleStudent_Successful() // this test is meant to succeed
        {
            Student result = new Student();
            result = testerObj.GetGoogleStudent(valid_googleId);
            Assert.That(result, Is.TypeOf(typeof(Student)));
        }
        [Test]
        public void GetGoogleStudent_NotSuccessful() // this test is meant to fail
        {
            Student result = new Student();
            Assert.That(result = testerObj.GetGoogleStudent(invalid_googleId), Throws.Nothing);
        }

        // Multiple-Input test for StudentDao.UpdateStudentCharacter

        [Test]
        public void UpdateStudentCharacter_Successful() // this test is meant to succeed
        {
            int result;
            result = testerObj.UpdateStudentCharacter(valid_charId, valid_studentId);
            Assert.That(result, Is.EqualTo(1));
        }
        [Test]
        public void UpdateStudentCharacter_NotSuccesful_1() // this test is meant to fail
        {
            int result;
            Assert.That(result = testerObj.UpdateStudentCharacter(invalid_charId, valid_studentId), Throws.Nothing);
        }
        [Test]
        public void UpdateStudentCharacter_NotSuccesful_2() // this test is meant to fail
        {
            int result;
            Assert.That(result = testerObj.UpdateStudentCharacter(valid_charId, invalid_studentId_1), Throws.Nothing);
        }

        // BVT for StudentDao.GetStudentCharacter

        [Test]
        public void GetStudentCharacter_Successful() // this test is meant to succeed
        {
            Student result = new Student();
            result = testerObj.GetStudentCharacter(valid_studentId);
            Assert.That(result, Is.TypeOf(typeof(Student)));
        }
        [Test]
        public void GetStudentCharacter_NotSuccessful_1() // this test is meant to fail
        {
            Student result = new Student();
            Assert.That(result = testerObj.GetStudentCharacter(invalid_studentId_1), Throws.Nothing);
        }
        [Test]
        public void GetStudentCharacter_NotSuccessful_2() // this test is meant to fail
        {
            Student result = new Student();
            Assert.That(result = testerObj.GetStudentCharacter(invalid_studentId_2), Throws.Nothing);
        }
    }
}
