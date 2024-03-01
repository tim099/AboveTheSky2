
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
// Create time : 02/28 2024 19:39
using UCL.Core;
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
            var aRenderTexture = RenderTexture.GetTemporary(renderingData.cameraData.cameraTargetDescriptor);
            CommandBuffer aCmd = CommandBufferPool.Get();

            using (new ProfilingScope(aCmd, m_ProfilingSampler))
            {
                var aCamera = renderingData.cameraData.camera;
                RenderTextureDescriptor aRenderTextureDescriptor = renderingData.cameraData.cameraTargetDescriptor;
                if (s_RenderTexture == null)
                {
                    s_RenderTexture = RenderTexture.GetTemporary(aRenderTextureDescriptor);
                }
                if (s_RTHandle == null)
                {
                    s_RTHandle = UCL_RTHandleService.Ins.Alloc("CameraDepthTest", aRenderTextureDescriptor);
                        //RTHandles.Alloc(Vector2.one, depthBufferBits: DepthBits.Depth32, dimension: TextureDimension.Tex2D, name: "CameraDepth");
                    aCmd.GetTemporaryRT(Shader.PropertyToID(s_RTHandle.name), renderingData.cameraData.cameraTargetDescriptor, FilterMode.Point);
                }

                RenderingUtils.ReAllocateIfNeeded(ref s_RTHandle, renderingData.cameraData.cameraTargetDescriptor, FilterMode.Point,
                    TextureWrapMode.Clamp, name: "CameraDepthTest");
                RTHandles.SetReferenceSize(aCamera.pixelWidth, aCamera.pixelHeight);

                //aCmd.Blit(m_CameraColorTarget, s_RTHandle);
                Blitter.BlitCameraTexture(aCmd, m_CameraColorTarget, s_RTHandle);

                aCmd.Blit(m_CameraColorTarget, aRenderTexture);
                
                
                
                //Blitter.BlitCameraTexture(aCmd, m_CameraColorTarget, s_RTHandle);//, m_Material, 0

                //cmd.Blit(m_CameraColorTarget, s_RenderTexture, m_Material, 0);
                Blitter.BlitCameraTexture(aCmd, m_CameraColorTarget, m_CameraColorTarget, m_Material, 0);

                aCmd.Blit(m_CameraColorTarget, s_RenderTexture);
                aCmd.Blit(aRenderTexture, m_CameraColorTarget);//restore camera content

                
                //, m_Material, 0
                //Blitter.BlitCameraTexture(cmd, m_CameraColorTarget, s_RenderTexture, m_Material, 0);
            }
            context.ExecuteCommandBuffer(aCmd);
            aCmd.Clear();
            CommandBufferPool.Release(aCmd);

            RenderTexture.ReleaseTemporary(aRenderTexture);
        }

        public void Dispose(bool disposing)
        {
            if(s_RTHandle != null)
            {
                UCL_RTHandleService.Ins.Release(s_RTHandle);
                s_RTHandle = null;
            }
            if(s_RenderTexture != null)
            {
                RenderTexture.ReleaseTemporary(s_RenderTexture);
            }
        }
    }
}
