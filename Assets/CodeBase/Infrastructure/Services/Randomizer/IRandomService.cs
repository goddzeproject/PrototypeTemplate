using UnityEngine;

namespace CodeBase.Infrastructure.Services.Randomizer
{
  public interface IRandomService : IService
  {
    int Next(int minValue, int maxValue);
    Vector3 Position(int minValue, int maxValue);
  }
}