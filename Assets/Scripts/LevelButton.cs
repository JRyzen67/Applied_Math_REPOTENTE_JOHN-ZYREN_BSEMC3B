using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelButton : MonoBehaviour
{
    public int requiredPoints;
    public int levelIndex;
    public Button levelBtn;

    private void Start()
    {
        #region
        var currentScore = PlayerPrefs.GetInt("HighScore");
        if (currentScore >= requiredPoints)
        {
            levelBtn.interactable = true;
        }
        else
        {
            levelBtn.interactable = false;
        }
        #endregion
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelIndex);
    }
}
