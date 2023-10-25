using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceNotification : MonoBehaviour
{
    public TextMeshProUGUI thisUIText;
    public AudioClip successAudio;
    public AudioClip failureAudio;
    public AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        thisUIText = gameObject.GetComponent<TextMeshProUGUI>();
        Hide();
        SetMessage("");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void SetMessage(string str)
    {
        Hide();
        thisUIText.text = str;
        Show();
    }

    public void PlaySuccess()
    {
        audioSource.PlayOneShot(successAudio);
    }

    public void PlayFailure()
    {
        audioSource.PlayOneShot(failureAudio);
    }
}
