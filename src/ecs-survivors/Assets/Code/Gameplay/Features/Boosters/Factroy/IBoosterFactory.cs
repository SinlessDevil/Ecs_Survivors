using UnityEngine;

namespace Code.Gameplay.Features.Boosters.Factroy
{
    public interface IBoosterFactory
    {
        GameEntity CreateBooster(BoosterTypeId typeId, Vector2 at);
    }
}