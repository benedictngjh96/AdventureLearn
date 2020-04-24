using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AdventureLearn_TesterProject.DataAccessTest
{
    [TestFixture]
    class AssignmentScoreDaoTest
    {
        // input parameters for multiple-input test and BVT

        private int valid_assignment_id = 1;
        private int invalid_assignment_id = -1;

        private int valid_assignment_score = 100;
        private int invalid_assignment_score = -1;

        private int valid_student_id = 1;
        private int invalid_student_id_1 = -1;
        private int invalid_student_id_2 = 0;

        private AssignmentScoreDao testerObj = new AssignmentScoreDao();

        // Multiple-Input testing for AssignmentScoreDao.InsertAssignmentScore
        [Test]
        public void InsertAssignmentScore_Successful()  // this test is meant to succeed
        {
            int result;
            result = testerObj.InsertAssignmentScore(valid_student_id, valid_assignment_id, valid_assignment_score);
            Assert.That(result, Is.TypeOf(typeof(int)));
        }
        [Test]
        public void InsertAssignmentScore_NotSuccessful_1() // this test is meant to fail
        {
            int result;
            Assert.That(result = testerObj.InsertAssignmentScore(invalid_student_id_1, valid_assignment_id, valid_assignment_score), Throws.Nothing);
        }
        [Test]
        public void InsertAssignmentScore_NotSuccessful_2() // this test is meant to fail
        {
            int result;
            Assert.That(result = testerObj.InsertAssignmentScore(valid_student_id, invalid_assignment_id, valid_assignment_score), Throws.Nothing);
        }
        [Test]
        public void InsertAssignmentScore_NotSuccessful_3() // this test is meant to fail
        {
            int result;
            Assert.That(result = testerObj.InsertAssignmentScore(valid_student_id, valid_assignment_id, invalid_assignment_score), Throws.Nothing);
        }

        // BVT for AssignmentScoreDao.GetStudentCompletedAssignment

        [Test]
        public void GetStudentCompletedAssignment_Successful()  // this test is meant to succeed
        {
            List<AssignmentScore> result = new List<AssignmentScore>();
            result = testerObj.GetStudentCompletedAssignment(valid_student_id);
            Assert.That(result, Is.TypeOf(typeof(List<AssignmentScore>)));
        }
        [Test]
        public void GetStudentCompletedAssignment_NotSuccessful_1() // this test is meant to fail
        {
            List<AssignmentScore> result = new List<AssignmentScore>();
            Assert.That(result = testerObj.GetStudentCompletedAssignment(invalid_student_id_1), Throws.Nothing);
        }
        [Test]
        public void GetStudentCompletedAssignment_NotSuccessful_2() // this test is meant to fail
        {
            List<AssignmentScore> result = new List<AssignmentScore>();
            Assert.That(result = testerObj.GetStudentCompletedAssignment(invalid_student_id_2), Throws.Nothing);
        }
    }
}
