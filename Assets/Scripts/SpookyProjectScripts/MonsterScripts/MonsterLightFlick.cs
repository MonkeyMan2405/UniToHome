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
 
        //start flickering coroutine. scales by delta time as running purely in update caused it to run too fast, impacing flickering. also performance
        timer += Time.deltaTime;
        if (timer >=0.5f)
        {
            timer = 0;
            StartCoroutine(LightFlickering());
        }


        if (monsterDanger == 1.75f)
        {
            StopCoroutine(LightFlickering());
            StartCoroutine(ImpendingDoomFlickering());

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

    IEnumerator ImpendingDoomFlickering()
    {
        deskLight.enabled = false;
        yield return new WaitForSeconds(0.1f);
        deskLight.enabled = true;
    }

}
