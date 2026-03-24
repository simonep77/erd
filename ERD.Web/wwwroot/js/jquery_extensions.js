

(function ($) {

    $.fn.setDisabled = function (value) {

        return this.each(function () {
            var jqItem = $(this);

            $(this).prop("disabled", value);
        });

    };

}(jQuery));
