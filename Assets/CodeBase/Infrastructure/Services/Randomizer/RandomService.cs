using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Infrastructure.Services.Randomizer
{
  public class RandomService : IRandomService
  {
    public int Next(int min, int max) =>
      Random.Range(min, max);

    public Vector3 Position(int minValue, int maxValue) => 
      new(Next(minValue, maxValue), 0, Next(minValue, maxValue));
  }
}