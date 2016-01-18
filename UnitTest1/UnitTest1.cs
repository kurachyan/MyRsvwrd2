using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

using Rsvwrd;

namespace UnitTest1
{
    [TestClass]
    public class Rsvwrd_UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            CS_Rsvwrd rsvwrd = new CS_Rsvwrd();

            #region 予約語１：クラス確認
            rsvwrd.Clear();
            rsvwrd.Wbuf = "class";
            rsvwrd.Exec();

            Assert.IsTrue(rsvwrd.Rsv);
            Assert.AreEqual(1, rsvwrd.RsvKind, "RsKind[RSV_CLASS]");
            #endregion

            #region 予約語２：例外確認
            rsvwrd.Clear();
            rsvwrd.Exec("try");

            Assert.IsTrue(rsvwrd.Rsv);
            Assert.AreEqual(2, rsvwrd.RsvKind, "RsKind[RSV_EXCEPT]");
            #endregion

            #region 予約語３：アクセス修飾子確認
            rsvwrd.Clear();
            rsvwrd.Exec("public");

            Assert.IsTrue(rsvwrd.Rsv);
            Assert.AreEqual(3, rsvwrd.RsvKind, "RsKind[RSV_ACCESS]");
            #endregion

            #region 予約語４：制御フロー文確認
            rsvwrd.Clear();
            rsvwrd.Exec("if");

            Assert.IsTrue(rsvwrd.Rsv);
            Assert.AreEqual(4, rsvwrd.RsvKind, "RsKind[RSV_CONTROL]");
            #endregion

            #region 予約語５：データ型確認
            rsvwrd.Clear();
            rsvwrd.Exec("int");

            Assert.IsTrue(rsvwrd.Rsv);
            Assert.AreEqual(5, rsvwrd.RsvKind, "RsKind[RSV_DATA]");
            #endregion

            #region 予約語６：その他確認
            rsvwrd.Clear();
            rsvwrd.Exec("namespace");

            Assert.IsTrue(rsvwrd.Rsv);
            Assert.AreEqual(6, rsvwrd.RsvKind, "RsKind[RSV_OTHER]");
            #endregion

            #region 非予約語確認
            rsvwrd.Clear();
            rsvwrd.Exec("function");

            Assert.IsFalse(rsvwrd.Rsv);
            Assert.AreEqual(0, rsvwrd.RsvKind, "RsKind[RSV_NONE]");
            #endregion
        }
    }
}
