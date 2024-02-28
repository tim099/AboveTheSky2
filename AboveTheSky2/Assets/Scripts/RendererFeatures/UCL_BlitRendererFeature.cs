
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
// Create time : 02/28 2024 19:37
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UCL
{
    public class UCL_BlitRendererFeature : ScriptableRendererFeature
    {
        public Shader m_Shader;

        Material m_Material;

        UCL_BlitPass m_RenderPass = null;

        public override void AddRenderPasses(ScriptableRenderer renderer,
                                        ref RenderingData renderingData)
        {
            //Debug.LogError($"ColorBlitRendererFeature.AddRenderPasses(), cameraType:{renderingData.cameraData.cameraType}");
            if (renderingData.cameraData.cameraType == CameraType.Game)
                renderer.EnqueuePass(m_RenderPass);
        }

        public override void SetupRenderPasses(ScriptableRenderer renderer,
                                            in RenderingData renderingData)
        {
            //Debug.LogError($"ColorBlitRendererFeature.SetupRenderPasses(), cameraType:{renderingData.cameraData.cameraType}");
            if (renderingData.cameraData.cameraType == CameraType.Game)
            {
                // Calling ConfigureInput with the ScriptableRenderPassInput.Color argument
                // ensures that the opaque texture is available to the Render Pass.
                m_RenderPass.ConfigureInput(ScriptableRenderPassInput.Color);
                m_RenderPass.SetTarget(renderer.cameraColorTargetHandle);
            }
        }

        public override void Create()
        {

            m_Material = CoreUtils.CreateEngineMaterial(m_Shader);
            m_RenderPass = new UCL_BlitPass(m_Material);
            //Debug.LogError($"ColorBlitRendererFeature.Create() isActive:{isActive}");
        }

        protected override void Dispose(bool disposing)
        {
            CoreUtils.Destroy(m_Material);
        }
    }

}
