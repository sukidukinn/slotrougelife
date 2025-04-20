using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DungeonRunManager : MonoBehaviour
{
    public CardButtonUI[] cardButtons; // 3枚表示用
    private CardBase[] drawnCards;

    private bool hasSelected = false;

    void Start()
    {
        DrawInitialCards();
    }

    void DrawInitialCards()
    {
        drawnCards = new CardBase[3];

        for (int i = 0; i < 3; i++)
        {
            var card = GameManager.Instance.masterCardList[i % GameManager.Instance.masterCardList.Count].Clone();
            drawnCards[i] = card;
            cardButtons[i].SetCard(card, () => OnCardSelected(card));
        }
    }

    void OnCardSelected(CardBase card)
    {
        if (hasSelected) return;
        hasSelected = true;

        Debug.Log("カード選択完了: " + card.name);
        Invoke(nameof(ReturnToTitle), 1.5f); // 仮の遷移処理
    }

    void ReturnToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
