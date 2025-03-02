using UnityEngine;

namespace Code.Gameplay.Common.Visuals.StatusVisuals
{
    public class StatusVisuals : MonoBehaviour, IStatusVisuals
    {
        private static readonly int ColorProperty = Shader.PropertyToID("_Color");
        private static readonly int ColorIntensityProperty = Shader.PropertyToID("_Intensity");
        private static readonly int OutlineSizeProperty = Shader.PropertyToID("_OutlineSize");
        private static readonly int OutlineColorProperty = Shader.PropertyToID("_OutlineColor");
        private static readonly int OutlineSmoothnessProperty = Shader.PropertyToID("_OutlineSmoothness");

        public Renderer Renderer;
        public Animator Animator;

        public StatusEffect FreezeEffect = new()
        {
            Color = new StatusEffectColor(new Color32(56, 163, 190, 255), 1f),
            OutlineSize = 3,
            OutlineSmoothness = 8,
            AffectsAnimator = true,
            AnimatorSpeed = 0
        };

        public StatusEffectColor PoisonEffect = new(new Color32(0, 255, 0, 255), 0.6f);
        public StatusEffectColor SpeedEffect = new(new Color32(255, 255, 0, 255), 0.6f);
        public StatusEffectColor MaxHpEffect = new(new Color32(255, 0, 0, 255), 0.6f);

        public StatusEffect InvulnerabilityEffect = new()
        {
            Color = new StatusEffectColor(new Color32(255, 255, 255, 255), 1f),
            OutlineSize = 3,
            OutlineSmoothness = 8,
            AffectsAnimator = true,
            AnimatorSpeed = 0
        };

        private void ApplyEffect(StatusEffect effect)
        {
            Renderer.material.SetColor(OutlineColorProperty, effect.Color.Color);
            Renderer.material.SetFloat(OutlineSizeProperty, effect.OutlineSize);
            Renderer.material.SetFloat(OutlineSmoothnessProperty, effect.OutlineSmoothness);

            if (effect.AffectsAnimator)
            {
                Animator.speed = effect.AnimatorSpeed;
            }
        }

        private void ApplyEffect(StatusEffectColor effectColor)
        {
            Renderer.material.SetColor(ColorProperty, effectColor.Color);
            Renderer.material.SetFloat(ColorIntensityProperty, effectColor.Intensity);
        }

        private void UnapplyEffect()
        {
            Renderer.material.SetColor(OutlineColorProperty, Color.white);
            Renderer.material.SetFloat(OutlineSizeProperty, 0f);
            Renderer.material.SetFloat(OutlineSmoothnessProperty, 0f);
            Renderer.material.SetFloat(ColorIntensityProperty, 0f);
            Animator.speed = 1;
        }

        public void ApplyFreeze() => ApplyEffect(FreezeEffect);
        public void UnapplyFreeze() => UnapplyEffect();

        public void ApplyPoison() => ApplyEffect(PoisonEffect);
        public void UnapplyPoison() => UnapplyEffect();

        public void ApplySpeed() => ApplyEffect(SpeedEffect);
        public void UnapplySpeed() => UnapplyEffect();

        public void ApplyMaxHp() => ApplyEffect(MaxHpEffect);
        public void UnapplyMaxHp() => UnapplyEffect();

        public void ApplyInvulnerability() => ApplyEffect(InvulnerabilityEffect);
        public void UnapplyInvulnerability() => UnapplyEffect();
    }
}
