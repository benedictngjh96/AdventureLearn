using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AdventureLearn_TesterProject.DataAccessTest
{
    [TestFixture]
    class CreateLevelDAOTest
    {
        // input parameters for BVT and Multiple-Input tests

        private string valid_level_name = "Lorem Ipsum";
        private string invalid_level_name = "";

        private int valid_monster_id = 1;
        private int invalid_monster_id = -1;

        private int valid_time_limit = 30;
        private int invalid_time_limit = -1;

        private int valid_question_id = 1;
        private int invalid_question_id = -1;

        private string valid_question_title = "Lorem ipsum";
        private string invalid_question_title = "";

        private int valid_custom_level_id = 1;
        private int invalid_custom_level_id = -1;

        private string valid_option_1 = "A";
        private string invalid_option_1 = "";

        private string valid_option_2 = "B";
        private string invalid_option_2 = "";

        private string valid_option_3 = "C";
        private string invalid_option_3 = "";

        private string valid_option_4 = "D";
        private string invalid_option_4 = "";

        private int valid_correctOptionInt = 1;
        private int invalid_correctOptionInt = -1;

        private CreateLevelDAO testerObj = new CreateLevelDAO();


        // Multiple-Input test for CreateLevelDAO.InsertQuestion
        
        [Test]
        public void InsertQuestion_Successful() // this test is meant to succeed
        {
            Assert.That(() => testerObj.InsertQuestion(valid_option_1, valid_option_2, valid_option_3, valid_option_4, valid_correctOptionInt, valid_question_title), Throws.Nothing);
        }
        [Test]
        public void InsertQuestion_NotSuccessful_1() // this test is meant to fail
        {
            Assert.That(() => testerObj.InsertQuestion(invalid_option_1, valid_option_2, valid_option_3, valid_option_4, valid_correctOptionInt, valid_question_title), Throws.Nothing);
        }
        [Test]
        public void InsertQuestion_NotSuccessful_2() // this test is meant to fail
        {
            Assert.That(() => testerObj.InsertQuestion(valid_option_1, invalid_option_2, valid_option_3, valid_option_4, valid_correctOptionInt, valid_question_title), Throws.Nothing);
        }
        [Test]
        public void InsertQuestion_NotSuccessful_3() // this test is meant to fail
        {
            Assert.That(() => testerObj.InsertQuestion(valid_option_1, valid_option_2, invalid_option_3, valid_option_4, valid_correctOptionInt, valid_question_title), Throws.Nothing);
        }
        [Test]
        public void InsertQuestion_NotSuccessful_4() // this test is meant to fail
        {
            Assert.That(() => testerObj.InsertQuestion(valid_option_1, valid_option_2, valid_option_3, invalid_option_4, valid_correctOptionInt, valid_question_title), Throws.Nothing);
        }
        [Test]
        public void InsertQuestion_NotSuccessful_5() // this test is meant to fail
        {
            Assert.That(() => testerObj.InsertQuestion(valid_option_1, valid_option_2, valid_option_3, valid_option_4, invalid_correctOptionInt, valid_question_title), Throws.Nothing);
        }
        [Test]
        public void InsertQuestion_NotSuccessful_6() // this test is meant to fail
        {
            Assert.That(() => testerObj.InsertQuestion(valid_option_1, valid_option_2, valid_option_3, valid_option_4, valid_correctOptionInt, invalid_question_title), Throws.Nothing);
        }
        
        // Multiple-Input test for CreateLevelDAO.InsertCustomLevel
        [Test]
        public void InsertCustomLevel_Successful() // test is meant to succeed
        {
            Assert.That(() => testerObj.InsertCustomLevel(valid_level_name, valid_monster_id, valid_time_limit), Throws.Nothing);
        }
        [Test]
        public void InsertCustomLevel_NotSuccessful_1() // test is meant to fail
        {
            Assert.That(() => testerObj.InsertCustomLevel(invalid_level_name, valid_monster_id, valid_time_limit), Throws.Nothing);
        }
        [Test]
        public void InsertCustomLevel_NotSuccessful_2() // test is meant to fail
        {
            Assert.That(() => testerObj.InsertCustomLevel(valid_level_name, invalid_monster_id, valid_time_limit), Throws.Nothing);
        }
        [Test]
        public void InsertCustomLevel_NotSuccessful_3() // test is meant to fail
        {
            Assert.That(() => testerObj.InsertCustomLevel(valid_level_name, valid_monster_id, invalid_time_limit), Throws.Nothing);
        }

        // EC test for CreateLevelDAO.checkValidName

        [Test]
        public void checkValidName_Successful() // test is meant to succeed
        {
            int result;
            result = CreateLevelDAO.checkValidLevelName(valid_level_name);
            Assert.That(result, Is.EqualTo(1));
        }
    }
}
