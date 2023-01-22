using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure
{
    // First in script execution order
    [AddComponentMenu("RTS/Infrastructure/Bootstrapper")]
    public class Bootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            RegisterServices();
        }

        private void OnEnable()
        {
            SL.Single<PlayerInput>().Enable();
        }

        private void RegisterServices()
        {
            // Register HeroFactory, UnitFactory and UnitPool
            SL.RegisterSingle(new PlayerInput());
        }
    }
}
