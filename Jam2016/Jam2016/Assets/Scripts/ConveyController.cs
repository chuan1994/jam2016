using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConveyController : MonoBehaviour {

    [SerializeField]
    float waitTime;

    //Ingredient lists
    [SerializeField]
    List<GameObject> tierTwo = new List<GameObject>();
    [SerializeField]
    List<GameObject> tierThree = new List<GameObject>();
    [SerializeField]
    List<GameObject> tierFour = new List<GameObject>();

    List<GameObject> currentGen = new List<GameObject>();

    [SerializeField]
    bool produce;

    [SerializeField]
    Vector3 spawnPoint;

    [SerializeField]
    int difficulty;


	// Use this for initialization
	void Start () {
        newGenList();
        produce = true;
        StartCoroutine("Wrapper");
	}
	
	// Update is called once per frame
	void Update () {
	}
    //===========================================================================================
    //Getters and Setters
    void setWaitTime(float newWait) {
        waitTime = newWait;
    }

    void enableProduce(bool prod) {
        produce = prod;

        if (produce == true) {
            StartCoroutine("Wrapper");
        }
    }

    //===========================================================================================

    void newGenList(){
        int add = difficulty - currentGen.Count;
        int count = 0;

        while (count < add) {
            int pos;
            if (tierTwo.Count > 0)
            {
                pos = Random.Range(0, tierTwo.Count);
                currentGen.Add(tierTwo[pos]);
                tierTwo.RemoveAt(pos);
                count++;
            }
            else if (tierThree.Count > 0)
            {
                pos = Random.Range(0, tierThree.Count);
                currentGen.Add(tierThree[pos]);
                tierThree.RemoveAt(pos);
                count++;
            }
            else if (tierFour.Count > 0)
            {
                pos = Random.Range(0, tierFour.Count);
                currentGen.Add(tierFour[pos]);
                tierFour.RemoveAt(pos);
                count++;
            }
            else
            {
                count = add;
            }
        }
    }

    IEnumerator Wrapper() {
        while (produce) {
            yield return StartCoroutine("GenerateNext");
        }
    }


    IEnumerator GenerateNext(){
        newGenList();
        int pos = Random.Range(0, currentGen.Count);

        GameObject go = Instantiate(currentGen[pos]);
        go.GetComponent<BaseIngredientController>().conveyModeOn(difficulty);
        go.transform.position = spawnPoint;
        go.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        go.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.75f);

        yield return new WaitForSeconds(waitTime);
    }


    //Event to call when difficulty increases
    public void IncreaseDifficulty(int newDiff) {
        difficulty = newDiff;

        newGenList();
    }
}
