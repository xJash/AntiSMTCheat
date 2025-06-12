using AntiSMTCheat;
using HarmonyLib;
using Mirror;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
public static class PermissionEnforcer {

    //private static async void SendLogToServer(string logEntry) {
    //    try {
    //        var json = JsonUtility.ToJson(new { log = logEntry });
    //        using var request = new UnityWebRequest("http://127.0.0.1:5000/api/logs", "POST");
    //        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
    //        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
    //        request.downloadHandler = new DownloadHandlerBuffer();
    //        request.SetRequestHeader("Content-Type", "application/json");
    //        request.SetRequestHeader("X-API-KEY", "#h}=))r7az6P1Q5:^LK6]b5.*.c~jY.3"); // <-- match your server key here
    //
    //        var operation = request.SendWebRequest();
    //        while (!operation.isDone)
    //            await System.Threading.Tasks.Task.Yield();
    //
    //        if (request.result != UnityWebRequest.Result.Success)
    //            Debug.LogWarning($"[AntiSMTCheat] Failed to send log: {request.error}");
    //        else
    //            Debug.Log("[AntiSMTCheat] Log sent successfully.");
    //    } catch (System.Exception e) {
    //        Debug.LogWarning($"[AntiSMTCheat] Exception sending log: {e}");
    //    }
    //}


    private static readonly string InfractionLogPath = Path.Combine(Application.persistentDataPath, "PermissionInfractions.log");
    private static readonly Dictionary<string, int> InfractionCounts = [];
    private static readonly Dictionary<string, int> InfractionLimits = [];
    private static readonly System.Random InfractionRng = new();

    private static bool IsHostConnection(NetworkConnectionToClient conn) {
        return conn.connectionId == 0;
    }

    public static bool CommandPermissionPrefix(NetworkBehaviour obj, NetworkConnectionToClient senderConnection, MethodBase __originalMethod) {
        string methodKey = $"{obj.GetType().Name}.{__originalMethod.Name}";
        string offender = senderConnection.address ?? "unknown";

        Debug.Log($"[AntiSMTCheat] CommandPermissionPrefix called for: {methodKey}");

        if (IsHostConnection(senderConnection)) {
            Debug.Log($"[AntiSMTCheat] Host connection detected. Allowing command {methodKey}.");
            return true;
        }

        if (!PermissionDatabase.CommandPermissionMap.TryGetValue(methodKey, out var requiredPermission)) {
            Debug.LogWarning($"[AntiSMTCheat] No permission registered for {methodKey}. Defaulting to General.");
            requiredPermission = new HashSet<PermissionDatabase.CommandPermission> { PermissionDatabase.CommandPermission.General };
        }


        var identity = senderConnection.identity;
        if (identity == null) {
            Debug.LogWarning($"[AntiSMTCheat] Sender has no NetworkIdentity.");
            return false;
        }

        var permissions = identity.GetComponent<PlayerPermissions>();
        if (permissions == null) {
            Debug.LogWarning($"[AntiSMTCheat] No PlayerPermissions found on identity.");
            LogInfraction(offender, obj.GetType().Name, __originalMethod.Name, "Missing PlayerPermissions component", senderConnection);
            return false;
        }

        bool hasPermission = requiredPermission.Any(p => PermissionAssignment.HasRequiredPermission(permissions, p));


        if (!hasPermission) {
            Debug.LogWarning($"[AntiSMTCheat] Player {identity.gameObject.name} lacks permission {requiredPermission} for {methodKey}.");
            LogInfraction(offender, obj.GetType().Name, __originalMethod.Name, $"Lacked required permission: {requiredPermission}", senderConnection);
            return false;
        }

        Debug.Log($"[AntiSMTCheat] Permission granted: {identity.gameObject.name} -> {methodKey}");
        return true;
    }

    private static void LogInfraction(string offender, string objType, string method, string reason, NetworkConnectionToClient senderConnection) {
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string logEntry = $"[{timestamp}] Offender: {offender}, Command: {objType}.{method}, Reason: {reason}";

        File.AppendAllText(InfractionLogPath, logEntry + "\n");
        Debug.LogWarning($"[AntiSMTCheat] {logEntry}");

        //SendLogToServer(logEntry);

        if (!InfractionCounts.TryGetValue(offender, out int currentCount)) {
            currentCount = 0;
        }
        InfractionCounts[offender] = ++currentCount;

        if (!InfractionLimits.TryGetValue(offender, out int limit)) {
            limit = InfractionRng.Next(80, 121);
            InfractionLimits[offender] = limit;
            Debug.Log($"[AntiSMTCheat] Assigned new infraction limit to {offender}: {limit}");
        }

        if (currentCount >= limit) {
            Debug.LogWarning($"[AntiSMTCheat] {offender} exceeded infraction limit ({currentCount}/{limit}). Disconnecting.");
            senderConnection?.Disconnect();
        }
    }
}
