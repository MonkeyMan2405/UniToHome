using UnityEngine;

public class MonsterBreathe : MonoBehaviour
{
    public Transform monsterTorsoTransform;

    public float frequency;
    public float sizeMultiplier;


    public Vector3 originalScale;
    public Vector3 maxScale;

    public float pingPongValue;
    public float breathePause;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //for standardisation
        originalScale = monsterTorsoTransform.localScale;
        maxScale = originalScale * sizeMultiplier;
        monsterTorsoTransform.DetachChildren();
    }

    // Update is called once per frame
    void Update()
    {
       pingPongValue = Mathf.PingPong(Time.time * frequency, breathePause);
       monsterTorsoTransform.localScale = Vector3.Lerp(originalScale, maxScale, pingPongValue);
    }
}
