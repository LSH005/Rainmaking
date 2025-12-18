using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    Dictionary<int, ItemType> CraftId = new Dictionary<int, ItemType>()
    {
        // ItemID - ItemType
        { 382, ItemType.Pull }
    };

    [Header("ÆÐ³Î")]
    public GameObject BG;

    [Header("¸ðµç ½½·Ô")]
    public CraftingSlot[] slots;

    [Header("¿Ï·á ½½·Ô")]
    public Image completeSlot;

    int CurrentCraftId;
    public static bool isEnabled;

    void Start()
    {
        BG.SetActive(false);

        foreach (var slot in slots)
        {
            slot.manager = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) TogglePanel();
    }

    void TogglePanel()
    {
        isEnabled = !isEnabled;
        BG.SetActive(isEnabled);
        Cursor.lockState = isEnabled ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void AddCraftId(int value)
    {
        CurrentCraftId += value;
        if (CraftId.TryGetValue(CurrentCraftId, out ItemType type))
        {
            Sprite sprite = InventoryManager.instance.GetImageFromBlockType(type);
            completeSlot.sprite = sprite;
        }
        else
        {
            completeSlot.sprite = null;
        }
    }

    public void Craft()
    {
        if (CraftId.TryGetValue(CurrentCraftId, out ItemType type))
        {
            InventoryManager.instance.AddItem(ItemType.Pull, 1);
            ClearAllSlot();
        }
    }

    void ClearAllSlot()
    {
        completeSlot.sprite = null;
        foreach (var slot in slots)
        {
            slot.ClearSlot();
        }
        CurrentCraftId = 0;
    }
}
