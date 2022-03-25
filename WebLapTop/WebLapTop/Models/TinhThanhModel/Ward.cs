using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TimekeepingAiCamera.Library.VendorsModel
{
    public class Ward
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("code")]
        public int Code { get; set; } = 0;

        [JsonPropertyName("division_type")]
        public string DivisionType { get; set; } = "";//Kiểu phân chia

        [JsonPropertyName("codename")]
        public string Codename { get; set; } = "";

        [JsonPropertyName("district_code")]
        public int DistrictCode { get; set; } = 0;

    }
}
