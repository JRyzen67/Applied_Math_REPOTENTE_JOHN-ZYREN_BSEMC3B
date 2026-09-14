using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public int giveScore;
    public int requireScoreToWin;

    public void LoadMainMenu(int index)
    {
        if (GameManager.Instance.isLose)
        {
            GameManager.Instance.RestartGame();
        }
        else
        {
            SceneManager.LoadScene(index);
        }
            
    }
}
