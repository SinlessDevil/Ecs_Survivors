using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Armaments
{
    [Game] public class Armament : IComponent { }
    [Game] public class TargetLimit : IComponent { public int Value; }
    [Game] public class Processed : IComponent { }
    [Game] public class Target : IComponent { public GameEntity Value; }
    [Game] public class BounceRate : IComponent { public float Value; }
}