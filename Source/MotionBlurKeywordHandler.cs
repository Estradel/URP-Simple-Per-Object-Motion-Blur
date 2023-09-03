using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
[ExecuteInEditMode]
public sealed class MotionBlurKeywordHandler : MonoBehaviour
{
    const string IsGameViewAndPlayingKeyword = "_ISGAMEVIEWANDPLAYING";

    [Tooltip("Scene View camera is named 'SceneCamera'. Inspector preview for materials, models, and prefabs is named 'Preview Scene Camera'. You can add your own custom cameras like MinimapCamera.")]
    [SerializeField] private List<string> cameraBlacklist = new List<string>() { "SceneCamera", "Preview Scene Camera" };

    public void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += OnCameraPreRender;
    }

    public void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= OnCameraPreRender;
    }

    public void OnCameraPreRender(ScriptableRenderContext context, Camera camera)
    {
        if (Application.isPlaying)
        {
            if (cameraBlacklist.Contains(camera.gameObject.name))
                Shader.DisableKeyword(IsGameViewAndPlayingKeyword);
            else
                Shader.EnableKeyword(IsGameViewAndPlayingKeyword);
        }
        else
        {
            Shader.DisableKeyword(IsGameViewAndPlayingKeyword);
        }
    }
}
