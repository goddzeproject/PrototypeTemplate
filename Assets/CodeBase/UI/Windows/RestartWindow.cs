﻿using CodeBase.Infrastructure.Services;
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
            DoSomething();
        }

        /*protected override void SubscribeUpdates() =>
            Progress.WorldData.LootData.Changed += RefreshLevelText;

        protected override void Cleanup()
        {
            base.Cleanup();
            Progress.WorldData.LootData.Changed -= RefreshLevelText;
        }*/

        private void DoSomething()
        {
        }
    }
}