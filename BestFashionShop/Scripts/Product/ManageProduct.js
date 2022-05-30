var vmManageProduct = new Vue({
    el: '#ManageProduct',
    data: {
        DataListProduct: {},
        pageCount: 0,
        pageSize: 10,
        currentPage: 1,
        pageStart: 0,
        pageEnd: 0,
        totalRowCount: 0
    },
    methods: {
        getListProduct: function () {
            var self = this;
            $.ajax({
                url: '/api/ProductApi/GetAllProduct?pageSize=' + self.pageSize + '&currentPage=' + self.currentPage,
                type: 'GET',
                dataType: 'json',
                contentType: "application/x-www-form-urlencoded; charset=UTF-8"

            }).then(res => {
                if (res != null) {
                    let dataFg = JSON.parse(res);
                    self.DataListProduct = dataFg.data;
                    self.pageCount = dataFg.pageCount;
                    self.totalRowCount = dataFg.totalRowCount;
                    self.pageStart = dataFg.pageStart;
                    self.pageEnd = dataFg.pageEnd;
                }
            }).catch({

            })
        },
        DeleteProduct: function (productId) {
            var self = this;
            var conf = confirm('Bạn chắc chắn muốn xóa sản phẩm này?');
            if (conf) {
                $.ajax({
                    url: '/api/ProductApi/RemoveProductById?Id=' + productId,
                    type: 'DELETE',
                    dataType: 'json',
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                }).then(res => {
                    if (res !== 0) {
                        self.getListProduct();
                        alert('Xóa thành công');
                    }
                    else {
                        alert('Xóa thất bại');
                    }
                })
            }
        },
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
    vmManageProduct.getListProduct();
})