
// ATS_AutoHeader
// to change the auto header please go to ATS_AutoHeader.cs
// Create time : 02/28 2024 16:07
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
namespace URP
{
    public class BlitData
    {
        public int ID { get; set; }
        public Camera Camera;
        public URP_BlitPass BlitPass;
        public RTHandle Source;
        public CommandBuffer Cmd;
        public RenderTextureDescriptor OpaqueDesc;
        public Material Material;
        public int ShaderPassIndex = 0;
        public ScriptableRenderer Renderer;
        public RenderingData RenderingData;


        public List<RTHandle> m_TemporaryRTHandles = new List<RTHandle>();
        public List<RenderTexture> m_TemporaryRenderTextures = new List<RenderTexture>();
        public RTHandle GetTemporaryRT(int width, int height, int depthBuffer, FilterMode filterMode, 
            RenderTextureFormat format = RenderTextureFormat.Default)
        {
            string aName = $"TemporaryRT_{ID}_{m_TemporaryRTHandles.Count}";
            RTHandle targetHandle = RTHandles.Alloc(aName, name: aName); //new RenderTargetHandle();

            //targetHandle.Init($"TemporaryRT_{ID.ToString()}_{_temporaryColorTextures.Count}");
            m_TemporaryRTHandles.Add(targetHandle);
            //Cmd.GetTemporaryRT(targetHandle.id, width,height, depthBuffer, filterMode, format);
            Cmd.GetTemporaryRT(Shader.PropertyToID(targetHandle.name), width, height, depthBuffer, filterMode);
            return targetHandle;
        }
        public RTHandle GetTemporaryRT(FilterMode filterMode)
        {
            //RTHandle targetHandle = new RenderTargetHandle();
            //targetHandle.Init($"TemporaryRT_{ID.ToString()}_{_temporaryColorTextures.Count}");
            string aName = $"TemporaryRT_{ID}_{m_TemporaryRTHandles.Count}";
            RTHandle targetHandle = RTHandles.Alloc(aName, name: aName); //new RenderTargetHandle();

            
            m_TemporaryRTHandles.Add(targetHandle);
            Cmd.GetTemporaryRT(Shader.PropertyToID(targetHandle.name), OpaqueDesc, filterMode);
            return targetHandle;
        }
        public RTHandle GetTemporaryRT()
        {
            string aName = $"TemporaryRT_{ID}_{m_TemporaryRTHandles.Count}";
            RTHandle targetHandle = RTHandles.Alloc(aName, name: aName); //new RenderTargetHandle();

            m_TemporaryRTHandles.Add(targetHandle);
            Cmd.GetTemporaryRT(Shader.PropertyToID(targetHandle.name), OpaqueDesc);
            return targetHandle;
        }
        public RenderTexture GetTemporaryRenderTexture(FilterMode filterMode)
        {
            RenderTexture renderTexture = RenderTexture.GetTemporary(OpaqueDesc);
            renderTexture.filterMode = filterMode;
            m_TemporaryRenderTextures.Add(renderTexture);
            return renderTexture;
        }
        public void FrameCleanup(CommandBuffer cmd)
        {
            foreach (var temporaryRT in m_TemporaryRTHandles)
            {
                cmd.ReleaseTemporaryRT(Shader.PropertyToID(temporaryRT.name));
            }
            foreach (var temporaryRenderTexture in m_TemporaryRenderTextures)
            {
                RenderTexture.ReleaseTemporary(temporaryRenderTexture);
            }
        }
    }
    /// <summary>
    /// reference https://zhuanlan.zhihu.com/p/367518645
    /// this was used on https://gamedevbill.com, but originally taken from https://cyangamedev.wordpress.com/2020/06/22/urp-post-processing/
    /// </summary>
    public class URP_BlitPass : ScriptableRenderPass
    {
        public ScriptableRenderer m_Renderer;
        public Material _blitMaterial = null;
        public int _blitShaderPassIndex = 0;
        public RTHandle m_CameraColorTarget;
        //private RTHandle source { get; set; }
        public bool HasRequests => m_BlitRequests.Count > 0;
        string _profilerTag;
        List<BlitRequest> m_BlitRequests = new List<BlitRequest>();
        ProfilingSampler m_ProfilingSampler = null;
        public URP_BlitPass(RenderPassEvent renderPassEvent, Material blitMaterial, int blitShaderPassIndex, string tag)
        {
            this.renderPassEvent = renderPassEvent;
            this._blitMaterial = blitMaterial;
            this._blitShaderPassIndex = blitShaderPassIndex;
            _profilerTag = tag;
            m_ProfilingSampler = new ProfilingSampler($"URP_BlitPass_{renderPassEvent}");
        }
        public void Setup(ScriptableRenderer renderer)
        {
            this.m_Renderer = renderer;
            //this.source = renderer.cameraColorTargetHandle;//Error!!
        }
        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            //Debug.LogError($"ColorBlitPass.OnCameraSetup");
            ConfigureTarget(m_CameraColorTarget);
        }
        public void AddBlitRequest(BlitRequest blitRequest)
        {
            if (m_BlitRequests == null) m_BlitRequests = new List<BlitRequest>();
            m_BlitRequests.Add(blitRequest);
        }
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            //CommandBuffer cmd = CommandBufferPool.Get(_profilerTag);

            //RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
            //opaqueDesc.depthBufferBits = 0;

            //for (int i = 0; i < m_BlitRequests.Count; i++)
            //{
            //    var blitRequest = m_BlitRequests[i];
            //    BlitData blitData = new BlitData()
            //    {
            //        ID = i,
            //        Camera = blitRequest.Camera,
            //        BlitPass = this,
            //        //Source = source,
            //        Cmd = cmd,
            //        OpaqueDesc = opaqueDesc,
            //        Material = this._blitMaterial,
            //        ShaderPassIndex = this._blitShaderPassIndex,
            //        Renderer = this._renderer,
            //        RenderingData = renderingData,
            //    };
            //    blitRequest.Blit(blitData);
            //}

            //m_BlitRequests.Clear();
            //context.ExecuteCommandBuffer(cmd);
            //CommandBufferPool.Release(cmd);

            RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
            opaqueDesc.depthBufferBits = 0;

            CommandBuffer cmd = CommandBufferPool.Get();
            using (new ProfilingScope(cmd, m_ProfilingSampler))
            {
                //m_Material.SetFloat("_Intensity", m_Intensity);
                //Blitter.BlitCameraTexture(cmd, m_CameraColorTarget, m_CameraColorTarget, m_Material, 0);
                var aCameraColorTarget = m_Renderer.cameraColorTargetHandle;
                for (int i = 0; i < m_BlitRequests.Count; i++)
                {
                    BlitRequest blitRequest = m_BlitRequests[i];
                    //BlitData blitData = new BlitData()
                    //{
                    //    ID = i,
                    //    Camera = blitRequest.Camera,
                    //    BlitPass = this,
                    //    Source = aCameraColorTarget,
                    //    Cmd = cmd,
                    //    OpaqueDesc = opaqueDesc,
                    //    Material = this._blitMaterial,
                    //    ShaderPassIndex = this._blitShaderPassIndex,
                    //    Renderer = this.m_Renderer,
                    //    RenderingData = renderingData,
                    //};
                    Blitter.BlitCameraTexture(cmd, aCameraColorTarget, aCameraColorTarget, blitRequest.Material, 0);
                    //blitRequest.Blit(blitData);
                }
            }

            m_BlitRequests.Clear();
            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();

            CommandBufferPool.Release(cmd);
        }
        public override void FrameCleanup(CommandBuffer cmd)
        {
            for (int i = 0; i < m_BlitRequests.Count; i++)
            {
                var blitRequest = m_BlitRequests[i];
                blitRequest.FrameCleanup(cmd);
            }
        }
    }
}