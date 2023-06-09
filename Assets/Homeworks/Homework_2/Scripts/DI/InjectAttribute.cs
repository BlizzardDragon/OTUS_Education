using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture.DI
{
    [AttributeUsage(AttributeTargets.Method)]
    public class InjectAttribute : Attribute { }
}
