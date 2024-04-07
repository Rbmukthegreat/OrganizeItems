using BepInEx.Logging;
using GameNetcodeStuff;
using Unity.Netcode;
using UnityEngine;
using LogLevel = Unity.Netcode.LogLevel;

// This class has been ripped from https://github.com/bozzobrain/LethalCompanyShipMaid/blob/main/Networking/NetworkFunctions.cs
// It has been modified to work with the OrganizeItems plugin

namespace OrganizeItems.Networking
{
    internal class NetworkFunctions
    {
        private static readonly ManualLogSource Log = Plugin.Log;
        private static PlayerControllerB Player { get { return StartOfRound.Instance?.localPlayerController; } }

        public class NetworkingObjectManager : NetworkBehaviour
        {
            public static void MakeObjectFall(GrabbableObject obj, Vector3 placementPosition, Quaternion placementRotation)
            {
                obj.gameObject.transform.SetPositionAndRotation(placementPosition, placementRotation);
                obj.hasHitGround = false;
                obj.startFallingPosition = placementPosition;
                obj.floorYRot = -1;

                if (obj.transform.parent != null)
                {
                    obj.startFallingPosition = obj.transform.parent.InverseTransformPoint(obj.startFallingPosition);
                }
                obj.FallToGround(false);
                obj.floorYRot = -1;
            }

            [ClientRpc]
            public static void MakeObjectFallClientRpc(NetworkObjectReference obj, Vector3 placementPosition, Quaternion placementRotation)
            {
                NetworkManager networkManager = NetworkManager.Singleton;
                if (networkManager is null || !networkManager.IsListening)
                {
                    return;
                }

                FastBufferWriter bufferWriter = new(256, Unity.Collections.Allocator.Temp);
                bufferWriter.WriteValueSafe(in obj, default);
                bufferWriter.WriteValueSafe(in placementPosition);
                bufferWriter.WriteValueSafe(in placementRotation);
                NetworkManager.Singleton.CustomMessagingManager.SendNamedMessageToAll("MakeObjectFall", bufferWriter, NetworkDelivery.Reliable);

                if (obj.TryGet(out var networkObject))
                {
                    if (networkObject.TryGetComponent(out GrabbableObject component))
                    {
                        MakeObjectFall(component, placementPosition, placementRotation);
                    }
                    else
                    {
                        Log.LogError("Failed to get grabbable object ref from network object - ClientRpc");
                    }
                }
            }

            [ServerRpc]
            public static void MakeObjectFallServerRpc(NetworkObjectReference obj, Vector3 placementPosition, Quaternion placementRotation)
            {
                NetworkManager networkManager = NetworkManager.Singleton;
                if (networkManager == null)
                {
                    Log.LogError("Network Manager == null");
                    return;
                }
                if (!networkManager.IsListening)
                {
                    Log.LogError("Network Manager not listening");
                    return;
                }

                if (Player.OwnerClientId != networkManager.LocalClientId)
                {
                    if (networkManager.LogLevel <= LogLevel.Normal)
                    {
                        Log.LogError("Only the owner can invoke a ServerRpc that requires ownership!");
                    }

                    return;
                }

                FastBufferWriter bufferWriter = new(256, Unity.Collections.Allocator.Temp);
                bufferWriter.WriteValueSafe(in obj, default);
                bufferWriter.WriteValueSafe(placementPosition);
                bufferWriter.WriteValueSafe(placementRotation);
                NetworkManager.Singleton.CustomMessagingManager.SendNamedMessageToAll("MakeObjectFall", bufferWriter, NetworkDelivery.Reliable);

                if (obj.TryGet(out var networkObject))
                {
                    if (networkObject.TryGetComponent(out GrabbableObject component))
                    {
                        MakeObjectFall(component, placementPosition, placementRotation);
                    }
                    else
                    {
                        Log.LogError("Failed to get grabbable object ref from network object - ServerRpc");
                    }
                }
            }

            public static void NetworkManagerInit()
            {
                Log.LogMessage("Registering named message");
                NetworkManager.Singleton.CustomMessagingManager.RegisterNamedMessageHandler("MakeObjectFall", (senderClientId, reader) =>
                {
                    if (senderClientId != Player.playerClientId)
                    {
                        reader.ReadValueSafe(out NetworkObjectReference GrabbableObjectRef, default);
                        reader.ReadValueSafe(out Vector3 position);
                        reader.ReadValueSafe(out Quaternion rotation);
                        if (GrabbableObjectRef.TryGet(out var GrabbableObjectNetworkObj))
                        {
                            GrabbableObject component = GrabbableObjectNetworkObj.GetComponent<GrabbableObject>();
                            MakeObjectFall(component, position, rotation);
                        }
                    }
                });
            }

            [ClientRpc]
            public static void RunClientRpc(NetworkObjectReference obj, Vector3 placementPosition, Quaternion placementRotation)
            {
                MakeObjectFallServerRpc(obj, placementPosition, placementRotation);
            }
        }
    }
}
