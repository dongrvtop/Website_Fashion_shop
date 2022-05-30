var vmLayout = new Vue({
    el: '#_Layout',
    data: {
        IdUser: 0,
        UserFullName: "",
        UserRole: "ANONYMOUS",
        IsShowLstActionUser: false,
        keySearch: "",
        ListProductSearch: {},
        isFocusSearch: false,
    },
    methods: {
        HoverProduct: function () {
            $('#menu-Product').css('display', 'flex');
            $('.menu-desktop').addClass('menu-desktopTypeShadow');
            $('.menu-desktop').removeClass('menu-desktopTypeNotShadow');
        },
        HoverOutMenu: function () {
            $('#menu-Product').css('display', 'none');
            $('.menu-desktop').css('background', 'none');
            $('.menu-desktop').removeClass('menu-desktopTypeShadow');
            $('.menu-desktop').addClass('menu-desktopTypeNotShadow');
        },
        HoverMenu: function () {
            $('.menu-desktop').css('background', 'white');
        },
        checkAccount: function () {
            //if (!UserId) {
                $('#ModalLogin').modal('show');
            //}
            
        },
        LogOut: function () {
            var self = this;
            let conf = confirm('Bạn chắc chắn muốn đăng xuất?');
            if (!conf) {
                return false;
            }
            $.ajax({
                url: '/UserApi/Logout',
                type: 'POST',
                dataType: 'json',
                contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            }).then(res => {
                if (res == 1) {
                    alert('Đăng xuất thành công');
                    //self.IdUser = 0;
                    //self.UserFullName = "";
                    //self.UserRole = "";
                    //self.IsShowLstActionUser = false;
                    //location.reload();
                    window.location = 'https://localhost:44305/Home';
                }
            })
        },
        getListProduct: function (categoryId = 0, productCategoryId = 0, collectionId = 0) {
            var self = this;
            var host = 'https://localhost:44305/';
            var urlDetail = host + "Product/ListProduct";
            if (!categoryId && !productCategoryId && !collectionId) {
                //vmListProducts.getListProduct();
                window.location = urlDetail;
            }
            else {
                if (productCategoryId) {
                    urlDetail += '?ProductCategoryId=' + productCategoryId;
                    //vmListProducts.getListProduct(0, productCategoryId, 0);
                }
                else if (categoryId) {
                    urlDetail += '?CategoryId=' + categoryId;
                    //vmListProducts.getListProduct(categoryId, 0, 0);
                }
                else {
                    urlDetail += '?CollectionId=' + collectionId;
                    //vmListProducts.getListProduct(0, 0, collectionId);
                }
                window.location = urlDetail;
            }
        },
        SearchProduct: function () {
            var self = this;
            if (self.keySearch && self.keySearch !== '') {
                $.ajax({
                    url: '/api/ProductApi/SearchProduct?value=' + self.keySearch,
                    type: 'GET',
                    dataType: 'json',
                    contentType: "application/x-www-form-urlencoded; charset=UTF-8"
                }).then(res => {
                    if (res != null) {
                        var dataFg = JSON.parse(res);
                        self.ListProductSearch = {};
                        self.ListProductSearch = dataFg;
                    }
                })
            }
        },
        ViewDetailProduct: function (productId) {
            var self = this;
            debugger
            var host = 'https://localhost:44305/';
            var urlDetail = host + "Product/DetailProduct?Id=" + productId;
            window.location = urlDetail;
            //vmDetailProduct.getDataProduct(productId);
        },
    },
    watch: {
        keySearch: function () {
            var self = this;
            if (self.keySearch && self.keySearch != '') {
                self.SearchProduct();
            }
            
        }
    }
});
var vmFooterBottom = new Vue({
    el: '#footerBottom',
    data: {
        checkShowFooter: false,
    },
    methods: {
        ShowHideFooter: function () {
            var self = this;
            if (self.checkShowFooter) {
                self.checkShowFooter = false;
            }
            else {
                self.checkShowFooter = true;
            }
        }
    }
})
$(document).ready(function () {
    $(window).scroll(function (event) {
        var pos_body = $('html,body').scrollTop();
        // console.log(pos_body);
        if (pos_body > 20) {
            $('.menu-desktop').addClass('menu-desktopTypeShadow');
            $('.menu-desktop').removeClass('menu-desktopTypeNotShadow');
            $('.menu-desktop').css('background', 'white');
        }
        else {
            $('.menu-desktop').removeClass('menu-desktopTypeShadow');
            $('.menu-desktop').addClass('menu-desktopTypeNotShadow');
            $('.menu-desktop').css('background', 'none');
        }
    });
    $('.back-to-top').click(function (event) {
        $('html,body').animate({ scrollTop: 0 }, 1400);
    });
});