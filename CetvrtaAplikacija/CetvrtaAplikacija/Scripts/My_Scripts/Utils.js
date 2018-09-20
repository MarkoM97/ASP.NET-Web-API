$('.hoverable').on('mouseenter', function (e) {
    $(this)[0].style.opacity = 1;
});


$(document).on('mouseenter', '.hoverable', function (e) {
    $(this)[0].style.opacity = 1;
    $(this)[0].style.cursor = 'pointer';
});

$(document).on('mouseleave', '.hoverable', function (e) {
    $(this)[0].style.opacity = 0.9;
    $(this)[0].style.cursor = 'default';
});

$('.hoverable').on('mouseleave', function (e) {
    $(this)[0].style.opacity = 0.9;
});