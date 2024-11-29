using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("_music", "_sound")]
	public class ES3UserType_AudioMixer : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_AudioMixer() : base(typeof(Settings.Sound)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Settings.Sound)obj;
			
			writer.WritePrivateField("_music", instance);
			writer.WritePrivateField("_sound", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Settings.Sound)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "_music":
					instance = (Settings.Sound)reader.SetPrivateField("_music", reader.Read<System.Single>(), instance);
					break;
					case "_sound":
					instance = (Settings.Sound)reader.SetPrivateField("_sound", reader.Read<System.Single>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_AudioMixerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_AudioMixerArray() : base(typeof(Settings.Sound[]), ES3UserType_AudioMixer.Instance)
		{
			Instance = this;
		}
	}
}