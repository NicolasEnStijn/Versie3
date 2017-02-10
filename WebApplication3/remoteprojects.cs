using System;
using System.Collections.Generic;
using System.Web;

namespace WebApplication3
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class remoteprojects
    {

        private remoteprojectsProject[] projectsField;

        private remoteprojectsRegistryreferences registryreferencesField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("project", IsNullable = false)]
        public remoteprojectsProject[] projects
        {
            get
            {
                return this.projectsField;
            }
            set
            {
                this.projectsField = value;
            }
        }

        /// <remarks/>
        public remoteprojectsRegistryreferences registryreferences
        {
            get
            {
                return this.registryreferencesField;
            }
            set
            {
                this.registryreferencesField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class remoteprojectsProject
    {
        public enum bereikbaarheid
        {
            geeninfo,
            berijkbaar,
            nietberijkbaar
        }

        private string nameField;

        private string streetField;

        private string municipalityField;

        private string numberField;

        private string ipaddressField;

        private string hostnameField;

        private string serialportnumberField;

        private ushort tcpportField;

        private string connectionmodeField;

        private byte typeofsystemField;

        private string programtolaunchField;

        private string notitionsField;

        private byte registryreferenceField;

        private byte idField;

        private bereikbaarheid bereikbaarField;

        public bereikbaarheid berijkbaar
        {
            get
            {
                return this.bereikbaarField;
            }
            set
            {
                this.bereikbaarField = value;
            }
        }

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
        public string street
        {
            get
            {
                return this.streetField;
            }
            set
            {
                this.streetField = value;
            }
        }

        /// <remarks/>
        public string municipality
        {
            get
            {
                return this.municipalityField;
            }
            set
            {
                this.municipalityField = value;
            }
        }

        /// <remarks/>
        public string number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }

        /// <remarks/>
        public string ipaddress
        {
            get
            {
                return this.ipaddressField;
            }
            set
            {
                this.ipaddressField = value;
            }
        }

        /// <remarks/>
        public string hostname
        {
            get
            {
                return this.hostnameField;
            }
            set
            {
                this.hostnameField = value;
            }
        }

        /// <remarks/>
        public string serialportnumber
        {
            get
            {
                return this.serialportnumberField;
            }
            set
            {
                this.serialportnumberField = value;
            }
        }

        /// <remarks/>
        public ushort tcpport
        {
            get
            {
                return this.tcpportField;
            }
            set
            {
                this.tcpportField = value;
            }
        }

        /// <remarks/>
        public string connectionmode
        {
            get
            {
                return this.connectionmodeField;
            }
            set
            {
                this.connectionmodeField = value;
            }
        }

        /// <remarks/>
        public byte typeofsystem
        {
            get
            {
                return this.typeofsystemField;
            }
            set
            {
                this.typeofsystemField = value;
            }
        }

        /// <remarks/>
        public string programtolaunch
        {
            get
            {
                return this.programtolaunchField;
            }
            set
            {
                this.programtolaunchField = value;
            }
        }

        /// <remarks/>
        public string notitions
        {
            get
            {
                return this.notitionsField;
            }
            set
            {
                this.notitionsField = value;
            }
        }

        /// <remarks/>
        public byte registryreference
        {
            get
            {
                return this.registryreferenceField;
            }
            set
            {
                this.registryreferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte ID
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
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class remoteprojectsRegistryreferences
    {

        private remoteprojectsRegistryreferencesRegistryreference registryreferenceField;

        /// <remarks/>
        public remoteprojectsRegistryreferencesRegistryreference registryreference
        {
            get
            {
                return this.registryreferenceField;
            }
            set
            {
                this.registryreferenceField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class remoteprojectsRegistryreferencesRegistryreference
    {

        private string locationField;

        private byte idField;

        /// <remarks/>
        public string location
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte ID
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
}