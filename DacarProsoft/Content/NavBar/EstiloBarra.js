
$(document).ready(function () {
    pruba();
//    var $dropdown = $(".dropdown");
//    var $dropdownToggle = $(".dropdown-toggle");
//    var $dropdownMenu = $(".dropdown-menu");
//    var showClass = "show";

//    $(window).on("load resize", function () {
//        if (this.matchMedia("(min-width: 768px)").matches) {
//            $dropdown.hover(
//              function () {
//                  var $this = $(this);
//                  $this.addClass(showClass);
//                  $this.find($dropdownToggle).attr("aria-expanded", "true");
//                  $this.find($dropdownMenu).addClass(showClass);
//              },
//              function () {
//                  var $this = $(this);
//                  $this.removeClass(showClass);
//                  $this.find($dropdownToggle).attr("aria-expanded", "false");
//                  $this.find($dropdownMenu).removeClass(showClass);
//              }
//            );
//        } else {
//            $dropdown.off("mouseenter mouseleave");
//        }
//    });
});

function pruba(){
$('#dmenu').hover(function () {
    $('#navbarDropdown').trigger('click')
    //console.log('hover');
})
}