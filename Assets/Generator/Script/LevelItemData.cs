using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 51, fileName = "PaleteGeneration", menuName = "Generator/Palete")]
public class LevelItemData : ScriptableObject
{

    [Serializable]
    public struct ItemGeneration
    {
        public string Name;
        public GameObject prefabGeneration;
        public int numberOnPalete;
    }

    [SerializeField] private List<ItemGeneration> palete;

    public List<ItemGeneration> GetPalete => palete;

    public ItemGeneration GetPaleteOnName(string name)
    {
        foreach (var itemPalete in palete)
        {
            if (itemPalete.Name.Equals(name))
            {
                return itemPalete;
            }
        }

        return new ItemGeneration();
    }
}
