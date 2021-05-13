// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation

using System.Linq;
using NUnit.Framework;

namespace Monopoly
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void GetPlayersListReturnCorrectList()
        {
            var players = new[] {"Peter", "Ekaterina", "Alexander"};
            var expectedPlayers = new[]
            {
                new Player(1, "Peter", 6000),
                new Player(2, "Ekaterina", 6000),
                new Player(3, "Alexander", 6000)
            };
            var monopoly = new Monopoly(players);
            var actualPlayers = monopoly.GetPlayersList().ToArray();

            Assert.AreEqual(expectedPlayers, actualPlayers);
        }

        [Test]
        public void GetFieldsListReturnCorrectList()
        {
            var expectedCompanies =
                new[]
                {
                    new Field("Ford", FieldType.AUTO),
                    new Field("MCDonald", FieldType.FOOD),
                    new Field("Lamoda", FieldType.CLOTHER),
                    new Field("Air Baltic", FieldType.TRAVEL),
                    new Field("Nordavia", FieldType.TRAVEL),
                    new Field("Prison", FieldType.PRISON),
                    new Field("MCDonald", FieldType.FOOD),
                    new Field("DeutscheBank", FieldType.BANK),
                    new Field("TESLA", FieldType.AUTO)
                };
            var players = new string[] {"Peter", "Ekaterina", "Alexander"};
            var monopoly = new Monopoly(players);
            var actualCompanies = monopoly.GetFieldsList().ToArray();
            Assert.AreEqual(expectedCompanies, actualCompanies);
        }

        [Test]
        public void PlayerBuyNoOwnedCompanies()
        {
            var players = new string[] {"Peter", "Ekaterina", "Alexander"};
            var monopoly = new Monopoly(players);
            var x = monopoly.GetFieldByName("Ford");
            monopoly.Buy(1, x);
            var actualPlayer = monopoly.GetPlayerInfo(1);
            var expectedPlayer = new Player(1, "Peter", 5500);
            Assert.AreEqual(expectedPlayer, actualPlayer);
            var actualField = monopoly.GetFieldByName("Ford");
            Assert.AreEqual(1, actualField.OwnerId);
        }

        [Test]
        public void RentaShouldBeCorrectTransferMoney()
        {
            var players = new string[] {"Peter", "Ekaterina", "Alexander"};
            var monopoly = new Monopoly(players);
            var x = monopoly.GetFieldByName("Ford");
            monopoly.Buy(1, x);
            x = monopoly.GetFieldByName("Ford");
            monopoly.Renta(2, x);
            var player1 = monopoly.GetPlayerInfo(1);
            Assert.AreEqual(5750, player1.Money);
            var player2 = monopoly.GetPlayerInfo(2);
            Assert.AreEqual(5750, player2.Money);
        }

        #region Buy

        [Test]
        public void BuyForFieldAuto_ShouldSpendMoney()
        {
            var players = new string[] {"Peter", "Ekaterina"};
            var monopoly = new Monopoly(players);
            var field = monopoly.GetFieldByName("Ford");
            monopoly.Buy(1, field);
            var player1 = monopoly.GetPlayerInfo(1);
            Assert.AreEqual(5500, player1.Money);
        }

        [Test]
        public void BuyForFieldFood_ShouldSpendMoney()
        {
            var players = new string[] {"Peter", "Ekaterina"};
            var monopoly = new Monopoly(players);
            var field = monopoly.GetFieldByName("MCDonald");
            monopoly.Buy(1, field);
            var player1 = monopoly.GetPlayerInfo(1);
            Assert.AreEqual(5750, player1.Money);
        }

        [Test]
        public void BuyForFieldTravel_ShouldSpendMoney()
        {
            var players = new string[] {"Peter", "Ekaterina"};
            var monopoly = new Monopoly(players);
            var field = monopoly.GetFieldByName("Nordavia");
            monopoly.Buy(1, field);
            var player1 = monopoly.GetPlayerInfo(1);
            Assert.AreEqual(5300, player1.Money);
        }

        [Test]
        public void BuyForFieldClother_ShouldSpendMoney()
        {
            var players = new string[] {"Peter", "Ekaterina"};
            var monopoly = new Monopoly(players);
            var field = monopoly.GetFieldByName("Lamoda");
            monopoly.Buy(1, field);
            var player1 = monopoly.GetPlayerInfo(1);
            Assert.AreEqual(5900, player1.Money);
        }

        [Test]
        public void BuyForFieldPrison_ShouldFail()
        {
            var players = new string[] {"Peter", "Ekaterina"};
            var monopoly = new Monopoly(players);
            var field = monopoly.GetFieldByName("Prison");
            var isBuyOk = monopoly.Buy(1, field);
            Assert.False(isBuyOk);
        }

        [Test]
        public void BuyForFieldBank_ShouldFail()
        {
            var players = new string[] {"Peter", "Ekaterina"};
            var monopoly = new Monopoly(players);
            var field = monopoly.GetFieldByName("DeutscheBank");
            var isBuyOk = monopoly.Buy(1, field);
            Assert.False(isBuyOk);
        }

        #endregion

        #region Renta

        [Test]
        public void RentaForFieldAuto_ShouldTransferMoney()
        {
            var players = new string[] {"Peter", "Ekaterina"};
            var monopoly = new Monopoly(players);
            var field = monopoly.GetFieldByName("Ford");
            monopoly.Buy(1, field);
            monopoly.Renta(2, field);
            var player1 = monopoly.GetPlayerInfo(1);
            Assert.AreEqual(5750, player1.Money);
            var player2 = monopoly.GetPlayerInfo(2);
            Assert.AreEqual(5750, player2.Money);
        }

        [Test]
        public void RentaForFieldFood_ShouldTransferMoney()
        {
            var players = new string[] {"Peter", "Ekaterina"};
            var monopoly = new Monopoly(players);
            var field = monopoly.GetFieldByName("MCDonald");
            monopoly.Buy(1, field);
            monopoly.Renta(2, field);
            var player1 = monopoly.GetPlayerInfo(1);
            Assert.AreEqual(6000, player1.Money);
            var player2 = monopoly.GetPlayerInfo(2);
            Assert.AreEqual(5750, player2.Money);
        }

        [Test]
        public void RentaForFieldTravel_ShouldTransferMoney()
        {
            var players = new string[] {"Peter", "Ekaterina"};
            var monopoly = new Monopoly(players);
            var field = monopoly.GetFieldByName("Nordavia");
            monopoly.Buy(1, field);
            monopoly.Renta(2, field);
            var player1 = monopoly.GetPlayerInfo(1);
            Assert.AreEqual(5600, player1.Money);
            var player2 = monopoly.GetPlayerInfo(2);
            Assert.AreEqual(5700, player2.Money);
        }

        [Test]
        public void RentaForFieldClother_ShouldTransferMoney()
        {
            var players = new string[] {"Peter", "Ekaterina"};
            var monopoly = new Monopoly(players);
            var field = monopoly.GetFieldByName("Lamoda");
            monopoly.Buy(1, field);
            monopoly.Renta(2, field);
            var player1 = monopoly.GetPlayerInfo(1);
            Assert.AreEqual(6900, player1.Money);
            var player2 = monopoly.GetPlayerInfo(2);
            Assert.AreEqual(5900, player2.Money);
        }

        [Test]
        public void RentaForFieldPrison_ShouldSpendMoney()
        {
            var players = new string[] {"Peter", "Ekaterina"};
            var monopoly = new Monopoly(players);
            var field = monopoly.GetFieldByName("Prison");
            monopoly.Renta(2, field);
            var player2 = monopoly.GetPlayerInfo(2);
            Assert.AreEqual(5000, player2.Money);
        }

        [Test]
        public void RentaForFieldBank_ShouldTransferMoney()
        {
            var players = new string[] {"Peter", "Ekaterina"};
            var monopoly = new Monopoly(players);
            var field = monopoly.GetFieldByName("DeutscheBank");
            monopoly.Renta(2, field);
            var player2 = monopoly.GetPlayerInfo(2);
            Assert.AreEqual(5300, player2.Money);
        }

        #endregion

        #region Renta not owned

        [Test]
        public void RentaForNotOwnedFieldAuto_ShouldTransferMoney()
        {
            var players = new string[] {"Peter", "Ekaterina"};
            var monopoly = new Monopoly(players);
            var field = monopoly.GetFieldByName("Ford");
            monopoly.Renta(2, field);
            var isOperationOk = monopoly.Renta(2, field);
            Assert.False(isOperationOk);
        }

        [Test]
        public void RentaForNotOwnedFieldFood_ShouldTransferMoney()
        {
            var players = new string[] {"Peter", "Ekaterina"};
            var monopoly = new Monopoly(players);
            var field = monopoly.GetFieldByName("MCDonald");
            monopoly.Renta(2, field);
            var isOperationOk = monopoly.Renta(2, field);
            Assert.False(isOperationOk);
        }

        [Test]
        public void RentaForNotOwnedFieldTravel_ShouldTransferMoney()
        {
            var players = new string[] {"Peter", "Ekaterina"};
            var monopoly = new Monopoly(players);
            var field = monopoly.GetFieldByName("Nordavia");
            monopoly.Renta(2, field);
            var isOperationOk = monopoly.Renta(2, field);
            Assert.False(isOperationOk);
        }

        [Test]
        public void RentaForNotOwnedFieldClother_ShouldTransferMoney()
        {
            var players = new string[] {"Peter", "Ekaterina"};
            var monopoly = new Monopoly(players);
            var field = monopoly.GetFieldByName("Lamoda");
            monopoly.Renta(2, field);
            var isOperationOk = monopoly.Renta(2, field);
            Assert.False(isOperationOk);
        }

        #endregion

        #region Searching unknown elements

        [Test]
        public void GetUnknownPlayer_ReturnsNone()
        {
            var players = new[] { "Peter", "Ekaterina", "Alexander" };
            var expectedPlayer = Player.None;
            var monopoly = new Monopoly(players);

            var actualPlayer = monopoly.GetPlayerInfo(4);

            Assert.AreEqual(expectedPlayer, actualPlayer);
        }

        [Test]
        public void GetUnknownField_ReturnsNone()
        {
            var players = new[] { "Peter" };
            var expectedField = Field.None;
            var monopoly = new Monopoly(players);

            var actualField = monopoly.GetFieldByName("Any Unknown Field");

            Assert.AreEqual(expectedField, actualField);
        }

        [Test]
        public void BuyByUnknownPlayer_ShouldFail()
        {
            var players = new string[] { "Peter", "Ekaterina" };
            var monopoly = new Monopoly(players);
            var field = monopoly.GetFieldByName("Ford");
            var isOperationOk = monopoly.Buy(3, field);
            Assert.False(isOperationOk);
        }

        [Test]
        public void RentaByUnknownPlayer_ShouldFail()
        {
            var players = new string[] { "Peter", "Ekaterina" };
            var monopoly = new Monopoly(players);
            var field = monopoly.GetFieldByName("Ford");
            monopoly.Buy(1, field);
            var isOperationOk = monopoly.Renta(3, field);
            Assert.False(isOperationOk);
        }

        #endregion
    }
}
