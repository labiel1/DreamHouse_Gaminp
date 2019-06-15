using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class uiManager : MonoBehaviour {
    public Text scoreText;
    public Button[] buttons;
    public string[] NomeCena;
    int score ;
    float timer ;
    float delay = 1f;
    bool gameOver = false;
    public string CenaPlay = "Level1";
    public string MenuPlay = "mainmenu";
    public string CenaNova = "Level2";



    // Use this for initialization
    void Start () {
        timer = delay;
        
        StartCoroutine(Example());
        Pause();
    }
	
	// Update is called once per frame
	void Update () {
        if (timer <= 0 && !gameOver)
        {
            int minutes = Mathf.FloorToInt(score / 60F);
            int seconds = Mathf.FloorToInt(score - minutes * 60);
            string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
            timer = delay;
            scoreText.text = "Time : " + niceTime + " ";
            score++;

        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Replay();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Menu();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            NextScene();
        }

        timer -= Time.deltaTime;
        if (gameOver)
        {
           // Pause();
            foreach (Button button in buttons)
            {
                button.gameObject.SetActive(true);
            }
        }
	}



    IEnumerator Example()
    {
        if (CenaPlay == "Level3")
        {
          //  print("Timer Imprime" + Time.time);
            yield return new WaitForSeconds(60);
            NextScene();
         //   print("Timer Aguarda" + Time.time);
        }
    }

    public void gameOverActivated(){
        gameOver = true;
    }


	public void Pause(){
		if (Time.timeScale == 0) {
			Time.timeScale = 1;

            foreach (Button button in buttons)
            {
                button.gameObject.SetActive(false);
            }
        } else if (Time.timeScale == 1) {
			Time.timeScale = 0;

            foreach (Button button in buttons)
            {
                button.gameObject.SetActive(true);
            }
        }
	}


    public void Replay()
    {
        SceneManager.LoadScene(CenaPlay, LoadSceneMode.Single);
    }

    public void Menu()
    {
        SceneManager.LoadScene(MenuPlay, LoadSceneMode.Single);
    }

    public void Play()
    {
        SceneManager.LoadScene(CenaPlay, LoadSceneMode.Single);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(CenaNova, LoadSceneMode.Single);
    }
}
