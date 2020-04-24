using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AdventureLearn_TesterProject.DataAccessTest
{
    [TestFixture]
    class EditLevelDaoTest
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

        private EditLevelDao testerObj = new EditLevelDao();

        // Multiple-Input test for CreateLevelDAO.InsertQuestion

        [Test]
        public void updateQuestion_Successful() // this test is meant to succeed
        {
            Assert.That(() => testerObj.updateQuestion(valid_option_1, valid_option_2, valid_option_3, valid_option_4, valid_correctOptionInt, valid_question_title, valid_question_id), Throws.Nothing);
        }
        [Test]
        public void updateQuestion_NotSuccessful_1() // this test is meant to fail
        {
            Assert.That(() => testerObj.updateQuestion(invalid_option_1, valid_option_2, valid_option_3, valid_option_4, valid_correctOptionInt, valid_question_title, valid_question_id), Throws.Nothing);
        }
        [Test]
        public void updateQuestion_NotSuccessful_2() // this test is meant to fail
        {
            Assert.That(() => testerObj.updateQuestion(valid_option_1, invalid_option_2, valid_option_3, valid_option_4, valid_correctOptionInt, valid_question_title, valid_question_id), Throws.Nothing);
        }
        [Test]
        public void updateQuestion_NotSuccessful_3() // this test is meant to fail
        {
            Assert.That(() => testerObj.updateQuestion(valid_option_1, valid_option_2, invalid_option_3, valid_option_4, valid_correctOptionInt, valid_question_title, valid_question_id), Throws.Nothing);
        }
        [Test]
        public void updateQuestion_NotSuccessful_4() // this test is meant to fail
        {
            Assert.That(() => testerObj.updateQuestion(valid_option_1, valid_option_2, valid_option_3, invalid_option_4, valid_correctOptionInt, valid_question_title, valid_question_id), Throws.Nothing);
        }
        [Test]
        public void updateQuestion_NotSuccessful_5() // this test is meant to fail
        {
            Assert.That(() => testerObj.updateQuestion(valid_option_1, valid_option_2, valid_option_3, valid_option_4, invalid_correctOptionInt, valid_question_title, valid_question_id), Throws.Nothing);
        }
        [Test]
        public void updateQuestion_NotSuccessful_6() // this test is meant to fail
        {
            Assert.That(() => testerObj.updateQuestion(valid_option_1, valid_option_2, valid_option_3, valid_option_4, valid_correctOptionInt, invalid_question_title, invalid_question_id), Throws.Nothing);
        }

        // Multiple-Input test for EditLevelDao.updateLevelInitInfo

        [Test]
        public void updateLevelInitInfo_Successful() // this test is meant to succeed
        {
            Assert.That(() => testerObj.updateLevelInitInfo(valid_level_name, valid_monster_id, valid_time_limit), Throws.Nothing);
        }
        [Test]
        public void updateLevelInitInfo_NotSuccessful_1() // this test is meant to fail
        {
            Assert.That(() => testerObj.updateLevelInitInfo(invalid_level_name, valid_monster_id, valid_time_limit), Throws.Nothing);
        }
        [Test]
        public void updateLevelInitInfo_NotSuccessful_2() // this test is meant to fail
        {
            Assert.That(() => testerObj.updateLevelInitInfo(valid_level_name, invalid_monster_id, valid_time_limit), Throws.Nothing);
        }
        [Test]
        public void updateLevelInitInfo_NotSuccessful_3() // this test is meant to fail
        {
            Assert.That(() => testerObj.updateLevelInitInfo(valid_level_name, valid_monster_id, invalid_time_limit), Throws.Nothing);
        }
    }
}
