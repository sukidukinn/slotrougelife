using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DungeonSceneManager : MonoBehaviour
{
    [Header("操作ボタン")]
    public Button drawButton;
    public Button clearButton;
    public Button returnButton;

    [Header("カード表示UI")]
    public CardButtonUI[] cardButtons; // 左:0, 中央:1, 右:2

    [Header("共通メッセージ表示")]
    public TMP_Text messageText;

    private DungeonDeck deck;

    private void Start()
    {
        drawButton.onClick.AddListener(ShowNextDraw);
        clearButton.onClick.AddListener(TriggerClear);
        returnButton.onClick.AddListener(ReturnToTitle);

        deck = GameManager.Instance.SharedGameState.GetDeck();

        if (deck == null)
        {
            deck = CreateDummyDeck();
            GameManager.Instance.SharedGameState.SetDeck(deck);
        }

        ShowNextDraw();
    }

    void ShowNextDraw()
    {
        var drawn = deck.Draw(3);

        for (int i = 0; i < cardButtons.Length; i++)
        {
            if (i < drawn.Count)
            {
                var card = drawn[i];
                int index = i; // ローカルキャプチャ
                cardButtons[i].SetCard(card, () => OnCardSelected(index, card));
                cardButtons[i].gameObject.SetActive(true);
            }
            else
            {
                cardButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void OnCardSelected(int index, CardBase card)
    {
        string[] positions = { "左", "中央", "右" };
        string msg = $"{positions[index]}カード選択（{card.name}）";
        messageText.text = msg;

        // 必要に応じて報酬処理や遷移もここに追加
    }

    void TriggerClear()
    {
        GameManager.Instance.SharedGameState.SetReward(new List<CardBase>(), "テスト報酬");
        SceneManager.LoadScene("TitleScene");
    }

    void ReturnToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    private DungeonDeck CreateDummyDeck()
    {
        var deck = new DungeonDeck();
        deck.Initialize(System.DateTime.Now.Millisecond);

        for (int i = 0; i < 10; i++)
        {
            var dummy = new CardBase
            {
                id = $"TEST_{i}",
                name = $"仮カード{i + 1}",
                description = $"これは仮のカードです{i + 1}",
                type = "buff"
            };
            deck.AddCard(dummy);
        }

        var boss = new CardBase
        {
            id = "BOSS",
            name = "仮ボスカード",
            description = "これはボスです",
            type = "enemy"
        };

        deck.SetBossCard(boss);
        return deck;
    }
}
