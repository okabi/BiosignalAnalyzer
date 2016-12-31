using Microsoft.VisualStudio.TestTools.UnitTesting;
using BiosignalAnalyzer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiosignalAnalyzer.Tests
{
    [TestClass()]
    public class DataInfoTests
    {
        [TestMethod()]
        public void TimeToIntTest()
        {
            Assert.AreEqual(0, DataInfo.TimeToInt("000:00:00.000"), "ゼロの変換");
            Assert.AreEqual(123, DataInfo.TimeToInt("000:00:00.123"), "小数点以下の変換");
            Assert.AreEqual(12345, DataInfo.TimeToInt("000:00:12.345"), "秒までの変換");
            Assert.AreEqual(754567, DataInfo.TimeToInt("000:12:34.567"), "分までの変換");
            Assert.AreEqual(86400000, DataInfo.TimeToInt("24:00:00.000"), "時までの変換");
            Assert.AreEqual(-1, DataInfo.TimeToInt("00:12.345"), "指定書式以外は例外");
        }

        [TestMethod()]
        public void IntToTimeTest()
        {
            Assert.AreEqual("0:00:00.000", DataInfo.IntToTime(0), "ゼロの変換");
            Assert.AreEqual("0:00:00.123", DataInfo.IntToTime(123), "小数点以下の変換");
            Assert.AreEqual("0:00:12.345", DataInfo.IntToTime(12345), "秒までの変換");
            Assert.AreEqual("0:12:34.567", DataInfo.IntToTime(754567), "分までの変換");
            Assert.AreEqual("24:00:00.000", DataInfo.IntToTime(86400000), "時までの変換");
            Assert.AreEqual("", DataInfo.IntToTime(-1), "負の数は例外");
        }
    }
}