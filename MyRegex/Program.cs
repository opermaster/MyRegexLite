using System.Runtime.CompilerServices;
using System.Text;

namespace MyRegex
{
    public enum STATE_TYPE
    {
        STRING_LIT,
        META,
        MULTITUDE,
        RANGE,
    }
    public class State
    {
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

        B_SLASH, // \

        S_OP,    // [
        S_CP,    // ]

        C_OP,    // {
        C_CP,    // }

    }
    public class Token
    {
        public TOKEN_TYPE type;
        public string value;

        public Token(TOKEN_TYPE _type, string _value) {
            type = _type;
            value = _value;
        }
    }

    public class MyRegexC
    {
        Token[] LexString(string pattern) {
            Token[] tokens = new Token[pattern.Length];
            for (int i = 0; i < pattern.Length; i++) {
                switch (pattern[i]) {
                    case '\\':
                        tokens[i] = new Token(TOKEN_TYPE.B_SLASH, "\\");
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
        State[] ParseTokens(Token[] tokens) {
            List<State> d_states = new List<State>();
            int tp = 0;
            Token temp;
            int iter;
            StringBuilder sb;
            while (tp < tokens.Length) {
                Token t = tokens[tp];
                switch (t.type) {
                    case TOKEN_TYPE.CHAR:
                        d_states.Add(new State(STATE_TYPE.STRING_LIT, t.value));
                        break;
                    case TOKEN_TYPE.B_SLASH:
                        if (tokens[++tp].type == TOKEN_TYPE.CHAR) d_states.Add(new State(STATE_TYPE.META, "\\" + tokens[tp].value));
                        else d_states.Add(new State(STATE_TYPE.META, "\\D"));
                        break;
                    case TOKEN_TYPE.S_OP:
                        iter = tp;
                        temp = tokens[iter];
                        sb = new StringBuilder();
                        if (tokens[iter+2].value!="-") {
                            do {
                                sb.Append(temp.value);
                                iter++;
                                temp = tokens[iter];
                            } while (iter < tokens.Length && temp.type != TOKEN_TYPE.S_CP);
                            sb.Remove(0, 1);
                            d_states.Add(new State(STATE_TYPE.MULTITUDE, sb.ToString()));
                            tp = iter;
                        }else {
                            sb.Append(tokens[iter + 1].value);
                            sb.Append(tokens[iter + 3].value);
                            d_states.Add(new State(STATE_TYPE.RANGE, sb.ToString()));
                            tp += 3;
                        }

                        break;
                }
                
                tp++;
            }
            //foreach(var item in d_states) {
            //    Console.WriteLine($"{item.type} {item.value}");
            //}
            return d_states.ToArray();
        }
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
        public bool IsMatch(string pattern, string str) {
            return Matching(ParseTokens(LexString(pattern)), str);
        }
    }
    class Program
    {


        static void Main(string[] args) {
            Console.WriteLine("Hello, World!");
            MyRegexC regex = new MyRegexC();
            Console.WriteLine(regex.IsMatch("[abc][abc][abc]", "aaa"));
            Console.WriteLine(regex.IsMatch("[abc][abc][abc]", "bbb"));
            Console.WriteLine(regex.IsMatch("[abc][abc][abc]", "ccc"));
            Console.WriteLine(regex.IsMatch("[abc][abc][abc]", "abc"));
            Console.WriteLine(regex.IsMatch("[abc][abc][abc]", "cba"));

            Console.WriteLine(regex.IsMatch("[a-z][0-9]", "a9"));
        }

    }
}
