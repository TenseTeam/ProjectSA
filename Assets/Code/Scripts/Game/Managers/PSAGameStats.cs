namespace ProjectSA.Managers
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;

    public class PSAGameStats : GameStats
    {
        [field: Header("Gameover Messages")]
        [field: SerializeField, TextArea(2, 10)]
        public string GameoverDeathMessage { get; private set; }
        [field: SerializeField, TextArea(2, 10)]
        public string GameoverFailedRequestMessage { get; private set; }
        [field: SerializeField, TextArea(2, 10)]
        public string GameoverCraftedAllPotionsMessage { get; private set; }
        [field: SerializeField, TextArea(2, 10)]
        public string GameVictoryMessage { get; private set; }
    }
}