using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YusamPackage
{
    public enum ReflectionItemType
    {
        NULL,
        INSTANCE,
        CLASS,
        METHOD,
        COLLECTION,
        MethodAndCollection,
        PRIMITIVEINSTANCE,
        InstanceOrClass
    }
}