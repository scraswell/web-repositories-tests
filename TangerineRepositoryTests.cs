using System.Configuration;
using System.Linq;

using log4net;
using log4net.Config;
using NUnit.Framework;

namespace Craswell.WebRepositories.Tests
{
    /// <summary>
    /// Tangerine repository factory tests.
    /// </summary>
    [TestFixture]
    public class TangerineRepositoryTests
    {
        /// <summary>
        /// The passphrase.
        /// </summary>
        private static readonly string passphrase = ConfigurationManager.AppSettings["passphrase"];

        /// <summary>
        /// Tangerines the repository tests can get accounts.
        /// </summary>
        [TestCase]
        public void TangerineRepository_CanGetAccounts()
        {
            XmlConfigurator.Configure();

            using (WebRepositoryFactory factory = new WebRepositoryFactory(passphrase))
            {
                using (IConnectedWebRepository repo = factory.GetWebRepositories()
                    .Where(r => r.Id == 1)
                    .Single())
                {
                    var accounts = repo.GetAccounts();
                }
            }
        }

        /// <summary>
        /// Tangerines the repository tests can get accounts.
        /// </summary>
        [TestCase]
        public void TangerineRepository_CanGetStatement()
        {
            XmlConfigurator.Configure();

            using (WebRepositoryFactory factory = new WebRepositoryFactory(passphrase))
            {
                using (IConnectedWebRepository repo = factory.GetWebRepositories()
                    .Where(r => r.Id == 1)
                    .Single())
                {
                    repo.GetStatement();
                }
            }
        }
    }
}

