using System.Collections;
using UnityEngine.UI;


using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Image fadeimage;
    private blade blade;
    private spawner spawner;
    

    private int score;


    private void Awake()
    {
       blade = FindObjectOfType<blade>();
       spawner = FindObjectOfType<spawner>();
    }
    public void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        Time.timeScale = 1f;

        blade.enabled = true;
        spawner.enabled = true;

        score = 0;
        scoreText.text = score.ToString();

        ClearScene();
        
    }

    private void ClearScene()
    {
        fruit[] fruits  = FindObjectsOfType<fruit>();
        foreach(fruit fruit in fruits){
            Destroy(fruit.gameObject);
        }

        Bomb[] bombs  = FindObjectsOfType<Bomb>();
        foreach(Bomb bomb in bombs){
            Destroy(bomb.gameObject);
        }
    }


    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
    public void Explode()
    {
        blade.enabled = false;
        spawner.enabled = false;
        StartCoroutine(ExplodeSequence());
    }

    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while(elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeimage.color = Color.Lerp(Color.clear, Color.white, t);
            
            Time.timeScale = 1f - t;

            elapsed += Time.unscaledDeltaTime;
         
            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);
        NewGame();

        elapsed =0f;

         while(elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeimage.color = Color.Lerp(Color.white, Color.clear, t);
            
            

            elapsed += Time.unscaledDeltaTime;
         
            yield return null;
        }
    }

}
