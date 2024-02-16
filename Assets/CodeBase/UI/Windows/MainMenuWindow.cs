namespace CodeBase.UI.Windows
{
    public class MainMenuWindow : WindowBase
    {
        protected override void Initialize()
        {
            //_levelWatcher = AllServices.Container.Single<ILevelWatcher>();
            RefreshLevelText();
        }

        /*protected override void SubscribeUpdates() =>
            Progress.WorldData.LootData.Changed += RefreshLevelText;

        protected override void Cleanup()
        {
            base.Cleanup();
            Progress.WorldData.LootData.Changed -= RefreshLevelText;
        }*/

        private void RefreshLevelText()
        {
        }
    }
}