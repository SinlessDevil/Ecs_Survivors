using Code.Infrastructure.View.Registrars;
using UnityEngine;

namespace Code.Gameplay.Common.Registrars
{
    public class TransformRegistrar : EntityComponentRegistrar
    {
        public override void RegisterComponents()
        {
            Debug.Log("1 " + Entity + " fffffffffffffff " + transform);
            Entity.AddTransform(transform);
        }

        public override void UnregisterComponents()
        {
            Entity.RemoveTransform();
        }
    }
}