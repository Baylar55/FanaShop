$(document).ready(function () {

  function Accordion() {
    //#region accordion
    $("#accordion > li > span").click(function () {
      $(this)
        .closest("li")
        .siblings()
        .find("span")
        .removeClass("active")
        .next("div")
        .slideUp(250);
      $(this).toggleClass("active").next("div").slideToggle(250);
    });
    //#endregion accordion
  }
  Accordion();

  function TransferTabTitle() {
    $(document).on("click", ".tab", function () {
      $(".tab").removeClass("active-tab");
      $(this).addClass("active-tab");
      $(".content-title").text($(this).children().eq(0).text());
    });
  }
  TransferTabTitle();

  function OpenMiniCart() {
    $(document).on("click", ".basket", () => {
      $(".mini-cart").slideToggle();
    });
  }
  OpenMiniCart();

  function ToggleMobileNav() {
    //#region Open-Close-Mobile-Nav
    $(document).on("click", ".burger-menu", function () {
      $(".mobile-nav").slideToggle();
    });
    //#endregion Open-Close-Mobile-Nav

    //#region accordion
    $("#accordion > li > span").click(function () {
      $(this)
        .closest("li")
        .siblings()
        .find("span")
        .removeClass("active")
        .next("div")
        .slideUp(250);
      // $(this).toggleClass("active").next('div').slideToggle(250);
    });
    //#endregion accordion
  }
  ToggleMobileNav();

  function ShowingNavScroll() {
    //#region Showing-Nav-On-Scroll
    $(window).scroll(function () {
      var height = $(window).scrollTop();

      if (height > $('header').outerHeight()) {
        $('.header-bottom').addClass('header-fixed');
        $('.st-btn').addClass('st-btn-active');
      } else {
        $('.header-bottom').removeClass('header-fixed');
        $('.st-btn').removeClass('st-btn-active');
      }
    });
    //#endregion Showing-Nav-On-Scroll
  }
  ShowingNavScroll()

  function ShowingBanners() {

        //#region showing-banner-page-1
        $(document).on('click', '.bullet-1', () => {
            $('.banner-page-1').removeClass('second-banner third-banner').addClass('first-banner')
            $('.banner-page-2').removeClass('first-banner third-banner').addClass('second-banner')
            $('.banner-page-3').removeClass('first-banner second-banner').addClass('third-banner')
        })
        //#endregion showing-banner-page-1


        //#region showing-banner-page-2 
        $(document).on('click', '.bullet-2', () => {
            $('.banner-page-2').removeClass('second-banner third-banner').addClass('first-banner')
            $('.banner-page-1').removeClass('first-banner third-banner').addClass('second-banner')
            $('.banner-page-3').removeClass('first-banner second-banner').addClass('third-banner')
        })
        //#endregion showing-banner-page-2 



        //#region  showing-banner-page-3
        $(document).on('click', '.bullet-3', () => {
            $('.banner-page-3').removeClass('second-banner third-banner').addClass('first-banner')
            $('.banner-page-2').removeClass('first-banner third-banner').addClass('second-banner')
            $('.banner-page-1').removeClass('first-banner second-banner').addClass('third-banner')
        })
        //#endregion showing-banner-page-3


        //#region showing banner-pages with setInterval
        let customIndex = 1;
        window.setInterval(() => {

            if (customIndex > 2) {
                customIndex = 0;
            }

            //#region showing-banner-page-1 with SetInterval
            if (customIndex == 0) {
                $('.banner-page-1').removeClass('second-banner third-banner').addClass('first-banner')
                $('.banner-page-2').removeClass('first-banner third-banner').addClass('second-banner')
                $('.banner-page-3').removeClass('first-banner second-banner').addClass('third-banner')

                $('.bullet').removeClass('selected');
                $($('.bullet').eq(customIndex)).addClass('selected');
            }
            //#endregion showing-banner-page-1 with SetInterval

            //#region showing-banner-page-2 with SetInterval
            if (customIndex == 1) {
                $('.banner-page-2').removeClass('second-banner third-banner').addClass('first-banner')
                $('.banner-page-1').removeClass('first-banner third-banner').addClass('second-banner')
                $('.banner-page-3').removeClass('first-banner second-banner').addClass('third-banner')

                $('.bullet').removeClass('selected');
                $($('.bullet').eq(customIndex)).addClass('selected');
            }
            //#endregion showing-banner-page-2 with SetInterval

            //#region showing-banner-page-3 with SetInterval
            if (customIndex == 2) {

                $('.banner-page-3').removeClass('second-banner third-banner').addClass('first-banner')
                $('.banner-page-2').removeClass('first-banner third-banner').addClass('second-banner')
                $('.banner-page-1').removeClass('first-banner second-banner').addClass('third-banner')

                $('.bullet').removeClass('selected');
                $($('.bullet').eq(customIndex)).addClass('selected');

            }
            //#endregion showing-banner-page-3 with SetInterval

            customIndex += 1;

        }, 7000)
        //#endregion showing banner-pages with setInterval
    }
  ShowingBanners()

});
