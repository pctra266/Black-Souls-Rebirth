using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MenuButtonEffect : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI textMeshPro;
    public Texture2D normalCursor; // Chuột bình thường

    private float originalFontSize;
    private float targetFontSize;
    private Color originalColor;
    private Color targetColor;

    public float lerpSpeed = 10f;

    public Color hoverColor = new Color(1f, 1f, 1f, 1f); // Màu khi hover
    public Color defaultColor = new Color(0.8f, 0.8f, 0.8f, 1f); // Màu mặc định

    void Start()
    {
        originalFontSize = textMeshPro.fontSize;
        targetFontSize = originalFontSize;
        originalColor = textMeshPro.color;
        targetColor = originalColor;
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);

    }

    void Update()
    {
        textMeshPro.fontSize = Mathf.Lerp(textMeshPro.fontSize, targetFontSize, Time.deltaTime * lerpSpeed);
        textMeshPro.color = Color.Lerp(textMeshPro.color, targetColor, Time.deltaTime * lerpSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetFontSize = originalFontSize + 5f; // Tăng font size
        targetColor = hoverColor; // Sáng lên
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        targetFontSize = originalFontSize; // Trở về size cũ
        targetColor = defaultColor; // Trở về màu mặc định
    }
}
