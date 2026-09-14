using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamageTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageTxt;
    [SerializeField] private GameObject critIcon;
    [SerializeField] private Vector3 offset;

    public void Initialize()
    {
        if (damageTxt == null)
        {
            damageTxt = GetComponent<TextMeshProUGUI>() ?? GetComponentInChildren<TextMeshProUGUI>();
        }

        if (damageTxt != null)
        {
            damageTxt.text = string.Empty;
        }

        transform.localScale = Vector3.one;
    }

    public void ShowDamage(float damage, bool isCrit, Transform location)
    {
        if (damageTxt == null)
        {
            damageTxt = GetComponent<TextMeshProUGUI>() ?? GetComponentInChildren<TextMeshProUGUI>();
        }

        if (damageTxt == null) return;

        // DEBUG LOG: Open your Console window to see what is passed here!
        Debug.Log($"[DamageTextUI] Hit Value: {damage} | isCrit passed in: {isCrit}");

        damageTxt.DOKill();
        transform.DOKill();

        Vector3 spawnPos = location != null ? location.position + offset : transform.position;
        Vector3 pos = Camera.main != null ? Camera.main.WorldToScreenPoint(spawnPos) : spawnPos;

        // HARDCODED COLOR & SCALE VALUES (Ignores Inspector overrides)
        Color targetColor = isCrit ? new Color(1f, 0.5f, 0f) : Color.white;
        float targetScale = isCrit ? 2.5f : 1.0f;

        if (critIcon != null)
        {
            critIcon.SetActive(isCrit);
        }

        damageTxt.text = Mathf.RoundToInt(damage).ToString();
        
        targetColor.a = 1f;
        damageTxt.color = targetColor;
        
        damageTxt.transform.localScale = Vector3.one * targetScale;
        damageTxt.transform.position = pos;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(damageTxt.transform.DOMove(new Vector3(Random.Range(-50, 50), 100f, 0) + pos, 1f).SetEase(Ease.OutSine));
        sequence.Join(damageTxt.transform.DOScale(Vector3.one * targetScale, 0.3f).SetEase(Ease.OutSine));
        sequence.Append(damageTxt.DOFade(0f, 0.5f).SetEase(Ease.InQuad));
        sequence.SetTarget(gameObject);
        sequence.OnComplete(() => Destroy(gameObject));
    }

    void OnDisable()
    {
        DOTween.Kill(transform);
        if (damageTxt != null) DOTween.Kill(damageTxt);
    }
}