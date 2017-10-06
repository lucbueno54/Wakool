using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomDataAnnotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MaskedTextAttribute : Attribute
    {
        public string Mask { get; set; }
        public MaskedTextAttribute(string text)
        {
            this.Mask = text;
        }
        public MaskedTextAttribute(Mask text)
        {
            string mask = string.Empty;
            Type type = text.GetType();
            MemberInfo[] memberInfo = type.GetMember(text.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    mask = ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            this.Mask = mask;
        }

        public bool isChecked(string text)
        {
            text = new String(text.Where(c => char.IsDigit(c)).ToArray());
            text = new String(text.Where(c => char.IsNumber(c)).ToArray());
            return true;
        }
    }

    public enum Mask
    {
        [Description("999,999,999-99")]
        CPF,
        [Description("99,999,999/9999-99")]
        CNPJ,
        [Description("999,99999,99-9")]
        PIS,
        [Description("(99) 99999-9999")]
        TELEFONE
    }
}
