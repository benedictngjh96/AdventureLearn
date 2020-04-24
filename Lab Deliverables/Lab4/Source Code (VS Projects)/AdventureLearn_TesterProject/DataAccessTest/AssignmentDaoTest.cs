using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AdventureLearn_TesterProject.DataAccessTest
{
    [TestFixture]
    class AssignmentDaoTest
    {
        // input parameters for BVT

        private int valid_assignment_id = 1;
        private int invalid_assignment_id_1 = 0;
        private int invalid_assignment_id_2 = -1;

        private int valid_student_id = 1;
        private int invalid_student_id_1 = 0;
        private int invalid_student_id_2 = -1;

        private AssignmentDao testerObj = new AssignmentDao();

        // BVT for AssignmentDao.GetAssignment
        [Test]
        public void GetAssignment_Successful() // this test is meant to succeed
        {
            Assignment result = new Assignment();
            result = testerObj.GetAssignment(valid_assignment_id);
            Assert.That(result, Is.TypeOf(typeof(Assignment)));
            
        }        
        [Test]
        public void GetAssignment_NotSuccessful_1() // this test is meant to fail
        {
            Assignment result = new Assignment();
            Assert.That(result = testerObj.GetAssignment(invalid_assignment_id_1), Throws.Nothing);
        }
        [Test]
        public void GetAssignment_NotSuccessful_2() // this test is meant to fail
        {
            Assignment result = new Assignment();
            Assert.That(result = testerObj.GetAssignment(invalid_assignment_id_2), Throws.Nothing);
        }

        // BVT for AssignmentDao.GetStudentAssignment
        [Test]
        public void GetStudentAssignment_Successful() // this test is meant to succeed
        {
            List<PublishedAssignment> result = new List<PublishedAssignment>();
            result = testerObj.GetStudentAssignment(valid_student_id);
            Assert.That(result, Is.TypeOf(typeof(List<PublishedAssignment>)));
        }
        [Test]
        public void GetStudentAssignment_NotSuccessful_1() // this test is meant to fail
        {
            List<PublishedAssignment> result = new List<PublishedAssignment>();            
            Assert.That(result = testerObj.GetStudentAssignment(invalid_student_id_1), Throws.Nothing);
        }
        [Test]
        public void GetStudentAssignment_NotSuccessful_2() // this test is meant to fail
        {
            List<PublishedAssignment> result = new List<PublishedAssignment>();
            Assert.That(result = testerObj.GetStudentAssignment(invalid_student_id_2), Throws.Nothing);
        }

        // BVT for AssignmentDao.GetAssignmentMonster
        [Test]
        public void GetAssignmentMonster_Successful() // this test is meant to succeed
        {
            Monster result = new Monster();
            result = testerObj.GetAssignmentMonster(valid_assignment_id);
            Assert.That(result, Is.TypeOf(typeof(Monster)));

        }
        [Test]
        public void GetAssignmentMonster_NotSuccessful_1() // this test is meant to fail
        {
            Monster result = new Monster();
            Assert.That(result = testerObj.GetAssignmentMonster(invalid_assignment_id_1), Throws.Nothing);
        }
        [Test]
        public void GetAssignmentMonster_NotSuccessful_2() // this test is meant to fail
        {
            Monster result = new Monster();
            Assert.That(result = testerObj.GetAssignmentMonster(invalid_assignment_id_2), Throws.Nothing);
        }

    }
}
