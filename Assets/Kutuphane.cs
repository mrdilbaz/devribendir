using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kutuphane : MonoBehaviour {


	public UsulSablon sablon;
	public Usul[] usuller;

	private int usulIndex = 0;

	// Use this for initialization


	public  void OnEnable()
	{
		usulIndex = 0;
		Uygula(0);
	}

	public void Vur(){
		
	}

	public void Degistir(bool ileri){
		usulIndex = ileri ? (usulIndex == usuller.Length-1 ? 0 : usulIndex + 1) : 
							(usulIndex == 0 ? usuller.Length -1 : usulIndex -1);
		
		Uygula(usulIndex);
	}

	void Uygula(int index){
		Usul usul = usuller[index];
		sablon.UsulAdı = usul.name;
		sablon.zaman = usul.zaman;
		sablon.birim = usul.birim;
		sablon.vuruslar = usul.vuruslar;
		sablon.Olustur();
	}


}



