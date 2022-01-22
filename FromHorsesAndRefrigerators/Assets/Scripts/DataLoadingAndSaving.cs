using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ServerModels;

public static class DataLoadingAndSaving
{
	public static string recoveredValue;
	public static System.Action OnDataRecovered;


	public static void SetTitleData(string keyToSet, string valueToSet)
	{
		PlayFabServerAPI.SetTitleData(
			new SetTitleDataRequest
			{
				Key = keyToSet,
				Value = valueToSet
			},
			result => Debug.Log("Set titleData successful"),
			error => {
				Debug.Log("Got error setting titleData:");
				Debug.Log(error.GenerateErrorReport());
			}
		);
	}

	public static void GetTitleData(string keyToGet)
	{
		PlayFabServerAPI.GetTitleData(new GetTitleDataRequest(),
			result => {
				if (result.Data == null || !result.Data.ContainsKey(keyToGet)) Debug.Log("No such key");
				else
				{
					Debug.Log("Matching key found");
					recoveredValue = result.Data[keyToGet];
					OnDataRecovered?.Invoke();
				}
			},
			error => {
				Debug.Log("Got error getting titleData:");
				Debug.Log(error.GenerateErrorReport());
			});
	}

	public static void AddEntrytoKey(string key, string newEntry)  
	{
		GetTitleData(key);
		OnDataRecovered += () =>
		{
			string newData = recoveredValue + "/r/" + newEntry;
			SetTitleData(key, newData);
		};
	}
}
