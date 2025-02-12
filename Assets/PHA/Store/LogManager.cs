using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogManager : MonoBehaviour
{
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

    private int p_Quest;
    private int s_Quest;
    private int c_Quest;
    private int r_Quest;

    private int p_story;
    private int s_story;
    private int c_story;
    private int r_story;

    private int day;

    public bool todayStory = false;
    public bool playStory = false;
    public bool finishStory = false;

    public DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        Datamanager.Instance.LoadGameData();

        day = Datamanager.Instance.data.NowDay;

        p_Quest = Datamanager.Instance.data.PublicAuthority_Step;
        s_Quest = Datamanager.Instance.data.Cult_Step;
        c_Quest = Datamanager.Instance.data.CrimeSyndicate_Step;
        r_Quest = Datamanager.Instance.data.RevolutionaryArmy_Step;
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
            SceneManager.LoadScene("Select");
        }

        if (!playStory)
        {
            checkStory();
        }
    }

    public void checkStory()
    {
        if(!todayStory)
        {
            playStory = true;

            dialogueManager.setStoryLog(h_storyLog);
            dialogueManager.setProgress(day);
            dialogueManager.setName("½Ã¹Î");
            dialogueManager.setImage(h_image);

            dialogueManager.LoadDialogue();
            todayStory = true;
            
            return;
        }
        if(p_Quest - p_story == 2)
        {
            playStory = true;

            dialogueManager.setStoryLog(p_storyLog);
            dialogueManager.setProgress(p_Quest - 1);
            dialogueManager.setName("°ø±Ç·Â");
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
            dialogueManager.setName("»çÀÌºñ");
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
        //    dialogueManager.setName("¹üÁË Áý´Ü");
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
        //    dialogueManager.setName("Çõ¸í´Ü");
        //    dialogueManager.setImage(r_image);

        //    dialogueManager.LoadDialogue();
        //    Datamanager.Instance.data.Revolution_s++;
        //    Datamanager.Instance.SaveGameData();

        //    return;
        //}

        finishStory = true;
    }
}
