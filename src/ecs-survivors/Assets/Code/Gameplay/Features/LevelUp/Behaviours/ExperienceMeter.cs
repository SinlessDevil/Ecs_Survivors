using UnityEngine;
using UnityEngine.UI;

namespace Code.Gameplay.Features.LevelUp.Behaviours
{
    public class ExperienceMeter : MonoBehaviour
    {
        public Slider ProgressBar;
        public Image Fill;
        
        public void SetExperience(float experience, float experienceToNextLevel)
        {
            Fill.type = Image.Type.Tiled;
            ProgressBar.value = experience / experienceToNextLevel;
        }
    }
}