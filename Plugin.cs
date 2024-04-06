using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;

namespace OrganizeItems;

[BepInPlugin(modGUID, modName, modVersion)]
public class Plugin : BaseUnityPlugin
{
    private const string modGUID = "Rbmukthegreat.OrganizeItems";
    private const string modName = "Organize Items";
    private const string modVersion = "1.0.1";

    private readonly Harmony harmony = new(modGUID);

    internal static ManualLogSource Log;

    private void Awake()
    {
        Log = Logger;

        // Plugin startup logic
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

        // Apply all patches
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }
}
