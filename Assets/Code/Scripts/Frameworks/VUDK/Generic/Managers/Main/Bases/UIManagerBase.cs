namespace VUDK.Generic.Managers.Main.Bases
{
    using UnityEngine;

    [DefaultExecutionOrder(-895)]
    public abstract class UIManagerBase : MonoBehaviour
    {
        public static bool IsCursorEnabled { get; private set; }

        public void EnableCursor(CursorLockMode cursorLockMode = CursorLockMode.None)
        {
            IsCursorEnabled = true;
            Cursor.visible = true;
            Cursor.lockState = cursorLockMode;
        }
        
        public void DisableCursor()
        {
            IsCursorEnabled = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}