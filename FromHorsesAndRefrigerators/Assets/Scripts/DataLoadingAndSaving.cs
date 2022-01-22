using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ServerModels;
using System.Linq;

public static class DataLoadingAndSaving
{
	private static string recoveredValue;
	public static Dictionary<string, string> serverData;
	private static System.Action OnDataRecovered;
	public static System.Action OnDataRequestComplete;

	private static string splitMark = "/r/";

	public delegate void TaskCompletedCallBack(string taskResult);
	
	public static void RequestDatafromServer() 
	{
		GetAllTitleData();
	}
	

	public static void SetTitleData(string keyToSet, string valueToSet)
	{
		PlayFabServerAPI.SetTitleData(
			new SetTitleDataRequest
			{
				Key = keyToSet,
				Value = valueToSet
			},
			result => Debug.Log("Set titleData successful"),
			error =>
			{
				Debug.Log("Got error setting titleData:");
				Debug.Log(error.GenerateErrorReport());
			}
		);
	}

	public static void GetTitleData(string keyToGet)
	{
		PlayFabServerAPI.GetTitleData(new GetTitleDataRequest(),
			result =>
			{
				if (result.Data == null || !result.Data.ContainsKey(keyToGet)) Debug.Log("No such key");
				else
				{
					Debug.Log("Matching key found");
					recoveredValue = result.Data[keyToGet];
					OnDataRecovered?.Invoke();
				}
			},
			error =>
			{
				Debug.Log("Got error getting titleData:");
				Debug.Log(error.GenerateErrorReport());
			});
	}

	public static void GetAllTitleData()
	{
		PlayFabServerAPI.GetTitleData(new GetTitleDataRequest(),
			result =>
			{
				if (result.Data == null) Debug.Log("Title Data Null");
				else
				{
					Debug.Log("Matching key found");
					serverData = result.Data;
					OnDataRequestComplete?.Invoke();
				}
			},
			error =>
			{
				Debug.Log("Got error getting titleData:");
				Debug.Log(error.GenerateErrorReport());
			});
	}

	public static void AddEntryToKey(string key, string newEntry)
	{
		GetTitleData(key);
		OnDataRecovered += () =>
		{
			string newData = recoveredValue + splitMark + newEntry;
			SetTitleData(key, newData);
			OnDataRecovered = null;
		};
	}

	public static List<string> GetAllEntriesfromKey( string key)
	{
		if (serverData == null || serverData.Count == 0) 
		{
			Debug.Log("Server Data empty");
		}
		if (!serverData.ContainsKey(key))
		{
			Debug.Log("No such key");
			return null;
		}

		Debug.Log("Fitting key found. retreiving");
		string[] newData = serverData[key].Split(splitMark);
		List<string> converted = new List<string>(newData);
		converted.Remove(string.Empty);
		return converted;
	}


}