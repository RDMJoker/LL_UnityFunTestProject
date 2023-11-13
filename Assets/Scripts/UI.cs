using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _foodCount;
    [SerializeField] TextMeshProUGUI _endGameMessage;
    [SerializeField] PlayerInventory _currentPlayerInventory;
    [SerializeField] PickUpHandler _pickUpHandler;
    [SerializeField] Image _firstKey;
    [SerializeField] Image _secondKey;
    [SerializeField] Image _thirdKey;
    [SerializeField] Sprite _defaultKeySprite;
    [SerializeField] EndGame _endGame;
    private int keySlot = 0;
    void Update()
    {
        _foodCount.text = _currentPlayerInventory.CurrentFoodCount.ToString();
    }
    private void Awake()
    {
        _endGame.OnEndGame += PrintEndGame;
        _endGame.OnKillPlayer += PrintDeathScreen;
        _pickUpHandler.OnKeyPickup += UpdateKeyUI;
    }

    private void PrintDeathScreen()
    {
        _endGameMessage.text = "You died!";
    }

    private void UpdateKeyUI(int _keyID)
    {
        keySlot++;
        switch (CheckFreeUIKeySlot())
        {
            case 1:
                _firstKey.sprite = _defaultKeySprite;
                _firstKey.color = GetPickedUpKeyColor(_keyID);
                break;
            case 2:
                _secondKey.sprite = _defaultKeySprite;
                _secondKey.color = GetPickedUpKeyColor(_keyID);
                break;
            case 3:
                _thirdKey.sprite = _defaultKeySprite;
                _thirdKey.color = GetPickedUpKeyColor(_keyID);
                break;
            default:
                break;
        }
    }

    private int CheckFreeUIKeySlot()
    {
        if (keySlot <= 3)
        {
            return keySlot;
        }
        return 0;
    }

    private Color GetPickedUpKeyColor(int _keyID)
    {
        switch (_keyID)
        {
            case 1:
                return Color.red;
            case 2:
                return Color.green;
            case 3:
                return Color.blue;
            default:
                break;
        }
        return Color.clear;
    }

    private void PrintEndGame()
    {
        _endGameMessage.text = "You won!";
    }
}
