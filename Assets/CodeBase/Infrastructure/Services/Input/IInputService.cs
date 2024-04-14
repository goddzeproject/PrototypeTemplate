using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }

        bool IsAtackButtonUp();
        bool IsKeyDownPlaySpace();
        bool IsKeyDownPlayEnter();
        bool IsKeyDownLeftKey();
        bool IsKeyDownRightKey();
        bool IsKeyDownLeftArrow();
        bool IsKeyDownRightArrow();
    }
}