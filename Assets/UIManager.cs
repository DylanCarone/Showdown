using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI readyText;
    [SerializeField] Color32  readyTextColor;
    [SerializeField] private TextMeshProUGUI restartText;
    [SerializeField] float blinkInterval = 0.5f;
    [SerializeField] Image blinkImage;


    public void SetReadyText(string text)
    {
        readyText.text = text;
        readyText.color =  readyTextColor;
    }

    public void SetRestartText()
    {
        restartText.gameObject.SetActive(true);
        StartCoroutine(BlinkRoutine());
    }


    IEnumerator BlinkRoutine()
    {
        while (true)
        {
            restartText.enabled = !restartText.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    public void GunFlash()
    {
        StartCoroutine(BlinkAnimation());
    }
    IEnumerator BlinkAnimation()
    {
        blinkImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        blinkImage.gameObject.SetActive(false);
    }
}
