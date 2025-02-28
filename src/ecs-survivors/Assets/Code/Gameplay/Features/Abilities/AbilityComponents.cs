using Entitas;

namespace Code.Gameplay.Features.Abilities
{
    [Game] public class AbilityIdComponent : IComponent { public AbilityId Value; }
    [Game] public class VegetableBoltAbility : IComponent { }
    [Game] public class RadiatingCogBoltAbility : IComponent { }
    [Game] public class BouncingCoinAbility : IComponent { }
    [Game] public class ScatteringRuneStoneAbility : IComponent { }
}