using MSU;
using R2API;
using RoR2;
using RoR2.ContentManagement;
using RoR2.Orbs;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using RoR2.UI;
using EntityStates;
//using FortunesFromTheScrapyard.Survivors.Duke.EntityStates;
using RoR2.Projectile;
using RoR2.EntityLogic;
using System.Runtime.CompilerServices;
using ThreeEyedGames;
using RoR2.Skills;
using MSU.Config;
using Dewdrops;
using EmotesAPI;

namespace Dewdrops.Survivors.Welder
{
    public class Welder : DewdropsSurvivor
    {
        public override void Initialize()
        {
            CreateEffects();

            BodyCatalog.availability.CallWhenAvailable(CreateProjectiles);

            CreateUI();

            ModifyPrefab();

            Hooks();
        }

        public void ModifyPrefab()
        {
            var cb = characterPrefab.GetComponent<CharacterBody>();
            cb.preferredPodPrefab = Resources.Load<GameObject>("Prefabs/NetworkedObjects/SurvivorPod");
            cb._defaultCrosshairPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Crosshair/Bandit2Crosshair");
        }
        public override bool IsAvailable(ContentPack contentPack)
        {
            return true;
        }

        public override DewdropsAssetRequest<SurvivorAssetCollection> LoadAssetRequest()
        {
            return DewdropsAssets.LoadAssetAsync<SurvivorAssetCollection>("acWelder", DewdropsBundle.Main);
        }
        #region effects
        private void CreateEffects()
        {
        }

#endregion
        #region projectiles
        private void CreateProjectiles()
        {
        }
        #endregion

        #region UI
        private void CreateUI()
        {
        }
        #endregion
        private void Hooks()
        {
            GlobalEventManager.onServerDamageDealt += GlobalEventManager_onServerDamageDealt;
            RecalculateStatsAPI.GetStatCoefficients += RecalculateStatsAPI_GetStatCoefficients;

            if (DewdropsMain.emotesInstalled)
            {
                Emotes();
            }
        }
        private static void GlobalEventManager_onServerDamageDealt(DamageReport damageReport)
        {
            DamageInfo damageInfo = damageReport.damageInfo;
            if (!damageReport.attackerBody || !damageReport.victimBody)
            {
                return;
            }
            HealthComponent victim = damageReport.victim;
            GameObject inflictorObject = damageInfo.inflictor;
            CharacterBody victimBody = damageReport.victimBody;
            EntityStateMachine victimMachine = victimBody.GetComponent<EntityStateMachine>();
            CharacterBody attackerBody = damageReport.attackerBody;
            GameObject attackerObject = damageReport.attacker.gameObject;
            if (NetworkServer.active)
            {
                if (attackerBody && victimBody)
                {
                }
            }
        }

        private void RecalculateStatsAPI_GetStatCoefficients(CharacterBody sender, RecalculateStatsAPI.StatHookEventArgs args)
        {
        }
        private void Emotes()
        {
            On.RoR2.SurvivorCatalog.Init += (orig) =>
            {
                orig();
                var skele = DewdropsAssets.GetAssetBundle(DewdropsBundle.Survivors).LoadAsset<GameObject>("welder_emoteskeleton");
                CustomEmotesAPI.ImportArmature(this.characterPrefab, skele);
            };
            CustomEmotesAPI.animChanged += CustomEmotesAPI_animChanged;
        }
        private void CustomEmotesAPI_animChanged(string newAnimation, BoneMapper mapper)
        {
            if (newAnimation != "none")
            {
                if (mapper.transform.name == "welder_emoteskeleton")
                {
                    mapper.transform.parent.Find("meshWelderTorch").gameObject.SetActive(value: false);
                }
            }
            else
            {
                if (mapper.transform.name == "welder_emoteskeleton")
                {
                    mapper.transform.parent.Find("meshWelderTorch").gameObject.SetActive(value: true);
                }
            }
        }
    }

}


