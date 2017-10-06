using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomDataAnnotations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class IconAttribute : Attribute
    {
        public string Icon { get; set; }
        public IconAttribute(string icone)
        {
            Icon = "mdi " + icone;
        }
    }
}
