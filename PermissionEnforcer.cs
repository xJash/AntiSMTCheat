using HarmonyLib;
using Mirror;
using System.IO;
using System.Reflection;
using UnityEngine;

public static class PermissionEnforcer {
    private static bool IsHostConnection(NetworkConnectionToClient conn) {
        return conn.connectionId == 0;
    }

    public static bool CommandPermissionPrefix(NetworkBehaviour obj, NetworkConnectionToClient senderConnection, MethodBase __originalMethod) {
        string methodKey = $"{obj.GetType().Name}.{__originalMethod.Name}";

        Debug.Log($"[AntiSMTCheat] CommandPermissionPrefix called for: {obj.GetType().Name}");

        if (!PermissionDatabase.CommandPermissions.TryGetValue(methodKey, out PermissionDatabase.CommandPermission requiredPermission)) {
            Debug.LogWarning($"[AntiSMTCheat] No permission registered for {methodKey}. Denying.");
            //senderConnection?.Disconnect();
            return false;
        }

        if (!IsHostConnection(senderConnection)) {
            string offender = senderConnection.address ?? "unknown";
            Debug.LogWarning($"[AntiSMTCheat] Blocked non-host command from {offender} ({obj.GetType().Name})");
            LogInfraction(offender, obj.GetType().Name, __originalMethod.Name, "Non-host attempted restricted command");
            //senderConnection.Disconnect();
            return false;
        }

        Debug.Log($"[AntiSMTCheat] Host command allowed: {obj.GetType().Name} requiring {requiredPermission} permission.");
        return true;
    }

    private static readonly string InfractionLogPath = Path.Combine(Application.persistentDataPath, "PermissionInfractions.log");

    private static void LogInfraction(string offender, string objType, string method, string reason) {
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string logEntry = $"[{timestamp}] Offender: {offender}, Command: {objType}.{method}, Reason: {reason}";
        File.AppendAllText(InfractionLogPath, logEntry + "\n");
        Debug.LogWarning($"[AntiSMTCheat] {logEntry}");
    }


    [HarmonyPatch(typeof(NetworkBehaviour))]
    [HarmonyPatch("SendCommandInternal")]
    public static class Patch_NetworkBehavior_SendCommandInternal {
        private static bool Prefix(NetworkBehaviour __instance, string functionName, int cmdHash, NetworkWriter writer, int channelId, bool requiresAuthority) {
            NetworkConnectionToClient conn = __instance.connectionToClient;
            if (conn == null || !__instance.isServer || !__instance.isClient) {
                Debug.Log($"[AntiSMTCheat] Blocking command {functionName} from non-host.");
                //conn?.Disconnect();
                return false;
            }

            return true;
        }
    }
}
