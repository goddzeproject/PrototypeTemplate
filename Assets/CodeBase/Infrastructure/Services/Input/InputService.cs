using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const string Button = "Fire";
        
        private const KeyCode PlaySpace = KeyCode.Space;
        private const KeyCode LeftKey = KeyCode.A;
        private const KeyCode RightKey = KeyCode.D;
        private const KeyCode PlayEnter = KeyCode.Return;
        private const KeyCode LeftArrow = KeyCode.LeftArrow;
        private const KeyCode RightArrow = KeyCode.RightArrow;

        public abstract Vector2 Axis { get; }

        public bool IsAtackButtonUp() => 
            SimpleInput.GetButtonUp(Button);

        public bool IsKeyDownPlaySpace() => 
            SimpleInput.GetKeyDown(PlaySpace);

        public bool IsKeyDownPlayEnter() => 
            SimpleInput.GetKeyDown(PlayEnter);

        public bool IsKeyDownLeftKey() => 
            SimpleInput.GetKeyDown(LeftKey);

        public bool IsKeyDownRightKey() => 
            SimpleInput.GetKeyDown(RightKey);

        public bool IsKeyDownLeftArrow() => 
            SimpleInput.GetKeyDown(LeftArrow);

        public bool IsKeyDownRightArrow() => 
            SimpleInput.GetKeyDown(RightArrow);

        protected static Vector2 SimpleInputAxis() => 
            new(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}