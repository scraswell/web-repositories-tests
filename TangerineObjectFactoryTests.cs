using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using Craswell.Automation.DataAccess;
using Craswell.WebRepositories.Tangerine;

namespace Craswell.WebRepositories.Tests
{
    /// <summary>
    /// Tangerine object factory tests.
    /// </summary>
    [TestFixture]
    public class TangerineObjectFactoryTests
    {
        /// <summary>
        /// Statement date info
        /// </summary>
        public const string statementDateInfo = "October 01, 2015 to October 31, 2015\nClient #: 3234567987\n";

        /// <summary>
        /// Statement account info.
        /// </summary>
        public const string statementAccountInfo = "(Savings) 1234567987";

        /// <summary>
        /// Test account info.
        /// </summary>
        public static List<string> accountInfo = new List<string>()
        {
            "Tangerine Chequing Account - Joint Chequing - 1234567987",
            "$3,232.61",
            "Tangerine Chequing Account - Discretionary - 1234567987",
            "$6.44",
            "Tax-Free Savings Account - Savings - 1234567987",
            "$2.02"
        };

        /// <summary>
        /// Tangerines the repository tests can get accounts.
        /// </summary>
        [TestCase]
        public void TangerineObjectFactory_CanBuildAccountList()
        {
            TangerineObjectFactory factory = new TangerineObjectFactory();

            List<TangerineAccount> accounts = factory
                .BuildAccountList(accountInfo)
                .ToList();

            Assert.AreEqual(accountInfo.Count / 2, accounts.Count);
            Assert.AreEqual("Tangerine Chequing Account", accounts[0].Name);
            Assert.AreEqual("1234567987", accounts[0].Number);
            Assert.AreEqual(0, accounts[0].AccountIndex);
            Assert.AreEqual(3232.61, accounts[0].Balance);
            Assert.AreEqual("Tangerine Chequing Account", accounts[1].Name);
            Assert.AreEqual("1234567987", accounts[1].Number);
            Assert.AreEqual(6.44, accounts[1].Balance);
            Assert.AreEqual(1, accounts[1].AccountIndex);
            Assert.AreEqual("Tax-Free Savings Account", accounts[2].Name);
            Assert.AreEqual("1234567987", accounts[2].Number);
            Assert.AreEqual(2.02, accounts[2].Balance);
            Assert.AreEqual(2, accounts[2].AccountIndex);
        }

        [TestCase]
        public void TangerineObjectFactory_CanBuildTransactionList()
        {
            throw new NotImplementedException();
        }

        [TestCase]
        public void TangerineObjectFactory_CanBuildStatementData()
        {
            TangerineObjectFactory factory = new TangerineObjectFactory();

            TangerineStatement statement = factory.BuildStatement(statementDateInfo, statementAccountInfo);

            Assert.AreEqual(new DateTime(2015, 10, 31), statement.Timestamp);
            Assert.AreEqual("1234567987", statement.AccountNumber);
        }
    }
}

