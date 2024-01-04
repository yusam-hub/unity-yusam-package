#if UNITY_EDITOR

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

#endif