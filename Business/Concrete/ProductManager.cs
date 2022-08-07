using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult Add(Product product)
        {
            ValidationTool.Validate(new ProductValidator(), product);

            if (CheckProductName(product.ProductName).Success)
            {
                _productDal.Add(product);
                return new SuccessResult(Messages.ProductAdded);
            }
            return new ErrorResult();
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
           return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id), "Kategoriye ürünler listelendi.");
        }

        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.Id == productId), "Ürün listelendi.");
        }
        public IDataResult<List<ProductDetailDto>> GetProductDetailDto(int productId)
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetail(productId), "Ürün detayı listelendi.");
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        private IResult CheckProductName(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName.Equals(productName)).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExist);
            }
            return new SuccessResult();
        }
    }
}
