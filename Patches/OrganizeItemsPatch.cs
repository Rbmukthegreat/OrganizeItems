using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static OrganizeItems.Networking.NetworkFunctions;
using Random = UnityEngine.Random;

namespace OrganizeItems.Patches
{
    [HarmonyPatch]
    internal class OrganizeItemsPatch
    {
        private static readonly ManualLogSource Log = Plugin.Log;
        private static GameObject Ship;

        [HarmonyPatch(typeof(PlayerControllerB), "ConnectClientToPlayerObject")]
        [HarmonyPostfix]
        public static void OnLocalPlayerConnect()
        {
            NetworkingObjectManager.NetworkManagerInit();
        }

        [HarmonyPatch(typeof(HUDManager), "AddTextToChatOnServer")]
        [HarmonyPostfix]
        private static void GetText(string chatMessage)
        {
            if (chatMessage == ".organize")
            {
                Log.LogMessage("Organizing items!");
                OrganizeItems();
            }
        }

        private static void OrganizeItems()
        {
            var itemList = GetItemList();
            foreach (var type in ItemList.GoodItems.Keys)
            {
                Log.LogMessage($"Teleporting {type}!");
                TeleportItemsToPosition(Filter(type, itemList), ItemList.GoodItems[type]);
            }

            foreach (var type in ItemList.BadItems.Keys)
            {
                Log.LogMessage($"Teleporting {type}!");
                TeleportItemsToPosition(Filter(type, itemList), ItemList.BadItems[type]);
            }
        }

        private static List<GrabbableObject> Filter(string itemType, List<GrabbableObject> itemList)
        {
            string Clone = "(Clone)";
            return itemList.Where(item => item.name[..(item.name.Count() - Clone.Count())] == itemType).ToList();
        }

        private static List<GrabbableObject> GetItemList()
        {
            if (!Ship) Ship = GameObject.Find("/Environment/HangarShip");
            var items = Ship.GetComponentsInChildren<GrabbableObject>()
                .Where(obj => obj.itemProperties.isScrap && obj is not RagdollGrabbableObject)
                .ToList();
            return items;
        }

        private static void TeleportItemsToPosition(List<GrabbableObject> itemList, Vector3 position)
        {
            if (itemList.Count == 0) return;
            foreach (var item in itemList)
            {
                DropItem(item, position);
            }
        }

        private static void DropItem(GrabbableObject item, Vector3 position)
        {
            NetworkingObjectManager.RunClientRpc(item.NetworkObject, position + 
                new Vector3(Random.Range(-0.1f, 0.1f), 0, Random.Range(-0.1f, 0.1f)), item.transform.rotation);
        }
    }
}
