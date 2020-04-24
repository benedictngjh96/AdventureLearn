using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AdventureLearn_TesterProject.DataAccessTest
{
    [TestFixture]
    class CharacterDaoTest
    {
        // input parameters for BVT

        private int valid_student_id = 1;
        private int invalid_student_id_1 = 0;
        private int invalid_student_id_2 = -1;

        private CharacterDao testerObj = new CharacterDao();

        // BVT for CharacterDao.GetCharacter

        [Test]
        public void GetCharacter_Successful() // this test is meant to succeed
        {
            Character result = new Character();
            result = testerObj.GetCharacter(valid_student_id);
            Assert.That(result, Is.TypeOf(typeof(Character)));
        }
        [Test]
        public void GetCharacter_NotSuccessful_1() // this test is meant to fail
        {
            Character result = new Character();
            Assert.That(result = testerObj.GetCharacter(invalid_student_id_1), Throws.Nothing);
        }
        [Test]
        public void GetCharacter_NotSuccessful_2() // this test is meant to fail
        {
            Character result = new Character();
            Assert.That(result = testerObj.GetCharacter(invalid_student_id_2), Throws.Nothing);
        }

        // no input parameter testing

        [Test]
        public void GetAllCharacters_Successful() // this test is meant to succeed
        {
            List<Character> result = new List<Character>();
            result = testerObj.GetAllCharacters();
            Assert.That(result, Is.TypeOf(typeof(List<Character>)));
        }

        [Test]
        public void GetAllMonsters_Successful() // this test is meant to succeed
        {
            List<Monster> result = new List<Monster>();
            result = testerObj.GetAllMonsters();
            Assert.That(result, Is.TypeOf(typeof(List<Monster>)));
        }
    }
}
