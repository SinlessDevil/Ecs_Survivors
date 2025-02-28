using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.Effects.Factory;
using Entitas;

namespace Code.Gameplay.Features.EffectApplication.Systems
{
    public class ApplyEffectsOnTargetsSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IEffectFactory _effectFactory;
        private readonly IGroup<GameEntity> _damageDealers;

        public ApplyEffectsOnTargetsSystem(GameContext game, IEffectFactory effectFactory)
        {
            _game = game;
            _effectFactory = effectFactory;
            _damageDealers = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.TargetsBuffer,
                    GameMatcher.EffectSetups));
        }
    
        public void Execute()
        {
            foreach (GameEntity entity in _damageDealers)
            foreach (int targetId in entity.TargetsBuffer)
            foreach (EffectSetup setup in entity.EffectSetups)
            {
                _effectFactory.CreateEffect(setup, ProducerId(entity), targetId);
            }
        }
        
        private int ProducerId(GameEntity entity)
        {
            return entity.hasProducerId ? entity.ProducerId : entity.Id;
        }
    }
}