namespace ProjectSA.Gameplay.CraftingItems
{
    using System;
    using UnityEngine;
    using VUDK.Patterns.Initialization.Interfaces;
    using VUDK.Patterns.Pooling;
    using ProjectSA.Gameplay.CraftingItems.Data.ScriptableObjects;
    using VUDK.Generic.Serializable;

    public class Potion : PooledObjectBase, IInit<CraftedPotionData>
    {
        [Header("Potion Graphics Settings")]
        [SerializeField]
        private MeshRenderer _liquidMeshRenderer;
        [SerializeField]
        private Range<float> _fillRange;

        private Material _liquidMaterial;

        public CraftedPotionData PotionData { get; private set; }

        private void Awake()
        {
            CreateLiquidMaterial();
        }

        public void Init(CraftedPotionData arg)
        {
            PotionData = arg;
            SetLiquid(PotionData.PotionColor);
        }

        public bool Check()
        {
            return PotionData != null;
        }

        public void SetLiquid(Color color)
        {
            _liquidMaterial.SetColor("_BaseColor", color);
            _liquidMaterial.SetFloat("_Fill", _fillRange.Random());
        }

        private void CreateLiquidMaterial()
        {
            _liquidMaterial = new Material(_liquidMeshRenderer.material);
            _liquidMeshRenderer.material = _liquidMaterial;
        }
    }
}