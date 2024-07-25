namespace ProjectSA.Player.Cauldron
{
    using UnityEngine;

    public class CauldronGraphicsController : MonoBehaviour
    {
        public void ChangeColor(Color color) // TODO: Just for testing purposes
        {
            GetComponent<Renderer>().material.color = color;
        }
    }
}