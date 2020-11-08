using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.RemoteConfig;

public class RemoteConfigs : MonoBehaviour
{
    public bool storeIsActive = true;
    public bool canPurchaseShotgun = true;
    public int potionIncreasingTime = 35;
    public struct userAttributes { }
    public struct appAttributes { }

    private void Awake()
    {
        ConfigManager.FetchCompleted += DisableStore;
        ConfigManager.FetchCompleted += YellowPotionIncreasingTime;
        ConfigManager.FetchCompleted += CanPurchaseShotgun;
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }

    private void Update()
    {
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }

    private void DisableStore(ConfigResponse response)
    {
        storeIsActive = ConfigManager.appConfig.GetBool("ActiveStore");
    }
    private void YellowPotionIncreasingTime(ConfigResponse response)
    {
        storeIsActive = ConfigManager.appConfig.GetBool("YellowPotionIncreasingTime");
    }
    private void CanPurchaseShotgun(ConfigResponse response)
    {
        canPurchaseShotgun = ConfigManager.appConfig.GetBool("EnableShotgunPurchase");
    }

    private void OnDestroy() {
        ConfigManager.FetchCompleted -= DisableStore;
        ConfigManager.FetchCompleted -= YellowPotionIncreasingTime;
        ConfigManager.FetchCompleted -= CanPurchaseShotgun;
    }
}
