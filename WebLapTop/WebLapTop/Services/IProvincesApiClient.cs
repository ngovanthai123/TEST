using System.Collections.Generic;
using System.Threading.Tasks;
using TimekeepingAiCamera.Library.VendorsModel;

namespace TimekeepingAiCamera.Client.Responsitory.ProvincesApi
{
    public interface IProvincesApiClient
    {
        /// <summary>
        /// Lấy danh sách các tỉnh thành
        /// </summary>
        /// <returns></returns>
        public Task<List<Province>> GetProvincecs();
        /// <summary>
        /// Lấy danh sách quận huyện
        /// </summary>
        /// <param name="provinceCode">Mã tình thành</param>
        /// <returns></returns>
        public Task<List<District>> GetDistricts(int provinceCode);
        /// <summary>
        /// Lấy danh sách phường xã
        /// </summary>
        /// <param name="districtCode">Mã quận huyện</param>
        /// <returns></returns>
        public Task<List<Ward>> GetWards(int districtCode);
    }
}
