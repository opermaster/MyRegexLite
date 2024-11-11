using MyRegex;
using System.Text.RegularExpressions;
namespace RegexLiteTest
{
    public class Tests
    {
        MyRegexC rgx;
        State[] states;
        [SetUp]
        public void Setup() {
            rgx= new MyRegexC();
        }

        [Test]
        public void Test_Meta_StrLit_T() {
            states = [
                new State(STATE_TYPE.META, "\\d"),
                new State(STATE_TYPE.META, "\\d"),
                new State(STATE_TYPE.STRING_LIT, "-"),
                new State(STATE_TYPE.META, "\\d"),
                new State(STATE_TYPE.META, "\\d"),
            ];
            Assert.True(rgx.Matching(states, "22-34"));
        }

        public void Test_Meta_StrLit_F() {
            Assert.False(rgx.Matching(states, "a2-34"));
        }

        [Test]
        public void Test_Mul_StrLit_T() {
            states = [
                new State(STATE_TYPE.MULTITUDE, "abc"),
                new State(STATE_TYPE.STRING_LIT, "-"),
                new State(STATE_TYPE.MULTITUDE, "abc"),
            ];
            Assert.True(rgx.Matching(states, "a-b"));
        }
        [Test]
        public void Test_Mul_StrLit_F() {
            Assert.False(rgx.Matching(states, "1-b"));
        }
        [Test]
        public void Test_Range_StrLit_T() {
            states = [
                new State(STATE_TYPE.RANGE, "az"),
                new State(STATE_TYPE.STRING_LIT, "-"),
                new State(STATE_TYPE.RANGE, "09"),
            ];
            Assert.True(rgx.Matching(states, "a-3"));
        }
        [Test]
        public void Test_Range_StrLit_F() {
            Assert.False(rgx.Matching(states, "1-d"));
        }
    }
}