using System.Xml.Serialization;

public class Dialogue_Message_XML {

	[XmlAttribute("id")]
	public string id;

	[XmlElement("text")]
	public string text;
}
