var vmOrderIndex = new Vue({
    el: '#OrderIndex',
    data: {
        dataCart: {},
        TotalPrice: 0,
        StrTotalPrice: "",
        TotalPayment: 0,
        StrTotalPayment: "",
        TotalPriceSale: 0,
        StrTotalPriceSale: "",
        productCount: 0,
        UserId: 0,
        ShipName: "",
        ShipEmail: "",
        ShipAddress: "",
        ShipPhone:"",
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
        Submit: function () {
            var self = this;
            var dataOrder = {};
            dataOrder.ShipName = self.ShipName;
            dataOrder.ShipAddress = self.ShipAddress;
            dataOrder.ShipPhone = self.ShipPhone;
            dataOrder.Status = self.Status;
            dataOrder.UserId = UserId;
            dataOrder.PaymentPrice = self.TotalPayment;
            dataOrder.LstOrderDetail = [];
            dataOrder.LstCartId = [];
            for (var item of self.dataCart) {
                var dataItem = {};
                dataItem.ProductId = item.Product.Id;
                dataItem.ProductQuantity = item.Quantity;
                dataOrder.LstOrderDetail.push(dataItem);
                dataOrder.LstCartId.push(item.Id);
            }
            $.ajax({
                url: '/api/OrderApi/AddOrders',
                data: dataOrder,
                type: 'POST',
                dataType: 'json',
                contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            }).then(res => {
                if (res === 1) {
                    alert('Đặt hàng thành công. Vui lòng chờ cửa hàng xác nhận đơn hàng.');
                    window.location = '/Orders/MyOrder';
                }
                else if (res === 2) {
                    alert('Sản phẩm bạn mua hiện tại không đủ số lượng!');
                }
            })
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
                    if (item.Product.PromotionPercent?.trim()) {
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
    vmOrderIndex.getDetailCart();
})