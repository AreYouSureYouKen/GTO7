using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BasicEnemy : MonoBehaviour
{

    public float startingHealth = 100f;
    public RectTransform CanvasRect;
    public Image HealthBar;
    public Image currentHealthBar;
    private float _currentHealth;
    // Use this for initialization
    void Start()
    {
        _currentHealth = startingHealth;
        currentHealthBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // floating health bar taken from http://answers.unity3d.com/questions/799616/unity-46-beta-19-how-to-convert-from-world-space-t.html and changed a bit.
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(this.gameObject.transform.position + Vector3.up * 1.2f);
        Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

        HealthBar.rectTransform.anchoredPosition = WorldObject_ScreenPosition;
    }

    public void Damage(float dmg)
    {
        _currentHealth -= dmg;
        if (_currentHealth <= 0f)
        {
            Destroy(this.gameObject);
            Destroy(HealthBar);
        }

        float percentageLeft = _currentHealth / startingHealth;
        currentHealthBar.fillAmount = percentageLeft;
    }
}
