using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHolder : MonoBehaviour
{
	[Header("ObjectHolder vars")]

	#region StaticProperties
	private static ObjectHolder instance;
	private static int instantiated;
	public static ObjectHolder Instance
	{
		get
		{
			if(instantiated > 0)
				return instance;
			else
			{
				throw new NullReferenceException();
			}
		}
	}
	#endregion

	#region PublicVars
	//[Header("floats")]

	//[Header("ints")]
	//[Header("bools")]
	[Header("GO, Transforms")]
	public Dictionary<string, System.Object> dictionary = new Dictionary<string, object>();
	#endregion

	#region PrivateVars
	//[Header("floats")]
	//[Header("ints")]
	//[Header("bools")]
	//[Header("GO, Transforms")]
	#endregion

	#region PublicFunctions
	public System.Object GetObjWithKey(string key)
	{
		if (dictionary.ContainsKey(key))
		{
			return dictionary[key];
		}
		else
		{
			throw new NullReferenceException("no obj with key " + key);
		}
	}
	#endregion

	#region PrivateFunctions
	private void Awake()
	{
		instance = this;
		instantiated++;
	}

	private void OnDestroy()
	{
		instance = null;
		instantiated--;
	}
	#endregion
}
