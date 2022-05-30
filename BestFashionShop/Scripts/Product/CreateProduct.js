var vmCreateProduct = new Vue({
    el: '#AddOrUpdateProduct',
    data: {
        ProductId: 0,
        ProductName: "",
        CollectionId: 0,
        CategoryId: 0,
        ProductCode: "",
        ProductPrice: 0,
        ProductQuantity: 0,
        ProductDescription: "",
        ProductDetail: "",
        PromotionPercent: "",
        IsNewProduct: false,
        IsHotProduct: false,
        lstUrlImage: [
            {
                Id: 0,
                Url: "",
                DefaultImage: false
            }
        ],
        lstProductCategory: null,
        lstCollection: null,
        DetailProduct: {},
        TitlePage: "Thêm mới sản phẩm",
    },
    methods: {
        AddUrlImage: function () {
            var self = this;
            self.lstUrlImage.push({ Url: '', DefaultImage: false });
        },
        DeleteUrlImage: function (index) {
            var self = this;
            if (self.lstUrlImage.length === 1) return false;
            self.lstUrlImage.splice(index, 1);
        },
        getProductCategory: function () {
            var self = this;
            $.ajax({
                url: '/api/ProductCategoryApi/GetAllProductCategory',
                type: 'GET',
                dataType: 'json',
                contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            }).then(res => {
                if (res != null) {
                    self.lstProductCategory = null;
                    self.lstProductCategory = JSON.parse(res);
                }
            })
        },
        getCollection: function () {
            var self = this;
            $.ajax({
                url: '/api/MyCollectionApi/GetAllMyCollection',
                type: 'GET',
                dataType: 'json',
                contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            }).then(res => {
                if (res != null) {
                    self.lstCollection = null;
                    self.lstCollection = JSON.parse(res);
                }
            })
        },
        getDetailProduct: function () {
            var self = this;
            if (!location.href.includes('=')) {
                return false;
            }
            self.ProductId = location.href.split('=')[1];
            if (!self.ProductId) {
                return false;
            }
            $.ajax({
                url: '/api/ProductApi/GetProductById?Id=' + self.ProductId,
                type: 'GET',
                dataType: 'json',
                contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            }).then(res => {
                if (res != null) {
                    self.DetailProduct = {};
                    self.DetailProduct = JSON.parse(res);
                    self.ProductName = self.DetailProduct.Name;
                    self.CollectionId = self.DetailProduct.CollectionId;
                    self.CategoryId = self.DetailProduct.CategoryId;
                    self.ProductCode = self.DetailProduct.Code;
                    self.ProductPrice = self.DetailProduct.Price;
                    self.ProductQuantity = self.DetailProduct.Quantity;
                    self.ProductDescription = self.DetailProduct.Description;
                    self.ProductDetail = self.DetailProduct.Detail;
                    self.PromotionPercent = self.DetailProduct.PromotionPercent;
                    self.IsNewProduct = self.DetailProduct.NewProduct;
                    self.IsHotProduct = self.DetailProduct.TopHot;
                    self.lstUrlImage = self.DetailProduct.ProductImages;
                }
            })
        },
        Submit: function (type) {
            var self = this;
            var dataProduct = {};
            dataProduct.Id = self.ProductId;
            dataProduct.Name = self.ProductName;
            dataProduct.Code = self.ProductCode;
            dataProduct.Price = self.ProductPrice;
            dataProduct.Quantity = self.ProductQuantity;
            dataProduct.Description = self.ProductDescription;
            dataProduct.Detail = self.ProductDetail;
            dataProduct.PromotionPercent = self.PromotionPercent;
            dataProduct.NewProduct = self.IsNewProduct;
            dataProduct.TopHot = self.IsHotProduct;
            dataProduct.ProductImages = self.lstUrlImage;
            dataProduct.CategoryId = self.CategoryId;
            dataProduct.CollectionId = self.CollectionId;

            if (type === 0) {
                $.ajax({
                    url: '/api/ProductApi/AddProduct',
                    data: dataProduct,
                    type: 'POST',
                    dataType: 'json',
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                }).then(res => {
                    if (res !== 0) {
                        alert('Thêm thành công');
                    }
                    else {
                        alert('Thêm thất bại');
                    }
                });
            }
            else if (type === 1) {
                $.ajax({
                    url: '/api/ProductApi/UpdateProduct',
                    data: dataProduct,
                    type: 'PUT',
                    dataType: 'json',
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                }).then(res => {
                    if (res !== 0) {
                        alert('Sửa thành công');
                    }
                    else {
                        alert('Sửa thất bại');
                    }
                });
            }
            else {
                var conf = confirm('Bạn chắc chắn muốn xóa sản phẩm này?');
                if (conf) {
                    $.ajax({
                        url: '/api/ProductApi/RemoveProductById?Id=' + self.ProductId,
                        type: 'DELETE',
                        dataType: 'json',
                        contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                    }).then(res => {
                        if (res !== 0) {
                            alert('Xóa thành công');
                        }
                        else {
                            alert('Xóa thất bại');
                        }
                    })
                }
            }
        },
        getTitlePage: function () {
            var self = this;
            if (location.href.includes('=')) {
                self.TitlePage = "Cập nhật thông tin sản phẩm";
            }
            else {
                self.TitlePage = "Thêm mới sản phẩm";
            }
        }
    }
});
$(document).ready(function () {
   vmCreateProduct.getDetailProduct();
//    vmCreateProduct.getCollection();
})
