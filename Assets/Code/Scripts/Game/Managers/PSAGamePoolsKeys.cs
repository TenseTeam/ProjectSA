namespace ProjectSA.Managers
{
    using UnityEngine;
    using VUDK.Features.Main.ScriptableKeys;
    using VUDK.Generic.Managers.Main.Bases;

    public class PSAGamePoolsKeys : GamePoolsKeysBase
    {
        [field: SerializeField]
        public ScriptableKey PotionPoolKey { get; private set; }
    }
}