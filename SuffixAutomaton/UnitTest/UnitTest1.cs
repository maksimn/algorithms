using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest {

    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void IsAchievableTest1() {
            SuffixAutomaton sa = new SuffixAutomaton();
            String s = "abc0bca1bbc2";
            for (int i = 0; i < s.Length; i++) {
                sa.Extend(s[i]);
            }
            Assert.AreEqual(true, sa.IsAchievable(2, '0', new Char[] { '1', '2' }));
            Assert.AreEqual(false, sa.IsAchievable(2, '1', new Char[] { '0', '2' }));
            Assert.AreEqual(false, sa.IsAchievable(2, '2', new Char[] { '0', '1' }));
        }
        [TestMethod]
        public void IsAllAchievableTest1() {
            SuffixAutomaton sa = new SuffixAutomaton();
            String s = "abc0bca1bbc2"; 
            for (int i = 0; i < s.Length; i++) {
                sa.Extend(s[i]);
            }
            sa.N = 3;
            Assert.AreEqual(true, sa.IsAllAchievable(0, new Char[] { '0', '1', '2' }));
            Assert.AreEqual(false, sa.IsAllAchievable(1, new Char[] { '0', '1', '2' }));
            Assert.AreEqual(false, sa.IsAllAchievable(2, new Char[] { '0', '1', '2' }));
        }
        [TestMethod]
        public void MaxStateTest() {
            SuffixAutomaton sa = new SuffixAutomaton();
            String s = "abc0bca1bbc2";
            sa.N = 3;
            for (int i = 0; i < s.Length; i++) {
                sa.Extend(s[i]);
            }
            Assert.AreEqual(8, sa.FindStateThatMatchesMaxSubstring(new char[] { '0', '1', '2' }));
        }
        [TestMethod]
        public void MaxCommonSubstrTest1() {
            SuffixAutomaton sa = new SuffixAutomaton();
            String s = "aaa0aaa1aaa2";
            sa.N = 3;
            for (int i = 0; i < s.Length; i++) {
                sa.Extend(s[i]);
            }
            Assert.AreEqual("aaa", sa.MaxCommonSubstring(new Char[] { '0', '1', '2' }));
        }
        [TestMethod]
        public void MaxCommonSubstrTest2() {
            SuffixAutomaton sa = new SuffixAutomaton();
            String s = "arrc0zrrv1trrn2";
            sa.N = 3;
            for (int i = 0; i < s.Length; i++) {
                sa.Extend(s[i]);
            }
            Assert.AreEqual("rr", sa.MaxCommonSubstring(new Char[] { '0', '1', '2' }));
        }
        [TestMethod]
        public void MaxStateTest2() {
            SuffixAutomaton sa = new SuffixAutomaton();
            String s = "arrc0zrrv1trrn2";
            sa.N = 3;
            for (int i = 0; i < s.Length; i++) {
                sa.Extend(s[i]);
            }
            Assert.AreEqual(10, sa.FindStateThatMatchesMaxSubstring(new char[] { '0', '1', '2' }));
        }
        [TestMethod]
        public void IsAllAchievableTest2() {
            SuffixAutomaton sa = new SuffixAutomaton();
            String s = "arrc0zrrv1trrn2";
            sa.N = 3;
            for (int i = 0; i < s.Length; i++) {
                sa.Extend(s[i]);
            }
            Assert.AreEqual(true, sa.IsAllAchievable(0, new Char[] { '0', '1', '2' }));
            Assert.AreEqual(true, sa.IsAllAchievable(10, new Char[] { '0', '1', '2' }));
            Assert.AreEqual(false, sa.IsAllAchievable(12, new Char[] { '0', '1', '2' }));
        }
        [TestMethod]
        public void IsAchievableTest2() {
            SuffixAutomaton sa = new SuffixAutomaton();
            String s = "arrc0zrrv1trrn2";
            sa.N = 3;
            for (int i = 0; i < s.Length; i++) {
                sa.Extend(s[i]);
            }
            Assert.AreEqual(true, sa.IsAchievable(10, '0', new Char[] { '1', '2' }));
            Assert.AreEqual(true, sa.IsAchievable(10, '1', new Char[] { '0', '2' }));
//            Assert.AreEqual(true, sa.IsAchievable(10, '2', new Char[] { '0', '1' }));
        }
    }
}
