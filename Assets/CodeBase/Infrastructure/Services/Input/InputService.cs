using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const string Button = "Fire";
        private const KeyCode Play = KeyCode.Space;

        public abstract Vector2 Axis { get; }

        public bool IsAtackButtonUp() => 
            SimpleInput.GetButtonUp(Button);

        public bool IsKeyDownPlay() => 
            SimpleInput.GetKeyDown(Play);

        protected static Vector2 SimpleInputAxis() => 
            new(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}