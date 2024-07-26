namespace ProjectSA.Gameplay.InteractSystem.Interactables.Base
{
    using System.Collections.Generic;
    using UnityEngine;

    public class GameInteractableGraphicsController : MonoBehaviour
    {
        [Header("Highlight settings")]
        [SerializeField]
        private float _highlightScale = 1.025f;
        
        private MeshRenderer[] _meshRenderers;
        private List<Material> _highlightMaterials = new List<Material>();
        
        public void CreateHighlightMaterial()
        {
            _meshRenderers = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in _meshRenderers)
            {
                Material highlightMaterial = new Material(Shader.Find("Shader Graphs/shd_Highlight"));
                _highlightMaterials.Add(highlightMaterial);
                renderer.materials = new Material[]
                {
                    renderer.material,
                    highlightMaterial
                };
            }
            
            DisableHighlight();
        }
        
        public void EnableHighlight()
        {
            foreach (Material highlightMaterial in _highlightMaterials)
                highlightMaterial.SetFloat("_Scale", _highlightScale);
        }
        
        public void DisableHighlight()
        {
            foreach (Material highlightMaterial in _highlightMaterials)
                highlightMaterial.SetFloat("_Scale", 0f);
        }
    }
}