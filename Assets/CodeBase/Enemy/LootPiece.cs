using System.Collections;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LootPiece : MonoBehaviour
    {
        public GameObject LoopPrefab;
        public GameObject PickupFxPrefab;
        public TextMeshPro LootText;
        public GameObject PickupPopup;
        
        
        private Loot _loot;
        private bool _picked;
        private WorldData _worldData;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
        }

        public void Initialize(Loot loot)
        {
            _loot = loot;
        }

        private void OnTriggerEnter(Collider other) => PickUp();

        private void PickUp()
        {
            if (_picked)
                return;
            
            _picked = true;

            UpdateWorldData();
            HidePrefab();
            GameObject VFX = PlayPickupFx();
            ShowText();
            StartCoroutine(StartDestroyTimer(VFX));
        }

        private void UpdateWorldData()
        {
            _worldData.LootData.Collect(_loot);
        }

        private void HidePrefab() => 
            LoopPrefab.SetActive(false);

        private IEnumerator StartDestroyTimer(GameObject VFX)
        {
            yield return new WaitForSeconds(1.5f);
            
            Destroy(gameObject);
            Destroy(VFX);
        }

        private GameObject PlayPickupFx()
        {
            GameObject VFX = Instantiate(PickupFxPrefab, transform.position, Quaternion.identity);
            return VFX;
        }

        

        private void ShowText()
        {
            LootText.text = $"{_loot.Value}";
            PickupPopup.SetActive(true);
        }
    }
}