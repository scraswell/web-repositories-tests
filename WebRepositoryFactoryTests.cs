using System.Collections.Generic;

using Craswell.Automation.DataAccess;
using NHibernate;
using NUnit.Framework;

namespace Craswell.WebRepositories.Tests
{
    /// <summary>
    /// Web repository factory tests.
    /// </summary>
    [TestFixture()]
    public class WebRepositoryFactoryTests
    {
        /// <summary>
        /// The data access layer.
        /// </summary>
        private DataAccessLayer dataAccessLayer;

        /// <summary>
        /// The session.
        /// </summary>
        private ISession session;

        /// <summary>
        /// Tests that we can create a web repository.
        /// </summary>
        [Test()]
        public void WebRepositoryFactory_CanCreateWebRepository()
        {
            this.dataAccessLayer = new DataAccessLayer();
            this.session = this.dataAccessLayer.OpenSession();

            int newRepoId;
            IDictionary<string, string> sq;

            using (WebRepositoryFactory factory = new WebRepositoryFactory("this is a bad passphrase"))
            {

                sq = new Dictionary<string, string>() {
                    { "question1", "answer1" },
                    { "question2", "answer2" },
                    { "question3", "answer3" }
                };

                newRepoId = factory.CreateWebRepository(
                                WebRepositoryType.Tangerine,
                                "my test repository",
                                "https://www.tangerine.ca/en/index.html",
                                "myuseraccountname",
                                "123456",
                                sq);
            }

            WebRepositoryData repo = this.session
                .Get<WebRepositoryData>(newRepoId);


            Assert.AreEqual(newRepoId, repo.Id);
            Assert.AreEqual("my test repository", repo.Name);
            Assert.AreEqual(WebRepositoryType.Tangerine, repo.Type);

            // check that data is encrypted.
            foreach (string key in repo.Configuration.SecurityQuestions.Keys)
            {
                Assert.IsFalse(sq.ContainsKey(key));
            }

            Assert.AreNotEqual(
                "myuseraccountname",
                repo.Configuration.Username);

            Assert.AreNotEqual(
                "123456",
                repo.Configuration.Password);

            using (ITransaction tx = session.BeginTransaction())
            {
                session.Delete(repo);
                tx.Commit();
            }

            this.session.Dispose();
            this.session = null;

            this.dataAccessLayer.Dispose();
            this.dataAccessLayer = null;
        }
    }
}

