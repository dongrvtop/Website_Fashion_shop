var vmMyOrders = new Vue({
    el: '#MyOrders',
    data: {
        DataListOrder: {},
        pageCount: 0,
        pageSize: 5,
        currentPage: 1,
        pageStart: 0,
        pageEnd: 0,
        totalRowCount: 0
    },
    methods: {
        getListOrder: function () {
            var self = this;
            $.ajax({
                url: '/api/OrderApi/GetMyOrder?pageSize=' + self.pageSize + '&currentPage=' + self.currentPage + '&UserId=' + UserId,
                type: 'GET',
                dataType: 'json',
                contentType: "application/x-www-form-urlencoded; charset=UTF-8"

            }).then(res => {
                if (res != null) {
                    let dataFg = JSON.parse(res);
                    self.DataListOrder = dataFg.data;
                    self.pageCount = dataFg.pageCount;
                    self.totalRowCount = dataFg.totalRowCount;
                    self.pageStart = dataFg.pageStart;
                    self.pageEnd = dataFg.pageEnd;
                }
            }).catch({

            })
        },
        CancelOrder: function (OrderId) {
            var self = this;
            $.ajax({
                url: '/api/OrderApi/UpdateOrders?Id=' + OrderId + '&Status=' + 4,
                type: 'POST',
                dataType: 'json',
                contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            }).then(res => {
                if (res === 1) {
                    alert('Hủy đơn hàng thành công');
                    self.getListOrder();
                }
            }).catch({

            })
        },
        recieveOrder: function (OrderId) {
            var self = this;
            $.ajax({
                url: '/api/OrderApi/UpdateOrders?Id=' + OrderId + '&Status=' + 3,
                type: 'POST',
                dataType: 'json',
                contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            }).then(res => {
                if (res === 1) {
                    alert('Cảm ơn bạn đã ủng hộ cửa hàng');
                    self.getListOrder();
                }
            }).catch({

            })
        },
        //DeleteUser: function (userId) {
        //    var self = this;
        //    var conf = confirm('Bạn chắc chắn muốn xóa tài khoản này?');
        //    if (conf) {
        //        $.ajax({
        //            url: '/api/UserApi/RemoveUserByAdmin?UserId=' + userId,
        //            type: 'DELETE',
        //            dataType: 'json',
        //            contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        //        }).then(res => {
        //            if (res === 1) {
        //                alert('Xóa thành công');
        //                self.getListUser();
        //            }
        //            else if (res === 2) {
        //                alert('Không thể xóa tài khoản có quyền ADMIN');
        //            }
        //            else if (res === 3) {
        //                alert('Tài khoản không tồn tại');
        //            }
        //            else {
        //                alert('Lỗi');
        //            }
        //        })
        //    }
        //},
        FirstPage: function () {
            var self = this;
            self.currentPage = 1;
        },
        PrePage: function () {
            var self = this;
            if (self.currentPage > 1) {
                self.currentPage -= 1;
            }
        },
        NextPage: function () {
            var self = this;
            if (self.currentPage < self.pageCount) {
                self.currentPage += 1;
            }
        },
        LastPage: function () {
            var self = this;
            self.currentPage = self.pageCount;
        },
    },
    watch: {
        currentPage: function () {
            var self = this;
            var regex = /^\d+$/;
            if (!regex.test(self.currentPage)) {
                self.currentPage = "";
            }
            if (self.currentPage) {
                if (self.currentPage > self.pageCount) {
                    self.currentPage = self.pageCount;
                }
                if (self.currentPage < 1) {
                    self.currentPage = 1;
                }
                self.getListProduct();
            }
        },
        pageSize: function () {
            var self = this;
            self.getListProduct();
        }
    }
})
$(document).ready(function () {
    vmMyOrders.getListOrder();
})