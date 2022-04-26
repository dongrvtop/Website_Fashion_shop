var vmHomePage = new Vue({
    el: '#HomePage',
    data: {
        test:'hello',
    },
    methods: {
        loadSlideCollection: function () {
                $('.center').slick({
                    centerMode: true,
                    centerPadding: '60px',
                    slidesToShow: 3
                });
        }
    }
});

$(document).ready(function () {
    vmHomePage.loadSlideCollection();
})