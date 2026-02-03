using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Linq;

public class LoadAvatarByAddressables : MonoBehaviour
{
    public static LoadAvatarByAddressables Instance;
    //Data
    public List<Sprite> avatarPrefabs = new();
    //Label
    private const string pokemonLabel = "Pokemon";

    private void Awake()
    {
        if (Instance == null)
        {
            StartLoadingPrefabs();
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /*public void StartLoadingPrefabs()
    {
        //Take Holder Prefabs
        Addressables.LoadAssetsAsync<GameObject>(pokemonLabel, (obj) => { }).Completed += handle =>
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                avatarPrefabs = handle.Result.OrderBy(prefabs => prefabs.name).ToList();
            }
        });
        //Take Judge Line Prefabs
        Addressables.LoadAssetsAsync<GameObject>(judgeLineLabel, (obj) => { }).Completed += handle =>
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                judgeLinePrefabs = handle.Result.OrderBy(prefabs => prefabs.name).ToList();
            }
        });
        //Take Note Prefabs
        Addressables.LoadAssetsAsync<GameObject>(noteLabel, (obj) => { }).Completed += handle =>
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                notePrefabs = handle.Result.OrderBy(prefabs => prefabs.name).ToList();
            }
        });
    }*/
    public void StartLoadingPrefabs()
    {
        //Take Avatar Prefabs
        Addressables.LoadAssetsAsync<Sprite>(pokemonLabel, (obj) => { }).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                avatarPrefabs = handle.Result.OrderBy(prefabs => prefabs.name).ToList();
            }
        };
    }
}