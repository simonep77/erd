

(function ($) {

    //Imposta disabilitazione
    $.fn.setDisabled = function (value) {
        return this.each(function () {
            var jqItem = $(this);

            $(this).prop("disabled", value);
        });
    };

    //Imposta loader in elemento
    $.fn.setLoader = function (state = 'on', text = "Caricamento..") {
        return this.each(function () {
            var jqItem = $(this);
            if (state === 'on') {
                jqItem.setDisabled(true);
                jqItem.data("oldhtml", jqItem.html());
                jqItem.html('<span class="spinner-border spinner-border-sm me-2" role="status"></span>' + text);
            }
            else if (state === 'off') {
                jqItem.setDisabled(false);
                jqItem.html(jqItem.data("oldhtml") || "No 'on'");
                jqItem.removeData("oldhtml");
            }
            
        });
    };

}(jQuery));
