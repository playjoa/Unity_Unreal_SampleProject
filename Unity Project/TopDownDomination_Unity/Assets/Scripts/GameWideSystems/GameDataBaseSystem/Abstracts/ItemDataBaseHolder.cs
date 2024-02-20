using System.Collections.Generic;
using GameWideSystems.GameDataBaseSystem.Interfaces;
using UnityEngine;
using Utils.Extensions;
using Utils.UniqueId.Components;

namespace GameWideSystems.GameDataBaseSystem.Abstracts
{
    public class ItemDataBaseHolder<TDataItem> : IDataBase where TDataItem : ScriptableObjectWithId
    {
        public List<TDataItem> DataItems { get; }
        
        protected readonly Dictionary<string, TDataItem> ItemDataBase = new();

        private bool _initiated = false;
        
        public ItemDataBaseHolder(List<TDataItem> dataItemsList)
        {
            DataItems = dataItemsList;
        }

        public void InitiateDataBase()
        {
            if (_initiated) return;
            
            BuildItemsDataBase(DataItems);
            _initiated = true;
        }
        
        protected virtual void BuildItemsDataBase(List<TDataItem> dataItemList)
        {
            foreach (var dataItem in dataItemList)
            {
                if (ItemDataBase.ContainsKey(dataItem.Id))
                {
                    Debug.LogWarning($"Duplicated DataItem Id, for {dataItem.name} Id: {dataItem.Id}");
                    continue;
                }
                
                ItemDataBase.Add(dataItem.Id, dataItem);
            }
        }
        
        public bool TryGetDataItem(string dataItemId, out TDataItem dataItem)
        {
            dataItem = null;
            return ItemDataBase.TryGetValue(dataItemId, out dataItem);
        }

        public TDataItem GetRandomDataItem()
        {
            return ItemDataBase.RandomValue();
        }
    }
}