using System.Linq;
using System.Xml;
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
                        new EnemySpawnerData(x.GetComponent<UniqueId>().Id, x.enemyTypeId, x.transform.position, x.SpawnPosition, x.unitsToSpawn,x.SpawnCooldown))
                    .ToList();
        }
        
        EditorUtility.SetDirty(target);
    }
}