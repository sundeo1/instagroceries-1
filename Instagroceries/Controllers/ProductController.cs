using Instagroceries.Data.Interfaces;
using Instagroceries.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagroceries.Controllers
{
    public class ProductController:Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        
        public ProductController(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        //Craete an action method that returns a list of products
        public ViewResult List() 
        {
            ViewBag.Name = "Sundeo, is that you?";
           // var products = _productRepository.Products;
            ProductListViewModel vm = new ProductListViewModel();
            vm.Products = _productRepository.Products;
            vm.CurrentCategory = "Product Category";
            return View(vm);
           // return View(products);
        }
    }
}

