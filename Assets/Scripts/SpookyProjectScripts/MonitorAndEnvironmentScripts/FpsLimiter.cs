using UnityEngine;

public class FpsLimiter : MonoBehaviour
{
    [SerializeField]
    private int targetFPS;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = targetFPS;
    }

}
