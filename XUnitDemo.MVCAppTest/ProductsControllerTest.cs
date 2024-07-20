using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using XUnitDemo.MVCApp.Controllers;
using XUnitDemo.MVCApp.Models;
using XUnitDemo.MVCApp.Repository;

namespace XUnitDemo.MVCAppTest
{
    public class ProductsControllerTest
    {
        private Mock<IRepository<Product>> _mockProductRepo;
        private ProductsController _productsController;
        private List<Product> _products;

        public ProductsControllerTest()
        {
            _mockProductRepo = new Mock<IRepository<Product>>();
            _productsController = new ProductsController(_mockProductRepo.Object);

            _products = new List<Product>()
            {
                new Product { Id = 1, Color = "Red", Name = "Pencil", Price = 5, Stock = 50},
                new Product { Id = 2, Color = "Blue", Name = "Notebook", Price = 10, Stock = 100}
            };
        }

        [Fact]
        public async void Index_ActionExecutes_ReturnView()
        {
            //Arrange

            //Act
            var view = await _productsController.Index();
            
            //Assert
            Assert.IsType<ViewResult>(view);
        }

        [Fact]
        public async void Index_ActionExecutes_RepositoryGetAllCalled()
        {
            //Arrange
            _mockProductRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(_products);

            //Act
            var view = await _productsController.Index();
            var viewResult = Assert.IsType<ViewResult>(view);
            var productList = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.Model);

            //Assert
            Assert.Equal(2,productList.Count());
        }

        [Fact]
        public async void Details_IdIsNull_RedirectToIndexAction()
        {
            //Arrange

            //Act
            var result = await _productsController.Details(null);
            var redirect = Assert.IsType<RedirectToActionResult>(result);

            //Assert
            //Assert.IsType<NotFoundResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public async void Details_ProductNotFound_ReturnNotFound()
        {
            //Arrange
            _mockProductRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Product)null);

            //Act
            var result = await _productsController.Details(It.IsAny<int>());
            var redirect = Assert.IsType<NotFoundResult>(result);

            //Assert
            Assert.Equal<int>((int)HttpStatusCode.NotFound, redirect.StatusCode);
        }

        [Fact]
        public async void Details_ProductFound_ReturnViewAndModel()
        {
            //Arrange
            Product product = _products.FirstOrDefault();
            _mockProductRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);

            //Act
            var result = await _productsController.Details(It.IsAny<int>());
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<Product>(viewResult.Model);

            //Assert
            Assert.Equal(product.Id, viewModel.Id);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CreateGET_ReturnView()
        {
            //Arrange

            //Act
            var result = _productsController.Create();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void CreatePOST_WhenModelStateIsNotValid_ReturnViewAndCreateMethodIsNotExecute()
        {
            //Arrange
            _productsController.ModelState.AddModelError("Name", "Name field is required.");
            
            //Act
            var result = await _productsController.Create(_products.First());
            var viewResult = Assert.IsType<ViewResult>(result);

            //Assert
            Assert.IsType<Product>(viewResult.Model);
            _mockProductRepo.Verify(x => x.CreateAsync(It.IsAny<Product>()), Times.Never());
        }

        [Fact]
        public async void CreatePOST_WhenModelStateIsValid_CreateMethodExecuteAndRedirectToIndexAction()
        {
            //Arrange
            Product product = null;
            _mockProductRepo.Setup(x => x.CreateAsync(It.IsAny<Product>())).Callback<Product>(x => product = x);

            //Act
            var result = await _productsController.Create(_products.First());
            var redirect = Assert.IsType<RedirectToActionResult>(result);

            //Assert
            Assert.Equal("Index", redirect.ActionName);
            _mockProductRepo.Verify(x => x.CreateAsync(It.IsAny<Product>()), Times.Once());
            Assert.Equal(_products.First().Id, product.Id);
        }

        [Fact]
        public async void EditGET_WhenIdIsNull_ReturnNotFound()
        {
            //Arrange
            _mockProductRepo.Setup(x => x.Update(null));

            //Act
            var result = await _productsController.Edit(null);
            var redirect = Assert.IsType<NotFoundResult>(result);

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, redirect.StatusCode);
        }

        [Fact]
        public async void EditGET_WhenIdIsNotNullProductNotFound_RedirectToIndex()
        {
            //Arrange
            Product product = null;
            _mockProductRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);

            //Act
            var result = await _productsController.Edit(It.IsAny<int>());
            var redirect = Assert.IsType<RedirectToActionResult>(result);

            //Assert
            Assert.Equal("Index", redirect.ActionName);
            _mockProductRepo.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async void EditGET_WhenIdIsNotNullAndProductFound_ReturnViewAndModel()
        {
            //Arrange
            _mockProductRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_products.First());

            //Act
            var result = await _productsController.Edit(It.IsAny<int>());
            var resultView = Assert.IsType<ViewResult>(result);
            var resultViewModel = Assert.IsAssignableFrom<Product>(resultView.Model);

            //Assert
            Assert.Equal(_products.First().Id, resultViewModel.Id);
            _mockProductRepo.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async void EditPOST_WhenIdIsNull_ReturnNotFound()
        {
            //Arrange
            
            //Act
            var result = await _productsController.Edit(null);
            var viewResult = Assert.IsType<NotFoundResult>(result);

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, viewResult.StatusCode);
        }

        [Fact]
        public async void EditPOST_WhenIdIsNotNullAndModelStateIsNotValid_ReturnView()
        {
            //Arrange
            _productsController.ModelState.AddModelError("Name", "Name field is required");

            //Act
            var result = await _productsController.Edit(It.IsAny<int>(),_products.First());
            var resultView = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<Product>(resultView.Model);

            //Assert
            Assert.Equal(_products.First().Id, viewModel.Id);
        }

        [Fact]
        public async void EditPOST_WhenIdIsNotNullAndModelStateValid_RedirectToIndex()
        {
            //Arrange
            _mockProductRepo.Setup(x => x.Update(It.IsAny<Product>())).Callback<Product>(x => x = _products.First());

            //Act
            var result = await _productsController.Edit(It.IsAny<int>(),It.IsAny<Product>());
            var viewResult = Assert.IsType<RedirectToActionResult>(result);

            //Assert
            Assert.Equal("Index", viewResult.ActionName);
            _mockProductRepo.Verify(x => x.Update(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async void EditPOST_WhenIdIsNotNullAndModelStateValid_ThrowException()
        {
            //Arrange
            var exception = new DbUpdateConcurrencyException();
            _mockProductRepo.Setup(x => x.Update(It.IsAny<Product>())).Throws(exception);
            _mockProductRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_products.First());

            //Act
            var ex = await Assert.ThrowsAsync<DbUpdateConcurrencyException>(() => _productsController.Edit(_products.First().Id, _products.First()));

            //Assert
            _mockProductRepo.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            _mockProductRepo.Verify(x => x.Update(It.IsAny<Product>()), Times.Once);
            Assert.Equal(exception, ex);

        }
    }
}
