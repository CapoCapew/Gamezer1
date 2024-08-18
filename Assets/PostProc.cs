using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DarkStylePostProcessing : MonoBehaviour
{
    public PostProcessVolume volume;

    void Start()
    {
        // Créer un nouveau profil de post-processing
        var profile = ScriptableObject.CreateInstance<PostProcessProfile>();

        // Ajouter et configurer l'occlusion ambiante
        var ao = profile.AddSettings<AmbientOcclusion>();
        ao.intensity.value = 1.0f;
        ao.thicknessModifier.value = 1.5f;

        // Ajouter et configurer le vignettage
        var vignette = profile.AddSettings<Vignette>();
        vignette.intensity.value = 0.45f;
        vignette.smoothness.value = 0.8f;

        // Ajouter et configurer le grain de film
        var grain = profile.AddSettings<Grain>();
        grain.intensity.value = 0.3f;
        grain.size.value = 1.0f;

        // Ajouter et configurer la correction des couleurs
        var colorGrading = profile.AddSettings<ColorGrading>();
        colorGrading.saturation.value = -20f;
        colorGrading.contrast.value = 30f;
        colorGrading.gamma.value = new Vector4(1.0f, 0.9f, 0.9f, 1.0f);

        // Appliquer le profil au volume
        volume = gameObject.AddComponent<PostProcessVolume>();
        volume.isGlobal = true;
        volume.profile = profile;
    }
}
