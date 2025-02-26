using Code.Common.Entity;
using Code.Infrastructure.Identifiers;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.View
{
    public class SelfInitializedEntityView : MonoBehaviour
    {
        public EntityBehavior EntityBehavior;
        
        private IIdentifierService _identifierService;

        [Inject]
        private void Construct(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }
        
        private void Awake()
        {
            Debug.Log("2");
            GameEntity entity = CreateEntity.Empty()
                .AddId(_identifierService.Next());
            
            EntityBehavior.SetEntity(entity);
        }
    }
}