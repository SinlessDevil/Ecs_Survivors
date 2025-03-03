using Entitas;

namespace Code.Gameplay.Features.Boosters
{
    [Game] public class Booster : IComponent { }
    [Game] public class BoosterSpawnTimer : IComponent { public float Value; }
    [Game] public class BoosterTypeIdComponent : IComponent { public BoosterTypeId Value; }
    
    [Game] public class ApplyBooster : IComponent { }
}