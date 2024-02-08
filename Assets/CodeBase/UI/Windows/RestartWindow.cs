using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Levels;
using TMPro;
using UnityEngine.Serialization;

namespace CodeBase.UI.Windows
{
    public class RestartWindow : WindowBase
    {
        public TextMeshProUGUI levelText;
        private ILevelWatcher _levelWatcher;
        
        protected override void Initialize()
        {
            _levelWatcher = AllServices.Container.Single<ILevelWatcher>();
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
            levelText.text = _levelWatcher.ReturnCurrentLevel().ToString();
        }
    }
}