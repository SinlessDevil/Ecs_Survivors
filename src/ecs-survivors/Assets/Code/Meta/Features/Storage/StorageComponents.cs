using Entitas;

namespace Code.Meta.Features.Storage
{
    [Meta] public class Storage : IComponent { }
    
    [Meta] public class Gold : IComponent { public float Value; }
    [Meta] public class GoldPerSecond : IComponent { public float Value; }
    
    [Meta] public class Gem : IComponent { public float Value; }
    [Meta] public class GemPerSecond : IComponent { public float Value; }
    [Meta] public class GemChance : IComponent { public float Value; }
}