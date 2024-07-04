using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Game : MonoBehaviour
{
    [SerializeField] private int Score;
    public int[] CostInt;
    private int ClickScore = 1;

    public GameObject ShopPanel;
    public GameObject BonusPanel;

    public Text[] CostText;
    public Text ScoreText;

    public GameObject popupMessage; 

    private void Start()
    {
        LoadProgress();
    }

    public void OnClickButton()
    {
        Score += ClickScore;
        ShowPopupMessage("+1");
        SaveProgress();
    }

    private void Update()
    {
        ScoreText.text = Score + "$";
    }

    public void ShowAndHideShopPanel()
    {
        ShopPanel.SetActive(!ShopPanel.activeSelf);
    }

    public void ShowAndHideBonusPanel()
    {
        BonusPanel.SetActive(!BonusPanel.activeSelf);
    }

    public void OnClickBuyLevel()
    {
        if (Score >= CostInt[0])
        {
            Score -= CostInt[0];
            CostInt[0] *= 2;
            ClickScore *= 2;
            CostText[0].text = CostInt[0] + "$";

            ShowPopupMessage("Level Up!"); 

            SaveProgress();
        }
    }

    private void SaveProgress()
    {
        PlayerPrefs.SetInt("Score", Score);

        for (int i = 0; i < CostInt.Length; i++)
        {
            PlayerPrefs.SetInt("CostInt_" + i, CostInt[i]);
        }

        PlayerPrefs.SetInt("ClickScore", ClickScore);

        PlayerPrefs.Save();
    }

    private void LoadProgress()
    {
        Score = PlayerPrefs.GetInt("Score", Score);

        for (int i = 0; i < CostInt.Length; i++)
        {
            CostInt[i] = PlayerPrefs.GetInt("CostInt_" + i, CostInt[i]);
            CostText[i].text = CostInt[i] + "$";
        }

        ClickScore = PlayerPrefs.GetInt("ClickScore", ClickScore);
    }

    private void ShowPopupMessage(string message)
    {
        popupMessage.SetActive(true);
        popupMessage.GetComponent<Text>().text = message;

                popupMessage.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                popupMessage.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack)
                    .SetDelay(1f)
                    .OnComplete(() =>
                    {
                        popupMessage.SetActive(false);
                    });
            });
    }
}
