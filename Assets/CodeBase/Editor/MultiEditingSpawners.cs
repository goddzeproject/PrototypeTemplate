using System;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(SpawnMarker))]
    public class MultiEditingSpawners : UnityEditor.Editor
    {
        private bool isEdit;
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            if (Selection.objects.Length > 1)
            {
                foreach (var selectedObject in Selection.objects)
                {
                    EditObject(selectedObject);
                }
            }
            else
                EditObject(target);
        }
        
        private void EditObject(Object obj)
        {
            SpawnMarker script = (SpawnMarker)obj;
            // script.enemyTypeId = EnemyTypeId.Lich;
            // script.unitsToSpawn = 1;
            // script.FirstDelay = 15f;

        }
        
    }
}