using System;
using System.ComponentModel;

namespace VS4Mac.JsonToClass.Model
{
    public enum AnyType
    {
        [Description("object")]
        Object,
        [Description("dynamic")]
        Dynamic
    }
}
