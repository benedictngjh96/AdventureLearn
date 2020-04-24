using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AdventureLearn_TesterProject.DataAccessTest
{
    [TestFixture]
    class LeaderboardDaoTest
    {
        // input parameters for BVT

        private int valid_worldId = 1;
        private int invalid_worldId_1 = 0;
        private int invalid_worldId_2 = -1;

        private LeaderboardDao testerObj = new LeaderboardDao();

        // BVT for LeaderboardDao.GetLeaderboardScore

        [Test]
        public void GetLeaderboardScore_Successful() // this test is meant to succeed
        {
            List<Leaderboard> result = new List<Leaderboard>();
            Assert.That(result = testerObj.GetLeaderboardScore(valid_worldId), Throws.Nothing);
        }

        [Test]
        public void GetLeaderboardScore_NotSuccessful_1() // this test is meant to fail
        {
            List<Leaderboard> result = new List<Leaderboard>();
            Assert.That(result = testerObj.GetLeaderboardScore(invalid_worldId_1), Throws.Nothing);
        }

        [Test]
        public void GetLeaderboardScore_NotSuccessful_2() // this test is meant to fail
        {
            List<Leaderboard> result = new List<Leaderboard>();
            Assert.That(result = testerObj.GetLeaderboardScore(invalid_worldId_2), Throws.Nothing);
        }

    }
}
