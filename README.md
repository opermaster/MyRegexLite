# Simple Regex(glob) engine on C#, maded for understranding Regex itself!

# Usage 
```c#
MyRegexC regex=new MyRegexC();
bool result = regex.Matching(string pattern,string str);
```
## Example
```c#
regex.IsMatch("abc-def", "abc-def");
regex.IsMatch("\\d\\d-\\d\\d-\\d\\d", "12-34-56");
regex.IsMatch("\\D\\D-\\D\\D", "af-ab");
regex.IsMatch("[abc][abc][abc]", "aaa");
regex.IsMatch("[a-z][0-9][abc][abc]", "a9bc");
```

## Possibilities
1. string literals:      a, b, 1 etc.
2. Meta-Symbols:         \D - any character not a number, \d - any number.
3. Multiples and ranges: [abc], [a-z].
4. Repeats:              one of previous and {n} - n -number of repeats.