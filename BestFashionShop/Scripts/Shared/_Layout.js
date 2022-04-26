var vmLayout = new Vue({
    el: '#_Layout',
    data: {
        
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