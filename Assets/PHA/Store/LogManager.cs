using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogManager : MonoBehaviour
{
    public GameObject canvasObject;

    public StoryLog p_storyLog; 
    public StoryLog s_storyLog;
    public StoryLog c_storyLog;
    public StoryLog r_storyLog;
    public StoryLog h_storyLog;

    public UnityEngine.UI.Image p_image;
    public UnityEngine.UI.Image s_image;
    public UnityEngine.UI.Image c_image;
    public UnityEngine.UI.Image r_image;
    public UnityEngine.UI.Image h_image;

    public UnityEngine.UI.Image p_ending;
    public UnityEngine.UI.Image s_ending;
    public UnityEngine.UI.Image c_ending;
    public UnityEngine.UI.Image r_ending;
    public UnityEngine.UI.Image background;

    private int p_Quest;
    private int s_Quest;
    private int c_Quest;
    private int r_Quest;

    private int p_story;
    private int s_story;
    private int c_story;
    private int r_story;

    private int day;
    private string ending;

    public bool todayStory = false;
    public bool playStory = false;
    public bool finishStory = false;
    public bool endingStory = false;

    public DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        Datamanager.Instance.LoadGameData();

        day = Datamanager.Instance.data.NowDay;
        ending = Datamanager.Instance.data.Ending;

        p_Quest = Datamanager.Instance.data.PublicAuthority_Step;
        s_Quest = Datamanager.Instance.data.Cult_Step;
        c_Quest = Datamanager.Instance.data.CrimeSyndicate_Step;
        r_Quest = Datamanager.Instance.data.RevolutionaryArmy_Step;

        p_ending.gameObject.SetActive(false);
        s_ending.gameObject.SetActive(false);
        c_ending.gameObject.SetActive(false);
        r_ending.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        p_story = Datamanager.Instance.data.Public_s;
        s_story = Datamanager.Instance.data.Cult_s;
        c_story = Datamanager.Instance.data.Crime_s;
        r_story = Datamanager.Instance.data.Revolution_s;

        if (finishStory)
        {
            if (Datamanager.Instance.data.NowDay < 3)
            {
                Datamanager.Instance.data.NowDay++;
                UnityEngine.Debug.Log($"NowDay 값 증가 후: {Datamanager.Instance.data.NowDay}"); // 값 확인
                Datamanager.Instance.SaveGameData();
            }
            if (day == 3)
            {
                canvasObject.SetActive(false);
                if(Input.anyKeyDown)
                {
                    Datamanager.Instance.data.NowDay = 1;
                    Datamanager.Instance.data.PublicAuthority_Step = 1;
                    Datamanager.Instance.data.RevolutionaryArmy_Step = 1;
                    Datamanager.Instance.data.Cult_Step = 1;
                    Datamanager.Instance.data.CrimeSyndicate_Step = 1;
                    Datamanager.Instance.data.Public_s = 0;
                    Datamanager.Instance.data.Revolution_s = 0;
                    Datamanager.Instance.data.Crime_s = 0;
                    Datamanager.Instance.data.Cult_s = 0;
                    Datamanager.Instance.data.Ending = null;

                    Datamanager.Instance.SaveGameData();

                    SceneManager.LoadScene("MainTittleScene");
                }
            }
            else SceneManager.LoadScene("Select");
        }

        if (!playStory)
        {
            checkStory();
        }
    }

    public void checkStory()
    {
        if (!endingStory)
        {
            if (day == 3)
            {
                switch (ending)
                {
                    case "Public":
                        p_ending.gameObject.SetActive(true);

                        playStory = true;

                        dialogueManager.setStoryLog(p_storyLog);
                        dialogueManager.setProgress(p_Quest - 1);
                        dialogueManager.setName("공권력");
                        dialogueManager.setImage(p_image);

                        dialogueManager.LoadDialogue();
                        Datamanager.Instance.data.Public_s++;
                        Datamanager.Instance.SaveGameData();

                        endingStory = true;
                        break;
                    case "Cult":
                        s_ending.gameObject.SetActive(true);

                        playStory = true;

                        dialogueManager.setStoryLog(s_storyLog);
                        dialogueManager.setProgress(s_Quest - 1);
                        dialogueManager.setName("사이비");
                        dialogueManager.setImage(s_image);

                        dialogueManager.LoadDialogue();
                        Datamanager.Instance.data.Cult_s++;
                        Datamanager.Instance.SaveGameData();

                        endingStory = true;
                        break;
                    //case "Crime":
                    //    c_ending.gameObject.SetActive(true);

                    //    playStory = true;

                    //    dialogueManager.setStoryLog(c_storyLog);
                    //    dialogueManager.setProgress(c_Quest - 1);
                    //    dialogueManager.setName("범죄 집단");
                    //    dialogueManager.setImage(c_image);

                    //    dialogueManager.LoadDialogue();
                    //    Datamanager.Instance.data.Crime_s++;
                    //    Datamanager.Instance.SaveGameData();

                    //    endingStory = true;
                    //    break;
                    //case "Revolution":
                    //    r_ending.gameObject.SetActive(true);

                    //    playStory = true;

                    //    dialogueManager.setStoryLog(r_storyLog);
                    //    dialogueManager.setProgress(r_Quest - 1);
                    //    dialogueManager.setName("혁명단");
                    //    dialogueManager.setImage(r_image);

                    //    dialogueManager.LoadDialogue();
                    //    Datamanager.Instance.data.Revolution_s++;
                    //    Datamanager.Instance.SaveGameData();

                    //    endingStory = true;
                    //    break;
                    default:
                        playStory = true;

                        dialogueManager.setStoryLog(h_storyLog);
                        dialogueManager.setProgress(day);
                        dialogueManager.setName("시민");
                        dialogueManager.setImage(h_image);

                        dialogueManager.LoadDialogue();
                        endingStory = true;
                        break;

                }
                return;
            }


            if (!todayStory)
            {
                playStory = true;

                dialogueManager.setStoryLog(h_storyLog);
                dialogueManager.setProgress(day);
                dialogueManager.setName("시민");
                dialogueManager.setImage(h_image);

                dialogueManager.LoadDialogue();
                todayStory = true;

                if (day == 3)
                    endingStory = true;

                return;
            }

            if (p_Quest - p_story == 2)
            {
                playStory = true;

                dialogueManager.setStoryLog(p_storyLog);
                dialogueManager.setProgress(p_Quest - 1);
                dialogueManager.setName("공권력");
                dialogueManager.setImage(p_image);

                dialogueManager.LoadDialogue();
                Datamanager.Instance.data.Public_s++;
                Datamanager.Instance.SaveGameData();

                return;
            }
            if (s_Quest - s_story == 2)
            {
                playStory = true;

                dialogueManager.setStoryLog(s_storyLog);
                dialogueManager.setProgress(s_Quest - 1);
                dialogueManager.setName("사이비");
                dialogueManager.setImage(s_image);

                dialogueManager.LoadDialogue();
                Datamanager.Instance.data.Cult_s++;
                Datamanager.Instance.SaveGameData();

                return;
            }
            //if (c_Quest - c_story == 2)
            //{
            //    playStory = true;

            //    dialogueManager.setStoryLog(c_storyLog);
            //    dialogueManager.setProgress(c_Quest - 1);
            //    dialogueManager.setName("범죄 집단");
            //    dialogueManager.setImage(c_image);

            //    dialogueManager.LoadDialogue();
            //    Datamanager.Instance.data.Crime_s++;
            //    Datamanager.Instance.SaveGameData();

            //    return;
            //}
            //if (r_Quest - r_story == 2)
            //{
            //    playStory = true;

            //    dialogueManager.setStoryLog(r_storyLog);
            //    dialogueManager.setProgress(r_Quest - 1);
            //    dialogueManager.setName("혁명단");
            //    dialogueManager.setImage(r_image);

            //    dialogueManager.LoadDialogue();
            //    Datamanager.Instance.data.Revolution_s++;
            //    Datamanager.Instance.SaveGameData();

            //    return;
            //}
        }

        finishStory = true;
    }
    
}
