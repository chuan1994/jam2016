﻿using UnityEngine;
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

    bool notPlaying;

    void Start() {
        notPlaying = true;
    }
	//Changed to designate when to start -Andy
	public void GameStart () {
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

    public void IncreaseDifficulty() {
        difficulty++;
        if (difficulty % 2 == 0) {
            if (waitTime > 4) {
                waitTime -= 0.5f;
            }
        }

        newGenList();
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
        if (notPlaying)
        {
            notPlaying = false;
            while (produce)
            {
                yield return StartCoroutine("GenerateNext");
            }
        }
    }


    IEnumerator GenerateNext(){
        newGenList();
        int pos = Random.Range(0, currentGen.Count);

        GameObject go = Instantiate(currentGen[pos]);
        go.GetComponent<BaseIngredientController>().conveyModeOn(0.5f);
        go.transform.position = spawnPoint;
        go.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        go.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.75f);

        yield return new WaitForSeconds(waitTime);
    }
}
