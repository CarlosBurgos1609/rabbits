﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
	

	public void Jugar()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
	}
	//public void Salir(){
	//	Debug.Log("salir....");
	//	Aplication.Quit();
//
	//}
}
