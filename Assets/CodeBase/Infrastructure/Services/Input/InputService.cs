using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const string Button = "Fire";
        
        private const KeyCode Play = KeyCode.Space;
        private const KeyCode Left = KeyCode.A;
        private const KeyCode Right = KeyCode.D;

        public abstract Vector2 Axis { get; }

        public bool IsAtackButtonUp() => 
            SimpleInput.GetButtonUp(Button);

        public bool IsKeyDownPlay() => 
            SimpleInput.GetKeyDown(Play);

        public bool IsKeyDownLeft() => 
            SimpleInput.GetKeyDown(Left);

        public bool IsKeyDownRight() => 
            SimpleInput.GetKeyDown(Right);

        protected static Vector2 SimpleInputAxis() => 
            new(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}