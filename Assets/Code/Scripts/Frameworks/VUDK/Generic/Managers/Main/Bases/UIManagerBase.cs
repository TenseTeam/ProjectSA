namespace VUDK.Generic.Managers.Main.Bases
{
    using UnityEngine;

    [DefaultExecutionOrder(-895)]
    public abstract class UIManagerBase : MonoBehaviour
    {
        public void EnableCursor(CursorLockMode cursorLockMode = CursorLockMode.None)
        {
            Cursor.visible = true;
            Cursor.lockState = cursorLockMode;
        }
        
        public void DisableCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}