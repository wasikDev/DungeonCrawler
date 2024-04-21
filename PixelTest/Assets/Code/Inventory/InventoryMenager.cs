using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject InventoryMenu;
    private bool menuActivated = false;
    public ItemSlot[] itemSlot;
    public ItemSO[] itemSos;
    public HealthManager healthManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Inventory")&& menuActivated) 
        {
            Time.timeScale = 1;
        InventoryMenu.SetActive(false);
            menuActivated = false;
        
        }
        else if (Input.GetButtonDown("Inventory") && !menuActivated)
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActivated = true;

        }
    }

    public bool UseItem(string itemName)
    {
        for (int i = 0; i < itemSos.Length; i++)
        {
            if (itemSos[i].itemName == itemName)
            {
                bool usable = itemSos[i].UseItem();
                return usable;
            }
        
        }
        return false;

    }


    public int AddItem(string itemName,int quantity,Sprite itemSprite,string itemDescription)
    {
       for(int i=0;i<itemSlot.Length;i++)
        {
            if(itemSlot[i].isFull==false && itemSlot[i].itemName == itemName || itemSlot[i].quantity ==0 )
            {
               int leftOverItems =  itemSlot[i].AddItem(itemName,quantity,itemSprite, itemDescription);
                if (leftOverItems > 0)
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription);
                return leftOverItems;
            }

        }
        return quantity;
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }
}
