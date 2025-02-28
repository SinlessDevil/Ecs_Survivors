namespace Code.Gameplay.Features.Abilities.Factory
{
    public interface IAbilityFactory
    {
        GameEntity CreateVegetableBoltAbility(int level);
        GameEntity CreateRadiatingCogBoltAbility(int level);
        GameEntity CreateBouncingCoinBoltAbility(int level);
    }
}