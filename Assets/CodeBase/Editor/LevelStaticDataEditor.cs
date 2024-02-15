using System.Linq;
using System.Xml;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniqueId = CodeBase.Logic.UniqueId;

[CustomEditor(typeof(LevelStaticData))]
public class LevelStaticDataEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LevelStaticData levelData = (LevelStaticData)target;

        if (GUILayout.Button("Collect"))
        {
            levelData.EnemySpawners =
                FindObjectsOfType<SpawnMarker>()
                    .Select(x =>
                        new EnemySpawnerData(x.GetComponent<UniqueId>().Id,
                            x.enemyTypeId,
                            x.transform.position,
                            x.SpawnDirection,
                            x.unitsToSpawn,
                            x.SpawnCooldown,
                            x.FirstDelay))
                    .ToList();
        }

        GUILayout.Space(20f);

        if (GUILayout.Button("Set Up"))
        {
            //return;
            string prefabPath = "Assets/Resources/Enemies/Spawners/SpawnMarker.prefab"; 
            SpawnMarker spawnMarker = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(SpawnMarker)) as SpawnMarker;
            
            foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
            {
                Instantiate(spawnMarker, spawnMarker.transform.position, Quaternion.identity);

                spawnMarker.GetComponent<UniqueId>().Id = spawnerData.Id;
                spawnMarker.enemyTypeId = spawnerData.enemyTypeId;
                spawnMarker.transform.position = spawnerData.Position;
                spawnMarker.SpawnDirection = spawnerData.SpawnDirection;
                spawnMarker.unitsToSpawn = spawnerData.AmountEnemies;
                spawnMarker.SpawnCooldown = spawnerData.SpawnCooldown;
                spawnMarker.FirstDelay = spawnerData.FirstDelay;
            }
        }

        GUILayout.Space(5f);

        if (GUILayout.Button("Clear"))
        {
            var spawnMarkers = FindObjectsOfType<SpawnMarker>().ToList();
            spawnMarkers.ForEach(x => DestroyImmediate(x.gameObject));
        }

        EditorUtility.SetDirty(target);
    }
}