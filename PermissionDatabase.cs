using System.Collections.Generic;

public static class PermissionDatabase {
    public enum CommandPermission {
        Cashier,
        General,
        Manager,
        Restocker,
        Text,
        Security
    }

    public static readonly IReadOnlyDictionary<string, HashSet<CommandPermission>> CommandPermissionMap = new Dictionary<string, HashSet<CommandPermission>>
        {
            // AchievementsManager
            { "AchievementsManager.InvokeUserCode_CmdAddAchievementPoint__Int32__Int32", new HashSet<CommandPermission> { CommandPermission.General, CommandPermission.Restocker, CommandPermission.Manager, CommandPermission.Cashier } },
            { "AchievementsManager.InvokeUserCode_CmdMaxFundsCheckouted__Single", new HashSet<CommandPermission> { CommandPermission.Cashier } },
            { "AchievementsManager.InvokeUserCode_CmdRequestGachaponSphere__Int32", new HashSet<CommandPermission> { CommandPermission.General } },
            { "AchievementsManager.InvokeUserCode_CmdRequestGachaponDeletion__GameObject", new HashSet<CommandPermission> { CommandPermission.General } },

            // CardboardBaler
            { "CardboardBaler.InvokeUserCode_CmdAddBoxToBaler", new HashSet<CommandPermission> { CommandPermission.Restocker } },

            // Data_Container
            { "Data_Container.InvokeUserCode_CmdUpdateArrayValues__Int32__Int32__Int32", new HashSet<CommandPermission> { CommandPermission.Restocker } },
            { "Data_Container.InvokeUserCode_CmdContainerClear__Int32", new HashSet<CommandPermission> { CommandPermission.General } },
            { "Data_Container.InvokeUserCode_CmdUpdateArrayValuesStorage__Int32__Int32__Int32", new HashSet<CommandPermission> { CommandPermission.General, CommandPermission.Restocker} },
            { "Data_Container.InvokeUserCode_CmdActivateCashMethod__Int32", new HashSet<CommandPermission> { CommandPermission.Cashier } },
            { "Data_Container.InvokeUserCode_CmdActivateCreditCardMethod", new HashSet<CommandPermission> { CommandPermission.Cashier } },
            { "Data_Container.InvokeUserCode_CmdReceivePayment__Single", new HashSet<CommandPermission> { CommandPermission.Cashier } },
            { "Data_Container.InvokeUserCode_CmdResetFirstCustomer", new HashSet<CommandPermission> { CommandPermission.General } },
            { "Data_Container.InvokeUserCode_CmdCloseCheckout", new HashSet<CommandPermission> { CommandPermission.Cashier, CommandPermission.Manager } },
            { "Data_Container.InvokeUserCode_CmdRequestCloseState", new HashSet<CommandPermission> { CommandPermission.General } },

            // DebtManager
            { "DebtManager.InvokeUserCode_CmdRequestInvoiceGeneration", new HashSet<CommandPermission> { CommandPermission.General } },
            { "DebtManager.InvokeUserCode_CmdRequestLoan__Int32__Int32__Int32", new HashSet<CommandPermission> { CommandPermission.Manager } },
            { "DebtManager.InvokeUserCode_CmdPayInvoice__Int32", new HashSet<CommandPermission> { CommandPermission.General } },

            // DecorationExtraData
            { "DecorationExtraData.InvokeUserCode_CmdChangeColor__Int32", new HashSet<CommandPermission> { CommandPermission.General } },
            { "DecorationExtraData.InvokeUserCode_CmdChangeText__String", new HashSet<CommandPermission> { CommandPermission.General } },

            // DemolishableManager
            { "DemolishableManager.InvokeUserCode_CmdDemolishItem__Int32__Int32", new HashSet<CommandPermission> { CommandPermission.Manager } },

            // DemolishDebrisControl
            { "DemolishDebrisControl.InvokeUserCode_CmdRemoveDebris__Int32", new HashSet<CommandPermission> { CommandPermission.General } },

            // DrawTheWordManager
            { "DrawTheWordManager.InvokeUserCode_CmdEnablePaintingMinigame", new HashSet<CommandPermission> { CommandPermission.General } },
            { "DrawTheWordManager.InvokeUserCode_CmdRequestGameStart__String__GameObject", new HashSet<CommandPermission> { CommandPermission.General } },
            { "DrawTheWordManager.InvokeUserCode_CmdSwapBrushColor__Int32", new HashSet<CommandPermission> { CommandPermission.General } },
            { "DrawTheWordManager.InvokeUserCode_CmdSwapBrushSize__Single", new HashSet<CommandPermission> { CommandPermission.General } },
            { "DrawTheWordManager.InvokeUserCode_CmdSendBrushInformation__Boolean__Vector3", new HashSet<CommandPermission> { CommandPermission.General } },

            // EasterBehaviour
            { "EasterBehaviour.InvokeUserCode_CmdFredString", new HashSet<CommandPermission> { CommandPermission.General } },
            { "EasterBehaviour.InvokeUserCode_CmdFredWalkState__Boolean", new HashSet<CommandPermission> { CommandPermission.General } },

            // EasterChecker
            { "EasterChecker.InvokeUserCode_CmdSpawnEaster__Int32", new HashSet<CommandPermission> { CommandPermission.General } },

            // EventCageSpawnNets
            { "EventCageSpawnNets.InvokeUserCode_CmdDepositAnimalInside__Int32", new HashSet<CommandPermission> { CommandPermission.General } },

            // GameData
            { "GameData.InvokeUserCode_CmdAlterFunds__Single", new HashSet<CommandPermission> { CommandPermission.Manager, CommandPermission.Restocker, CommandPermission.Security, CommandPermission.Cashier } },
            { "GameData.InvokeUserCode_CmdAlterFundsWithoutExperience__Single", new HashSet<CommandPermission> { CommandPermission.General, CommandPermission.Manager } },
            { "GameData.InvokeUserCode_CmdAcquireFranchise__Int32", new HashSet<CommandPermission> { CommandPermission.Manager } },
            { "GameData.InvokeUserCode_CmdOpenSupermarket", new HashSet<CommandPermission> { CommandPermission.Manager } },
            { "GameData.InvokeUserCode_CmdEndDayFromButton", new HashSet<CommandPermission> { CommandPermission.Manager } },
            { "GameData.InvokeUserCode_CmdmoneySpentOnProducts__Single", new HashSet<CommandPermission> { CommandPermission.Manager } },
            { "GameData.InvokeUserCode_CmdHostDisconnect", new HashSet<CommandPermission> { CommandPermission.General } },

            // HalloweenGhost
            { "HalloweenGhost.InvokeUserCode_CmdHitFromPlayer__Vector3", new HashSet<CommandPermission> { CommandPermission.General } },

            // ManagerBlackboard
            { "ManagerBlackboard.InvokeUserCode_CmdAddProductToSpawnList__Int32", new HashSet<CommandPermission> { CommandPermission.Manager } },
            { "ManagerBlackboard.InvokeUserCode_CmdSpawnBoxEmpty", new HashSet<CommandPermission> { CommandPermission.Manager } },
            { "ManagerBlackboard.InvokeUserCode_CmdSpawnBoxFromPlayer__Vector3__Int32__Int32__Single", new HashSet<CommandPermission> { CommandPermission.Restocker } },

            // MiniTransportBehaviour
            { "MiniTransportBehaviour.InvokeUserCode_CmdRequestOwnership__GameObject", new HashSet<CommandPermission> { CommandPermission.Restocker } },
            { "MiniTransportBehaviour.InvokeUserCode_CmdRemoveOwnership", new HashSet<CommandPermission> { CommandPermission.General } },
            { "MiniTransportBehaviour.InvokeUserCode_CmdOnPeopleHit__Vector3", new HashSet<CommandPermission> { CommandPermission.General } },
            { "MiniTransportBehaviour.InvokeUserCode_CmdCollisionHit__Vector3__Single", new HashSet<CommandPermission> { CommandPermission.Restocker } },
            { "MiniTransportBehaviour.InvokeUserCode_CmdHorn", new HashSet<CommandPermission> { CommandPermission.Restocker } },
            { "MiniTransportBehaviour.InvokeUserCode_CmdUpdateArrayValuesStorage__Int32__Int32__Int32", new HashSet<CommandPermission> { CommandPermission.General } },

            // MisterGift
            { "MisterGift.InvokeUserCode_CmdPlayerTookIt", new HashSet<CommandPermission> { CommandPermission.General } },

            // MisterGrusch
            { "MisterGrusch.InvokeUserCode_CmdHitFromPlayer", new HashSet<CommandPermission> { CommandPermission.General } },

            // NetworkSpawner
            { "NetworkSpawner.InvokeUserCode_CmdSpawn__Int32__Vector3__Vector3", new HashSet<CommandPermission> { CommandPermission.Manager } },
            { "NetworkSpawner.InvokeUserCode_CmdSpawnProp__Int32__Vector3__Vector3", new HashSet<CommandPermission> { CommandPermission.General } },
            { "NetworkSpawner.InvokeUserCode_CmdSpawnDecoration__Int32__Vector3__Vector3", new HashSet<CommandPermission> { CommandPermission.General } },
            { "NetworkSpawner.InvokeUserCode_CmdObjectMove__GameObject__Vector3__Quaternion", new HashSet<CommandPermission> { CommandPermission.Manager } },
            { "NetworkSpawner.InvokeUserCode_CmdSpawnBox__Int32__Vector3__Int32__Int32", new HashSet<CommandPermission> { CommandPermission.General } },
            { "NetworkSpawner.InvokeUserCode_CmdSpawnTrayFromPlayer__Vector3__String__Single", new HashSet<CommandPermission> { CommandPermission.General } },
            { "NetworkSpawner.InvokeUserCode_CmdSpawnOrderBoxFromPlayer__Vector3__Single__String__String__String", new HashSet<CommandPermission> { CommandPermission.General } },
            { "NetworkSpawner.InvokeUserCode_CmdDestroyBox__GameObject", new HashSet<CommandPermission> { CommandPermission.Restocker, CommandPermission.General, CommandPermission.Manager} },
            { "NetworkSpawner.InvokeUserCode_CmdSetSupermarketText__String", new HashSet<CommandPermission> { CommandPermission.Manager } },
            { "NetworkSpawner.InvokeUserCode_CmdSetSupermarketColor__Color", new HashSet<CommandPermission> { CommandPermission.General } },

            // PlayerNetwork
            { "PlayerNetwork.InvokeUserCode_CmdCrouch__Boolean", new HashSet<CommandPermission> { CommandPermission.General } },
            { "PlayerNetwork.InvokeUserCode_CmdSetBoxColorToEveryone__Int32", new HashSet<CommandPermission> { CommandPermission.Restocker } },
            { "PlayerNetwork.InvokeUserCode_CmdUpdateTrayToEveryone__String", new HashSet<CommandPermission> { CommandPermission.Restocker } },
            { "PlayerNetwork.InvokeUserCode_CmdChangeCharacter__Int32", new HashSet<CommandPermission> { CommandPermission.General } },
            { "PlayerNetwork.InvokeUserCode_CmdChangeHat__Int32", new HashSet<CommandPermission> { CommandPermission.General } },
            { "PlayerNetwork.InvokeUserCode_CmdChangeEquippedItem__Int32", new HashSet<CommandPermission> { CommandPermission.Restocker, CommandPermission.General } },
            { "PlayerNetwork.InvokeUserCode_CmdPlayAnimation__Int32", new HashSet<CommandPermission> { CommandPermission.Manager, CommandPermission.Security, CommandPermission.General } },
            { "PlayerNetwork.InvokeUserCode_CmdDebtCollectorAchievement", new HashSet<CommandPermission> { CommandPermission.General } },
            { "PlayerNetwork.InvokeUserCode_CmdPushPlayer__Vector3", new HashSet<CommandPermission> { CommandPermission.General } },
            { "PlayerNetwork.InvokeUserCode_CmdPlayPricingSound", new HashSet<CommandPermission> { CommandPermission.General } },
            { "PlayerNetwork.InvokeUserCode_CmdPlayPose__Int32", new HashSet<CommandPermission> { CommandPermission.General } },

            // PlayerObjectController
            { "PlayerObjectController.InvokeUserCode_CmdSendMessage__String__NetworkConnectionToClient", new HashSet<CommandPermission> { CommandPermission.General } },
            { "PlayerObjectController.InvokeUserCode_CmdSetPlayerName__String", new HashSet<CommandPermission> { CommandPermission.General } },

            // NetworkGameBehaviors
            { "NetworkGameBehaviors.InvokeUserCode_CmdServerEnableVoiceChat", new HashSet<CommandPermission> { CommandPermission.General } },
            { "NetworkGameBehaviors.InvokeUserCode_CmdRefreshStatus", new HashSet<CommandPermission> { CommandPermission.General } },

            // NPC_Info
            { "NPC_Info.InvokeUserCode_CmdAnimationPlay__Int32", new HashSet<CommandPermission> { CommandPermission.Security, CommandPermission.General } },
            { "NPC_Info.InvokeUserCode_CmdSurveillanceSet", new HashSet<CommandPermission> { CommandPermission.Security, CommandPermission.Manager } },

            // NPC_Manager
            { "NPC_Manager.InvokeUserCode_CmdUpdateRecycleStatus", new HashSet<CommandPermission> { CommandPermission.General } },
            { "NPC_Manager.InvokeUserCode_CmdRequestRecycleStatus", new HashSet<CommandPermission> { CommandPermission.General } },
            { "NPC_Manager.InvokeUserCode_CmdRerollCall", new HashSet<CommandPermission> { CommandPermission.General } },
            { "NPC_Manager.InvokeUserCode_CmdHireEmployeeData__Int32__String", new HashSet<CommandPermission> { CommandPermission.Manager } },
            { "NPC_Manager.InvokeUserCode_CmdDismissEmployeeData__Int32", new HashSet<CommandPermission> { CommandPermission.Manager } },
            { "NPC_Manager.InvokeUserCode_CmdRequestHappinessLevel", new HashSet<CommandPermission> { CommandPermission.General } },
            { "NPC_Manager.InvokeUserCode_CmdChangeEmployeeName__Int32__String", new HashSet<CommandPermission> { CommandPermission.Manager } },
            { "NPC_Manager.InvokeUserCode_CmdChangeEmployeeHat__Int32__Int32", new HashSet<CommandPermission> { CommandPermission.General } },
            { "NPC_Manager.InvokeUserCode_CmdChangeEmployeePriority__Int32__Int32", new HashSet<CommandPermission> { CommandPermission.Manager } },
            { "NPC_Manager.InvokeUserCode_CmdChangeAllEmployeePriorities__Int32", new HashSet<CommandPermission> { CommandPermission.General } },
            { "NPC_Manager.InvokeUserCode_CmdLoadPrioritiesLayout__Int32[]", new HashSet<CommandPermission> { CommandPermission.Manager } },

            // OrderPackaging
            { "OrderPackaging.InvokeUserCode_CmdActivateOrderDepartment", new HashSet<CommandPermission> { CommandPermission.General } },
            { "OrderPackaging.InvokeUserCode_CmdEnableOrderNotifications", new HashSet<CommandPermission> { CommandPermission.General } },
            { "OrderPackaging.InvokeUserCode_CmdAssignOrderToTable__Int32__Int32", new HashSet<CommandPermission> { CommandPermission.General } },
            { "OrderPackaging.InvokeUserCode_CmdCreateBoxOrder__Int32__String", new HashSet<CommandPermission> { CommandPermission.General } },
            { "OrderPackaging.InvokeUserCode_CmdAddItemToTable__Int32__GameObject__Int32", new HashSet<CommandPermission> { CommandPermission.General } },
            { "OrderPackaging.InvokeUserCode_CmdRemoveItemFromTable__Int32__GameObject__Int32", new HashSet<CommandPermission> { CommandPermission.General } },
            { "OrderPickupPoint.InvokeUserCode_CmdAddOrderBox__Int32__String", new HashSet<CommandPermission> { CommandPermission.General } },
            { "OtherPropsBehaviour.InvokeUserCode_CmdURLRequested__String", new HashSet<CommandPermission> { CommandPermission.General } },
            { "OtherPropsBehaviour.InvokeUserCode_CmdUpdateDoorSetter__Int32", new HashSet<CommandPermission> { CommandPermission.Manager } },
            { "PaintableDecoration.InvokeUserCode_CmdSetPaintable__Int32__Int32", new HashSet<CommandPermission> { CommandPermission.General } },

            // PaintablesManager
            { "PaintablesManager.InvokeUserCode_CmdUpdateSingleParentMaterial__Int32__Int32__Int32__Int32", new HashSet<CommandPermission> { CommandPermission.General } },

            // PictureFrameBehaviour
            { "PictureFrameBehaviour.InvokeUserCode_CmdChangeURL__String", new HashSet<CommandPermission> { CommandPermission.General } },
            { "PictureFrameBehaviour.InvokeUserCode_CmdSetPictureSize__Single__Single", new HashSet<CommandPermission> { CommandPermission.General } },
            { "PictureFrameBehaviour.InvokeUserCode_CmdChangeDefaultImage__Int32", new HashSet<CommandPermission> { CommandPermission.General } },

            // ProductCheckoutSpawn
            { "ProductCheckoutSpawn.InvokeUserCode_CmdAddProductValueToCheckout", new HashSet<CommandPermission> { CommandPermission.Cashier } },

            // ProductListing
            { "ProductListing.InvokeUserCode_CmdUpdateProductPrice__Int32__Single", new HashSet<CommandPermission> { CommandPermission.General } },
            { "ProductListing.InvokeUserCode_CmdUnlockProductTier__Int32", new HashSet<CommandPermission> { CommandPermission.General } },
            { "ProductListing.InvokeUserCode_CmdSetProductOnSale__Int32__Int32", new HashSet<CommandPermission> { CommandPermission.Manager } },

            // SeasonalCageEventAnimal
            { "SeasonalCageEventAnimal.InvokeUserCode_CmdHitFromPlayer__GameObject", new HashSet<CommandPermission> { CommandPermission.General } },

            // StolenProductSpawn
            { "StolenProductSpawn.InvokeUserCode_CmdRecoverStolenProduct", new HashSet<CommandPermission> { CommandPermission.Security } },

            // TrashSpawn
            { "TrashSpawn.InvokeUserCode_CmdClearTrash", new HashSet<CommandPermission> { CommandPermission.General } },

            // UpgradesManager
            { "UpgradesManager.InvokeUserCode_CmdAddSpace__Int32", new HashSet<CommandPermission> { CommandPermission.General } },
            { "UpgradesManager.InvokeUserCode_CmdAddStorage__Int32", new HashSet<CommandPermission> { CommandPermission.Manager } },
            { "UpgradesManager.InvokeUserCode_CmdAddAddon__Int32", new HashSet<CommandPermission> { CommandPermission.General } },
            { "UpgradesManager.InvokeUserCode_CmdAcquirePerk__Int32__Int32", new HashSet<CommandPermission> { CommandPermission.General } },
            { "UpgradesManager.InvokeUserCode_CmdTimeAcceleration", new HashSet<CommandPermission> { CommandPermission.General } },
            { "UpgradesManager.InvokeUserCode_CmdChangeTimeAcceleration__Boolean", new HashSet<CommandPermission> { CommandPermission.General } },

            // ServerAuthorityExamplePlayerController
            { "ServerAuthorityExamplePlayerController.InvokeUserCode_CmdTeleport", new HashSet<CommandPermission> { CommandPermission.General } },
            { "ServerAuthorityExamplePlayerController.InvokeUserCode_CmdMove__KeyCode", new HashSet<CommandPermission> { CommandPermission.General } },

            // SmoothSyncMirror
            { "Smooth.SmoothSyncMirror.InvokeUserCode_CmdTeleport__Vector3__Vector3__Vector3__Single", new HashSet<CommandPermission> { CommandPermission.General } },

            // Dissonance
            { "Dissonance.Integrations.MirrorIgnorance.MirrorIgnorancePlayer.InvokeUserCode_CmdSetPlayerName__String", new HashSet<CommandPermission> { CommandPermission.General } },
        };
}