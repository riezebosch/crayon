[![build](https://ci.appveyor.com/api/projects/status/8dnbd2u8t737lm21/branch/master?svg=true)](https://ci.appveyor.com/project/riezebosch/crayon/branch/master)
[![nuget](https://img.shields.io/nuget/v/Crayon.svg)](https://www.nuget.org/packages/Crayon/)

# Crayon
An easy peasy tiny library for coloring console output in inline strings using ANSI escape codes.

## Examples

```c#
using static Crayon.Output;

Console.WriteLine(Green($"green {Red($"{Bold("bold")} red")} green"));
Console.WriteLine(Bright.Blue($"Bright {Green("and normal green")}"));
Console.WriteLine(Green($"The difference {Bold("between bold")} and {Bright.Green("bright green")}"));
Console.WriteLine(Bold().Green().Text($"starting green {Red("then red")} must be green again"));
```

![screenshot](screenshot.png)

## Rainbows 🌈

Thanks to [DevinR528](https://github.com/devinRagotzy) we now have rainbows!

```c#
var rainbow = new Rainbow(0.5);
for(var i = 0; i < 15; i++)
{
    Console.WriteLine(rainbow.Next().Bold().Text("rainbow"));
}
```

## How compatible is it?

It works fine on everything except for older Windows versions.

## Credits

My journey for using ANSI codes in C# first brought me here: https://www.jerriepelser.com/blog/using-ansi-color-codes-in-net-console-apps/

The ANSI coloring details was inspired by this blog post: http://www.lihaoyi.com/post/BuildyourownCommandLinewithANSIescapecodes.html

The code for enabling ANSI colors feature on Windows was borrowed from this issue: https://github.com/Microsoft/WSL/issues/1873 

No greater compliment than someone [taking your work for inspiration](https://github.com/silkfire/Pastel). In return I peaked into his code for the RGB support!