//
// API.AI Unity SDK - Unity libraries for API.AI
// =================================================
//
// Copyright (C) 2015 by Speaktoit, Inc. (https://www.speaktoit.com)
// https://www.api.ai
//
// ***********************************************************************************************************************
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with
// the License. You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on
// an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the
// specific language governing permissions and limitations under the License.
//
// ***********************************************************************************************************************

using System;
using UnityEngine;
using fastJSON;

namespace ApiAiSDK.Unity.Android
{
	public class AndroidRecognizer
	{
		AndroidJavaObject recognitionHelper;

		public AndroidRecognizer()
		{
			if (Application.platform != RuntimePlatform.Android) {
				throw new InvalidOperationException("AndroidRecognizer can't be used on other platforms than Android");
			}

			recognitionHelper = new AndroidJavaObject("ai.api.unityhelper.RecognitionHelper");
		}

		public void Initialize()
		{
			AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
			AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");

			recognitionHelper.Call("initialize", currentActivity);
		}

		public AndroidRecognitionResult Recognize(string lang)
		{
			var recognitionResultString = recognitionHelper.Call<string>("recognize", lang);
			Debug.Log(recognitionResultString);
			return fastJSON.JSON.ToObject<AndroidRecognitionResult>(recognitionResultString);
		}

	}
}

