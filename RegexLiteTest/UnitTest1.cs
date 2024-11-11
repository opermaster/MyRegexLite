using MyRegex;
using System.Text.RegularExpressions;
namespace RegexLiteTest
{
    public class Tests {
        MyRegexC rgx;
        State[] states;
        [SetUp]
        public void Setup() {
            rgx = new MyRegexC();
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

        [Test]
        public void Test_StrLits_T() {
            Assert.True(rgx.IsMatch("abc-def", "abc-def"));
        }
        [Test]
        public void Test_StrLits_F() {
            Assert.False(rgx.IsMatch("abc-def", "def-abc"));
        }

        [Test]
        public void Test_Meta_Num() {
            Assert.True(rgx.IsMatch("\\d\\d-\\d\\d-\\d\\d", "12-34-56"));
        }
        [Test]
        public void Test_Meta_Char() {
            Assert.True(rgx.IsMatch("\\D\\D-\\D\\D", "af-ab"));
        }
        [Test]
        public void Test_Mul() {
            Assert.True(rgx.IsMatch("[abc][abc][abc]", "aaa"));
            Assert.True(rgx.IsMatch("[abc][abc][abc]", "bbb"));
            Assert.True(rgx.IsMatch("[abc][abc][abc]", "ccc"));
            Assert.True(rgx.IsMatch("[abc][abc][abc]", "abc"));
            Assert.True(rgx.IsMatch("[abc][abc][abc]", "cba"));
        }
        [Test]
        public void Test_Reg() {
            Assert.True(rgx.IsMatch("[a-z][0-9][abc][abc]", "a9bc"));
            Assert.True(rgx.IsMatch("[A-Z][a-z]", "Ba"));
            Assert.False(rgx.IsMatch("[A-Z][a-z]", "ab"));
        }
    }
}