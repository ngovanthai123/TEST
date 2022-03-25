using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TimekeepingAiCamera.Library.VendorsModel
{
    //Tỉnh thành
    public class Province
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("code")]
        public int Code { get; set; } = 0;

        [JsonPropertyName("division_type")]
        public string DivisionType { get; set; } = ""; //Kiểu phân chia

        [JsonPropertyName("codename")]
        public string Codename { get; set; } = "";

        [JsonPropertyName("phone_code")]
        public int Phone_code { get; set; } = 0;

        [JsonPropertyName("districts")]
        public List<District> Districts { get; set; } = new();
    }
}
