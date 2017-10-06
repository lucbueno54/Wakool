using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class EntityBase
    {
        public int ID { get; set; }
    }
}

/* Atributos mapeados:
 * 
 * DisplayName;
 * Required;
 * StringLength; 
 * RegularExpression;
 * 
 * 
 * Tipos de dados mapeados:
 * 
 * string == varchar(*)
 * int == int
 * bool == bit
 * double == float
 * datetime == datetime
 * decimal == decimal (18,2)
 * 
 */
