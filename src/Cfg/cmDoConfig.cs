using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace s7.cmDo.Cfg
{
    [Serializable]
    public class cmDoConfig
    {
        private const string m_CfgFile = "cmDoCfg.xml";
        public static string ConfigPath { get { return Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), m_CfgFile); } }
        public static bool Exists { get { return File.Exists(ConfigPath); } }
        public static cmDoConfig CurrentConfig { get { return m_CurrentConfig; } }

        private static cmDoConfig m_CurrentConfig = null;

        public string UserID { get; set; }
        public string ProtectedPassword { get; set; }
        public DefaultTask DefaultTask { get; set; }

        public void Save()
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(this.GetType());
            StreamWriter writer = File.CreateText(ConfigPath);
            xs.Serialize(writer, this);
            writer.Flush();
            writer.Close();
        }

        public static cmDoConfig Load() { return Load(true); }

        public static cmDoConfig Load(bool force)
        {
            if (Exists)
            {
                if (!force && m_CurrentConfig != null)
                    return m_CurrentConfig;
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(cmDoConfig));
                StreamReader reader = File.OpenText(ConfigPath);
                cmDoConfig c = xs.Deserialize(reader) as cmDoConfig;
                reader.Close();
                m_CurrentConfig = c;
                return m_CurrentConfig == null ? new cmDoConfig() : m_CurrentConfig;
            }
            else
            {
                return new cmDoConfig();
            }
        }
    }
}
