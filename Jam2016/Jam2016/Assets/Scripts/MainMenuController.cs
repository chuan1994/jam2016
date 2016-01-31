using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GameStart()
    {
        StartCoroutine(fadeOutMenu());
    }

    IEnumerator fadeOutMenu()
    {
        while (gameObject.GetComponent<CanvasGroup>().alpha != 0)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = gameObject.GetComponent<CanvasGroup>().alpha - 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.SetActive(false);
    }

    public void showMenu()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1f;
        gameObject.SetActive(true);
    }
    public void PlayAudio()
    {
        audioSource.Play();
    }
}
