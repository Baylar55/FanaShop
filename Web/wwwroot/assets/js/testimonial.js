$(document).ready(function () {
    function TestimonialSlider() {
        $("#testimonial-slider").slick({
            prevArrow:
                '<div class="slick-prev"><i class="fa-solid fa-chevron-left"></i></div>',
            nextArrow:
                '<div class="slick-next"><i class="fa-solid fa-chevron-right"></i></div>',
            dots: true,
            infinite: true,
            speed: 300,
            slidesToShow: 1,
            adaptiveHeight: true,
        });
    }
    TestimonialSlider();
});
