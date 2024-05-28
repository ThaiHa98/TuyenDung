using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TuyenDung.Data.Model.Enum
{
    public enum StatusToken
    {
        [EnumMember(Value = "Valid")]
        Valid,
        [EnumMember(Value = "Expired")]
        Expired
    }
}
