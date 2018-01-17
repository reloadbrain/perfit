﻿using UnityEngine;
using System.Collections.Generic;

public class AttachClothing : MonoBehaviour
{
    #region Fields
    //gameObjects
    public GameObject avatar;
	public ClothAnimation clothScript;

	public GameObject wornDress;

    //lists
	public List<ClothingItem> wornItems = new List<ClothingItem>();
    //ints
    private int totalSlots;
    #endregion

    #region Monobehaviour

    public void InitializeClothingItemsList()
    {
        totalSlots = 2;

        for (int i = 0; i < totalSlots; i++)
        {
			wornItems.Add(new ClothingItem());
        }

        AddClothingToList(0); //Dress
		AddClothingToList(1); //Dress

//        AddEquipmentToList(1); //Chest
//        AddEquipmentToList(2); //Hair 
//        AddEquipmentToList(3); //Beard 
//        AddEquipmentToList(4); //Mustache
//        AddEquipmentToList(5); //HandRight
//        AddEquipmentToList(6); //ChestArmor
//        AddEquipmentToList(7); //Feet
    }

    public void AddClothingToList(int id)
    {
        for (int i = 0; i < wornItems.Count; i++)
        {
            if (wornItems[id].ItemID == -1)
            {
                wornItems[id] = ItemDatabase.instance.FetchItemByID(id);
                break;
            }
        }
    }

	public void AddClothes(ClothingItem clothesToAdd)
    {
        if (clothesToAdd.ItemType == "Dress")
            wornDress = AddClothesHelper(wornDress, clothesToAdd);
		//else if
    }

	public GameObject AddClothesHelper(GameObject wornItem, ClothingItem itemToAddToWornItem)
    {
        wornItem = Wear(itemToAddToWornItem.ItemPrefab, wornItem);
		wornItem.name = itemToAddToWornItem.FileName;
        return wornItem;
    }

	public void RemoveClothes(ClothingItem clothesToRemove)
    {
        if (clothesToRemove.ItemType == "Dress")
            wornDress = RemoveClothesHelper(wornDress, 0);
		//else if
    
    }

    public GameObject RemoveClothesHelper(GameObject wornItem, int nakedItemIndex)
    {
        wornItem = RemoveWorn(wornItem);
        wornItems[nakedItemIndex] = ItemDatabase.instance.FetchItemByID(nakedItemIndex);
        return wornItem;
    }

    #endregion

    private GameObject RemoveWorn(GameObject wornClothing)
    {
        if (wornClothing == null)
            return null;
        GameObject.Destroy(wornClothing);
        return null;
    }

    private GameObject Wear(GameObject clothing, GameObject wornClothing)
    {
        if (clothing == null) return null;
        clothing = (GameObject)GameObject.Instantiate(clothing);

		//clothScript.GenerateCloth (clothing);

        wornClothing = AttachModels(clothing, avatar);
        return wornClothing;
    }

    public GameObject AttachModels(GameObject ClothingModel, GameObject Character)
    {
		
		SkinnedMeshRenderer skinnedCharMeshRenderer = Character.GetComponentInChildren<SkinnedMeshRenderer>(),
				skinnedMeshRenderers = ClothingModel.GetComponentInChildren<SkinnedMeshRenderer>();
		ClothingModel.transform.parent = Character.transform;
		//    skinnedMeshRenderers.bones = skinnedCharMeshRenderer.bones;
		return ClothingModel;
    }
}