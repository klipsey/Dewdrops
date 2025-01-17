﻿using MSU;
using R2API.ScriptableObjects;
using RoR2;
using RoR2.ContentManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
namespace Dewdrops
{
    public abstract class DewdropsScene : ISceneContentPiece, IContentPackModifier
    {
        public SceneAssetCollection assetCollection { get; private set; }
        public NullableRef<MusicTrackDef> mainTrack { get; protected set; }
        public NullableRef<MusicTrackDef> bossTrack { get; protected set; }
        public NullableRef<Texture2D> bazaarTextureBase { get; protected set; } // ???
        public SceneDef asset { get; protected set; }
        public float? weightRelativeToSiblings { get; protected set; }
        public bool? preLoop { get; protected set; }
        public bool? postLoop { get; protected set; }

        public abstract DewdropsAssetRequest<SceneAssetCollection> LoadAssetRequest();
        public abstract void Initialize();
        public abstract bool IsAvailable(ContentPack contentPack);
        public virtual IEnumerator LoadContentAsync()
        {
            DewdropsAssetRequest<SceneAssetCollection> request = LoadAssetRequest();

            request.StartLoad();
            while (!request.isComplete)
                yield return null;

            assetCollection = request.asset;

            asset = assetCollection.sceneDef;
            
            if (assetCollection.mainTrackDef)
                mainTrack = assetCollection.mainTrackDef.value;

            if (assetCollection.bossTrackDef)
                bossTrack = assetCollection.bossTrackDef.value;

            weightRelativeToSiblings = assetCollection.customWeightRelativeToSiblings;
            postLoop = assetCollection.appearsPostLoop;
            preLoop = assetCollection.appearsPreLoop;
        }


        public virtual void ModifyContentPack(ContentPack contentPack)
        {
            contentPack.AddContentFromAssetCollection(assetCollection);
        }

        public virtual void OnServerStageComplete(Stage stage)
        {
        }

        public virtual void OnServerStageBegin(Stage stage)
        {           
        }
    }
}
