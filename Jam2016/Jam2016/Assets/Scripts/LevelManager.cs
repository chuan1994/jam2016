using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public int SCORE;

    [SerializeField]
    GameObject conveyController;
    [SerializeField]
    GameObject timer;

    GameObject mixIngredient1;
    GameObject mixIngredient2;

    public GameObject product;

    public delegate void enable();
    public static event enable enableActions;
    public static event enable enableIngredients;

    public GameObject gameOverScreen;

    void Awake()
    {
        Subscribe();
    }

	// Use this for initialization
	void Start () {
        enableIngredient();
	}
    
    void OnDestroy()
    {
        Unsubscribe();
    }

	// Update is called once per frame
	void Update () {
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
        }*/
	}

    public void startLevel() {
        StartCoroutine("initDelay");
    }

    IEnumerator initDelay()
    {
        yield return new WaitForSeconds(1.8f);
        conveyController.GetComponent<ConveyController>().GameStart();
        timer.GetComponent<TimeController>().setTime(120);
        timer.GetComponent<TimeController>().gameOver = false;
        timer.SetActive(true);
        while (timer.activeSelf) {
            yield return StartCoroutine("increasedLevel");
        }
    }
    
    IEnumerator increasedLevel()
    {
        yield return new WaitForSeconds(15);
        conveyController.GetComponent<ConveyController>().IncreaseDifficulty();
    }




    private void ProcessStash(Vector3 stashPosition)
    {
        enableIngredient();
        if (product.GetComponent<BaseIngredientController>() != null)
        {
            product.transform.position = stashPosition;
        }

        else {
            Destroy(product);
        }
    }

    //Upon click of an area for product action
    private void ProcessClick(GameObject clickedObject)
    {
        if (clickedObject.tag.Equals("trash"))
        {
            Destroy(product);
        }
        else if (clickedObject.tag.Equals("box"))
        {

        }
        enableIngredient();
    }

    //Upon production from two ingredients
    private void ProcessProduct(GameObject product)
    {
        enableAction();
        this.product = product;
        Destroy(this.product.GetComponent<ShelfIngredientController>());
    }

    private void enableAction()
    {
        if (enableActions != null)
        {
            enableActions();
        }
    }

    private void enableIngredient()
    {
        if (enableIngredients != null)
        {
            enableIngredients();
        }
    }

    private void TargetClicked(GameObject target) {
        if (target.GetComponent<BaseIngredientController>().id == product.GetComponent<BaseIngredientController>().id)
        {
            Vector3 movePoint = target.transform.position;
            Destroy(target);
            product.transform.position = movePoint;
            product.GetComponent<BaseIngredientController>().conveyModeOn(5);
            product.GetComponent<Collider2D>().enabled = false;
        } else
        {
            Destroy(product);
        }
        enableIngredient();
    }

    void targetReached(GameObject go) {
        if (go.GetComponent<SpriteRenderer>().color.a != 1f)
        {
            Destroy(go);
            timer.GetComponent<TimeController>().badMove();
        }
        else {
            Destroy(go);
            SCORE++;
            timer.GetComponent<TimeController>().goodMove(SCORE);
        }
    }

    void gameOver(int score)
    {
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
        GameObject.Find("Score").GetComponent<Text>().text = score+"";
    }

    void deathRestart() {
        if (enableIngredients != null) {
            enableIngredients();
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //event subscription and unsubscription
    private void Subscribe()
    {
        PentagonController.PentHandler += ProcessProduct;
        ShelfIngredientController.ShelfMouseDown += ProcessStash;
        GeneralClickController.eventUponClick += ProcessClick;
        ConveyEvent.mousePressedEvent += TargetClicked;
        BaseIngredientController.reached += targetReached;
        TimeController.gameOverHandler += gameOver;
        DeathController.sendEvent += deathRestart;
    }

    private void Unsubscribe()
    {
        ShelfIngredientController.ShelfMouseDown -= ProcessStash;
        GeneralClickController.eventUponClick -= ProcessClick;
        PentagonController.PentHandler -= ProcessProduct;
        ConveyEvent.mousePressedEvent -= TargetClicked;
        BaseIngredientController.reached -= targetReached;
        TimeController.gameOverHandler -= gameOver;
        DeathController.sendEvent += deathRestart;

    }
}
