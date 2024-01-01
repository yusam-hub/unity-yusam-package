using UnityEngine;

namespace YusamPackage
{
    public class YusamComment : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] protected string header = "COMMENT HEADER";
        [Multiline]
        [SerializeField] protected string comment = "Comment value";

        [SerializeField] protected bool inEdit;

#endif
    }
}