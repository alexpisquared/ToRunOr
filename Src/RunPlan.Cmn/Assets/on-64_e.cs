using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmn.Assets
{
	/// <remarks/>
	//[System.SerializableAttribute()]
	//[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.w3.org/2005/Atom")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "https://www.w3.org/2005/Atom", IsNullable = false)]
	public partial class feed
	{

		private string titleField;

		private feedLink[] linkField;

		private feedAuthor authorField;

		private System.DateTime updatedField;

		private string idField;

		private string logoField;

		private string iconField;

		private string rightsField;

		private feedEntry[] entryField;

		private string langField;

		/// <remarks/>
		public string title
		{
			get
			{
				return this.titleField;
			}
			set
			{
				this.titleField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("link")]
		public feedLink[] link
		{
			get
			{
				return this.linkField;
			}
			set
			{
				this.linkField = value;
			}
		}

		/// <remarks/>
		public feedAuthor author
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
		public System.DateTime updated
		{
			get
			{
				return this.updatedField;
			}
			set
			{
				this.updatedField = value;
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
		public string logo
		{
			get
			{
				return this.logoField;
			}
			set
			{
				this.logoField = value;
			}
		}

		/// <remarks/>
		public string icon
		{
			get
			{
				return this.iconField;
			}
			set
			{
				this.iconField = value;
			}
		}

		/// <remarks/>
		public string rights
		{
			get
			{
				return this.rightsField;
			}
			set
			{
				this.rightsField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("entry")]
		public feedEntry[] entry
		{
			get
			{
				return this.entryField;
			}
			set
			{
				this.entryField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "https://www.w3.org/XML/1998/namespace")]
		public string lang
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
	//[System.SerializableAttribute()]
	//[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.w3.org/2005/Atom")]
	public partial class feedLink
	{

		private string relField;

		private string hrefField;

		private string typeField;

		private string hreflangField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string rel
		{
			get
			{
				return this.relField;
			}
			set
			{
				this.relField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string href
		{
			get
			{
				return this.hrefField;
			}
			set
			{
				this.hrefField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string hreflang
		{
			get
			{
				return this.hreflangField;
			}
			set
			{
				this.hreflangField = value;
			}
		}
	}

	/// <remarks/>
	//[System.SerializableAttribute()]
	//[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.w3.org/2005/Atom")]
	public partial class feedAuthor
	{

		private string nameField;

		private string uriField;

		/// <remarks/>
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
		public string uri
		{
			get
			{
				return this.uriField;
			}
			set
			{
				this.uriField = value;
			}
		}
	}

	/// <remarks/>
	//[System.SerializableAttribute()]
	//[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.w3.org/2005/Atom")]
	public partial class feedEntry
	{

		private string titleField;

		private feedEntryLink linkField;

		private System.DateTime updatedField;

		private System.DateTime publishedField;

		private feedEntryCategory categoryField;

		private feedEntrySummary summaryField;

		private string idField;

		/// <remarks/>
		public string title
		{
			get
			{
				return this.titleField;
			}
			set
			{
				this.titleField = value;
			}
		}

		/// <remarks/>
		public feedEntryLink link
		{
			get
			{
				return this.linkField;
			}
			set
			{
				this.linkField = value;
			}
		}

		/// <remarks/>
		public System.DateTime updated
		{
			get
			{
				return this.updatedField;
			}
			set
			{
				this.updatedField = value;
			}
		}

		/// <remarks/>
		public System.DateTime published
		{
			get
			{
				return this.publishedField;
			}
			set
			{
				this.publishedField = value;
			}
		}

		/// <remarks/>
		public feedEntryCategory category
		{
			get
			{
				return this.categoryField;
			}
			set
			{
				this.categoryField = value;
			}
		}

		/// <remarks/>
		public feedEntrySummary summary
		{
			get
			{
				return this.summaryField;
			}
			set
			{
				this.summaryField = value;
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
	}

	/// <remarks/>
	//[System.SerializableAttribute()]
	//[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.w3.org/2005/Atom")]
	public partial class feedEntryLink
	{

		private string typeField;

		private string hrefField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string href
		{
			get
			{
				return this.hrefField;
			}
			set
			{
				this.hrefField = value;
			}
		}
	}

	/// <remarks/>
	//[System.SerializableAttribute()]
	//[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.w3.org/2005/Atom")]
	public partial class feedEntryCategory
	{

		private string termField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string term
		{
			get
			{
				return this.termField;
			}
			set
			{
				this.termField = value;
			}
		}
	}

	/// <remarks/>
	//[System.SerializableAttribute()]
	//[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.w3.org/2005/Atom")]
	public partial class feedEntrySummary
	{

		private string typeField;

		private string valueField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlTextAttribute()]
		public string Value
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

}
