using System.Runtime.CompilerServices;

namespace MyRegex
{
    public enum STATE_TYPE {
        STRING_LIT,
        META,
        MULTITUDE,
        RANGE,
    }
    public class State {
        public string value;
        public STATE_TYPE type;
        public State(STATE_TYPE _type, string _value) { 
            value = _value;
            type = _type;
        }
        public State(STATE_TYPE _type) {
            type = _type;
        }
    }

    public enum TOKEN_TYPE
    {
        CHAR,

        B_SLASH,

        S_OP,
        S_CP,

        C_OP,
        C_CP,

        OP,
        CP,
    }
    public class Token {
        public TOKEN_TYPE type;
        public string value;

        public Token(TOKEN_TYPE _type, string _value) {
            type = _type;
            value = _value;
        }
    }

    public class MyRegexC {
        public bool Matching(State[] states, string str) {
            bool is_match = true;
            for (int i = 0; i < states.Length; i++) {
                State st = states[i];
                switch (st.type) {
                    case STATE_TYPE.STRING_LIT:
                        is_match = str[i] == st.value[0];
                        break;
                    case STATE_TYPE.MULTITUDE:
                        bool temp = false;
                        foreach (char item in st.value) {
                            temp = item == str[i];
                            if (temp) break;
                        }
                        is_match = temp;
                        break;
                    case STATE_TYPE.META:
                        switch (st.value) {
                            case "\\d":
                                is_match = char.IsDigit(str[i]);
                                break;
                            case "\\D":
                                is_match = !char.IsDigit(str[i]);
                                break;
                        }
                        break;
                    case STATE_TYPE.RANGE:
                        is_match = str[i] >= st.value[0] && str[i] <= st.value[1];
                        break;
                }
                if (!is_match) break;
            }
            return is_match;
        }
        static public bool IsMatch(string pattern,string str) {

            return false;
        }
    }
    class Program
    {
        static public Token[] LexString(string pattern) {
            Token[] tokens = new Token[pattern.Length];
            for (int i = 0; i < pattern.Length; i++) {
                switch (pattern[i]) {
                    case '\\':
                        tokens[i] = new Token(TOKEN_TYPE.B_SLASH, "\\");
                        break;
                    case '(':
                        tokens[i] = new Token(TOKEN_TYPE.OP, "(");
                        break;
                    case ')':
                        tokens[i] = new Token(TOKEN_TYPE.CP, ")");
                        break;
                    case '[':
                        tokens[i] = new Token(TOKEN_TYPE.S_OP, "[");
                        break;
                    case ']':
                        tokens[i] = new Token(TOKEN_TYPE.S_CP, "]");
                        break;
                    default:
                        tokens[i] = new Token(TOKEN_TYPE.CHAR, pattern[i].ToString());
                        break;
                }
            }
            return tokens;
        }

        
        static void Main(string[] args) {
            Console.WriteLine("Hello, World!");
            MyRegexC regex=new MyRegexC();
            var result = LexString("\\d\\d-\\d\\d");
            State[] states =[
                new State(STATE_TYPE.META,"\\D"),
                new State(STATE_TYPE.META, "\\d"),
                new State(STATE_TYPE.STRING_LIT,"-"),
                new State(STATE_TYPE.META, "\\d"),
                new State(STATE_TYPE.META, "\\d"),
            ];
            State[] states1 = [
                new State(STATE_TYPE.MULTITUDE, "abc"),
                new State(STATE_TYPE.STRING_LIT, "-"),
                new State(STATE_TYPE.MULTITUDE, "abc"),
            ];
            State[] states2 = [
                new State(STATE_TYPE.RANGE, "az"),
                new State(STATE_TYPE.STRING_LIT, "-"),
                new State(STATE_TYPE.RANGE, "09"),
            ];
            Console.WriteLine("___________");
            Console.WriteLine(regex.Matching(states, "a2-34"));
            Console.WriteLine(regex.Matching(states, "22-34"));
            Console.WriteLine(regex.Matching(states1, "ab-34"));
            Console.WriteLine(regex.Matching(states1, "b-a"));
            Console.WriteLine(regex.Matching(states2, "a-3"));
            Console.WriteLine(regex.Matching(states2, "9-h"));
        }
       
    }
}
