using System.Collections;
using UnityEngine;

public class MonsterLightFlick : MonsterStateMachine
{

    public MonsterStateMachine msmRef;
    private float flickerRdm;
    private float timer;

    [SerializeField]
    private Light deskLight;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >=0.5f)
        {
            timer = 0;
            StartCoroutine(LightFlickering());
        }
        
    }


    IEnumerator LightFlickering()
    {
        flickerRdm = Random.Range(monsterMin, monsterDanger);   
        if (flickerRdm <= 1.5f && flickerRdm >= 0f)
        {
            Debug.Log(flickerRdm);
            deskLight.enabled = false;
            yield return new WaitForSeconds (0.1f);
            deskLight.enabled = true;
        }
       
    }

      // IEnumerator LightFlickering()
    // {
    //     Debug.LogError(1);
    //     flickerRdm = UnityEngine.Random.Range(0, MContext.danger * 10);
    //     if (flickerRdm <= 5)
    //     {
    //         MContext.deskLight.enabled = false;
    //         yield return new WaitForSeconds(0.1f);
    //         MContext.deskLight.enabled = true;
    //     }

    // }
}
