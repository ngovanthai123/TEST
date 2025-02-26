﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLapTop.Data;
using WebLapTop.Models;
namespace WebLapTop.Services
{
    public class  ItemProductService:IProduct
    {
        private readonly WebLapTopContext _db;
        private List<Sanpham> products = new List<Sanpham>();
        public ItemProductService (WebLapTopContext db)
        {
            _db = db;
            this.products = _db.Sanphams.ToList();
        }
        public IEnumerable<Sanpham> getProductAll()
        {
            return products;
        }
        public int totalProduct()
        {
            return products.Count + 1;
        }
        public int numberPage(int totalProduct, int limit)
        {
            float numberpage = ((float)totalProduct) / ((float)limit);
            return (int)Math.Ceiling(numberpage);
        }
        public IEnumerable<Sanpham> paginationProduct(int start, int limit)
        {
            var sanpham = (from s in _db.Sanphams select s);
            var dataProduct = sanpham.OrderByDescending(x => x.Id).Skip(start).Take(limit);
            return dataProduct.ToList();
        }
    }

}
