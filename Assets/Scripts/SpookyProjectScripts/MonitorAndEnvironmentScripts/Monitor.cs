using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Monitor : MonoBehaviour
{
    public int patternAmount = 4;
    private int incrementPatternDifficulty;
    private int patternReset;
    public List <int> directionsList = new List<int>();
    public List <Sprite> monitorSpriteList = new List<Sprite>();
    private int direction;

    private int inputNumber;
    private float timer;

    //for monitor display
    [SerializeField]
    private float scoreLeft;
    public TextMeshProUGUI monitorTextUGUI;
    public Image monitorImage;


    //nesw = 1,2,3,4
    //will check this when event is called to see if input should be recieved yet
    private bool finishedDisplaying;
    private bool isReady;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       monitorImage.sprite = monitorSpriteList[0];

       isReady = true;

       patternReset = patternAmount;

        //display remaining on monitor screen
        monitorTextUGUI.text = scoreLeft.ToString();
    }

    // Update is called once per frame


    private void OnEnable()
    {
        WorkingState.GeneratePattern.AddListener(GeneratePuzzlePattern);
        WorkingState.MonitorInput.AddListener(PlayerInputCheck);
    }

    private void OnDisable()
    {
        WorkingState.GeneratePattern.RemoveListener(GeneratePuzzlePattern);
        WorkingState.MonitorInput.RemoveListener(PlayerInputCheck);
    }


    public void GeneratePuzzlePattern()
    {
        //called from working state

        if (isReady == true)
        {
            //prevent generating multiple
            isReady = false;

            while (patternAmount > 0)
            {
                direction = Random.Range(1, 5);
                directionsList.Add(direction);
                patternAmount--;
            }

            patternAmount = patternReset;

            //once pattern is generated, start display
            StartCoroutine(DisplayPatternOnScreen());

        }

        

    }


    IEnumerator DisplayPatternOnScreen()
    {
        foreach (int i in directionsList)
        {
            yield return new WaitForSeconds(0.6f);

            SoundManager.PlaySoundAt(SoundType.MonitorPattern, 1f, 0.5f, gameObject.transform);
            monitorImage.sprite = monitorSpriteList[i];

            yield return new WaitForSeconds(0.5f);
            monitorImage.sprite = monitorSpriteList[0];
        }


        finishedDisplaying = true;
        //player can now input

        StopCoroutine(DisplayPatternOnScreen());

    }


    public void PlayerInputCheck(int directionInput)
    {
        //first check if player can input, prevent early input

        if (finishedDisplaying == true)
        {
            //check if 1st input matches 1st direction number from list, increment input number to check the second

            if (directionInput == directionsList[inputNumber])
            {
                monitorImage.sprite = monitorSpriteList[directionInput + 4];
                inputNumber++;

                finishedDisplaying = false;

                //check if last one
                if (inputNumber == directionsList.Count)
                {
                    SoundManager.PlaySoundAt(SoundType.MonitorRight, 1f, 0.5f, gameObject.transform);
                    StartCoroutine(Complete());
                    inputNumber = 0;
                }
                else
                {
                    SoundManager.PlaySoundAt(SoundType.MonitorRight, 1f, 0.5f, gameObject.transform);
                    StartCoroutine(CorrectAndPrepare());
                }
                    
            }
            //if wrong input
            else
            {
                finishedDisplaying = false;
                inputNumber = 0;

                StopAllCoroutines();
                StartCoroutine(Incorrect());
            }

        }
    }



    IEnumerator CorrectAndPrepare()
    {
        yield return new WaitForSeconds (0.4f);
        monitorImage.sprite = monitorSpriteList[0];
        yield return new WaitForSeconds(0.1f);
        finishedDisplaying = true;
        StopCoroutine(CorrectAndPrepare());
    }


    IEnumerator Complete()
    {
        //complete animation

        yield return new WaitForSeconds(0.5f);
        monitorImage.sprite = monitorSpriteList[0];
        yield return new WaitForSeconds(0.25f);
        SoundManager.PlaySoundAt(SoundType.MonitorRight, 1f, 1f, gameObject.transform);
        monitorImage.sprite = monitorSpriteList[9];
        yield return new WaitForSeconds(0.25f);
        monitorImage.sprite = monitorSpriteList[0];
        yield return new WaitForSeconds(0.25f);
        SoundManager.PlaySoundAt(SoundType.MonitorRight, 1f, 1f, gameObject.transform);
        monitorImage.sprite = monitorSpriteList[9];
        yield return new WaitForSeconds(0.25f);
        monitorImage.sprite = monitorSpriteList[0];
        yield return new WaitForSeconds(0.25f);
        monitorImage.sprite = monitorSpriteList[9];
        SoundManager.PlaySoundAt(SoundType.MonitorRight, 1f, 1f, gameObject.transform);
        yield return new WaitForSeconds(0.25f);
        monitorImage.sprite = monitorSpriteList[0];
        yield return new WaitForSeconds(0.25f);
        monitorImage.sprite = monitorSpriteList[11];
        yield return new WaitForSeconds(0.25f);
        monitorImage.sprite = monitorSpriteList[0];


        StopCoroutine(Complete());
    
        ResetAndCheckAndDifficulty();

    }

    IEnumerator Incorrect()
    {
        SoundManager.PlaySoundAt(SoundType.MonitorWrong, 1f, 0.5f, gameObject.transform);
        monitorImage.sprite = monitorSpriteList[10];
        yield return new WaitForSeconds(0.25f);
        monitorImage.sprite = monitorSpriteList[0];
        yield return new WaitForSeconds(0.25f);
        SoundManager.PlaySoundAt(SoundType.MonitorWrong, 1f, 0.5f, gameObject.transform);
        monitorImage.sprite = monitorSpriteList[10];
        yield return new WaitForSeconds(0.25f);
        monitorImage.sprite = monitorSpriteList[0];
        yield return new WaitForSeconds(0.25f);
        SoundManager.PlaySoundAt(SoundType.MonitorWrong, 1f, 0.5f, gameObject.transform);
        monitorImage.sprite = monitorSpriteList[10];
        yield return new WaitForSeconds(0.25f);
        monitorImage.sprite = monitorSpriteList[0];
        yield return new WaitForSeconds(0.25f);
        monitorImage.sprite = monitorSpriteList[11];
        yield return new WaitForSeconds(0.25f);
        monitorImage.sprite = monitorSpriteList[0];

        isReady = true;
        directionsList.Clear();
        StopCoroutine(Incorrect());

    }


    public void ResetAndCheckAndDifficulty()
    {

        directionsList.Clear();

        //increment score and disaply on monitor
        scoreLeft -= 1f;
        monitorTextUGUI.text = scoreLeft.ToString();

        incrementPatternDifficulty++;

        if(incrementPatternDifficulty == 3)
        {
            incrementPatternDifficulty = 0;
            //Increase Pattern Amount
            patternReset++;
        }


        if (scoreLeft == 0)
        {
            SceneManager.LoadScene("MonsterWinMenu", LoadSceneMode.Single);
            SceneManager.SetActiveScene(SceneManager.GetActiveScene());
        }

     

        //reset
        isReady = true;

    }


}
