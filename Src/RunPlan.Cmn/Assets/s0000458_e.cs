using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmn.Assets
{
  /// https://dd.weather.gc.ca/citypage_weather/xml/ON/s0000458_e.xml
  /// ...which is Toronto
  /// ...looks like a daily everything about TO!!!
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
  [Obsolete(@"Use C:\c\aslink\websvctypes\xsd.cls.site.cs ", true)]
  public partial class siteData
  {

    string licenseField;

    siteDataDateTime[] dateTimeField;

    siteDataLocation locationField;

    object warningsField;

    siteDataCurrentConditions currentConditionsField;

    siteDataForecastGroup forecastGroupField;

    siteDataYesterdayConditions yesterdayConditionsField;

    siteDataRiseSet riseSetField;

    siteDataAlmanac almanacField;

    /// <remarks/>
    public string license
    {
      get
      {
        return this.licenseField;
      }
      set
      {
        this.licenseField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("dateTime")]
    public siteDataDateTime[] dateTime
    {
      get
      {
        return this.dateTimeField;
      }
      set
      {
        this.dateTimeField = value;
      }
    }

    /// <remarks/>
    public siteDataLocation location
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

    /// <remarks/>
    public object warnings
    {
      get
      {
        return this.warningsField;
      }
      set
      {
        this.warningsField = value;
      }
    }

    /// <remarks/>
    public siteDataCurrentConditions currentConditions
    {
      get
      {
        return this.currentConditionsField;
      }
      set
      {
        this.currentConditionsField = value;
      }
    }

    /// <remarks/>
    public siteDataForecastGroup forecastGroup
    {
      get
      {
        return this.forecastGroupField;
      }
      set
      {
        this.forecastGroupField = value;
      }
    }

    /// <remarks/>
    public siteDataYesterdayConditions yesterdayConditions
    {
      get
      {
        return this.yesterdayConditionsField;
      }
      set
      {
        this.yesterdayConditionsField = value;
      }
    }

    /// <remarks/>
    public siteDataRiseSet riseSet
    {
      get
      {
        return this.riseSetField;
      }
      set
      {
        this.riseSetField = value;
      }
    }

    /// <remarks/>
    public siteDataAlmanac almanac
    {
      get
      {
        return this.almanacField;
      }
      set
      {
        this.almanacField = value;
      }
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataDateTime
  {

    ushort yearField;

    siteDataDateTimeMonth monthField;

    siteDataDateTimeDay dayField;

    byte hourField;

    byte minuteField;

    ulong timeStampField;

    string textSummaryField;

    string nameField;

    string zoneField;

    sbyte uTCOffsetField;

    /// <remarks/>
    public ushort year
    {
      get
      {
        return this.yearField;
      }
      set
      {
        this.yearField = value;
      }
    }

    /// <remarks/>
    public siteDataDateTimeMonth month
    {
      get
      {
        return this.monthField;
      }
      set
      {
        this.monthField = value;
      }
    }

    /// <remarks/>
    public siteDataDateTimeDay day
    {
      get
      {
        return this.dayField;
      }
      set
      {
        this.dayField = value;
      }
    }

    /// <remarks/>
    public byte hour
    {
      get
      {
        return this.hourField;
      }
      set
      {
        this.hourField = value;
      }
    }

    /// <remarks/>
    public byte minute
    {
      get
      {
        return this.minuteField;
      }
      set
      {
        this.minuteField = value;
      }
    }

    /// <remarks/>
    public ulong timeStamp
    {
      get
      {
        return this.timeStampField;
      }
      set
      {
        this.timeStampField = value;
      }
    }

    /// <remarks/>
    public string textSummary
    {
      get
      {
        return this.textSummaryField;
      }
      set
      {
        this.textSummaryField = value;
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
    public string zone
    {
      get
      {
        return this.zoneField;
      }
      set
      {
        this.zoneField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public sbyte UTCOffset
    {
      get
      {
        return this.uTCOffsetField;
      }
      set
      {
        this.uTCOffsetField = value;
      }
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataDateTimeMonth
  {

    string nameField;

    byte valueField;

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
    [System.Xml.Serialization.XmlTextAttribute()]
    public byte Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataDateTimeDay
  {

    string nameField;

    byte valueField;

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
    [System.Xml.Serialization.XmlTextAttribute()]
    public byte Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataLocation
  {

    string continentField;

    siteDataLocationCountry countryField;

    siteDataLocationProvince provinceField;

    siteDataLocationName nameField;

    string regionField;

    /// <remarks/>
    public string continent
    {
      get
      {
        return this.continentField;
      }
      set
      {
        this.continentField = value;
      }
    }

    /// <remarks/>
    public siteDataLocationCountry country
    {
      get
      {
        return this.countryField;
      }
      set
      {
        this.countryField = value;
      }
    }

    /// <remarks/>
    public siteDataLocationProvince province
    {
      get
      {
        return this.provinceField;
      }
      set
      {
        this.provinceField = value;
      }
    }

    /// <remarks/>
    public siteDataLocationName name
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
    public string region
    {
      get
      {
        return this.regionField;
      }
      set
      {
        this.regionField = value;
      }
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataLocationCountry
  {

    string codeField;

    string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string code
    {
      get
      {
        return this.codeField;
      }
      set
      {
        this.codeField = value;
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

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataLocationProvince
  {

    string codeField;

    string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string code
    {
      get
      {
        return this.codeField;
      }
      set
      {
        this.codeField = value;
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

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataLocationName
  {

    string codeField;

    string latField;

    string lonField;

    string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string code
    {
      get
      {
        return this.codeField;
      }
      set
      {
        this.codeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string lat
    {
      get
      {
        return this.latField;
      }
      set
      {
        this.latField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string lon
    {
      get
      {
        return this.lonField;
      }
      set
      {
        this.lonField = value;
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

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataCurrentConditions
  {

    siteDataCurrentConditionsStation stationField;

    siteDataCurrentConditionsDateTime[] dateTimeField;

    string conditionField;

    siteDataCurrentConditionsIconCode iconCodeField;

    siteDataCurrentConditionsTemperature temperatureField;

    siteDataCurrentConditionsDewpoint dewpointField;
    siteDataCurrentConditionsWindChill windChillField;

    siteDataCurrentConditionsPressure pressureField;

    siteDataCurrentConditionsVisibility visibilityField;

    siteDataCurrentConditionsRelativeHumidity relativeHumidityField;

    siteDataCurrentConditionsWind windField;

    /// <remarks/>
    public siteDataCurrentConditionsStation station
    {
      get
      {
        return this.stationField;
      }
      set
      {
        this.stationField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("dateTime")]
    public siteDataCurrentConditionsDateTime[] dateTime
    {
      get
      {
        return this.dateTimeField;
      }
      set
      {
        this.dateTimeField = value;
      }
    }

    /// <remarks/>
    public string condition
    {
      get
      {
        return this.conditionField;
      }
      set
      {
        this.conditionField = value;
      }
    }

    /// <remarks/>
    public siteDataCurrentConditionsIconCode iconCode
    {
      get
      {
        return this.iconCodeField;
      }
      set
      {
        this.iconCodeField = value;
      }
    }

    /// <remarks/>
    public siteDataCurrentConditionsTemperature temperature
    {
      get
      {
        return this.temperatureField;
      }
      set
      {
        this.temperatureField = value;
      }
    }

    /// <remarks/>
    public siteDataCurrentConditionsDewpoint dewpoint
    {
      get
      {
        return this.dewpointField;
      }
      set
      {
        this.dewpointField = value;
      }
    }
    public siteDataCurrentConditionsWindChill windChill
    {
      get
      {
        return this.windChillField;
      }
      set
      {
        this.windChillField = value;
      }
    }

    /// <remarks/>
    public siteDataCurrentConditionsPressure pressure
    {
      get
      {
        return this.pressureField;
      }
      set
      {
        this.pressureField = value;
      }
    }

    /// <remarks/>
    public siteDataCurrentConditionsVisibility visibility
    {
      get
      {
        return this.visibilityField;
      }
      set
      {
        this.visibilityField = value;
      }
    }

    /// <remarks/>
    public siteDataCurrentConditionsRelativeHumidity relativeHumidity
    {
      get
      {
        return this.relativeHumidityField;
      }
      set
      {
        this.relativeHumidityField = value;
      }
    }

    /// <remarks/>
    public siteDataCurrentConditionsWind wind
    {
      get
      {
        return this.windField;
      }
      set
      {
        this.windField = value;
      }
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataCurrentConditionsStation
  {

    string codeField;

    string latField;

    string lonField;

    string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string code
    {
      get
      {
        return this.codeField;
      }
      set
      {
        this.codeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string lat
    {
      get
      {
        return this.latField;
      }
      set
      {
        this.latField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string lon
    {
      get
      {
        return this.lonField;
      }
      set
      {
        this.lonField = value;
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

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataCurrentConditionsDateTime
  {

    ushort yearField;

    siteDataCurrentConditionsDateTimeMonth monthField;

    siteDataCurrentConditionsDateTimeDay dayField;

    byte hourField;

    byte minuteField;

    ulong timeStampField;

    string textSummaryField;

    string nameField;

    string zoneField;

    sbyte uTCOffsetField;

    /// <remarks/>
    public ushort year
    {
      get
      {
        return this.yearField;
      }
      set
      {
        this.yearField = value;
      }
    }

    /// <remarks/>
    public siteDataCurrentConditionsDateTimeMonth month
    {
      get
      {
        return this.monthField;
      }
      set
      {
        this.monthField = value;
      }
    }

    /// <remarks/>
    public siteDataCurrentConditionsDateTimeDay day
    {
      get
      {
        return this.dayField;
      }
      set
      {
        this.dayField = value;
      }
    }

    /// <remarks/>
    public byte hour
    {
      get
      {
        return this.hourField;
      }
      set
      {
        this.hourField = value;
      }
    }

    /// <remarks/>
    public byte minute
    {
      get
      {
        return this.minuteField;
      }
      set
      {
        this.minuteField = value;
      }
    }

    /// <remarks/>
    public ulong timeStamp
    {
      get
      {
        return this.timeStampField;
      }
      set
      {
        this.timeStampField = value;
      }
    }

    /// <remarks/>
    public string textSummary
    {
      get
      {
        return this.textSummaryField;
      }
      set
      {
        this.textSummaryField = value;
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
    public string zone
    {
      get
      {
        return this.zoneField;
      }
      set
      {
        this.zoneField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public sbyte UTCOffset
    {
      get
      {
        return this.uTCOffsetField;
      }
      set
      {
        this.uTCOffsetField = value;
      }
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataCurrentConditionsDateTimeMonth
  {

    string nameField;

    byte valueField;

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
    [System.Xml.Serialization.XmlTextAttribute()]
    public byte Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataCurrentConditionsDateTimeDay
  {

    string nameField;

    byte valueField;

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
    [System.Xml.Serialization.XmlTextAttribute()]
    public byte Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataCurrentConditionsIconCode
  {

    string formatField;

    byte valueField;

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
    [System.Xml.Serialization.XmlTextAttribute()]
    public byte Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataCurrentConditionsTemperature
  {

    string unitTypeField;

    string unitsField;

    decimal valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string unitType
    {
      get
      {
        return this.unitTypeField;
      }
      set
      {
        this.unitTypeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string units
    {
      get
      {
        return this.unitsField;
      }
      set
      {
        this.unitsField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public decimal Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataCurrentConditionsDewpoint
  {

    string unitTypeField;

    string unitsField;

    decimal valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string unitType
    {
      get
      {
        return this.unitTypeField;
      }
      set
      {
        this.unitTypeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string units
    {
      get
      {
        return this.unitsField;
      }
      set
      {
        this.unitsField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public decimal Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataCurrentConditionsWindChill
  {

    string unitTypeField;

    decimal valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string unitType
    {
      get
      {
        return this.unitTypeField;
      }
      set
      {
        this.unitTypeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public decimal Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataCurrentConditionsPressure
  {

    string unitTypeField;

    string unitsField;

    decimal changeField;

    string tendencyField;

    decimal valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string unitType
    {
      get
      {
        return this.unitTypeField;
      }
      set
      {
        this.unitTypeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string units
    {
      get
      {
        return this.unitsField;
      }
      set
      {
        this.unitsField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal change
    {
      get
      {
        return this.changeField;
      }
      set
      {
        this.changeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string tendency
    {
      get
      {
        return this.tendencyField;
      }
      set
      {
        this.tendencyField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public decimal Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataCurrentConditionsVisibility
  {

    string unitTypeField;

    string unitsField;

    decimal valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string unitType
    {
      get
      {
        return this.unitTypeField;
      }
      set
      {
        this.unitTypeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string units
    {
      get
      {
        return this.unitsField;
      }
      set
      {
        this.unitsField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public decimal Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataCurrentConditionsRelativeHumidity
  {

    string unitsField;

    byte valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string units
    {
      get
      {
        return this.unitsField;
      }
      set
      {
        this.unitsField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public byte Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataCurrentConditionsWind
  {

    siteDataCurrentConditionsWindSpeed speedField;

    siteDataCurrentConditionsWindGust gustField;

    string directionField;

    siteDataCurrentConditionsWindBearing bearingField;

    /// <remarks/>
    public siteDataCurrentConditionsWindSpeed speed
    {
      get
      {
        return this.speedField;
      }
      set
      {
        this.speedField = value;
      }
    }

    /// <remarks/>
    public siteDataCurrentConditionsWindGust gust
    {
      get
      {
        return this.gustField;
      }
      set
      {
        this.gustField = value;
      }
    }

    /// <remarks/>
    public string direction
    {
      get
      {
        return this.directionField;
      }
      set
      {
        this.directionField = value;
      }
    }

    /// <remarks/>
    public siteDataCurrentConditionsWindBearing bearing
    {
      get
      {
        return this.bearingField;
      }
      set
      {
        this.bearingField = value;
      }
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataCurrentConditionsWindSpeed
  {

    string unitTypeField;

    string unitsField;

    byte valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string unitType
    {
      get
      {
        return this.unitTypeField;
      }
      set
      {
        this.unitTypeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string units
    {
      get
      {
        return this.unitsField;
      }
      set
      {
        this.unitsField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public byte Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataCurrentConditionsWindGust
  {

    string unitTypeField;

    string unitsField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string unitType
    {
      get
      {
        return this.unitTypeField;
      }
      set
      {
        this.unitTypeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string units
    {
      get
      {
        return this.unitsField;
      }
      set
      {
        this.unitsField = value;
      }
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataCurrentConditionsWindBearing
  {

    string unitsField;

    decimal valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string units
    {
      get
      {
        return this.unitsField;
      }
      set
      {
        this.unitsField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public decimal Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataForecastGroup
  {

    siteDataForecastGroupDateTime[] dateTimeField;

    siteDataForecastGroupRegionalNormals regionalNormalsField;

    siteDataForecastGroupForecast[] forecastField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("dateTime")]
    public siteDataForecastGroupDateTime[] dateTime
    {
      get
      {
        return this.dateTimeField;
      }
      set
      {
        this.dateTimeField = value;
      }
    }

    /// <remarks/>
    public siteDataForecastGroupRegionalNormals regionalNormals
    {
      get
      {
        return this.regionalNormalsField;
      }
      set
      {
        this.regionalNormalsField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("forecast")]
    public siteDataForecastGroupForecast[] forecast
    {
      get
      {
        return this.forecastField;
      }
      set
      {
        this.forecastField = value;
      }
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataForecastGroupDateTime
  {

    ushort yearField;

    siteDataForecastGroupDateTimeMonth monthField;

    siteDataForecastGroupDateTimeDay dayField;

    byte hourField;

    byte minuteField;

    ulong timeStampField;

    string textSummaryField;

    string nameField;

    string zoneField;

    sbyte uTCOffsetField;

    /// <remarks/>
    public ushort year
    {
      get
      {
        return this.yearField;
      }
      set
      {
        this.yearField = value;
      }
    }

    /// <remarks/>
    public siteDataForecastGroupDateTimeMonth month
    {
      get
      {
        return this.monthField;
      }
      set
      {
        this.monthField = value;
      }
    }

    /// <remarks/>
    public siteDataForecastGroupDateTimeDay day
    {
      get
      {
        return this.dayField;
      }
      set
      {
        this.dayField = value;
      }
    }

    /// <remarks/>
    public byte hour
    {
      get
      {
        return this.hourField;
      }
      set
      {
        this.hourField = value;
      }
    }

    /// <remarks/>
    public byte minute
    {
      get
      {
        return this.minuteField;
      }
      set
      {
        this.minuteField = value;
      }
    }

    /// <remarks/>
    public ulong timeStamp
    {
      get
      {
        return this.timeStampField;
      }
      set
      {
        this.timeStampField = value;
      }
    }

    /// <remarks/>
    public string textSummary
    {
      get
      {
        return this.textSummaryField;
      }
      set
      {
        this.textSummaryField = value;
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
    public string zone
    {
      get
      {
        return this.zoneField;
      }
      set
      {
        this.zoneField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public sbyte UTCOffset
    {
      get
      {
        return this.uTCOffsetField;
      }
      set
      {
        this.uTCOffsetField = value;
      }
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataForecastGroupDateTimeMonth
  {

    string nameField;

    byte valueField;

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
    [System.Xml.Serialization.XmlTextAttribute()]
    public byte Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataForecastGroupDateTimeDay
  {

    string nameField;

    byte valueField;

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
    [System.Xml.Serialization.XmlTextAttribute()]
    public byte Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataForecastGroupRegionalNormals
  {

    string textSummaryField;

    siteDataForecastGroupRegionalNormalsTemperature[] temperatureField;

    /// <remarks/>
    public string textSummary
    {
      get
      {
        return this.textSummaryField;
      }
      set
      {
        this.textSummaryField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("temperature")]
    public siteDataForecastGroupRegionalNormalsTemperature[] temperature
    {
      get
      {
        return this.temperatureField;
      }
      set
      {
        this.temperatureField = value;
      }
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataForecastGroupRegionalNormalsTemperature
  {

    string unitTypeField;

    string unitsField;

    string classField;

    decimal valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string unitType
    {
      get
      {
        return this.unitTypeField;
      }
      set
      {
        this.unitTypeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string units
    {
      get
      {
        return this.unitsField;
      }
      set
      {
        this.unitsField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string @class
    {
      get
      {
        return this.classField;
      }
      set
      {
        this.classField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public decimal Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataForecastGroupForecast
  {

    siteDataForecastGroupForecastPeriod periodField;

    string textSummaryField;

    siteDataForecastGroupForecastCloudPrecip cloudPrecipField;

    siteDataForecastGroupForecastAbbreviatedForecast abbreviatedForecastField;

    siteDataForecastGroupForecastTemperatures temperaturesField;

    object windsField;

    siteDataForecastGroupForecastPrecipitation precipitationField;

    siteDataForecastGroupForecastUV uvField;

    siteDataForecastGroupForecastRelativeHumidity relativeHumidityField;

    /// <remarks/>
    public siteDataForecastGroupForecastPeriod period
    {
      get
      {
        return this.periodField;
      }
      set
      {
        this.periodField = value;
      }
    }

    /// <remarks/>
    public string textSummary
    {
      get
      {
        return this.textSummaryField;
      }
      set
      {
        this.textSummaryField = value;
      }
    }

    /// <remarks/>
    public siteDataForecastGroupForecastCloudPrecip cloudPrecip
    {
      get
      {
        return this.cloudPrecipField;
      }
      set
      {
        this.cloudPrecipField = value;
      }
    }

    /// <remarks/>
    public siteDataForecastGroupForecastAbbreviatedForecast abbreviatedForecast
    {
      get
      {
        return this.abbreviatedForecastField;
      }
      set
      {
        this.abbreviatedForecastField = value;
      }
    }

    /// <remarks/>
    public siteDataForecastGroupForecastTemperatures temperatures
    {
      get
      {
        return this.temperaturesField;
      }
      set
      {
        this.temperaturesField = value;
      }
    }

    /// <remarks/>
    public object winds
    {
      get
      {
        return this.windsField;
      }
      set
      {
        this.windsField = value;
      }
    }

    /// <remarks/>
    public siteDataForecastGroupForecastPrecipitation precipitation
    {
      get
      {
        return this.precipitationField;
      }
      set
      {
        this.precipitationField = value;
      }
    }

    /// <remarks/>
    public siteDataForecastGroupForecastUV uv
    {
      get
      {
        return this.uvField;
      }
      set
      {
        this.uvField = value;
      }
    }

    /// <remarks/>
    public siteDataForecastGroupForecastRelativeHumidity relativeHumidity
    {
      get
      {
        return this.relativeHumidityField;
      }
      set
      {
        this.relativeHumidityField = value;
      }
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataForecastGroupForecastPeriod
  {

    string textForecastNameField;

    string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string textForecastName
    {
      get
      {
        return this.textForecastNameField;
      }
      set
      {
        this.textForecastNameField = value;
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

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataForecastGroupForecastCloudPrecip
  {

    string textSummaryField;

    /// <remarks/>
    public string textSummary
    {
      get
      {
        return this.textSummaryField;
      }
      set
      {
        this.textSummaryField = value;
      }
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataForecastGroupForecastAbbreviatedForecast
  {

    siteDataForecastGroupForecastAbbreviatedForecastIconCode iconCodeField;

    siteDataForecastGroupForecastAbbreviatedForecastPop popField;

    string textSummaryField;

    /// <remarks/>
    public siteDataForecastGroupForecastAbbreviatedForecastIconCode iconCode
    {
      get
      {
        return this.iconCodeField;
      }
      set
      {
        this.iconCodeField = value;
      }
    }

    /// <remarks/>
    public siteDataForecastGroupForecastAbbreviatedForecastPop pop
    {
      get
      {
        return this.popField;
      }
      set
      {
        this.popField = value;
      }
    }

    /// <remarks/>
    public string textSummary
    {
      get
      {
        return this.textSummaryField;
      }
      set
      {
        this.textSummaryField = value;
      }
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataForecastGroupForecastAbbreviatedForecastIconCode
  {

    string formatField;

    byte valueField;

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
    [System.Xml.Serialization.XmlTextAttribute()]
    public byte Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataForecastGroupForecastAbbreviatedForecastPop
  {

    string unitsField;

    string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string units
    {
      get
      {
        return this.unitsField;
      }
      set
      {
        this.unitsField = value;
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

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataForecastGroupForecastTemperatures
  {

    string textSummaryField;

    siteDataForecastGroupForecastTemperaturesTemperature temperatureField;

    /// <remarks/>
    public string textSummary
    {
      get
      {
        return this.textSummaryField;
      }
      set
      {
        this.textSummaryField = value;
      }
    }

    /// <remarks/>
    public siteDataForecastGroupForecastTemperaturesTemperature temperature
    {
      get
      {
        return this.temperatureField;
      }
      set
      {
        this.temperatureField = value;
      }
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataForecastGroupForecastTemperaturesTemperature
  {

    string unitTypeField;

    string unitsField;

    string classField;

    decimal valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string unitType
    {
      get
      {
        return this.unitTypeField;
      }
      set
      {
        this.unitTypeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string units
    {
      get
      {
        return this.unitsField;
      }
      set
      {
        this.unitsField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string @class
    {
      get
      {
        return this.classField;
      }
      set
      {
        this.classField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public decimal Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataForecastGroupForecastPrecipitation
  {

    object textSummaryField;

    siteDataForecastGroupForecastPrecipitationPrecipType precipTypeField;

    /// <remarks/>
    public object textSummary
    {
      get
      {
        return this.textSummaryField;
      }
      set
      {
        this.textSummaryField = value;
      }
    }

    /// <remarks/>
    public siteDataForecastGroupForecastPrecipitationPrecipType precipType
    {
      get
      {
        return this.precipTypeField;
      }
      set
      {
        this.precipTypeField = value;
      }
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataForecastGroupForecastPrecipitationPrecipType
  {

    string startField;

    string endField;

    string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string start
    {
      get
      {
        return this.startField;
      }
      set
      {
        this.startField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string end
    {
      get
      {
        return this.endField;
      }
      set
      {
        this.endField = value;
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

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataForecastGroupForecastUV
  {

    byte indexField;

    string textSummaryField;

    string categoryField;

    /// <remarks/>
    public byte index
    {
      get
      {
        return this.indexField;
      }
      set
      {
        this.indexField = value;
      }
    }

    /// <remarks/>
    public string textSummary
    {
      get
      {
        return this.textSummaryField;
      }
      set
      {
        this.textSummaryField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string category
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
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataForecastGroupForecastRelativeHumidity
  {

    string unitsField;

    byte valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string units
    {
      get
      {
        return this.unitsField;
      }
      set
      {
        this.unitsField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public byte Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataYesterdayConditions
  {

    siteDataYesterdayConditionsTemperature[] temperatureField;

    siteDataYesterdayConditionsPrecip precipField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("temperature")]
    public siteDataYesterdayConditionsTemperature[] temperature
    {
      get
      {
        return this.temperatureField;
      }
      set
      {
        this.temperatureField = value;
      }
    }

    /// <remarks/>
    public siteDataYesterdayConditionsPrecip precip
    {
      get
      {
        return this.precipField;
      }
      set
      {
        this.precipField = value;
      }
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataYesterdayConditionsTemperature
  {

    string unitTypeField;

    string unitsField;

    string classField;

    decimal valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string unitType
    {
      get
      {
        return this.unitTypeField;
      }
      set
      {
        this.unitTypeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string units
    {
      get
      {
        return this.unitsField;
      }
      set
      {
        this.unitsField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string @class
    {
      get
      {
        return this.classField;
      }
      set
      {
        this.classField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public decimal Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataYesterdayConditionsPrecip
  {

    string unitTypeField;

    string unitsField;

    string valueField; //todo: org decimal, but has words like 'Trace'

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string unitType
    {
      get
      {
        return this.unitTypeField;
      }
      set
      {
        this.unitTypeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string units
    {
      get
      {
        return this.unitsField;
      }
      set
      {
        this.unitsField = value;
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

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataRiseSet
  {

    string disclaimerField;

    siteDataRiseSetDateTime[] dateTimeField;

    /// <remarks/>
    public string disclaimer
    {
      get
      {
        return this.disclaimerField;
      }
      set
      {
        this.disclaimerField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("dateTime")]
    public siteDataRiseSetDateTime[] dateTime
    {
      get
      {
        return this.dateTimeField;
      }
      set
      {
        this.dateTimeField = value;
      }
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataRiseSetDateTime
  {

    ushort yearField;

    siteDataRiseSetDateTimeMonth monthField;

    siteDataRiseSetDateTimeDay dayField;

    byte hourField;

    byte minuteField;

    ulong timeStampField;

    string textSummaryField;

    string nameField;

    string zoneField;

    sbyte uTCOffsetField;

    /// <remarks/>
    public ushort year
    {
      get
      {
        return this.yearField;
      }
      set
      {
        this.yearField = value;
      }
    }

    /// <remarks/>
    public siteDataRiseSetDateTimeMonth month
    {
      get
      {
        return this.monthField;
      }
      set
      {
        this.monthField = value;
      }
    }

    /// <remarks/>
    public siteDataRiseSetDateTimeDay day
    {
      get
      {
        return this.dayField;
      }
      set
      {
        this.dayField = value;
      }
    }

    /// <remarks/>
    public byte hour
    {
      get
      {
        return this.hourField;
      }
      set
      {
        this.hourField = value;
      }
    }

    /// <remarks/>
    public byte minute
    {
      get
      {
        return this.minuteField;
      }
      set
      {
        this.minuteField = value;
      }
    }

    /// <remarks/>
    public ulong timeStamp
    {
      get
      {
        return this.timeStampField;
      }
      set
      {
        this.timeStampField = value;
      }
    }

    /// <remarks/>
    public string textSummary
    {
      get
      {
        return this.textSummaryField;
      }
      set
      {
        this.textSummaryField = value;
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
    public string zone
    {
      get
      {
        return this.zoneField;
      }
      set
      {
        this.zoneField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public sbyte UTCOffset
    {
      get
      {
        return this.uTCOffsetField;
      }
      set
      {
        this.uTCOffsetField = value;
      }
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataRiseSetDateTimeMonth
  {

    string nameField;

    byte valueField;

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
    [System.Xml.Serialization.XmlTextAttribute()]
    public byte Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataRiseSetDateTimeDay
  {

    string nameField;

    byte valueField;

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
    [System.Xml.Serialization.XmlTextAttribute()]
    public byte Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataAlmanac
  {

    siteDataAlmanacTemperature[] temperatureField;

    siteDataAlmanacPrecipitation[] precipitationField;

    siteDataAlmanacPop popField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("temperature")]
    public siteDataAlmanacTemperature[] temperature
    {
      get
      {
        return this.temperatureField;
      }
      set
      {
        this.temperatureField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("precipitation")]
    public siteDataAlmanacPrecipitation[] precipitation
    {
      get
      {
        return this.precipitationField;
      }
      set
      {
        this.precipitationField = value;
      }
    }

    /// <remarks/>
    public siteDataAlmanacPop pop
    {
      get
      {
        return this.popField;
      }
      set
      {
        this.popField = value;
      }
    }
  }

  /// <remarks/>
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataAlmanacTemperature
  {

    string classField;

    string periodField;

    string unitTypeField;

    string unitsField;

    ushort yearField;

    bool yearFieldSpecified;

    decimal valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string @class
    {
      get
      {
        return this.classField;
      }
      set
      {
        this.classField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string period
    {
      get
      {
        return this.periodField;
      }
      set
      {
        this.periodField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string unitType
    {
      get
      {
        return this.unitTypeField;
      }
      set
      {
        this.unitTypeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string units
    {
      get
      {
        return this.unitsField;
      }
      set
      {
        this.unitsField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ushort year
    {
      get
      {
        return this.yearField;
      }
      set
      {
        this.yearField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool yearSpecified
    {
      get
      {
        return this.yearFieldSpecified;
      }
      set
      {
        this.yearFieldSpecified = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public decimal Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataAlmanacPrecipitation
  {

    string classField;

    string periodField;

    string unitTypeField;

    string unitsField;

    ushort yearField;

    decimal valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string @class
    {
      get
      {
        return this.classField;
      }
      set
      {
        this.classField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string period
    {
      get
      {
        return this.periodField;
      }
      set
      {
        this.periodField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string unitType
    {
      get
      {
        return this.unitTypeField;
      }
      set
      {
        this.unitTypeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string units
    {
      get
      {
        return this.unitsField;
      }
      set
      {
        this.unitsField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ushort year
    {
      get
      {
        return this.yearField;
      }
      set
      {
        this.yearField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public decimal Value
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
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
  public partial class siteDataAlmanacPop
  {

    string unitsField;

    decimal valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string units
    {
      get
      {
        return this.unitsField;
      }
      set
      {
        this.unitsField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public decimal Value
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
