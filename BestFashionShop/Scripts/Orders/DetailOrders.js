var vmDetailOrder = new Vue({
    el: '#DetailOrder',
    data: {
        OrderId: 0,
        DetailOrder: {},
        TitlePage: 'Chi tiết đơn hàng',
    },
    methods: {
        getDetailOrder: function () {
            var self = this;
            if (!location.href.includes('=')) {
                return false;
            }
            self.OrderId = location.href.split('=')[1];
            if (!self.OrderId) {
                return false;
            }
            $.ajax({
                url: '/api/OrderApi/GetDetailOrder?OrderId=' + self.OrderId,
                type: 'GET',
                dataType: 'json',
                contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            }).then(res => {
                if (res != null) {
                    self.DetailOrder = {};
                    self.DetailOrder = JSON.parse(res);
                    
                }
            })
        },
    }
})
$(document).ready(function () {
    vmDetailOrder.getDetailOrder();
})