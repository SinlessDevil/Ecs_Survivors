using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Time;
using Code.Infrastructure.Serialization;
using Code.Progress.Data;
using Code.Progress.Provider;
using UnityEngine;

namespace Code.Progress.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "PlayerProgress";

        private readonly MetaContext _metaContext;
        private readonly IProgressProvider _progressProvider;
        private readonly ITimeService _timeService;

        public SaveLoadService(MetaContext metaContext, 
            IProgressProvider progressProvider,
            ITimeService timeService)
        {
            _metaContext = metaContext;
            _progressProvider = progressProvider;
            _timeService = timeService;
        }

        public void CreateProgress()
        {
            _progressProvider.SetProgressData(new ProgressData()
            {
                LastSimulationTickTime = _timeService.UtcNow
            });
        }

        public bool HasSaveProgress => PlayerPrefs.HasKey(ProgressKey);
        
        public void SaveProgress()
        {
            PreserveMetaEntitiesSnapshots();
            PlayerPrefs.SetString(ProgressKey, JsonUtility.ToJson(_progressProvider.ProgressData));
            PlayerPrefs.Save();
        }
        
        public void LoadProgress()
        {
            HydrateProgress(PlayerPrefs.GetString(ProgressKey));
        }


        private void HydrateProgress(string serializedProgress)
        {
            _progressProvider.SetProgressData(serializedProgress.FromJson<ProgressData>());
            HydrateMetaEntities();
        }

        private void HydrateMetaEntities()
        {
            List<EntitySnapshot> shapshots = _progressProvider.EntityData.MetaEntitiesSnapshots;
            foreach (EntitySnapshot snapshot in shapshots)
            {
                _metaContext
                    .CreateEntity()
                    .HydrateWith(snapshot);
            }
        }

        private void PreserveMetaEntitiesSnapshots()
        {
            _progressProvider.EntityData.MetaEntitiesSnapshots = _metaContext
                .GetEntities()
                .Where(RequiresSave)
                .Select(e => e.AsSavedEntity())
                .ToList();
        }

        private static bool RequiresSave(MetaEntity e)
        {
            return e.GetComponents().Any(c => c is ISavedComponent);
        }
    }
}