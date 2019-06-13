using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AguardaCena : MonoBehaviour
{
    public string CenaPlay = "Level1";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PulaCena());
    }

    IEnumerator PulaCena()
    {

            print("Timer Imprime" + Time.time);
            yield return new WaitForSeconds(10);
            NextScene();
            print("Timer Aguarda" + Time.time);

    }

    public void NextScene()
    {
        SceneManager.LoadScene(CenaPlay, LoadSceneMode.Single);
    }
}
