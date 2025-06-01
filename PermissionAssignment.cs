using HarmonyLib;
using Mirror;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

namespace AntiSMTCheat {
    internal class PermissionAssignment {
        public enum Role {
            Cashier,
            General,
            Manager,
            Restocker,
            Security
        }

        private static readonly string PermissionSavePath = Path.Combine(Application.persistentDataPath, "UserPermissions.json");
        private static Dictionary<string, HashSet<Role>> UserRoles = new Dictionary<string, HashSet<Role>>();

        public static void OnChatReceived(string playerName, string message) {
            Debug.Log($"[AntiSMTCheat] OnChatReceived called by '{playerName}' with message: {message}");

            if (!message.StartsWith("!")) {
                return;
            }

            // Parse command pattern: !role+[or-] targetUser
            Match match = Regex.Match(message, @"^!(\w+)([+-])\s+(.+)$");
            if (!match.Success) {
                Debug.LogWarning("[AntiSMTCheat] Chat command regex did not match.");
                return;
            }

            string roleName = match.Groups[1].Value;
            string op = match.Groups[2].Value;
            string targetUser = match.Groups[3].Value;

            if (!Enum.TryParse<Role>(roleName, true, out Role role)) {
                Debug.LogWarning($"[AntiSMTCheat] Invalid role name: {roleName}");
                return;
            }

            if (!UserRoles.ContainsKey(targetUser)) {
                UserRoles[targetUser] = new HashSet<Role>();
            }

            if (op == "+") {
                bool added = UserRoles[targetUser].Add(role);
                if (added)
                    Debug.Log($"[AntiSMTCheat] {playerName} granted role {role} to {targetUser}");
                else
                    Debug.Log($"[AntiSMTCheat] Role {role} was already assigned to {targetUser}");
            } else {
                bool removed = UserRoles[targetUser].Remove(role);
                if (removed)
                    Debug.Log($"[AntiSMTCheat] {playerName} revoked role {role} from {targetUser}");
                else
                    Debug.Log($"[AntiSMTCheat] Role {role} was not assigned to {targetUser}");
            }

            SavePermissions();
        }

        private static void SavePermissions() {
            try {
                string json = JsonUtility.ToJson(new SerializableUserRoles(UserRoles));
                File.WriteAllText(PermissionSavePath, json);
                Debug.Log("[AntiSMTCheat] Permissions saved.");
            } catch (Exception ex) {
                Debug.LogError($"[AntiSMTCheat] Failed to save permissions: {ex}");
            }
        }

        public static void LoadPermissions() {
            if (!File.Exists(PermissionSavePath)) {
                Debug.Log("[AntiSMTCheat] Permissions file not found, starting fresh.");
                return;
            }

            try {
                string json = File.ReadAllText(PermissionSavePath);
                SerializableUserRoles data = JsonUtility.FromJson<SerializableUserRoles>(json);
                UserRoles = data?.ToDictionary() ?? new Dictionary<string, HashSet<Role>>();
                Debug.Log("[AntiSMTCheat] Permissions loaded.");
            } catch (Exception ex) {
                Debug.LogError($"[AntiSMTCheat] Failed to load permissions: {ex}");
                UserRoles = new Dictionary<string, HashSet<Role>>();
            }
        }

        [Serializable]
        public class SerializableUserRoles {
            public List<UserRoleEntry> entries = new List<UserRoleEntry>();

            public SerializableUserRoles(Dictionary<string, HashSet<Role>> source) {
                foreach (var kvp in source) {
                    entries.Add(new UserRoleEntry {
                        username = kvp.Key,
                        roles = kvp.Value.Select(r => r.ToString()).ToList()
                    });
                }
            }

            public Dictionary<string, HashSet<Role>> ToDictionary() {
                return entries.ToDictionary(
                    e => e.username,
                    e => new HashSet<Role>(e.roles.Select(r => Enum.Parse<Role>(r, true)))
                );
            }

            [Serializable]
            public class UserRoleEntry {
                public string username;
                public List<string> roles = new List<string>();
            }
        }
    }

    [HarmonyPatch]
    public static class Patch_ChatCommand {
        static MethodBase TargetMethod() {
            var type = typeof(PlayerObjectController);
            return type.GetMethod("UserCode_CmdSendMessage__String__NetworkConnectionToClient", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        static void Prefix(string message, NetworkConnectionToClient sender) {
            string playerName = "Unknown";

            if (sender != null && sender.identity != null) {
                playerName = sender.identity.gameObject.name;
            }

            Debug.Log($"[AntiSMTCheat] Chat message from {playerName}: {message}");

            AntiSMTCheat.PermissionAssignment.OnChatReceived(playerName, message);
        }
    }


}
