using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Helper
{
    public static Regex EngNameRegex = new Regex(@"[^A-Za-z0-9@_.!#$%]");
    public static Regex TitleRegex = new Regex(@"\{t}|{title}");
    public static Regex ItemNameRegex = new Regex(@"\{item}");
    public static Regex CountRegex = new Regex(@"\{c}|{count}");
}
