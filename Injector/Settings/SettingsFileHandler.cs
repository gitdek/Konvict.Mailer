
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Injector.Core.Services.Settings
{
  public class SettingsFileHandler
  {
    #region Protected fields

    protected static IDictionary<Type, XmlSerializer> _xmlSerializers = new Dictionary<Type, XmlSerializer>();
    /// <summary>
    /// XML document to write to.
    /// </summary>
    protected XmlDocument _document;

    /// <summary>
    /// Keeps track of modifications.
    /// </summary>
    protected bool _modified;

    /// <summary>
    /// File path of the physical XML file.
    /// </summary>
    protected string _filePath;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the path of the physical XML file.
    /// </summary>
    public string FilePath
    {
      get { return _filePath; }
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new XML settings file handler instance.
    /// </summary>
    /// <param name="filePath">Path of the file to read from and/or write into.</param>
    public SettingsFileHandler(string filePath)
    {
      _filePath = filePath;
      Clear();
    }

    #endregion

    #region Protected methods

    protected XmlElement GetPropertyElement(string entryName, bool createIfNotExists)
    {
      XmlElement root = _document.DocumentElement;
      if (root == null)
      {
        if (!createIfNotExists)
          return null;
        root = _document.CreateElement("Configuration");
        _document.AppendChild(root);
      }
      XmlElement entryElement = root.SelectSingleNode("Property[@Name=\"" + entryName + "\"]") as XmlElement;
      if (entryElement == null)
      {
        if (!createIfNotExists)
          return null;
        entryElement = _document.CreateElement("Property");
        XmlAttribute attribute = _document.CreateAttribute("Name");
        attribute.Value = entryName;
        entryElement.Attributes.Append(attribute);
        entryElement = root.AppendChild(entryElement) as XmlElement;
      }
      return entryElement;
    }

    protected static void TakeBackup(string filePath)
    {
      if (File.Exists(filePath + ".bak"))
        try
        {
          File.Delete(filePath + ".bak");
        }
        catch (Exception) { }
      if (File.Exists(filePath))
        try
        {
          File.Move(filePath, filePath + ".bak");
        }
        catch (Exception) { }
    }

    protected static XmlSerializer GetSerializer(Type type)
    {
      XmlSerializer result;
      if (_xmlSerializers.ContainsKey(type))
        result = _xmlSerializers[type];
      else
      {
        result = new XmlSerializer(type);
        _xmlSerializers[type] = result;
      }
      return result;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Reads the value of a given entry.
    /// </summary>
    /// <param name="entryName">The entry name.</param>
    /// <param name="type">Type of the value to retrieve.</param>
    /// <returns>Value of the specified entry.</returns>
    public object GetValue(string entryName, Type type)
    {
      XmlNode entryElement = GetPropertyElement(entryName, false);
      if (entryElement == null)
        return null;
      XmlSerializer xs = GetSerializer(type);
      using (StringReader reader = new StringReader(entryElement.InnerXml))
        return xs.Deserialize(reader);
    }

    /// <summary>
    /// Sets the value of a given entry.
    /// </summary>
    /// <param name="entryName">The entry name.</param>
    /// <param name="value">Value to set.</param>
    public void SetValue(string entryName, object value)
    {
      // If the value is null, remove the entry
      if (value == null)
      {
        RemoveEntry(entryName);
        return;
      }
      XmlNode entryElement = GetPropertyElement(entryName, true);
      XmlSerializer xs = GetSerializer(value.GetType());
      StringBuilder sb = new StringBuilder(); // Will contain the data, formatted as XML
      using (XmlWriter writer = new XmlInnerElementWriter(sb))
        xs.Serialize(writer, value);
      entryElement.InnerXml = sb.ToString();
      _modified = true;
    }

    /// <summary>
    /// Removes the value with the specified <paramref name="entryName"/>.
    /// </summary>
    /// <param name="entryName">Name of the entry to remove.</param>
    public void RemoveEntry(string entryName)
    {
      XmlNode entryElement = GetPropertyElement(entryName, false);
      if (entryElement == null)
        return;
      entryElement.ParentNode.RemoveChild(entryElement);
    }

    /// <summary>
    /// Loads the settings file.
    /// </summary>
    public void Load()
    {
      XmlTextReader reader = null;
      if (File.Exists(_filePath))
        // Try to get the file
        reader = new XmlTextReader(_filePath);
      else if (File.Exists(_filePath + ".bak"))
        // Try to get the backup
        reader = new XmlTextReader(_filePath + ".bak");
      if (reader != null)
        using (reader)
          _document.Load(reader);
    }

    public void Clear()
    {
      _document = new XmlDocument();
      _modified = false;
    }

    /// <summary>
    /// Saves any changes.
    /// </summary>
    public void Flush()
    {
      if (!_modified)
        return;
      // Create needed directories if they don't exist
      DirectoryInfo configDir = new DirectoryInfo(Path.GetDirectoryName(_filePath));
      if (!configDir.Exists)
        configDir.Create();
      // Try to take a backup
      TakeBackup(_filePath);
      // Write the file
      using (StreamWriter stream = new StreamWriter(_filePath, false))
      {
        _document.Save(stream);
        stream.Flush();
      }
      _modified = false;
    }

    public void Close()
    {
      Flush();
    }

    #endregion
  }
}
