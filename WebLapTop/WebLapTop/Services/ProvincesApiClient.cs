using TimekeepingAiCamera.Library.VendorsModel;
using TimekeepingAiCamera.Client.Responsitory.ProvincesApi;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;

namespace TimekeepingAiCamera.Client.Responsitory.ProvincesApi
{
    public class ProvincesApiClient : IProvincesApiClient
    {

        public async Task<List<Province>> GetProvincecs()
        {
            var http = new HttpClient();
            var response = await http.GetAsync("https://provinces.open-api.vn/api/");
            if (response.IsSuccessStatusCode) {
                return await response.Content.ReadFromJsonAsync<List<Province>>();
            }
            return new();
        }

 
        public async Task<List<District>> GetDistricts(int provinceCode)
        {
            var http = new HttpClient();
            var response = await http.GetAsync($"https://provinces.open-api.vn/api/p/{provinceCode}?depth=2");
            if (response.IsSuccessStatusCode) {
                var province = await response.Content.ReadFromJsonAsync<Province>();
                return province.Districts;
            }
            return new ();
        }



        public async Task<List<Ward>> GetWards(int districtCode)
        {
            var http = new HttpClient();
            var response = await http.GetAsync($"https://provinces.open-api.vn/api/d/{districtCode}?depth=2");
            if (response.IsSuccessStatusCode) {
                var district = await response.Content.ReadFromJsonAsync<District>();
                return district.Wards;
            }
            return new();
        }
    }
}
