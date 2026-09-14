using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Animation settings")]
    public Ease easeType;
    public float duration;
    public RectTransform gameoverRect;

    public TextMeshProUGUI gameOverLabel;
    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI highScoreLabel;
    public TextMeshProUGUI healthLabel;
    public TextMeshProUGUI statsLabel;
    

    public Button restartBtn;

    [Header("Damage UI")]
    public GameObject damageUIPrefab;
    public RectTransform damageTextParent;



    private void Start()
    {
        //restartBtn.onClick.AddListener(GameManager.Instance.RestartGame);
        SetScoreUI();
        SetHighScoreUI();
        SetHealthUI();
       
    }

    public void SetScoreUI()
    {
        scoreLabel.text = GameManager.Instance.score.ToString();

       
        
        statsLabel.text = GameManager.Instance.score.ToString();
    }

    public void SetHighScoreUI()
    {
        highScoreLabel.text = GameManager.Instance.highScore.ToString();
    }

    public void SetHealthUI()
    {
        healthLabel.text = GameManager.Instance.health.ToString();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ShowGameOverUI();
        }

        //SetHealthLabel();
    }

  

   

    public void ShowGameOverUI(bool isGameOver = true)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.SetUpdate(true);
        sequence.Append(gameoverRect.DOScale(Vector3.one,duration).SetEase(easeType));
        gameOverLabel.text = isGameOver ? "GAME OVER" : "GAME WON";
        SetScoreUI();
        Time.timeScale = 0;
    }

    public void HideGameOverUI()
    {        
        Sequence sequence = DOTween.Sequence();
        sequence.SetUpdate(true);
        sequence.Append(gameoverRect.DOScale(Vector3.zero, duration).SetEase(easeType));
        Time.timeScale = 1f;
    }


    public void DamageUI(float damage, bool isCrit, Transform location)
    {
    GameObject uiObject = Instantiate(damageUIPrefab, damageTextParent); // parent immediately
    uiObject.transform.localPosition = Vector3.zero; // reset — ShowDamage sets real position anyway

        DamageTextUI ui = uiObject.GetComponent<DamageTextUI>();
          ui.Initialize();
         ui.ShowDamage(damage, isCrit, location); // uncomment this — this was the actual bug
    }
}
