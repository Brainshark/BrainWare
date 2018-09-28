$(document).ready(function () {
    var $orders = $('#orders');
    $.ajax({
        'url': '/api/order',
        'type': 'GET',
        'success': function (data) {

            var $orderList = $('<ul/>');

            if (data) {
                $.each(data,
                    function () {
                        var $orderItem = $('<li/>').text(this.Description + ' (Total: $' + this.OrderTotal + ')')
                            .appendTo($orderList);

                        var $productList = $('<ul/>');

                        $.each(this.OrderProducts, function () {
                            $('<li/>')
                                .text(this.Product.Name + ' (' + this.Quantity + ' @@ $' + this.Price + '/ea)')
                                .appendTo($productList);
                        });

                        $productList.appendTo($orderItem);
                    });

                $orders.append($orderList);
            }
        }
    });
});