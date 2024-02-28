
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
// Create time : 02/28 2024 16:07
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace URP
{
    #region BlitRequest

    public class BlitRequest
    {
        /// <summary>
        /// blit once
        /// </summary>
        public bool RemoveAfterBlit = true;
        public RenderPassEvent RenderPassEvent = RenderPassEvent.AfterRendering;
        /// <summary>
        /// Target Camera
        /// </summary>
        public Camera Camera = null;
        public Material Material = null;
        public BlitRequest() { }
        public virtual void Blit(BlitData blitData)
        {

        }
        public virtual void FrameCleanup(CommandBuffer cmd) { }
    }
    public class BlitToRenderTexture : BlitRequest
    {
        public BlitToRenderTexture() { }

        public override void Blit(BlitData blitData)
        {
            //Eagle.Log.Warning("blitShaderPassIndex:" + blitShaderPassIndex);
            //RenderTexture = RenderTexture.GetTemporary(blitData.OpaqueDesc);
            RTHandle = blitData.GetTemporaryRT();
            if(Material == null)
            {
                Material = blitData.Material;
            }
            if (blitData.Cmd == null)
            {
                Debug.LogError("blitData.Cmd");
                return;
            }
            if (Material == null)
            {
                Debug.LogError("Material == null");
                return;
            }
            if (blitData.Source == null)
            {
                Debug.LogError("blitData.Source == null");
                return;
            }
            if (RTHandle == null)
            {
                Debug.LogError("RTHandle == null");
                return;
            }
            try
            {
                //RTHandle
                Blitter.BlitCameraTexture(blitData.Cmd, blitData.Source, blitData.Source, Material, blitData.ShaderPassIndex);
            }
            catch(System.Exception e)
            {
                Debug.LogException(e);
            }


            //blitData.BlitPass.Blit(blitData.Cmd, blitData.Source, RTHandle, Material, blitData.ShaderPassIndex);
            //blitData.BlitPass.Blit(blitData.Cmd, blitData.Source, RTHandle, Material, blitData.ShaderPassIndex);

            //RenderTexture = RenderTexture.GetTemporary(renderTarget.rt.width, renderTarget.rt.height, renderTarget.rt.depth, renderTarget.rt.graphicsFormat);
            //Graphics.CopyTexture(renderTarget.rt, RenderTexture);

            //blitData.BlitPass.Blit(blitData.Cmd, blitData.Source, RenderTexture, blitData.Material, blitData.ShaderPassIndex);

            CompleteCallback?.Invoke(this);
            Completed = true;
        }
        /// <summary>
        /// blit to RTHandle
        /// </summary>
        public RTHandle RTHandle = null;
        
        public System.Action<BlitToRenderTexture> CompleteCallback = null;

        public bool Completed { get; private set; } = false;
    }

    public class BlitToCamera : BlitRequest
    {
        public BlitToCamera()
        {

        }
        public override void Blit(BlitData blitData)
        {
            if (RenderAction == null) return;
            _blitData = blitData;
            RenderAction.Invoke(blitData);
        }
        public override void FrameCleanup(CommandBuffer cmd)
        {
            if (_blitData != null)
            {
                _blitData.FrameCleanup(cmd);
            }
        }
        BlitData _blitData;
        public System.Action<BlitData> RenderAction = null;
    }

    #endregion
    /// <summary>
    /// reference https://zhuanlan.zhihu.com/p/367518645
    /// this was used on https://gamedevbill.com, but originally taken from https://cyangamedev.wordpress.com/2020/06/22/urp-post-processing/
    /// </summary>
    public class URP_BlitRendererFeature : ScriptableRendererFeature
    {
        #region static
        public static void AddBlitRequest(BlitRequest blitRequest)
        {
            s_BlitRequests.Add(blitRequest);
        }
        public static void RemoveBlitRequest(BlitRequest blitRequest)
        {
            s_BlitRequests.Remove(blitRequest);
        }
        static readonly List<BlitRequest> s_BlitRequests = new List<BlitRequest>();
        #endregion

        [System.Serializable]
        public class BlitSettings
        {
            public Material m_BlitMaterial = null;
            public int m_BlitMaterialPassIndex = 0;
        }
        //ProfilingSampler m_ProfilingSampler = new ProfilingSampler("URP_BlitRendererFeature");
        public BlitSettings m_Settings = new BlitSettings();
        readonly Dictionary<RenderPassEvent, URP_BlitPass> m_BlitPassDic = new Dictionary<RenderPassEvent, URP_BlitPass>();
        RTHandle m_CameraColorTarget;
        public override void Create()
        {
            var passIndex = m_Settings.m_BlitMaterial != null ? m_Settings.m_BlitMaterial.passCount - 1 : 1;
            m_Settings.m_BlitMaterialPassIndex = Mathf.Clamp(m_Settings.m_BlitMaterialPassIndex, -1, passIndex);
        }
        private URP_BlitPass GetBlitPass(RenderPassEvent renderPassEvent)
        {

            if (!m_BlitPassDic.ContainsKey(renderPassEvent))
            {
                var aNewPass = new URP_BlitPass(renderPassEvent, m_Settings.m_BlitMaterial,
                    m_Settings.m_BlitMaterialPassIndex, $"{name}_{renderPassEvent}");

                aNewPass.ConfigureInput(ScriptableRenderPassInput.Color);
                m_BlitPassDic.Add(renderPassEvent, aNewPass);

                
            }
            var aPass = m_BlitPassDic[renderPassEvent];
            aPass.m_CameraColorTarget = m_CameraColorTarget;
            return aPass;
        }
        public override void SetupRenderPasses(ScriptableRenderer renderer,
                                    in RenderingData renderingData)
        {
            //Debug.LogError($"ColorBlitRendererFeature.SetupRenderPasses(), cameraType:{renderingData.cameraData.cameraType}");
            m_CameraColorTarget = renderer.cameraColorTargetHandle;
            //foreach(var aPass in m_BlitPassDic.Values)
            //{
            //    aPass.m_CameraColorTarget = m_CameraColorTarget;
            //}
        }
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            //Debug.LogError($"AddRenderPasses s_BlitRequests.Count:{s_BlitRequests.Count}");
            //renderingData.ConfigureInput(ScriptableRenderPassInput.Normal);
            if (s_BlitRequests.Count == 0)
            {
                return;
            }

            var targetCamera = renderingData.cameraData.camera;
            for (int i = s_BlitRequests.Count - 1; i >= 0; i--)
            {
                BlitRequest blitRequest = s_BlitRequests[i];
                if (blitRequest.Camera == null || blitRequest.Camera == targetCamera)
                {
                    var aPass = GetBlitPass(blitRequest.RenderPassEvent);
                    
                    aPass.AddBlitRequest(blitRequest);
                    if (blitRequest.RemoveAfterBlit) s_BlitRequests.RemoveAt(i);
                }
            }


            //CommandBuffer cmd = CommandBufferPool.Get();
            //using (new ProfilingScope(cmd, m_ProfilingSampler))
            {
                foreach (var blitPass in m_BlitPassDic.Values)
                {
                    if (blitPass.HasRequests)
                    {
                        blitPass.Setup(renderer);
                        renderer.EnqueuePass(blitPass);
                    }
                }
                //m_Material.SetFloat("_Intensity", m_Intensity);
                //Blitter.BlitCameraTexture(cmd, m_CameraColorTarget, m_CameraColorTarget, m_Material, 0);
            }
            //context.ExecuteCommandBuffer(cmd);
            //cmd.Clear();

            //CommandBufferPool.Release(cmd);

            //var src = renderer.cameraColorTarget;


        }

        protected override void Dispose(bool iDisposing)
        {
            base.Dispose(iDisposing);
        }
    }
}
