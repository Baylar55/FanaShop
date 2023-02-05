jQuery(function ($) {
    $(document).on('click', '#addToCart', function () {
        var id = $(this).data('id');
        var notification = $("#notification");
        var quantity =parseInt($(`.product-quantity-${id}`).text()) 
        notification.addClass("show");
        setTimeout(function () { notification.removeClass("show"); }, 3000);
        var count = $(".cart-items-count");
        var price = parseFloat($(".product-price").text());
        var currentCount = $(".cart-items-count").html();
        var sumPrice = $('#sum-price').html()
        var sumPriceLast = $('#sum-price')
        $.ajax({
            method: "POST",
            url: "/basket/add",
            data: {
                id: id
            },
            success: function () {
                count.html("");
                currentCount++;
                count.append(currentCount);
                var parsedcount = parseFloat(count.text())

                console.log(sumPrice)

                $(`.product-quantity-${id}`).text(quantity+1)
            }
        })
    })

    $(document).on("click", ".basket", function () {
        $.ajax({
            method: "get",
            url: "/basket/minibasket",
            success: function (result) {
                $('.cart-items').html("");
                $('.cart-items').append(result);
                console.log(result)
            }
        })
    })

    $(document).on("click", '.basket-delete', function () {
        var id = $(this).data('id')
        $.ajax({
            method: "POST",
            url: "/basket/delete",
            data: {
                id: id
            },
            success: function () {
                $(`.basket-products[id=${id}]`).remove();
            }
        })
    })
})


$(document).on("click", '.decrease', function () {
    var id = $(this).data('id')
    var quantity = $(`.product-quantity-${id}`).html()
    var price = $(this).data('price')
    var totalPrice = $(`.total-product-price-${id}`);
    var count = $(".cart-items-count");
    var currentCount = $(".cart-items-count").html();
    $.ajax({
        method: "POST",
        url: "/basket/decreasecount",
        data: {
            id: id
        },
        success: function () {
            count.html("");
            currentCount--;
            count.append(currentCount);
            var total = price * quantity
            totalPrice.html("");
            totalPrice.html(total + "$")
        }
    })
})

$(document).on("click", '.increase', function () {
    var id = $(this).data('id')
    var quantity = $(`.product-quantity-${id}`).html()
    var price = $(this).data('price')
    var totalPrice = $(`.total-product-price-${id}`);
    var count = $(".cart-items-count");
    var currentCount = $(".cart-items-count").html();
    var sumPrice = $('#sum-price').html()
    var sumPriceLast = $('#sum-price')
    $.ajax({
        method: "POST",
        url: "/basket/increasecount",
        data: {
            id: id
        },
        success: function () {
            count.html("");
            currentCount++;
            count.append(currentCount);
            var total = price * quantity
            sumPriceLast.html("")
            sumPriceLast.append(sumPrice - price * count)
            totalPrice.html("");
            totalPrice.html(total+"$")
        }
    })
})

$(document).on('click', '.add-wishlist', function () {
    var id = $(this).data('id');
    $.ajax({
        method: "POST",
        url: "/wishlist/add",
        data: {
            id: id
        },
        success: function () {
            $(`.add-wishlist[data-id=${id}]`).addClass("added-basket")
        },
    })
})

$(".wish-delete").click(function () {
    var id = $(this).data("id")

    $.ajax({
        method: "POST",
        url: "/wishlist/delete",
        data: {
            id: id
        },
        success: function () {
            $(`.wishlist-item[id=${id}]`).remove()
        }
    })
})