using MonoBehaviour = UnityEngine.MonoBehaviour;
using Quaternion = UnityEngine.Quaternion;
using Time = UnityEngine.Time;
using Vector3 = UnityEngine.Vector3;

namespace CodeBase.Logic.Enemy.Behaviour
{
    public class SimpleMovement : MonoBehaviour
    {
        private Vector3 Direction = Vector3.forward;

        private void Start()
        {
            transform.rotation = Quaternion.LookRotation(Vector3.right);
        }

        private void Update()
        {
            transform.Translate(Direction * Time.deltaTime);
        }
    }
}