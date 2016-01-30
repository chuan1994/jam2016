using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
}
