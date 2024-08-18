using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public Material morningSkybox;
    public Material afternoonSkybox;
    public Material eveningSkybox;
    public Material nightSkybox;

    private float virtualTime = 12f; // Heure virtuelle initiale (12h)
    private float timeSpeed = 60f; // Vitesse de l'heure virtuelle (60 fois plus rapide)

    void Update()
    {
        // Avancer l'heure virtuelle
        virtualTime += Time.deltaTime * timeSpeed / 3600f; // 3600 secondes dans une heure

        // Réinitialiser l'heure virtuelle après 24h
        if (virtualTime >= 24f)
        {
            virtualTime -= 24f;
        }

        // Changer la skybox en fonction de l'heure virtuelle
        if (virtualTime >= 6f && virtualTime < 12f)
        {
            RenderSettings.skybox = morningSkybox;
        }
        else if (virtualTime >= 12f && virtualTime < 18f)
        {
            RenderSettings.skybox = afternoonSkybox;
        }
        else if (virtualTime >= 18f && virtualTime < 21f)
        {
            RenderSettings.skybox = eveningSkybox;
        }
        else
        {
            RenderSettings.skybox = nightSkybox;
        }

        // Transition fluide entre les skyboxes
        DynamicGI.UpdateEnvironment();
    }
}
