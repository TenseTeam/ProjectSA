#if UNITY_EDITOR
namespace ProjectSA
{
    using UnityEngine;

    public class DebugTesting : MonoBehaviour
    {
        [ContextMenu("Delete All PlayerPrefs")]
        public void DeleteAllPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
#endif