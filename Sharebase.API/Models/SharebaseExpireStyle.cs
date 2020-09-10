using Sharebase.API.Helpers;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Sharebase.API.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SharebaseExpireStyle
    {
        [EnumMember(Value = "days")]
        days,
        [EnumMember(Value = "hours")]
        hours,
        [EnumMember(Value = "minutes")]
        minutes,
        [EnumMember(Value = "date")]
        date,
        [EnumMember(Value = "never")]
        never
    }
}