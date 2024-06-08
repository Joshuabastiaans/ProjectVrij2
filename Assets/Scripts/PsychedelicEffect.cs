using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable]
[VolumeComponentMenu("Custom/PsychedelicEffect")]
public class PsychedelicEffect : VolumeComponent, IPostProcessComponent
{
    public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f);

    public bool IsActive() => intensity.value > 0f;
    public bool IsTileCompatible() => false;
}

public class PsychedelicEffectRenderer : ScriptableRendererFeature
{
    class CustomRenderPass : ScriptableRenderPass
    {
        public Material material;
        public PsychedelicEffect effect;

        public void Setup(PsychedelicEffect effect, Material material)
        {
            this.effect = effect;
            this.material = material;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (material == null)
                return;

            CommandBuffer cmd = CommandBufferPool.Get("PsychedelicEffect");

            RenderTargetIdentifier source = renderingData.cameraData.renderer.cameraColorTargetHandle;

            material.SetFloat("_Intensity", effect.intensity.value);
            cmd.Blit(source, source, material);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }

    CustomRenderPass m_ScriptablePass;
    public Material material;

    public override void Create()
    {
        m_ScriptablePass = new CustomRenderPass();
        m_ScriptablePass.renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        var stack = VolumeManager.instance.stack;
        var effect = stack.GetComponent<PsychedelicEffect>();

        if (effect.IsActive())
        {
            m_ScriptablePass.Setup(effect, material);
            renderer.EnqueuePass(m_ScriptablePass);
        }
    }
}
