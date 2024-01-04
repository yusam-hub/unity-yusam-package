#if UNITY_EDITOR
using UnityEngine;

namespace YusamPackage
{
    public class YusamComment : MonoBehaviour
    {

        [SerializeField] protected string header = "COMMENT HEADER";
        [Multiline]
        [SerializeField] protected string comment = "Comment value";

        [SerializeField] protected bool inEdit;
    }
}
#endif
