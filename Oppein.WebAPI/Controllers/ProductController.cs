using Oppein.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Oppein.WebAPI.Controllers
{
    //[RoutePrefix("apiD/productsT")]
    public class ProductController : ApiController
    {

        ////我们的 Product API简单的设计为下面格式:
        ////添加获取产品分页API: api/products/product/getList
        ////添加获取单个产品API: api/products/product/get? productId = 产品ID
        ////添加产品新增API: api/products/product/add? productId = 产品ID
        ////添加产品更新API: api/products/product/update? productId = 产品ID
        ////添加产品删除API: api/products/product/delete? productId = 产品ID



        /// <summary>
        /// 产品分页数据获取
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("product/getList")]
        //public Page<Product> GetProductList()
        public List<Product> GetProductList()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 获取单个产品
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet, Route("product/get")]
        public Product GetProduct(Guid productId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost, Route("product/add")]
        public Guid AddProduct(Product product)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="product"></param>
        [HttpPost, Route("product/update")]
        public void UpdateProduct(Guid productId, Product product)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="productId"></param>
        [HttpDelete, Route("product/delete")]
        public void DeleteProduct(Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}
