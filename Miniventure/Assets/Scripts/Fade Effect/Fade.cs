﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public void FadeIn(float fadeOutTime, System.Action nextEvent = null)
	{
		StartCoroutine(CoFadeIn(fadeOutTime,nextEvent));
	}

	public void FadeOut(float fadeOutTime, System.Action nextEvent = null)
	{
		StartCoroutine(CoFadeOut(fadeOutTime, nextEvent));
	}

	// 투명 -> 불투명
	IEnumerator CoFadeIn(float fadeOutTime, System.Action nextEvent = null)
	{
		Debug.Log("페이드인");
		Image img = this.gameObject.GetComponent<Image>();
		//SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
		Color tempColor = img.color;
		while(tempColor.a < 1f){
			tempColor.a += Time.deltaTime / fadeOutTime;
			img.color = tempColor;

			if(tempColor.a >= 1f) tempColor.a = 1f;

			yield return null;
		}

		img.color = tempColor;
		if(nextEvent != null) nextEvent();
	}

	// 불투명 -> 투명
	IEnumerator CoFadeOut(float fadeOutTime, System.Action nextEvent = null)
	{
		Debug.Log("페이드아웃");
		Image img = this.gameObject.GetComponent<Image>();
		Color tempColor = img.color;
		while(tempColor.a > 0f){
			tempColor.a -= Time.deltaTime / fadeOutTime;
			img.color = tempColor;

			if(tempColor.a <= 0f) tempColor.a = 0f;

			yield return null;
		}
		img.color = tempColor;
		if(nextEvent != null) nextEvent();
	}

	public void FadeOutSprite(SpriteRenderer renderer, float fadeOutTime, System.Action nextEvent = null)
	{
		StartCoroutine(CoFadeOutSprite(renderer, fadeOutTime, nextEvent));
	}

	IEnumerator CoFadeOutSprite(SpriteRenderer renderer, float fadeOutTime, System.Action nextEvent = null)
    {
        Debug.Log("페이드아웃");
		Color tempColor = renderer.color;
		while(tempColor.a > 0f){
			tempColor.a -= Time.deltaTime / fadeOutTime;
			renderer.color = tempColor;

			if(tempColor.a <= 0f) tempColor.a = 0f;

			yield return null;
		}
		renderer.color = tempColor;
		if(nextEvent != null) nextEvent();
    }
}
