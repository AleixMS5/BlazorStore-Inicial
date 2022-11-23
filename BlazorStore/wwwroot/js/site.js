(function () {
    window.initializeCarousel = function () {
        $(".carousel").carousel({
            interval: 5000
        });
    };

    window.bootstrapModal = {
        show: function (element) {
            $(element).modal("show");
        }
    };
})();