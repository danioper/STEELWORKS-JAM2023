using UnityEngine;
using System.Collections;

public class menuMusic : MonoBehaviour
{

	void Awake()
	{
		Destroy(GameObject.Find("GameMusic"));
		DontDestroyOnLoad(gameObject);
	}

}