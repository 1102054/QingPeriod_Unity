using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cherrydev;

public class ch4_audioSpeaker : MonoBehaviour
{
    public cherrydev.DialogBehaviour buBoss_dialogBehaviour;
    public cherrydev.DialogBehaviour walker_dialogBehaviour;
    public cherrydev.DialogBehaviour fisher_dialogBehaviour;
    public cherrydev.DialogBehaviour youngBoss_dialogBehaviour;
    public cherrydev.DialogBehaviour youngMan_dialogBehaviour;
    public cherrydev.DialogBehaviour chaBoss_dialogBehaviour;
    public cherrydev.DialogBehaviour two_dialogBehaviour;
    
    private AudioSource audioSource;

    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        buBoss_dialogBehaviour.BindExternalFunction("buBoss1", () => PlayAudio("buBoss1"));
        buBoss_dialogBehaviour.BindExternalFunction("buBoss2", () => PlayAudio("buBoss2"));
        buBoss_dialogBehaviour.BindExternalFunction("buBoss3", () => PlayAudio("buBoss3"));
        
        walker_dialogBehaviour.BindExternalFunction("walker1", () => PlayAudio("walker1"));
        walker_dialogBehaviour.BindExternalFunction("walker2", () => PlayAudio("walker2"));

        fisher_dialogBehaviour.BindExternalFunction("fisher0", () => PlayAudio("fisher0"));
        fisher_dialogBehaviour.BindExternalFunction("fisher1", () => PlayAudio("fisher1"));
        fisher_dialogBehaviour.BindExternalFunction("fisher2", () => PlayAudio("fisher2"));
        fisher_dialogBehaviour.BindExternalFunction("fisher3_0", () => PlayAudio("fisher3_0"));
        fisher_dialogBehaviour.BindExternalFunction("fisher3", () => PlayAudio("fisher3"));
        fisher_dialogBehaviour.BindExternalFunction("fisher4", () => PlayAudio("fisher4"));
        fisher_dialogBehaviour.BindExternalFunction("fisher5", () => PlayAudio("fisher5"));
        fisher_dialogBehaviour.BindExternalFunction("fisher6", () => PlayAudio("fisher6"));
        fisher_dialogBehaviour.BindExternalFunction("fisher7", () => PlayAudio("fisher7"));
        fisher_dialogBehaviour.BindExternalFunction("fisher_yes", () => PlayAudio("fisher_yes"));
        fisher_dialogBehaviour.BindExternalFunction("fisher_no1", () => PlayAudio("fisher_no1"));
        fisher_dialogBehaviour.BindExternalFunction("fisher_no2", () => PlayAudio("fisher_no2"));
        fisher_dialogBehaviour.BindExternalFunction("fisher_no3", () => PlayAudio("fisher_no3"));
        fisher_dialogBehaviour.BindExternalFunction("fisher_no4", () => PlayAudio("fisher_no4"));

        youngBoss_dialogBehaviour.BindExternalFunction("youngBoss1", () => PlayAudio("youngBoss1"));
        youngBoss_dialogBehaviour.BindExternalFunction("youngBoss2", () => PlayAudio("youngBoss2"));
        youngBoss_dialogBehaviour.BindExternalFunction("youngBoss3", () => PlayAudio("youngBoss3"));

        youngMan_dialogBehaviour.BindExternalFunction("youngMan1_1", () => PlayAudio("youngMan1_1"));
        youngMan_dialogBehaviour.BindExternalFunction("youngMan1_2", () => PlayAudio("youngMan1_2"));
        youngMan_dialogBehaviour.BindExternalFunction("youngMan1_3", () => PlayAudio("youngMan1_3"));
        youngMan_dialogBehaviour.BindExternalFunction("youngMan1_4", () => PlayAudio("youngMan1_4"));
        youngMan_dialogBehaviour.BindExternalFunction("youngMan2", () => PlayAudio("youngMan2"));
        youngMan_dialogBehaviour.BindExternalFunction("youngMan3", () => PlayAudio("youngMan3"));
        youngMan_dialogBehaviour.BindExternalFunction("youngMan4", () => PlayAudio("youngMan4"));
        youngMan_dialogBehaviour.BindExternalFunction("youngMan5", () => PlayAudio("youngMan5"));
        youngMan_dialogBehaviour.BindExternalFunction("youngMan6", () => PlayAudio("youngMan6"));

        chaBoss_dialogBehaviour.BindExternalFunction("chaBoss1", () => PlayAudio("chaBoss1"));
        chaBoss_dialogBehaviour.BindExternalFunction("chaBoss2", () => PlayAudio("chaBoss2"));
        chaBoss_dialogBehaviour.BindExternalFunction("chaBoss3", () => PlayAudio("chaBoss3"));
        chaBoss_dialogBehaviour.BindExternalFunction("chaBoss4", () => PlayAudio("chaBoss4"));

        two_dialogBehaviour.BindExternalFunction("two0", () => PlayAudio("two0"));
        two_dialogBehaviour.BindExternalFunction("two1", () => PlayAudio("two1"));
        two_dialogBehaviour.BindExternalFunction("two2", () => PlayAudio("two2"));
        two_dialogBehaviour.BindExternalFunction("two3", () => PlayAudio("two3"));
        two_dialogBehaviour.BindExternalFunction("two4", () => PlayAudio("two4"));
        two_dialogBehaviour.BindExternalFunction("two5", () => PlayAudio("two5"));
        two_dialogBehaviour.BindExternalFunction("two6", () => PlayAudio("two6"));
        two_dialogBehaviour.BindExternalFunction("two_yes", () => PlayAudio("two_yes"));
        two_dialogBehaviour.BindExternalFunction("two_no", () => PlayAudio("two_no"));
        /*
        */

        audioSource = GetComponent<AudioSource>();
        LoadAudioClips();
    }

    void PlayAudio(string key)
    {
        if (audioClips.ContainsKey(key))
        {
            audioSource.clip = audioClips[key];
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("音效 " + key + " 不存在！");
        }
    }

    void LoadAudioClips()
    {
        audioClips.Add("buBoss1", Resources.Load<AudioClip>("DialogVoice/ch4_buBoss/ch4_buBoss1"));
        audioClips.Add("buBoss2", Resources.Load<AudioClip>("DialogVoice/ch4_buBoss/ch4_buBoss2"));
        audioClips.Add("buBoss3", Resources.Load<AudioClip>("DialogVoice/ch4_buBoss/ch4_buBoss3"));

        audioClips.Add("walker1", Resources.Load<AudioClip>("DialogVoice/ch4_walker/ch4_walker1"));
        audioClips.Add("walker2", Resources.Load<AudioClip>("DialogVoice/ch4_walker/ch4_walker2"));

        audioClips.Add("fisher0", Resources.Load<AudioClip>("DialogVoice/ch4_fisher/ch4_fisher0"));
        audioClips.Add("fisher1", Resources.Load<AudioClip>("DialogVoice/ch4_fisher/ch4_fisher1"));
        audioClips.Add("fisher2", Resources.Load<AudioClip>("DialogVoice/ch4_fisher/ch4_fisher2"));
        audioClips.Add("fisher3_0", Resources.Load<AudioClip>("DialogVoice/ch4_fisher/ch4_fisher3_0"));
        audioClips.Add("fisher3", Resources.Load<AudioClip>("DialogVoice/ch4_fisher/ch4_fisher3"));
        audioClips.Add("fisher4", Resources.Load<AudioClip>("DialogVoice/ch4_fisher/ch4_fisher4"));
        audioClips.Add("fisher5", Resources.Load<AudioClip>("DialogVoice/ch4_fisher/ch4_fisher5"));
        audioClips.Add("fisher6", Resources.Load<AudioClip>("DialogVoice/ch4_fisher/ch4_fisher6"));
        audioClips.Add("fisher7", Resources.Load<AudioClip>("DialogVoice/ch4_fisher/ch4_fisher7"));
        audioClips.Add("fisher_yes", Resources.Load<AudioClip>("DialogVoice/ch4_fisher/ch4_fisher_yes"));
        audioClips.Add("fisher_no1", Resources.Load<AudioClip>("DialogVoice/ch4_fisher/ch4_fisher_no1"));
        audioClips.Add("fisher_no2", Resources.Load<AudioClip>("DialogVoice/ch4_fisher/ch4_fisher_no2"));
        audioClips.Add("fisher_no3", Resources.Load<AudioClip>("DialogVoice/ch4_fisher/ch4_fisher_no3"));
        audioClips.Add("fisher_no4", Resources.Load<AudioClip>("DialogVoice/ch4_fisher/ch4_fisher_no4"));

        audioClips.Add("youngBoss1", Resources.Load<AudioClip>("DialogVoice/ch4_youngBoss/ch4_youngBoss1"));
        audioClips.Add("youngBoss2", Resources.Load<AudioClip>("DialogVoice/ch4_youngBoss/ch4_youngBoss2"));
        audioClips.Add("youngBoss3", Resources.Load<AudioClip>("DialogVoice/ch4_youngBoss/ch4_youngBoss3"));

        audioClips.Add("youngMan1_1", Resources.Load<AudioClip>("DialogVoice/ch4_youngMan/ch4_youngMan1_1"));
        audioClips.Add("youngMan1_2", Resources.Load<AudioClip>("DialogVoice/ch4_youngMan/ch4_youngMan1_2"));
        audioClips.Add("youngMan1_3", Resources.Load<AudioClip>("DialogVoice/ch4_youngMan/ch4_youngMan1_3"));
        audioClips.Add("youngMan1_4", Resources.Load<AudioClip>("DialogVoice/ch4_youngMan/ch4_youngMan1_4"));
        audioClips.Add("youngMan2", Resources.Load<AudioClip>("DialogVoice/ch4_youngMan/ch4_youngMan2"));
        audioClips.Add("youngMan3", Resources.Load<AudioClip>("DialogVoice/ch4_youngMan/ch4_youngMan3"));
        audioClips.Add("youngMan4", Resources.Load<AudioClip>("DialogVoice/ch4_youngMan/ch4_youngMan4"));
        audioClips.Add("youngMan5", Resources.Load<AudioClip>("DialogVoice/ch4_youngMan/ch4_youngMan5"));
        audioClips.Add("youngMan6", Resources.Load<AudioClip>("DialogVoice/ch4_youngMan/ch4_youngMan6"));

        audioClips.Add("chaBoss1", Resources.Load<AudioClip>("DialogVoice/ch4_chaBoss/ch4_chaBoss1"));
        audioClips.Add("chaBoss2", Resources.Load<AudioClip>("DialogVoice/ch4_chaBoss/ch4_chaBoss2"));
        audioClips.Add("chaBoss3", Resources.Load<AudioClip>("DialogVoice/ch4_chaBoss/ch4_chaBoss3"));
        audioClips.Add("chaBoss4", Resources.Load<AudioClip>("DialogVoice/ch4_chaBoss/ch4_chaBoss4"));

        audioClips.Add("two0", Resources.Load<AudioClip>("DialogVoice/ch4_two/ch4_two0"));
        audioClips.Add("two1", Resources.Load<AudioClip>("DialogVoice/ch4_two/ch4_two1"));
        audioClips.Add("two2", Resources.Load<AudioClip>("DialogVoice/ch4_two/ch4_two2"));
        audioClips.Add("two3", Resources.Load<AudioClip>("DialogVoice/ch4_two/ch4_two3"));
        audioClips.Add("two4", Resources.Load<AudioClip>("DialogVoice/ch4_two/ch4_two4"));
        audioClips.Add("two5", Resources.Load<AudioClip>("DialogVoice/ch4_two/ch4_two5"));
        audioClips.Add("two6", Resources.Load<AudioClip>("DialogVoice/ch4_two/ch4_two6"));
        audioClips.Add("two_yes", Resources.Load<AudioClip>("DialogVoice/ch4_two/ch4_two_yes"));
        audioClips.Add("two_no", Resources.Load<AudioClip>("DialogVoice/ch4_two/ch4_two_no"));
        /*
        */
    }
}