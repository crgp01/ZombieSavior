using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.RemoteConfig;

public class RemoteConfigs : MonoBehaviour
{
    public bool storeIsActive = true;
    public struct userAttributes { }
    public struct appAttributes { }
    // Start is called before the first frame update
    private void Awake()
    {
        ConfigManager.FetchCompleted += DisableStore;
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }

    // Update is called once per frame
    void Update()
    {
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());

    }

    void DisableStore(ConfigResponse response)
    {
        storeIsActive = ConfigManager.appConfig.GetBool("ActiveStore");
    }
}
