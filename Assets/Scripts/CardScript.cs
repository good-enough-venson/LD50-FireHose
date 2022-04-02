using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public TextMeshProUGUI label;
    public Image cardBack;
    public GameObject waterGraphic;
    public GameObject ladderGraphic;
    public GameObject argonGraphic;
    public GameObject stimGraphic;
    public CardData data;

    public float tintAmount = 0.2f;

    public void UpdateGraphics() {
        if (label) label.text = data.value.ToString();
        if (cardBack) cardBack.color = Vector4.Lerp(Color.white, data.color, tintAmount);
        if (waterGraphic) waterGraphic.SetActive(data.type == CardType.Water);       
        if (ladderGraphic) ladderGraphic.SetActive(data.type == CardType.Ladder);
        if (argonGraphic) argonGraphic.SetActive(data.type == CardType.Argon);
        if (stimGraphic) stimGraphic.SetActive(data.type == CardType.Stim);
    }

    void OnValidate() {
        UpdateGraphics();
    }
}

[System.Serializable]
public class CardData {
    public string name = "Card";
    public CardType type = CardType.Water;
    public int value = 1;

    public float regenTime = 0;
    public Color color;
}

public enum CardType { Water, Ladder, Argon, Stim }
