using UnityEngine;

namespace CodeBase.Hero.Piano
{
    public class AnimationManager : MonoBehaviour
    {
        public static AnimationManager Instance; // Статическая переменная для доступа к синглтону

        public PianoKeyLogic[] PianoKeyLogics;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Убедитесь, что объект не удаляется при загрузке сцены
            }
            else
                Destroy(gameObject);
        }

        public void PlayAnimKey(int key)
        {
            PianoKeyLogics[key].Play(key);
        }
        
        // public bool IsAnimationPlaying { get; private set; } // Свойство для отслеживания анимации
        //

        //
        // public void StartAnimation() => IsAnimationPlaying = true;
        // public void StopAnimation() => IsAnimationPlaying = false;
    }
}