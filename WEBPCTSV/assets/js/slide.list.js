$(document).ready(function () {

    //slide logo of party with school
    $(function animate() {
        $(".ads-slide .ads-slide-item:first").each(function () {
            $(this).animate(
                {
                    marginLeft: -$(this).outerWidth(true),
                    opacity: "hide"

                }, 2000, function () {
                    $(this).insertAfter(".ads-slide .ads-slide-item:last");
                    $(this).fadeIn();
                    $(this).css({ marginLeft: 0 });
                    setTimeout(function () { animate() }, 2000);
                });
        });
    });

    // Slide notify in homepage
    $(function animate() {
        $(".notify-box .listbox-content-item:first").each(function () {
            $(this).delay(5000);
            $(this).animate(
                {
                    marginTop: -$(this).outerHeight(true),
                    opacity: "hide"

                }, 2000, function () {
                    $(this).insertAfter(".notify-box .listbox-content-item:last");
                    $(this).fadeIn();
                    $(this).css({ marginTop: 0 });
                    setTimeout(function () { animate() }, 2000);
                });
        });
    });
});