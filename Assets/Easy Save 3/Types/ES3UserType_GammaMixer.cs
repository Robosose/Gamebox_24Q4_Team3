using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("_gamma")]
	public class ES3UserType_GammaMixer : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_GammaMixer() : base(typeof(Gamma)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Gamma)obj;
			
			writer.WritePrivateField("_gamma", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Gamma)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "_gamma":
					instance = (Gamma)reader.SetPrivateField("_gamma", reader.Read<System.Single>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_GammaMixerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_GammaMixerArray() : base(typeof(Gamma[]), ES3UserType_GammaMixer.Instance)
		{
			Instance = this;
		}
	}
}