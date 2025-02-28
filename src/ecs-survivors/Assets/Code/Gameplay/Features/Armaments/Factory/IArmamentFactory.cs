using UnityEngine;

namespace Code.Gameplay.Features.Armaments.Factory
{
    public interface IArmamentFactory
    {
        GameEntity CreateVegetableBolt(int level, Vector3 at);
        GameEntity CreateRadiatingCogBolt(int level, Vector3 at);
        GameEntity CreateBouncingCoinBolt(int level, Vector3 at);
    }
}