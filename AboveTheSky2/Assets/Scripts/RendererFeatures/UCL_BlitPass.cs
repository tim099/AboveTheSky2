
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
// Create time : 02/28 2024 19:39
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UCL
{
    public class UCL_BlitPass : ScriptableRenderPass
    {
        public static RenderTexture s_RenderTexture = null;
        public static RTHandle s_RTHandle = null;
        ProfilingSampler m_ProfilingSampler = new ProfilingSampler("ColorBlit");
        Material m_Material;
        RTHandle m_CameraColorTarget;

        public UCL_BlitPass(Material material, RenderPassEvent iRenderPassEvent)
        {
            //Debug.LogError($"ColorBlitPass");
            m_Material = material;
            renderPassEvent = iRenderPassEvent;
        }

        public void SetTarget(RTHandle colorHandle)
        {
            //Debug.LogError($"ColorBlitPass.SetTarget");
            m_CameraColorTarget = colorHandle;
        }

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            //Debug.LogError($"ColorBlitPass.OnCameraSetup");
            ConfigureTarget(m_CameraColorTarget);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            //Debug.LogError($"ColorBlitPass.Execute");
            var cameraData = renderingData.cameraData;
            if (cameraData.camera.cameraType != CameraType.Game)
                return;

            if (m_Material == null)
                return;

            CommandBuffer cmd = CommandBufferPool.Get();
            using (new ProfilingScope(cmd, m_ProfilingSampler))
            {
                if(s_RenderTexture == null)
                {
                    s_RenderTexture = RenderTexture.GetTemporary(renderingData.cameraData.cameraTargetDescriptor);
                }
                //if (s_RTHandle == null)
                //{
                //    s_RTHandle = RTHandles.Alloc(Vector2.one, depthBufferBits: DepthBits.Depth32, dimension: TextureDimension.Tex2D, name: "CameraDepth");
                //    cmd.GetTemporaryRT(Shader.PropertyToID(s_RTHandle.name), renderingData.cameraData.cameraTargetDescriptor, FilterMode.Point);
                //}



                
                var aRenderTexture = RenderTexture.GetTemporary(renderingData.cameraData.cameraTargetDescriptor);
                cmd.Blit(m_CameraColorTarget, aRenderTexture);
                //Blitter.BlitCameraTexture(cmd, m_CameraColorTarget, s_RTHandle);//, m_Material, 0
                //cmd.Blit(m_CameraColorTarget, s_RenderTexture, m_Material, 0);
                Blitter.BlitCameraTexture(cmd, m_CameraColorTarget, m_CameraColorTarget, m_Material, 0);
                cmd.Blit(m_CameraColorTarget, s_RenderTexture);
                cmd.Blit(aRenderTexture, m_CameraColorTarget);//restore camera content


                RenderTexture.ReleaseTemporary(aRenderTexture);
                //, m_Material, 0
                //Blitter.BlitCameraTexture(cmd, m_CameraColorTarget, s_RenderTexture, m_Material, 0);
            }
            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();

            CommandBufferPool.Release(cmd);
        }
    }
}
