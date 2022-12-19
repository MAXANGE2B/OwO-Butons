using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class dictionnary : MonoBehaviour {

    public static readonly string[] MorseCodeWords =
    {
        "furries",
        "fursuit",
        "fursona",
        "",
        "",
        ""
    };

    public static readonly string[] _morseCode = new string[26]
    {
        ".-",
        "-...",
        "-.-.",
        "-..",
        ".",
        "..-.",
        "--.",
        "....",
        "..",
        ".---",
        "-.-",
        ".-..",
        "--",
        "-.",
        "---",
        ".--.",
        "--.-",
        ".-.",
        "...",
        "-",
        "..-",
        "...-",
        ".--",
        "-..-",
        "-.--",
        "--.."
        };

    public static readonly string[][] MorseToOwO = new string[][] 
    {
       new string[] {"123", "213", "231", "321", "312", "132" },
       new string[] {"213", "123", "321", "312", "132", "231" },
       new string[] {"321", "132", "213", "132", "231", "123" },
       new string[] {"231", "321", "312", "123", "132", "213" },
       new string[] {"312", "123", "132", "231", "321", "213" },
       new string[] {"132", "321", "213", "312", "123", "231" },
       new string[] {"312", "213", "231", "132", "321", "123" },
       new string[] {"132", "231", "321", "123", "231", "312" },
       new string[] {"321", "123", "231", "132", "312", "213" },
       new string[] {"312", "213", "132", "321", "231", "123" },
       new string[] {"321", "231", "123", "312", "213", "132" },
       new string[] {"123", "312", "132", "321", "231", "213" },
       new string[] {"213", "321", "231", "123", "312", "132" },
       new string[] {"312", "321", "123", "213", "132", "231" },
       new string[] {"132", "231", "213", "312", "321", "123" },
       new string[] {"231", "321", "132", "123", "213", "312" },
       new string[] {"213", "312", "123", "231", "321", "132" },
       new string[] {"231", "321", "132", "213", "312", "123" }
    };

    public static readonly string[][] OwOToButtons = new string[][]
    {
       new string[] {"O", "w", "O" }, 
       new string[] {"U", "w", "U" },
       new string[] {"Q", "w", "Q" },
       new string[] {"Q", "w", "O" },
       new string[] {"O", "w", "Q" },
       new string[] {"Q", "w", "U" },
       new string[] {"U", "w", "Q" },
       new string[] {"O", "w", "U" },
       new string[] {"U", "w", "O" },
       new string[] {"O", "v", "O" },
       new string[] {"U", "v", "U" },
       new string[] {"Q", "v", "Q" },
       new string[] {"Q", "v", "O" },
       new string[] {"O", "v", "Q" },
       new string[] {"Q", "v", "U" },
       new string[] {"U", "v", "Q" },
       new string[] {"O", "v", "U" },
       new string[] {"U", "v", "O" }
    };
    public static readonly string[] OwO =
    {
        "OwO",
        "UwU",
        "QwQ",
        "QwO",
        "OwQ",
        "QwU",
        "UwQ",
        "OwU",
        "UwO",
        "OvO",
        "UvU",
        "QvQ",
        "QvO",
        "OvQ",
        "QvU",
        "UvQ",
        "OvU",
        "UvO"
    };
}
