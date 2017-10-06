using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wakool.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class VisibleAttribute : Attribute
    {
        public bool visivel { get; set; }
        public VisibleAttribute(bool visible = true)
        {
            visivel = visible;
        }
    }
}
