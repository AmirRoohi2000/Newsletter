using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace LiveSwitch.TextControl
{
    [RunInstaller(true)]
    public partial class Newsletter_Installer : System.Configuration.Install.Installer
    {
        public Newsletter_Installer()
        {
            InitializeComponent();
        }
    }
}
