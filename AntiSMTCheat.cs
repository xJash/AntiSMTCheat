using BepInEx;
using AntiSMTCheat;
using HarmonyLib;
using Mirror;
using System;
using System.Linq;
using System.Reflection;

[BepInPlugin("com.yourname.AntiSMTCheat", "AntiSMTCheat", "1.0.0")]
public class AntiSMTCheatPlugin : BaseUnityPlugin {

    private void Awake() {
        Harmony harmony = new("com.yourname.AntiSMTCheat");
        PermissionAssignment.LoadPermissions();
        PatchAllCommandsWithPermissionCheck(harmony);
        Logger.LogInfo("AntiSMTCheat initialized.");
    }

    private void PatchAllCommandsWithPermissionCheck(Harmony harmony) {

        MethodInfo prefix = typeof(PermissionEnforcer).GetMethod(nameof(PermissionEnforcer.CommandPermissionPrefix), BindingFlags.Static | BindingFlags.Public);

        System.Collections.Generic.IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a.GetName().Name == "Assembly-CSharp")
            .SelectMany(a => a.GetTypes());

        foreach (Type type in types) {

            foreach (MethodInfo cmdMethod in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(m => m.GetCustomAttribute<CommandAttribute>() != null)) {

                try {
                    MethodInfo serverMethod = cmdMethod.DeclaringType
                        .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                        .FirstOrDefault(m => m.Name.StartsWith($"InvokeUserCode_{cmdMethod.Name}", StringComparison.Ordinal));

                    _ = harmony.Patch(serverMethod, prefix: new HarmonyMethod(prefix));


                    Logger.LogInfo($"Patched command: {type.FullName}.{serverMethod.Name}");
                } catch (Exception ex) {
                    Logger.LogWarning($"Failed to patch {type.FullName}.InvokeUserCode_{cmdMethod.Name}: {ex.Message}");
                }
            }
        }
    }
}
