# Simple Regex(glob) engine on C#

# Usage 
```c#
MyRegexC regex=new MyRegexC();
regex.Matching(pattern,str);
```
## Example
```c#
regex.IsMatch("abc-def", "abc-def");
regex.IsMatch("\\d\\d-\\d\\d-\\d\\d", "12-34-56");
regex.IsMatch("\\D\\D-\\D\\D", "af-ab");
regex.IsMatch("[abc][abc][abc]", "aaa");
regex.IsMatch("[a-z][0-9][abc][abc]", "a9bc");
```