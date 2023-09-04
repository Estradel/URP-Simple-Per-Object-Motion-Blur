using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
[ExecuteInEditMode]
public sealed class MotionBlurActivator : MonoBehaviour
{
    [Tooltip("Scene View camera is named 'SceneCamera'. Inspector preview for materials, models, and prefabs is named 'Preview Scene Camera'. You can add your own custom cameras like MinimapCamera.")]
    [SerializeField] private string[] cameraBlacklist = new string[2] { "SceneCamera", "Preview Scene Camera" };
    [SerializeField] private ScriptableRendererFeature motionBlurFeature;

    private bool MotionBlurEnabled { get => true; }

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
        if (FeatureNotReferenced())
            return;

        bool shouldBeActive = cameraBlacklist.Contains(camera.gameObject.name) == false && MotionBlurEnabled && Application.isPlaying;

        if (motionBlurFeature.isActive != shouldBeActive)
            motionBlurFeature.SetActive(shouldBeActive);
    }

    private bool FeatureNotReferenced()
    {
        bool notReferenced = motionBlurFeature == null;

        if (notReferenced)
            Debug.LogError("MotionBlurKeywordHandler's motionBlurFeature is not referenced! Please assign it in inspector.", gameObject);

        return notReferenced;
    }
}