
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Vadaszat.Model;
using Vadaszat.Persistence;

namespace VadaszatTest
{
    [TestClass]
    public class AknaGameModelTests
    {
        private AknaGameModel palyateszt = null!; // a tesztelendő modell
        private aknaTable mocktable = null!; // mockolt játéktábla
        private Mock<IsaknaDataAccess> mock = null!; // az adatelérés mock-ja
        [TestInitialize]
        public void inicializal()
        {
            mocktable = new aknaTable(6);
            mock = new Mock<IsaknaDataAccess>();
            //            mock.Setup(mock => mock.LoadAsync(It.IsAny<String>()))
            //.Returns(() => Task.FromResult(mocktable));
            //           mock.Setup(mock => mock.LoadAsync(It.IsAny<String>()))
            //.Returns(() => Task.FromResult((mocktable, 0, 0, 0, 0, 0)));
            palyateszt = new AknaGameModel(mock.Object);
            for (int i = 0; i < 8; i++)
            {
                for (int t = 0; t < 8; t++)
                {
                    mocktable.SetValue(i, t, 1);
                    mocktable.Setbomb(i, t, 1);
                    mocktable.SetFelnyil(i, t, 1);
                }
            }
            for (int i = 0; i < 8; i++)
            {
                mocktable.SetValue(i, 0, -30);
                mocktable.SetValue(i, 7, -30);
                mocktable.SetValue(0, i, -30);
                mocktable.SetValue(7, i, -30);
            }
            mocktable.Setbomb(3, 3, 0);
            mocktable.SetValue(4, 4, 1);
            mocktable.SetValue(4, 3, 1);
            mocktable.SetValue(4, 2, 1);
            mocktable.SetValue(3, 4, 1);
            mocktable.SetValue(3, 2, 1);
            mocktable.SetValue(2, 2, 1);
            mocktable.SetValue(2, 3, 1);
            mocktable.SetValue(2, 4, 1);
            mocktable.SetFelnyil(1, 1, 0);
            mocktable.SetFelnyil(6, 6, 0);
            mocktable.Setbomb(1);
            mocktable.Setfelnyit(2);
            palyateszt.Setax(1);
            palyateszt.Setay(1);
            palyateszt.Setbx(6);
            palyateszt.Setby(6);
            palyateszt.Setab(0);
            palyateszt.Gameover += new EventHandler<OverEventArgs>(Gameoverlesz);
            palyateszt.setaknaTable(mocktable);
        }
        [TestMethod()]
        //rossz idoben lepes
        //rossz ehylre lepes
        //halal
        //megfelelo szam
        //felnyitas müködött
        public void Joidobenlepes()
        {
            Assert.AreEqual(mocktable.GetFelnyit(), 2);
            palyateszt.megnez(0, 1);
            //az A szereplo uj kordinata
            Assert.AreEqual(palyateszt.Getax(), 1);
            Assert.AreEqual(palyateszt.Getay(), 2);
            //felnyitottak szama novekedett
            Assert.AreEqual(mocktable.GetFelnyit(), 3);
        }
        [TestMethod()]
        public void rosszhelyrelepes()
        {
            Assert.AreEqual(mocktable.GetFelnyit(), 2);
            palyateszt.megnez(5, 5);
            //az A szereplo regi kordinata
            Assert.AreEqual(palyateszt.Getax(), 1);
            Assert.AreEqual(palyateszt.Getay(), 1);
            //felnyitottak szama maradt mert rossz volt a lepes
            Assert.AreEqual(mocktable.GetFelnyit(), 2);
        }

        [TestMethod()]
        public void vegeajateknak()
        {
            palyateszt.megnez(0, 1);
            palyateszt.Setbx(2);
            palyateszt.Setby(3);
            //bombaközelben vagyunk tehát egyes lesz a value
            Assert.AreEqual(mocktable.GetValue(2, 3), 1);
            //most a B jatekos lep
            palyateszt.megnez(2, 2);
        }
        public void Gameoverlesz(Object? sender, EventArgs e)
        {
            Assert.AreEqual(palyateszt.GetisGameover(), true);
        }

    }
}