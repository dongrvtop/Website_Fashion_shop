var vmDetailProduct = new Vue({
    el: '#DetailProduct',
    data: {
        ProductID: 0,
        dataProduct: null,
        QuantityProduct:1,
    },
    methods: {
        getDataProduct: function (productId = 0) {
            var self = this;
            self.ProductID = location.href.split('=')[1];
            if (productId === 0) {
                $.ajax({
                    url: '/api/ProductApi/GetProductById?Id=' + self.ProductID,
                    type: 'GET',
                    dataType: 'json',
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                }).then(res => {
                    if (res != null) {
                        self.dataProduct = null;
                        self.dataProduct = JSON.parse(res);
                        setTimeout(function () {
                            self.loadImageProduct();
                        }, 200)
                    }
                });
            }
            else {
                self.ProductID = productId;
                $.ajax({
                    url: '/api/ProductApi/GetProductById?Id=' + productId,
                    type: 'GET',
                    dataType: 'json',
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                }).then(res => {
                    if (res != null) {
                        self.dataProduct = null;
                        self.dataProduct = JSON.parse(res);
                        setTimeout(function () {
                            self.loadImageProduct();
                        }, 200)
                    }
                });
            }
        },
        MinusQuantityProduct: function () {
            var self = this;
            if (self.QuantityProduct === 0) {
                return false;
            }
            self.QuantityProduct -= 1;
        },
        PlusQuantityProduct: function () {
            var self = this;
            if (self.QuantityProduct >= self.dataProduct.QuantityProduct) {
                return false;
            }
            self.QuantityProduct += 1;
        },
        AddCart: function () {
            var self = this;
            if (!self.dataProduct.Quantity) {
                alert('Sản phẩm này đã hết hàng, vui lòng chọn sản phẩm khác.');
                return false;
            }
            var data = {};
            data.ProductId = self.dataProduct.Id;
            data.UserId = UserId;
            data.Quantity = self.QuantityProduct;
            data.Price = self.dataProduct.Price;
            $.ajax({
                url: '/api/CartApi/AddOrUpdateCarts',
                data: data,
                type: 'POST',
                dataType: 'json',
                contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            }).then(res => {
                if (res === 1) {
                    alert('Đã thêm sản phẩm vào giỏ hàng!');
                }
            })
        },
        loadImageProduct: function () {
            var self = this;
            $('.slider-for').slick({
                slidesToShow: 1,
                slidesToScroll: 1,
                arrows: false,
                fade: true,
                asNavFor: '.slider-nav'
            });
            $('.slider-nav').slick({
                slidesToShow: 3,
                slidesToScroll: 1,
                asNavFor: '.slider-for',
                dots: false,
                centerMode: true,
                focusOnSelect: true
            });
            $('.slick-active').css('width', '100px');
        },
        checkAccount: function () {
            $('#ModalLogin').modal('show');
        },
    }
})
$(document).ready(function () {
    vmDetailProduct.getDataProduct();
})