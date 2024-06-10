using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
	private static BGM bgmInstance;
	void Awake()
	{
		if (bgmInstance == null)
		{
			bgmInstance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
}
