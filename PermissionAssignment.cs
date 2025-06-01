using System;
using UnityEngine;

namespace AntiSMTCheat {
    public static class PermissionAssignment {
        public static bool HasRequiredPermission(PlayerPermissions perms, PermissionDatabase.CommandPermission requiredPermission) {
            if (perms == null) {
                Debug.LogWarning("PermissionAssignment: PlayerPermissions is null!");
                return false;
            }

            return requiredPermission switch {
                PermissionDatabase.CommandPermission.General => perms.hasGP,
                PermissionDatabase.CommandPermission.Manager => perms.hasMP,
                PermissionDatabase.CommandPermission.Cashier => perms.hasCP,
                PermissionDatabase.CommandPermission.Restocker => perms.hasRP,
                PermissionDatabase.CommandPermission.Security => perms.hasSP,
                PermissionDatabase.CommandPermission.Text => perms.hasTP,
                _ => false
            };
        }
    }
}
