var vmMyCart = new Vue({
    el: '#MyCart',
    data: {
        dataCart: {},
        TotalPrice: 0,
        StrTotalPrice: "",
        TotalPayment:0,
        StrTotalPayment: "",
        TotalPriceSale: 0,
        StrTotalPriceSale: "",
        productCount:0,
        UserId:0,
    },
    methods: {
        getDetailCart: function () {
            var self = this;
            $.ajax({
                url: '/api/CartApi/GetCartByUserId?Id=' + UserId,
                type: 'GET',
                dataType: 'json',
                contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            }).then(res => {
                if (res != null) {
                    var dataRes = JSON.parse(res);
                    self.dataCart = dataRes;
                }
            })
        },
        MinusQuantity: function (cartIndex) {
            var self = this;
            if (self.dataCart[cartIndex].Quantity > 1) {
                self.dataCart[cartIndex].Quantity -= 1;
                self.UpdateCart(self.dataCart[cartIndex]);
            }
            else {
                self.dataCart[cartIndex].Quantity = 0;
                self.RemoveCart(self.dataCart[cartIndex].Id);
            }
        },
        PlusQuantity: function (cartIndex) {
            var self = this;
            self.dataCart[cartIndex].Quantity += 1;
            self.UpdateCart(self.dataCart[cartIndex]);
        },
        UpdateCart: function (cart) {
            var self = this;
            $.ajax({
                url: '/api/CartApi/AddOrUpdateCarts',
                data: cart,
                type: 'POST',
                dataType: 'json',
                contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            }).then(res => {
                if (res === 1) {
                    self.getDetailCart();
                    console.log('update success');
                }
                else {
                    console.log('lỗi update');
                }
            })
        },
        RemoveCart: function (cartId) {
            var self = this;
            var cof = confirm('Bạn chắc chắn muốn xóa sản phẩm này khỏi giỏ hàng?');
            if (cartId && cof) {
                $.ajax({
                    url: '/api/CartApi/DeleteCartsById?Id=' + cartId,
                    type: 'POST',
                    dataType: 'json',
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                }).then(res => {
                    if (res === 1) {
                        self.getDetailCart();
                        console.log('delete success');
                    }
                    else {
                        console.log('lỗi delete');
                    }
                })
            }
        },
        GoToOrder: function () {
            window.location = 'https://localhost:44305/Orders';
        },
        BuyNewProduct: function () {
            window.location = 'https://localhost:44305/Product/ListProduct';
        }
    },
    watch: {
        dataCart: function () {
            var self = this;
            if (self.dataCart) {
                self.TotalPrice = 0;
                self.TotalPayment = 0;
                self.TotalPriceSale = 0;
                self.productCount = 0;
                for (var item of self.dataCart) {
                    if (item.Product.PromotionPercent.trim()) {
                        self.TotalPayment += (item.Product.Price * (100 - parseInt(item.Product.PromotionPercent)) / 100) * item.Quantity;
                        self.TotalPriceSale += (item.Product.Price * parseInt(item.Product.PromotionPercent) / 100) * item.Quantity;
                    }
                    else {
                        self.TotalPayment += item.Product.Price * item.Quantity;
                    }
                    self.TotalPrice += item.Product.Price * item.Quantity;
                    self.productCount += item.Quantity;
                }
            }
        },
        TotalPrice: function () {
            var self = this;
            self.StrTotalPrice = self.TotalPrice.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1.');
        },
        TotalPayment: function () {
            var self = this;
            self.StrTotalPayment = self.TotalPayment.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1.');
        },
        TotalPriceSale: function () {
            var self = this;
            self.StrTotalPriceSale = self.TotalPriceSale.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1.');
        },
    }
})
$(document).ready(function () {
    vmMyCart.getDetailCart();
})