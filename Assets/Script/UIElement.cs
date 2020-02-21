using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIElement : MonoBehaviour
{
    public Text Health;
    public Text Point;
    public Text Score;

    PlayerController Player;
    // Start is called before the first frame update
    void Start()
    {
        Health.text = "100";
        Point.text = "00";
        Score.text = "00";
        Player = PlayerController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        Health.text = Player.Health.ToString();
        Point.text = "0"+Player.Points;
        Score.text = Point.text;
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
