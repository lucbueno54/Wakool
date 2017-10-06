using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wakool.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableName : Attribute
    {
        public TableName(string text)
        {
            Text = text;
        }
        public string Text { private set; get; }
    }
}
