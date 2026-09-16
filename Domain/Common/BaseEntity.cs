using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class BaseEntity<DataType>
    {
        public  DataType Id { get; set; }
        public DateTime ListedOn { get; set; }
        public string ListedBy { get; set; }
    }
}
