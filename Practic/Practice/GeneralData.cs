using System;
namespace Practice
{
	public class GeneralData
	{
		public Patient patient { get; set; }
		public Cornea rogovicaOD { get; set; }
		public Cornea rogovicaOS { get; set; }
		public TotalKeratometery TotalKeratometeryOD { get; set; }
		public TotalKeratometery TotalKeratometeryOS { get; set; }
        public CorneaBackSurface zadRogovicaOD { get; set; }
		public CorneaBackSurface zadRogovicaOS { get; set; }
		public OtherValues otherOD { get; set; }
		public OtherValues otherOS { get; set; }
		public BioIndicators bioIndicatorsOD { get; set; }
        public BioIndicators bioIndicatorsOS { get; set; }
    }
}

