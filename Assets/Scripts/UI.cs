using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [FormerlySerializedAs("_foodCount")] [SerializeField] TextMeshProUGUI foodCount;
    [FormerlySerializedAs("_endGameMessage")] [SerializeField] TextMeshProUGUI endGameMessage;
    [FormerlySerializedAs("_currentPlayerInventory")] [SerializeField] PlayerInventory currentPlayerInventory;
    [FormerlySerializedAs("_pickUpHandler")] [SerializeField] PickUpHandler pickUpHandler;
    [FormerlySerializedAs("_firstKey")] [SerializeField] Image firstKey;
    [FormerlySerializedAs("_secondKey")] [SerializeField] Image secondKey;
    [FormerlySerializedAs("_thirdKey")] [SerializeField] Image thirdKey;
    [FormerlySerializedAs("_defaultKeySprite")] [SerializeField] Sprite defaultKeySprite;
    [FormerlySerializedAs("_endGame")] [SerializeField] EndGame endGame;
    int keySlot;
    void Update()
    {
        foodCount.text = currentPlayerInventory.CurrentFoodCount.ToString();
    }

    void Awake()
    {
        endGame.OnEndGame += PrintEndGame;
        endGame.OnKillPlayer += PrintDeathScreen;
        pickUpHandler.OnKeyPickup += UpdateKeyUI;
    }

    void PrintDeathScreen()
    {
        endGameMessage.text = "You died!";
    }

    void UpdateKeyUI(int _keyID)
    {
        keySlot++;
        switch (CheckFreeUIKeySlot())
        {
            case 1:
                firstKey.sprite = defaultKeySprite;
                firstKey.color = GetPickedUpKeyColor(_keyID);
                break;
            case 2:
                secondKey.sprite = defaultKeySprite;
                secondKey.color = GetPickedUpKeyColor(_keyID);
                break;
            case 3:
                thirdKey.sprite = defaultKeySprite;
                thirdKey.color = GetPickedUpKeyColor(_keyID);
                break;
        }
    }

    int CheckFreeUIKeySlot()
    {
        return keySlot <= 3 ? keySlot : 0;
    }

    Color GetPickedUpKeyColor(int _keyID)
    {
        return _keyID switch
        {
            1 => Color.red,
            2 => Color.green,
            3 => Color.blue,
            _ => Color.clear
        };
    }

    void PrintEndGame()
    {
        endGameMessage.text = "You won!";
    }
}
