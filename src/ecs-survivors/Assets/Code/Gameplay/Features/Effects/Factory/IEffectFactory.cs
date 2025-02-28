namespace Code.Gameplay.Features.Effects.Factory
{
    public interface IEffectFactory
    {
        GameEntity CreateEffect(EffectSetup setup, int producerId, int targetId);
        GameEntity CreateDamage(int producerId, int targetId, float damage);
    }
}