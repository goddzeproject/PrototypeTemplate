using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Hero.Piano
{
    public class KeysStorage : MonoBehaviour
    {
        [FormerlySerializedAs("PianoKeys")] public Transform[] TargetKeys;
    }
}