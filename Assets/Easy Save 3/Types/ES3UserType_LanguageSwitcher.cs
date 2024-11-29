namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("_dropdownValue")]
	public class ES3UserType_LanguageSwitcher : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_LanguageSwitcher() : base(typeof(LanguageSwitcher)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (LanguageSwitcher)obj;
			
			writer.WritePrivateField("_dropdownValue", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (LanguageSwitcher)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "_dropdownValue":
					instance = (LanguageSwitcher)reader.SetPrivateField("_dropdownValue", reader.Read<System.Int32>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_LanguageSwitcherArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_LanguageSwitcherArray() : base(typeof(LanguageSwitcher[]), ES3UserType_LanguageSwitcher.Instance)
		{
			Instance = this;
		}
	}
}