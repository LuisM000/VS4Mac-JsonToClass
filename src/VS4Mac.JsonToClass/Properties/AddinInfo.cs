using System;
using System.Runtime.InteropServices;
using Mono.Addins;
using Mono.Addins.Description;

[assembly: Addin(
    "JsonToClass",
    Namespace = "VS4Mac.JsonToClass",
    Version = "0.2"
)]

[assembly: AddinName("Json to class")]
[assembly: AddinCategory("IDE extensions")]
[assembly: AddinDescription("Addin to convert JSON to class")]
[assembly: AddinAuthor("Luis Marcos Rivera")]

[assembly: CLSCompliant(false)]
[assembly: ComVisible(false)]