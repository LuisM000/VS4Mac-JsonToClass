using System;
using System.ComponentModel;

namespace VS4Mac.JsonToClass.Model
{
    public enum Feature
    {
        [Description("complete")]
        Complete,

        [Description("attributes-only")]
        AttributesOnly,

        [Description("just-types")]
        JustTypes
    }
}
