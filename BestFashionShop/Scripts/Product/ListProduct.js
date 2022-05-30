var vmListProducts = new Vue({
    el: '#ListProducts',
    data: {
        DataListProduct: {},
        interval_obj: null,
        pageSize: 20,
        currentPage: 1,
        totalRowCount: 0,
        pageStart: 0,
        pageEnd:0,
    },
    methods: {
        getListProduct: function () {
            var self = this;
            var url = location.href;
            var Id = 0;
            if (!location.href.includes('=')) {
                $.ajax({
                    url: '/api/ProductApi/GetAllProduct?pageSize=' + self.pageSize + '&currentPage=' + self.currentPage,
                    type: 'GET',
                    dataType: 'json',
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8"

                }).then(res => {
                    if (res != null) {
                        self.DataListProduct = null;
                        let dataFg = JSON.parse(res);
                        self.DataListProduct = dataFg.data;
                        self.pageCount = dataFg.pageCount;
                        self.totalRowCount = dataFg.totalRowCount;
                        self.pageStart = dataFg.pageStart;
                        self.pageEnd = dataFg.pageEnd;
                    }
                }).catch({

                })
            }
            else {
                Id = url.split('=')[1];
                if (location.href.includes('ProductCategoryId')) {
                    $.ajax({
                        url: '/api/ProductApi/GetListProductByProductCategoryId?Id=' + Id + '&pageSize=' + self.pageSize + '&currentPage=' + self.currentPage,
                        type: 'GET',
                        dataType: 'json',
                        contentType: "application/x-www-form-urlencoded; charset=UTF-8"

                    }).then(res => {
                        if (res != null) {
                            self.DataListProduct = null;
                            let dataFg = JSON.parse(res);
                            self.DataListProduct = dataFg.data;
                            self.pageCount = dataFg.pageCount;
                            self.totalRowCount = dataFg.totalRowCount;
                            self.pageStart = dataFg.pageStart;
                            self.pageEnd = dataFg.pageEnd;
                        }
                    }).catch({

                    })
                }
                else if (location.href.includes('CategoryId')) {
                    $.ajax({
                        url: '/api/ProductApi/GetListProductByProductCategoryId?Id=' + Id,
                        type: 'GET',
                        dataType: 'json',
                        contentType: "application/x-www-form-urlencoded; charset=UTF-8"

                    }).then(res => {
                        if (res != null) {
                            self.DataListProduct = null;
                            self.DataListProduct = JSON.parse(res);
                        }
                    }).catch({

                    })
                }
                else {
                    $.ajax({
                        url: '/api/ProductApi/GetListProductByCollectionId?Id=' + Id + '&pageSize=' + self.pageSize + '&currentPage=' + self.currentPage,
                        type: 'GET',
                        dataType: 'json',
                        contentType: "application/x-www-form-urlencoded; charset=UTF-8"

                    }).then(res => {
                        if (res != null) {
                            self.DataListProduct = null;
                            let dataFg = JSON.parse(res);
                            self.DataListProduct = dataFg.data;
                            self.pageCount = dataFg.pageCount;
                            self.totalRowCount = dataFg.totalRowCount;
                            self.pageStart = dataFg.pageStart;
                            self.pageEnd = dataFg.pageEnd;
                        }
                    }).catch({

                    })
                }
            }
        },
        ViewDetailProduct: function (productId) {
            var self = this;
            var host = 'https://localhost:44305/';
            var urlDetail = host + "Product/DetailProduct?Id=" + productId;
            window.location = urlDetail;
            //vmDetailProduct.getDataProduct(productId);
        },
        hoverImage: function (productIdx) {
            var self = this;
            var idx = 0;
            var prod = self.DataListProduct[productIdx];
            const imageCount = prod.ProductImages.length;
            self.interval_obj = setInterval(function () {
                for (var item of prod.ProductImages) {
                    item.DefaultImage = false;
                }
                idx = idx < (imageCount - 1) ? (idx += 1) : 0;
                prod.ProductImages[idx].DefaultImage = true;
            }, 500);
        },
        hoverOutImage: function (productIdx) {
            var self = this;
            clearInterval(self.interval_obj);
            var prod = self.DataListProduct[productIdx];
            for (var item of prod.ProductImages) {
                item.DefaultImage = false;
            }
            prod.ProductImages[0].DefaultImage = true;
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
    vmListProducts.getListProduct();
})