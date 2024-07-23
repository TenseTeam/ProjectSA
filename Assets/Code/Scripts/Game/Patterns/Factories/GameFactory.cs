namespace ProjectSA.Patterns.Factories
{
    using Managers;
    using VUDK.Generic.Managers.Main;

    /// <summary>
    /// Responsible for creating game objects.
    /// </summary>
    public static class GameFactory
    {
        private static PSAGamePoolsKeys GameGamePoolsKeys => MainManager.Ins.GamePoolsKeys as PSAGamePoolsKeys;
    }
}