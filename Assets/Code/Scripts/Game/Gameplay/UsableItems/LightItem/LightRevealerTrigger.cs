namespace ProjectSA.Gameplay.UsableItems.LightItem
{
    using UnityEngine;

    public class LightRevealerTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out LightHideObject lightHideObject))
                lightHideObject.ShowObject();
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out LightHideObject lightHideObject))
                lightHideObject.HideObject();
        }
    }
}