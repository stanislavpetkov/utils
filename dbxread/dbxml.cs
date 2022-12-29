
// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
//[System.SerializableAttribute()]
using System;
using System.Collections.Generic;
/// <remarks/>
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class DataBoxExport
{

    private List<DataBoxExportDataBoxRecord> dataBoxRecordField = new List<DataBoxExportDataBoxRecord>();

    private List<DataBoxExportType> typesField = new List<DataBoxExportType>();

    private List<DataBoxExportSequence> sequencesField = new List<DataBoxExportSequence>();


    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("DataBoxRecord")]
    public List<DataBoxExportDataBoxRecord> DataBoxRecord
    {
        get
        {
            return this.dataBoxRecordField;
        }
        set
        {
            this.dataBoxRecordField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("type", IsNullable = false)]
    public List<DataBoxExportType> Types
    {
        get
        {
            return this.typesField;
        }
        set
        {
            this.typesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("sequence", IsNullable = false)]
    public  List<DataBoxExportSequence> Sequences
    {
        get
        {
            return this.sequencesField;
        }
        set
        {
            this.sequencesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataBoxExportDataBoxRecord
{

    private List<DataBoxExportDataBoxRecordGenre> genresField = new List<DataBoxExportDataBoxRecordGenre>();

    private List<DataBoxExportDataBoxRecordKeyword> keywordsField = new List<DataBoxExportDataBoxRecordKeyword>();

    private DataBoxExportDataBoxRecordCountries countriesField;

    private DataBoxExportDataBoxRecordCompanies companiesField;

    private DataBoxExportDataBoxRecordPersons personsField;

    private List<DataBoxExportDataBoxRecordCustomProperty> customPropertiesField = new List<DataBoxExportDataBoxRecordCustomProperty>();

    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public List<DataBoxExportDataBoxRecordInstance> instancesField = new List<DataBoxExportDataBoxRecordInstance>();

    private string clipidField;

    private string titleField;

    private byte statusField;

    private string sequenceField;

    private string typeField;

    private string categoryField;

    private string languageField;

    private uint durationField;

    private bool durationFieldSpecified;

    private string noteField;

    private string tagField;

    private decimal receiptdateField;

    private string receiptdate_2Field;

    private decimal validfromField;

    private bool validfromFieldSpecified;

    private string validfrom_2Field;

    private decimal validtoField;

    private bool validtoFieldSpecified;

    private string validto_2Field;

    private string triviaField;

    private decimal proddateField;

    private bool proddateFieldSpecified;

    private string proddate_2Field;

    private byte priorityField;

    private bool priorityFieldSpecified;

    private string groupField;

    private string plotoutlineField;

    private string taglinesField;

    private string commentsField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("genre", IsNullable = false)]
    public List<DataBoxExportDataBoxRecordGenre> Genres
    {
        get
        {
            return this.genresField;
        }
        set
        {
            this.genresField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("keyword", IsNullable = false)]
    public List<DataBoxExportDataBoxRecordKeyword> Keywords
    {
        get
        {
            return this.keywordsField;
        }
        set
        {
            this.keywordsField = value;
        }
    }

    /// <remarks/>
    public DataBoxExportDataBoxRecordCountries Countries
    {
        get
        {
            return this.countriesField;
        }
        set
        {
            this.countriesField = value;
        }
    }

    /// <remarks/>
    public DataBoxExportDataBoxRecordCompanies Companies
    {
        get
        {
            return this.companiesField;
        }
        set
        {
            this.companiesField = value;
        }
    }

    /// <remarks/>
    public DataBoxExportDataBoxRecordPersons Persons
    {
        get
        {
            return this.personsField;
        }
        set
        {
            this.personsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("CustomProperty", IsNullable = false)]
    public List<DataBoxExportDataBoxRecordCustomProperty> CustomProperties
    {
        get
        {
            return this.customPropertiesField;
        }
        set
        {
            this.customPropertiesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("instance", IsNullable = false)]
    public List<DataBoxExportDataBoxRecordInstance> Instances
    {
        get
        {
            return this.instancesField;
        }
        set
        {
            this.instancesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string clipid
    {
        get
        {
            return this.clipidField;
        }
        set
        {
            this.clipidField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
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
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte status
    {
        get
        {
            return this.statusField;
        }
        set
        {
            this.statusField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string sequence
    {
        get
        {
            return this.sequenceField;
        }
        set
        {
            this.sequenceField = value;
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

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string language
    {
        get
        {
            return this.languageField;
        }
        set
        {
            this.languageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public uint duration
    {
        get
        {
            return this.durationField;
        }
        set
        {
            this.durationField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool durationSpecified
    {
        get
        {
            return this.durationFieldSpecified;
        }
        set
        {
            this.durationFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string note
    {
        get
        {
            return this.noteField;
        }
        set
        {
            this.noteField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string tag
    {
        get
        {
            return this.tagField;
        }
        set
        {
            this.tagField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal receiptdate
    {
        get
        {
            return this.receiptdateField;
        }
        set
        {
            this.receiptdateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string receiptdate_2
    {
        get
        {
            return this.receiptdate_2Field;
        }
        set
        {
            this.receiptdate_2Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal validfrom
    {
        get
        {
            return this.validfromField;
        }
        set
        {
            this.validfromField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool validfromSpecified
    {
        get
        {
            return this.validfromFieldSpecified;
        }
        set
        {
            this.validfromFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string validfrom_2
    {
        get
        {
            return this.validfrom_2Field;
        }
        set
        {
            this.validfrom_2Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal validto
    {
        get
        {
            return this.validtoField;
        }
        set
        {
            this.validtoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool validtoSpecified
    {
        get
        {
            return this.validtoFieldSpecified;
        }
        set
        {
            this.validtoFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string validto_2
    {
        get
        {
            return this.validto_2Field;
        }
        set
        {
            this.validto_2Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string trivia
    {
        get
        {
            return this.triviaField;
        }
        set
        {
            this.triviaField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal proddate
    {
        get
        {
            return this.proddateField;
        }
        set
        {
            this.proddateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool proddateSpecified
    {
        get
        {
            return this.proddateFieldSpecified;
        }
        set
        {
            this.proddateFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string proddate_2
    {
        get
        {
            return this.proddate_2Field;
        }
        set
        {
            this.proddate_2Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte priority
    {
        get
        {
            return this.priorityField;
        }
        set
        {
            this.priorityField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool prioritySpecified
    {
        get
        {
            return this.priorityFieldSpecified;
        }
        set
        {
            this.priorityFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string group
    {
        get
        {
            return this.groupField;
        }
        set
        {
            this.groupField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string plotoutline
    {
        get
        {
            return this.plotoutlineField;
        }
        set
        {
            this.plotoutlineField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string taglines
    {
        get
        {
            return this.taglinesField;
        }
        set
        {
            this.taglinesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string comments
    {
        get
        {
            return this.commentsField;
        }
        set
        {
            this.commentsField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataBoxExportDataBoxRecordGenre
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataBoxExportDataBoxRecordKeyword
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataBoxExportDataBoxRecordCountries
{

    private DataBoxExportDataBoxRecordCountriesCountry countryField;

    /// <remarks/>
    public DataBoxExportDataBoxRecordCountriesCountry country
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
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataBoxExportDataBoxRecordCountriesCountry
{

    private string nameField;

    private string posField;

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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataBoxExportDataBoxRecordCompanies
{

    private DataBoxExportDataBoxRecordCompaniesCompany companyField;

    /// <remarks/>
    public DataBoxExportDataBoxRecordCompaniesCompany company
    {
        get
        {
            return this.companyField;
        }
        set
        {
            this.companyField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataBoxExportDataBoxRecordCompaniesCompany
{

    private string nameField;

    private string posField;

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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataBoxExportDataBoxRecordPersons
{

    private DataBoxExportDataBoxRecordPersonsPerson personField;

    /// <remarks/>
    public DataBoxExportDataBoxRecordPersonsPerson person
    {
        get
        {
            return this.personField;
        }
        set
        {
            this.personField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataBoxExportDataBoxRecordPersonsPerson
{

    private string nameField;

    private string posField;

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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataBoxExportDataBoxRecordCustomProperty
{

    private string nameField;

    private string valueField;

    private uint typeField=0;

    [System.Xml.Serialization.XmlAttributeAttribute()]
    public uint type
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
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataBoxExportDataBoxRecordInstance
{

    private DataBoxExportDataBoxRecordInstanceStream streamField;

    private uint durationField;

    private bool durationFieldSpecified;

    private string nameField;

    private byte mainField;

    private string qualityField;

    /// <remarks/>
    public DataBoxExportDataBoxRecordInstanceStream stream
    {
        get
        {
            return this.streamField;
        }
        set
        {
            this.streamField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public uint duration
    {
        get
        {
            return this.durationField;
        }
        set
        {
            this.durationField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool durationSpecified
    {
        get
        {
            return this.durationFieldSpecified;
        }
        set
        {
            this.durationFieldSpecified = value;
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
    public byte main
    {
        get
        {
            return this.mainField;
        }
        set
        {
            this.mainField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string quality
    {
        get
        {
            return this.qualityField;
        }
        set
        {
            this.qualityField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataBoxExportDataBoxRecordInstanceStream
{

    private DataBoxExportDataBoxRecordInstanceStreamMedia mediaField;

    private string streamNameField;

    private uint oUT_PField;

    private bool oUT_PFieldSpecified;

    private uint width;

    private uint height;
    

    private string fileNameField;

    private byte mainField;

    private string languageIDField;

    private long fileSizeField;

    private bool fileSizeFieldSpecified;

    private byte statusField;

    private bool statusFieldSpecified;

    private uint videoBitrate = 0;

    private uint sampleRate = 48000;

    private uint audioBitRate = 256;

    private uint channels = 2;

    private uint frameRate = 25;

    private string vct = "UNK";


    [System.Xml.Serialization.XmlAttribute("VCT")]
    public string VCT
    {
        get
        {
            return this.vct;
        }
        set
        {
            this.vct = value;
        }
    }


    private string act = "UNK";


    [System.Xml.Serialization.XmlAttribute("ACT")]
    public string ACT
    {
        get
        {
            return this.act;
        }
        set
        {
            this.act = value;
        }
    }

    [System.Xml.Serialization.XmlAttribute("FrameRate")]
    public uint FrameRate
    {
        get
        {
            return this.frameRate;
        }
        set
        {
            this.frameRate = value;
        }
    }

    [System.Xml.Serialization.XmlAttribute("Channels")]
    public uint Channels
    {
        get
        {
            return this.channels;
        }
        set
        {
            this.channels = value;
        }
    }

    [System.Xml.Serialization.XmlAttribute("AudioBitRate")]
    public uint AudioBitRate
    {
        get
        {
            return this.audioBitRate;
        }
        set
        {
            this.audioBitRate = value;
        }
    }

    [System.Xml.Serialization.XmlAttribute("SampleRate")]
    public uint SampleRate
    {
        get
        {
            return this.sampleRate;
        }
        set
        {
            this.sampleRate = value;
        }
    }

    [System.Xml.Serialization.XmlAttribute("VideoBitRate")]
    public uint VideoBitrate
    {
        get
        {
            return this.videoBitrate;
        }
        set
        {
            this.videoBitrate = value;
        }
    }

    [System.Xml.Serialization.XmlAttribute("Width")]
    public uint Width
    {
        get
        {
            return this.width;
        }
        set
        {
            this.width = value;
        }
    }


    [System.Xml.Serialization.XmlAttribute("Height")]
    public uint Height
    {
        get
        {
            return this.height;
        }
        set
        {
            this.height = value;
        }
    }
    /// <remarks/>
    public DataBoxExportDataBoxRecordInstanceStreamMedia media
    {
        get
        {
            return this.mediaField;
        }
        set
        {
            this.mediaField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string StreamName
    {
        get
        {
            return this.streamNameField;
        }
        set
        {
            this.streamNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public uint OUT_P
    {
        get
        {
            return this.oUT_PField;
        }
        set
        {
            this.oUT_PField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool OUT_PSpecified
    {
        get
        {
            return this.oUT_PFieldSpecified;
        }
        set
        {
            this.oUT_PFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string FileName
    {
        get
        {
            return this.fileNameField;
        }
        set
        {
            this.fileNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte main
    {
        get
        {
            return this.mainField;
        }
        set
        {
            this.mainField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string LanguageID
    {
        get
        {
            return this.languageIDField;
        }
        set
        {
            this.languageIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public long FileSize
    {
        get
        {
            return this.fileSizeField;
        }
        set
        {
            this.fileSizeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool FileSizeSpecified
    {
        get
        {
            return this.fileSizeFieldSpecified;
        }
        set
        {
            this.fileSizeFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte Status
    {
        get
        {
            return this.statusField;
        }
        set
        {
            this.statusField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool StatusSpecified
    {
        get
        {
            return this.statusFieldSpecified;
        }
        set
        {
            this.statusFieldSpecified = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataBoxExportDataBoxRecordInstanceStreamMedia
{

    private string mediaNameField;

    private string poolField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string MediaName
    {
        get
        {
            return this.mediaNameField;
        }
        set
        {
            this.mediaNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Pool
    {
        get
        {
            return this.poolField;
        }
        set
        {
            this.poolField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataBoxExportType
{

    private List<DataBoxExportTypeCategory> categoryField = new List<DataBoxExportTypeCategory>();

    private List<DataBoxExportTypeGenre> genreField = new List<DataBoxExportTypeGenre>();

    private string nameField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("category")]
    public List<DataBoxExportTypeCategory> category
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
    [System.Xml.Serialization.XmlElementAttribute("genre")]
    public List<DataBoxExportTypeGenre> genre
    {
        get
        {
            return this.genreField;
        }
        set
        {
            this.genreField = value;
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
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataBoxExportTypeCategory
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataBoxExportTypeGenre
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DataBoxExportSequence
{

    private string nameField;

    private byte episodeCountField;

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
    public byte EpisodeCount
    {
        get
        {
            return this.episodeCountField;
        }
        set
        {
            this.episodeCountField = value;
        }
    }
}

