using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Levels;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.UI.Windows
{
    public class PauseWindow : WindowBase
    {
        public TextMeshProUGUI levelText;
        private ILevelWatcher _levelWatcher;
        
        protected override void Initialize()
        {
            Pause();
        }

        /*protected override void SubscribeUpdates() =>
            Progress.WorldData.LootData.Changed += RefreshLevelText;

        protected override void Cleanup()
        {
            base.Cleanup();
            Progress.WorldData.LootData.Changed -= RefreshLevelText;
        }*/

        private void Pause()
        {
            Time.timeScale = 0;
        }
    }
}