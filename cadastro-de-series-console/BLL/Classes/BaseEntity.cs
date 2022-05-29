using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Classes
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
    }
}
