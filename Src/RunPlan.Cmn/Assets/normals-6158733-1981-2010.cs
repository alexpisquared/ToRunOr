using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmn.Assets
{


	/// <remarks/>
	// [ System.SerializableAttribute()]
	// [ System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.opengis.net/om/1.0")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "https://www.opengis.net/om/1.0", IsNullable = false)]
	public partial class ObservationCollection
	{

		private ObservationCollectionMember memberField;

		/// <remarks/>
		public ObservationCollectionMember member
		{
			get
			{
				return this.memberField;
			}
			set
			{
				this.memberField = value;
			}
		}
	}

	/// <remarks/>
	// [ System.SerializableAttribute()]
	// [ System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.opengis.net/om/1.0")]
	public partial class ObservationCollectionMember
	{

		private ObservationCollectionMemberObservation observationField;

		/// <remarks/>
		public ObservationCollectionMemberObservation Observation
		{
			get
			{
				return this.observationField;
			}
			set
			{
				this.observationField = value;
			}
		}
	}

	/// <remarks/>
	// [ System.SerializableAttribute()]
	// [ System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.opengis.net/om/1.0")]
	public partial class ObservationCollectionMemberObservation
	{

		private ObservationCollectionMemberObservationMetadata metadataField;

		private string procedureField;

		private ObservationCollectionMemberObservationFeatureOfInterest featureOfInterestField;

		private ObservationCollectionMemberObservationResult resultField;

		/// <remarks/>
		public ObservationCollectionMemberObservationMetadata metadata
		{
			get
			{
				return this.metadataField;
			}
			set
			{
				this.metadataField = value;
			}
		}

		/// <remarks/>
		public string procedure
		{
			get
			{
				return this.procedureField;
			}
			set
			{
				this.procedureField = value;
			}
		}

		/// <remarks/>
		public ObservationCollectionMemberObservationFeatureOfInterest featureOfInterest
		{
			get
			{
				return this.featureOfInterestField;
			}
			set
			{
				this.featureOfInterestField = value;
			}
		}

		/// <remarks/>
		public ObservationCollectionMemberObservationResult result
		{
			get
			{
				return this.resultField;
			}
			set
			{
				this.resultField = value;
			}
		}
	}

	/// <remarks/>
	// [ System.SerializableAttribute()]
	// [ System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.opengis.net/om/1.0")]
	public partial class ObservationCollectionMemberObservationMetadata
	{

		private set setField;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace = "https://dms.ec.gc.ca/schema/point-observation/2.0")]
		public set set
		{
			get
			{
				return this.setField;
			}
			set
			{
				this.setField = value;
			}
		}
	}

	/// <remarks/>
	// [ System.SerializableAttribute()]
	// [ System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://dms.ec.gc.ca/schema/point-observation/2.0")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "https://dms.ec.gc.ca/schema/point-observation/2.0", IsNullable = false)]
	public partial class set
	{

		private setGeneral generalField;

		private setElement[] identificationelementsField;

		/// <remarks/>
		public setGeneral general
		{
			get
			{
				return this.generalField;
			}
			set
			{
				this.generalField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayAttribute("identification-elements")]
		[System.Xml.Serialization.XmlArrayItemAttribute("element", IsNullable = false)]
		public setElement[] identificationelements
		{
			get
			{
				return this.identificationelementsField;
			}
			set
			{
				this.identificationelementsField = value;
			}
		}
	}

	/// <remarks/>
	// [ System.SerializableAttribute()]
	// [ System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://dms.ec.gc.ca/schema/point-observation/2.0")]
	public partial class setGeneral
	{

		private setGeneralAuthor authorField;

		private setGeneralDataset datasetField;

		private setGeneralPhase phaseField;

		private string idField;

		private setGeneralLang langField;

		/// <remarks/>
		public setGeneralAuthor author
		{
			get
			{
				return this.authorField;
			}
			set
			{
				this.authorField = value;
			}
		}

		/// <remarks/>
		public setGeneralDataset dataset
		{
			get
			{
				return this.datasetField;
			}
			set
			{
				this.datasetField = value;
			}
		}

		/// <remarks/>
		public setGeneralPhase phase
		{
			get
			{
				return this.phaseField;
			}
			set
			{
				this.phaseField = value;
			}
		}

		/// <remarks/>
		public string id
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		/// <remarks/>
		public setGeneralLang lang
		{
			get
			{
				return this.langField;
			}
			set
			{
				this.langField = value;
			}
		}
	}

	/// <remarks/>
	// [ System.SerializableAttribute()]
	// [ System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://dms.ec.gc.ca/schema/point-observation/2.0")]
	public partial class setGeneralAuthor
	{

		private string nameField;

		private decimal versionField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public decimal version
		{
			get
			{
				return this.versionField;
			}
			set
			{
				this.versionField = value;
			}
		}
	}

	/// <remarks/>
	// [ System.SerializableAttribute()]
	// [ System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://dms.ec.gc.ca/schema/point-observation/2.0")]
	public partial class setGeneralDataset
	{

		private string nameField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}
	}

	/// <remarks/>
	// [ System.SerializableAttribute()]
	// [ System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://dms.ec.gc.ca/schema/point-observation/2.0")]
	public partial class setGeneralPhase
	{

		private string nameField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}
	}

	/// <remarks/>
	// [ System.SerializableAttribute()]
	// [ System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://dms.ec.gc.ca/schema/point-observation/2.0")]
	public partial class setGeneralLang
	{

		private string nameField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}
	}

	/// <remarks/>
	// [ System.SerializableAttribute()]
	// [ System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://dms.ec.gc.ca/schema/point-observation/2.0")]
	public partial class setElement
	{

		private string nameField;

		private string uomField;

		private string valueField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string uom
		{
			get
			{
				return this.uomField;
			}
			set
			{
				this.uomField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}
	}

	/// <remarks/>
	// [ System.SerializableAttribute()]
	// [ System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.opengis.net/om/1.0")]
	public partial class ObservationCollectionMemberObservationFeatureOfInterest
	{

		private FeatureCollection featureCollectionField;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace = "https://www.opengis.net/gml")]
		public FeatureCollection FeatureCollection
		{
			get
			{
				return this.featureCollectionField;
			}
			set
			{
				this.featureCollectionField = value;
			}
		}
	}

	/// <remarks/>
	// [ System.SerializableAttribute()]
	// [ System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.opengis.net/gml")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "https://www.opengis.net/gml", IsNullable = false)]
	public partial class FeatureCollection
	{

		private FeatureCollectionLocation locationField;

		/// <remarks/>
		public FeatureCollectionLocation location
		{
			get
			{
				return this.locationField;
			}
			set
			{
				this.locationField = value;
			}
		}
	}

	/// <remarks/>
	// [ System.SerializableAttribute()]
	// [ System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.opengis.net/gml")]
	public partial class FeatureCollectionLocation
	{

		private FeatureCollectionLocationPoint pointField;

		/// <remarks/>
		public FeatureCollectionLocationPoint Point
		{
			get
			{
				return this.pointField;
			}
			set
			{
				this.pointField = value;
			}
		}
	}

	/// <remarks/>
	// [ System.SerializableAttribute()]
	// [ System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.opengis.net/gml")]
	public partial class FeatureCollectionLocationPoint
	{

		private string posField;

		/// <remarks/>
		public string pos
		{
			get
			{
				return this.posField;
			}
			set
			{
				this.posField = value;
			}
		}
	}

	/// <remarks/>
	// [ System.SerializableAttribute()]
	// [ System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.opengis.net/om/1.0")]
	public partial class ObservationCollectionMemberObservationResult
	{

		private elementsElement[] elementsField;

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayAttribute(Namespace = "https://dms.ec.gc.ca/schema/point-observation/2.0")]
		[System.Xml.Serialization.XmlArrayItemAttribute("element", IsNullable = false)]
		public elementsElement[] elements
		{
			get
			{
				return this.elementsField;
			}
			set
			{
				this.elementsField = value;
			}
		}
	}

	/// <remarks/>
	// [ System.SerializableAttribute()]
	// [ System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://dms.ec.gc.ca/schema/point-observation/2.0")]
	public partial class elementsElement
	{

		private elementsElementElement[] elementField;

		private string nameField;

		private string uomField;

		private byte valueField;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("element")]
		public elementsElementElement[] element
		{
			get
			{
				return this.elementField;
			}
			set
			{
				this.elementField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string uom
		{
			get
			{
				return this.uomField;
			}
			set
			{
				this.uomField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public byte value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}
	}

	/// <remarks/>
	// [ System.SerializableAttribute()]
	// [ System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://dms.ec.gc.ca/schema/point-observation/2.0")]
	public partial class elementsElementElement
	{

		private elementsElementElementQualifier[] qualifierField;

		private string nameField;

		private string valueField;

		private string uomField;

		private string code_srcField;

		private string code_typeField;

		private string formatField;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("qualifier")]
		public elementsElementElementQualifier[] qualifier
		{
			get
			{
				return this.qualifierField;
			}
			set
			{
				this.qualifierField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string uom
		{
			get
			{
				return this.uomField;
			}
			set
			{
				this.uomField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string code_src
		{
			get
			{
				return this.code_srcField;
			}
			set
			{
				this.code_srcField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string code_type
		{
			get
			{
				return this.code_typeField;
			}
			set
			{
				this.code_typeField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string format
		{
			get
			{
				return this.formatField;
			}
			set
			{
				this.formatField = value;
			}
		}
	}

	/// <remarks/>
	// [ System.SerializableAttribute()]
	// [ System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://dms.ec.gc.ca/schema/point-observation/2.0")]
	public partial class elementsElementElementQualifier
	{

		private string nameField;

		private string valueField;

		private string formatField;

		private string uomField;

		private string code_srcField;

		private string code_typeField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string format
		{
			get
			{
				return this.formatField;
			}
			set
			{
				this.formatField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string uom
		{
			get
			{
				return this.uomField;
			}
			set
			{
				this.uomField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string code_src
		{
			get
			{
				return this.code_srcField;
			}
			set
			{
				this.code_srcField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string code_type
		{
			get
			{
				return this.code_typeField;
			}
			set
			{
				this.code_typeField = value;
			}
		}
	}

	/// <remarks/>
	// [ System.SerializableAttribute()]
	// [ System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://dms.ec.gc.ca/schema/point-observation/2.0")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "https://dms.ec.gc.ca/schema/point-observation/2.0", IsNullable = false)]
	public partial class elements
	{

		private elementsElement[] elementField;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("element")]
		public elementsElement[] element
		{
			get
			{
				return this.elementField;
			}
			set
			{
				this.elementField = value;
			}
		}
	}

}
