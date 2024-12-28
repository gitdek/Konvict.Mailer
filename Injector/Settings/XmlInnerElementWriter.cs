
using System.IO;
using System.Text;
using System.Xml;

namespace Injector.Core.Services.Settings
{
  /// <summary>
  /// Xml Writer that doesn't write the XML header.
  /// </summary>
  public class XmlInnerElementWriter : XmlTextWriter
  {
    #region Constructors/Destructors

    public XmlInnerElementWriter(StringBuilder output) : base(new StringWriter(output)) { }

    #endregion

    #region Base overrides

    public override void WriteStartDocument() { }

    public override void WriteStartDocument(bool standalone) { }

    #endregion
  }
}
