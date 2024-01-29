using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public interface IAssets : IService
    {
        GameObject Initialize(string path);
        GameObject Initialize(string path, Vector3 at);
    }
}